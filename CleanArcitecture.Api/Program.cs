using CleanArcitecture.Core;
using CleanArcitecture.Core.MiddelWare;
using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure;
using CleanArcitecture.Infrastructure.Context;
using CleanArcitecture.Infrastructure.Seeder;
using CleanArcitecture.Service;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
#region localization
builder.Services.AddLocalization(opt =>
{
    opt.ResourcesPath = "";
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    List<CultureInfo> cultureInfos = new List<CultureInfo>
                {
                   new CultureInfo("en-US"),
                   new CultureInfo("ar-Eg"),
                };
    options.DefaultRequestCulture = new RequestCulture("ar-Eg");
    options.SupportedCultures = cultureInfos;
    options.SupportedUICultures = cultureInfos;
});

#endregion
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddExceptionHandler(x =>
//{
//    x.
//});
//Connection To SQL Server
builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#region Dependancy Injection
builder.Services.AddInfrastructureDependencies(builder.Configuration)
                .AddServiceDependencies()
                .AddCoreDependencies();
#endregion


var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();
    await RoleSeeder.SeedRole(roleManager);
    await UserSeeder.SeedUser(userManager);
}
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var options = app.Services.GetService<IOptions<RequestLocalizationOptions>>();
app.UseRequestLocalization(options.Value);
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();
// Exception handling middleware
//app.UseExceptionHandler(errorApp =>
//{
//    errorApp.Run(async context =>
//    {
//        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
//        context.Response.ContentType = "application/json";
//        //IExceptionHandlerFeature:  get the exception that was thrown.
//        var contextFeature = context.Features.Get<IExceptionHandlerFeature>();
//        if (contextFeature != null)
//        {
//            var errorDetails = new
//            {
//                StatusCode = context.Response.StatusCode,
//                Message = "Internal Server Error.",
//                Detailed = contextFeature.Error.Message // Optionally include the error message
//            };

//            // Serialize error details to JSON and write to the response
//            await context.Response.WriteAsync(JsonSerializer.Serialize(errorDetails));
//        }
//    });
//});
//This middleware provides detailed exception information for developers.
//It should only be used in the development environment.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage(); // For development only
}
app.UseMiddleware<ErrorHandlerMiddleware>();
app.MapControllers();

app.Run();

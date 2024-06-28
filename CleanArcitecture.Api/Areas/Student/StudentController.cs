using CleanArcitecture.Core.Features.Students.Queries.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArcitecture.Api.Areas.Student
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public StudentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("GetStudents")]
        public async Task<IActionResult> GetStudents()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return Ok(response);
        }
    }
}

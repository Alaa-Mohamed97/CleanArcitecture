using CleanArcitecture.Api.Base;
using CleanArcitecture.Core.Features.Students.Command.Models;
using CleanArcitecture.Core.Features.Students.Queries.Models;
using CleanArcitecture.Domain.AppMetaData;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CleanArcitecture.Api.Areas.Student
{
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class StudentController : AppBaseController
    {
        public StudentController(IMediator mediator) : base(mediator)
        {

        }
        [HttpGet(Router.StudentRouting.List)]
        public async Task<IActionResult> GetStudents()
        {
            var response = await _mediator.Send(new GetStudentListQuery());
            return NewResult(response);
        }
        [HttpGet(Router.StudentRouting.GetById)]
        public async Task<IActionResult> GetStudentById([FromRoute] int Id)
        {
            var response = await _mediator.Send(new GetStudentDetailsQuery(Id));
            return NewResult(response);
        }
        [HttpPost(Router.StudentRouting.Create)]
        public async Task<IActionResult> Create([FromBody] AddStudentRequestCommand addStudentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var response = await _mediator.Send(addStudentDTO);
            return NewResult(response);
        }
        [Authorize(Policy = "UpdateStudent")]
        [HttpPut(Router.StudentRouting.Edit)]
        public async Task<IActionResult> Edit([FromBody] EditStudentCommand editStudentCommand)
        {
            var response = await _mediator.Send(editStudentCommand);
            return NewResult(response);
        }
        [HttpDelete(Router.StudentRouting.Delete)]

        public async Task<IActionResult> Delete([FromRoute] int Id)
        {
            var response = await _mediator.Send(new DeleteStudentCommand(Id));
            return NewResult(response);
        }
    }
}

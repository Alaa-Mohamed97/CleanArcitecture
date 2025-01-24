using CleanArcitecture.Core.Base;
using CleanArcitecture.Service.Dtos;
using MediatR;

namespace CleanArcitecture.Core.Features.Authentication.Command.Models
{
    public class ResetPasswordCommand : ResetPasswordDTO, IRequest<Response<string>>
    {
    }
}

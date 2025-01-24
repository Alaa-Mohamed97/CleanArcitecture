using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.ApplicationUser.Command.Models
{
    public class ChangeUserPasswordCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string CurrentPassword { get; set; } = null!;
        public string NewPassword { get; set; } = null!;
        public string ConfirmNewPassword { get; set; } = null!;
    }
}

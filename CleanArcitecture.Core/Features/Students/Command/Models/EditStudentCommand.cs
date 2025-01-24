using CleanArcitecture.Core.Base;
using MediatR;

namespace CleanArcitecture.Core.Features.Students.Command.Models
{
    public class EditStudentCommand : IRequest<Response<string>>
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameAr { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int? DID { get; set; }
    }
}

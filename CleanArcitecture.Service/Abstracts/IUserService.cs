using CleanArcitecture.Domain.Entities;

namespace CleanArcitecture.Service.Abstracts
{
    public interface IUserService
    {
        Task<User> GetCurrentUser();
        string GetUserId();

    }
}

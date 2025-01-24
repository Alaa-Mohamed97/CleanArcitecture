using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure.BaseRepositories;

namespace CleanArcitecture.Infrastructure.Abstracts
{
    public interface IRefreshTokenRepository : IGenericRepositoryAsync<UserRefreshToken>
    {

    }
}

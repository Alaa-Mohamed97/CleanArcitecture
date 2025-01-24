using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure.Abstracts;
using CleanArcitecture.Infrastructure.BaseRepositories;
using CleanArcitecture.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArcitecture.Infrastructure.Repositories
{
    public class RefreshTokenRepository : GenericRepositoryAsync<UserRefreshToken>, IRefreshTokenRepository
    {
        #region Fields
        private DbSet<UserRefreshToken> _userRefreshToken;
        #endregion

        #region Constructors
        public RefreshTokenRepository(AppDBContext dbContext) : base(dbContext)
        {
            _userRefreshToken = dbContext.Set<UserRefreshToken>();
        }
        #endregion

        #region Handle Functions

        #endregion
    }
}

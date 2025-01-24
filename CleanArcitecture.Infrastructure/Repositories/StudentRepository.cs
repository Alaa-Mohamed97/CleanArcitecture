using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure.Abstracts;
using CleanArcitecture.Infrastructure.BaseRepositories;
using CleanArcitecture.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArcitecture.Infrastructure.Repositories
{
    public class StudentRepository : GenericRepositoryAsync<Student>, IStudentRepository
    {
        #region fields
        private readonly DbSet<Student> _students;
        #endregion
        #region Contructors
        public StudentRepository(AppDBContext context) : base(context)
        {
            _students = context.Set<Student>();
        }
        #endregion
        #region Methods
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _students.Include(d => d.Department).ToListAsync();
        }
        #endregion
    }
}

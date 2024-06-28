using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure.Abstracts;
using CleanArcitecture.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArcitecture.Infrastructure.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        #region fields
        private readonly AppDBContext _context;
        #endregion
        #region Contructors
        public StudentRepository(AppDBContext context)
        {
            _context = context;
        }
        #endregion
        #region Methods
        public async Task<List<Student>> GetStudentsAsync()
        {
            return await _context.Students.ToListAsync();
        }
        #endregion
    }
}

using CleanArcitecture.Domain.Entities;
using CleanArcitecture.Infrastructure.Abstracts;
using CleanArcitecture.Service.Abstracts;
using Microsoft.EntityFrameworkCore;

namespace CleanArcitecture.Service.Services
{
    public class StudentService : IStudentService
    {
        #region fields
        private readonly IStudentRepository _studentRepository;
        #endregion
        #region Contructors
        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }


        #endregion
        #region Methods
        public async Task<List<Student>> GetStudentsListAsync()
        {
            return await _studentRepository.GetStudentsAsync();
        }
        public async Task<Student> GetStudentByIdAsync(int studentId)
        {
            return _studentRepository.GetTableNoTracking()
                    .Where(s => s.StudID == studentId)
                    .Include(d => d.Department)
                    .FirstOrDefault();
        }

        public async Task<string> Create(Student student)
        {
            await _studentRepository.AddAsync(student);
            return "Success";
        }
        public async Task<string> Edit(Student student)
        {

            await _studentRepository.UpdateAsync(student);
            return "Success";
        }

        public async Task<bool> IsNameExist(string studentName)
        {
            var isExist = _studentRepository.GetTableNoTracking()
                                           .Any(s => s.NameEn.Equals(studentName));

            if (isExist)
                return true;
            else
                return false;
        }

        public async Task<bool> IsNameExistExcludeSelf(string studentName, int id)
        {
            var isExist = _studentRepository.GetTableNoTracking()
                                            .Any(s => s.NameEn.Equals(studentName) &&
                                                       s.StudID != id);

            if (isExist)
                return true;
            else
                return false;
        }

        public async Task<string> Delete(Student student)
        {
            await _studentRepository.DeleteAsync(student);
            return "Success";

        }
        #endregion
    }
}

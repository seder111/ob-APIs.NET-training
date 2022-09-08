using Microsoft.EntityFrameworkCore;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly UniversityDbContext _context;

        public StudentsService(UniversityDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Student>> GetStudentsWithCourses()
        {
            var students = _context.Students;

            var studentsWithCourses = from student in students
                                      where student.Courses.Any()
                                      select student;
            return studentsWithCourses.Include(s => s.Courses);
        }

        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            throw new NotImplementedException();
        }
    }
}

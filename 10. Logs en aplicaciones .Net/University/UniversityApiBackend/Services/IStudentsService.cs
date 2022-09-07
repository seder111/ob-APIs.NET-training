using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IStudentsService
    {

        public Task<IEnumerable<Student>> GetStudentsWithCourses();
        public IEnumerable<Student> GetStudentsWithNoCourses();
    }
}

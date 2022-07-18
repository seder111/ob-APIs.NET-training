using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface ICoursesService
    {
        // TODO: resolve Methods

        IEnumerable<Course> GetCoursesSpecificCategory();
        IEnumerable<Course> GetCoursesWithoutChapters();
        IEnumerable<Course> GetCoursesOfSpecificStudent();
    }
}

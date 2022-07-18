using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend.Services
{
    public interface IChaptersService
    {

        IEnumerable<Chapter> GetChapterOfSpecificCourse();
    }
}

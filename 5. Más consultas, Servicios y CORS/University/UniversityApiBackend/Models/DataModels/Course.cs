using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{

    public enum Level
    {
        Basic,
        Medium,
        Advanced,
        Expert
    };
    public class Course : BaseEntity
    {
        [Required, StringLength(120)]
        public string NameCourse { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public ICollection<Category> Categories { get; set; } = new List<Category>();
        [Required]
        public Chapter Chapters { get; set; } = new Chapter();
        [Required]
        public ICollection<Student> Students { get; set; } = new List<Student>();
        [Required]
        public Level Level { get; set; } = Level.Basic;
    }
}

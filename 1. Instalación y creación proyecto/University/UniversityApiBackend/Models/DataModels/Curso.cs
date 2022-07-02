using System.ComponentModel.DataAnnotations;

namespace UniversityApiBackend.Models.DataModels
{

    public class Curso : BaseEntity
    {
        public enum AllLevels
        {
            Básico,
            Intermedio,
            Avanzado
        };


        [Required, StringLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required, StringLength(280)]
        public string ShortDescription { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string TargetAudiences { get; set; } = string.Empty;
        public string Objective { get; set; } = string.Empty;
        public string Requirements { get; set; } = string.Empty;
        public AllLevels Level { get; set; }

      

    }
}

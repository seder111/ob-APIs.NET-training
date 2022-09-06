namespace ApiStoreOB.DTO
{
    public class RatingGuid
    {
        public Guid guidId { get; set; } = Guid.NewGuid();
        public float? rate { get; set; }
        public int? count { get; set; }
    }
}

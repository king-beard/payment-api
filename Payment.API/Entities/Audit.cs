namespace Payment.API.Entities
{
    public class Audit
    {
        public DateTime Created { get; set; } = DateTime.UtcNow;
        public DateTime Updated { get; set; } = DateTime.UtcNow;
        public int IsActive { get; set; } = 1;
    }
}

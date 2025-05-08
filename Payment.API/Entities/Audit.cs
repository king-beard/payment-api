namespace Payment.API.Entities
{
    public class Audit
    {
        public DateTime Created { get; set; } = DateTime.Now;
        public DateTime Updated { get; set; } = DateTime.Now;
        public int IsActive { get; set; } = 1;
    }
}

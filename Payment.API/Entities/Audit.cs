namespace Payment.API.Entities
{
    public class Audit
    {
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public int IsActive { get; set; }
    }
}

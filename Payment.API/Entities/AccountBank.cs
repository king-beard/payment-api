namespace Payment.API.Entities
{
    public class AccountBank : Audit
    {
        public Guid Id { get; set; }
        public string AccountNumber { get; set; }
        public string Bank { get; set; }
    }
}

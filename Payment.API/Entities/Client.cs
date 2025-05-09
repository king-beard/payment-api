namespace Payment.API.Entities
{
    public class Client : Audit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}

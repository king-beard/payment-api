namespace Payment.API.Entities
{
    public class Clients : Audit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
    }
}

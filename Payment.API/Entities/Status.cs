namespace Payment.API.Entities
{
    public sealed class Status : Audit
    {
        public Guid Id { get; set; }
        public string Prefix { get; set; }
        public string Description { get; set; }
    } 
}

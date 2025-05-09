namespace Payment.API.Entities
{
    public sealed class Shop : Audit
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
    }
}

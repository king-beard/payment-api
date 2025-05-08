namespace Payment.API.Entities
{
    public sealed class Payments : Audit
    {
        public Guid Id { get; set; }
        public string Concept { get; set; }
        public string Amount { get; set; }
        public string ProductsNumber { get; set; }
        public Guid StatusId { get; set; }
        public Guid ClientId { get; set; }
        public Guid ShopId { get; set; }
    }
}

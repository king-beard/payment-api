namespace Payment.API.Entities
{
    public sealed class Payments : Audit
    {
        public Guid Id { get; set; }
        public string Concept { get; set; }
        public decimal Amount { get; set; }
        public int ProductsNumber { get; set; }
        public Guid StatusId { get; set; }
        //public Guid ClientId { get; set; }
        //public Guid ShopId { get; set; }
    }
}

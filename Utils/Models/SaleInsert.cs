namespace Utils.Models
{
    public class SaleInsert
    {
        public string OrderId { get; set; }
        public string CustomerId { get; set; }
        public string ProductId { get; set; }
        public decimal Sales { get; set; }
        public int Quantity { get; set; }
        public decimal Discount { get; set; }
        public decimal Profit { get; set; }
    }
}

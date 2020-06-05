namespace Utils.Models
{
    public class Sale
    {
        public Sale(string order, string customer, string product, int quantity, decimal sales)
        {
            Order = order;
            Customer = customer;
            Product = product;
            Quantity = quantity;
            Sales = sales;
        }

        public Sale()
        {
        }

        public override string ToString()
        {
            return
                $"Order Id: {Order}, Customer Name: {Customer}, Product: {Product}, Quantity: {Quantity}, Sales: {Sales}";
        }

        public string Order { get; set; }
        public string Customer { get; set; }
        public string Product { get; set; }
        public int Quantity { get; set; }
        public decimal Sales { get; set; }
    }
}

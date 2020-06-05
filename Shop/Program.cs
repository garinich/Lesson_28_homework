using System;
using System.Linq;
using BusinessLogicApp;
using Utils.Models;

namespace Shop
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var allSale = new SaleManager();
            var sales = allSale.GetAllSale();

            sales.ToList().ForEach(Console.WriteLine);

            Console.WriteLine($"\nDelete sale by id = 2001: {allSale.DeleteSaleById(2001)}");

            Console.WriteLine("\nGet sale by id = 2:");
            Console.WriteLine(allSale.GetSaleById(2));

            var count = allSale.InsertSale(
                saleInsert: new SaleInsert()
                {
                    OrderId = "CA-2016-152156",
                    CustomerId = "EW-12520",
                    ProductId = "SOF-B1-10004755",
                    Sales = (decimal) 123.25,
                    Quantity = 5,
                    Discount = 0,
                    Profit = (decimal) 34.35
                }
            );

            Console.WriteLine($"\nInsert sale: {count}");
        }
    }
}

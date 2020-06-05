using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Utils.Interfaces;
using Utils.Models;

namespace DataAccessDapper
{
    public class SaleRepoDapper : ISaleRepository
    {
        private readonly string connection = string.Empty;

        public SaleRepoDapper()
        {
            Directory.SetCurrentDirectory("../../../");
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

            connection = configuration.GetConnectionString("DefaultConnection");
        }

        public IList<Sale> GetAllSale()
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var selection =
                    "select sale.order_id as 'Order', c.name as 'Customer', p.name as 'Product', sale.quantity, sale.sales " +
                    "from sale " +
                    "INNER JOIN customer c on sale.customer_id = c.id " +
                    "INNER JOIN product p on sale.product_id = p.id " +
                    "group by sale.order_id, c.name, p.name, sale.quantity, sale.sales";
                var sales = sqlConnection.Query<Sale>(selection).AsList();

                return sales;
            }
        }

        public Sale GetSaleById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var selection =
                    "select sale.order_id as 'Order', c.name as 'Customer', p.name as 'Product', sale.quantity, sale.sales " +
                    "from sale " +
                    "INNER JOIN customer c on sale.customer_id = c.id " +
                    "INNER JOIN product p on sale.product_id = p.id " +
                    "WHERE sale.id = @id " +
                    "group by sale.order_id, c.name, p.name, sale.quantity, sale.sales";

                var sale = sqlConnection.Query<Sale>(selection, new {id}).First();

                return sale;
            }
        }

        public int InsertSale(SaleInsert saleInsert)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var selection =
                    "INSERT INTO sale(order_id, customer_id, product_id, sales, quantity, discount, profit) " +
                    "VALUES (@order_id, @customer_id, @product_id, @sales, @quantity, @discount, @profit)";

                var count = sqlConnection.Execute(selection, new
                {
                    order_id = saleInsert.OrderId,
                    customer_id = saleInsert.CustomerId,
                    product_id = saleInsert.ProductId,
                    sales = saleInsert.Sales,
                    quantity = saleInsert.Quantity,
                    discount = saleInsert.Discount,
                    profit = saleInsert.Profit
                });

                return count;
            }
        }

        public int DeleteSaleById(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(connection))
            {
                var deleteSelection = "Delete from sale where id = @id";
                var count = sqlConnection.Execute(deleteSelection, new {id});

                return count;
            }
        }
    }
}

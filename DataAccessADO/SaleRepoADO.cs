using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Microsoft.Extensions.Configuration;
using Utils.Interfaces;
using Utils.Models;

namespace DataAccessADO
{
    public class SaleRepoADO : ISaleRepository
    {
        private readonly string connection = string.Empty;

        public SaleRepoADO()
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
            var salesList = new List<Sale>();

            var ordersSQL =
                            "select sale.order_id as 'Order', c.name as 'Customer', p.name as 'Product', sale.quantity, sale.sales " +
                            "from sale " +
                            "INNER JOIN customer c on sale.customer_id = c.id " +
                            "INNER JOIN product p on sale.product_id = p.id " +
                            "group by sale.order_id, c.name, p.name, sale.quantity, sale.sales";
            var adapter = new SqlDataAdapter(ordersSQL, connection);
            var salesSet = new DataSet();

            adapter.Fill(salesSet, "Employee");

            foreach (DataRow row in salesSet.Tables["Employee"].Rows)
            {
                salesList.Add( new Sale((string)row[0], (string)row[1], (string)row[2], (int)row[3], (decimal)row[4]));
            }

            return salesList;
        }

        public Sale GetSaleById(int id)
        {
            using (SqlConnection connectionStr = new SqlConnection(connection))
            {
                var ordersSQL =
                    "select sale.order_id as 'Order', c.name as 'Customer', p.name as 'Product', sale.quantity, sale.sales " +
                    "from sale " +
                    "INNER JOIN customer c on sale.customer_id = c.id " +
                    "INNER JOIN product p on sale.product_id = p.id " +
                    "WHERE sale.id = @id " +
                    "group by sale.order_id, c.name, p.name, sale.quantity, sale.sales";
                var adapter = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(ordersSQL, connectionStr);

                cmd.Parameters.Add("@id", SqlDbType.Int);
                cmd.Parameters["@id"].Value = id;

                adapter.SelectCommand = cmd;

                var salesSet = new DataSet();

                adapter.Fill(salesSet, "Sale");

                DataRow row = salesSet.Tables["Sale"].Rows[0];

                return new Sale((string)row[0], (string)row[1], (string)row[2], (int)row[3], (decimal)row[4]);
            }

        }

        public int InsertSale(SaleInsert saleInsert)
        {
            int affectedRows;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = connection;
                cn.Open();
                var cmdText =
                    "INSERT INTO sale(order_id, customer_id, product_id, sales, quantity, discount, profit) " +
                    "VALUES (@order_id, @customer_id, @product_id, @sales, @quantity, @discount, @profit)";

                using (SqlCommand cmd = new SqlCommand(cmdText, cn))
                {
                    cmd.Parameters.Add("@order_id", SqlDbType.Char, 14);
                    cmd.Parameters["@order_id"].Value = saleInsert.OrderId;

                    cmd.Parameters.Add("@customer_id", SqlDbType.Char, 8);
                    cmd.Parameters["@customer_id"].Value = saleInsert.CustomerId;

                    cmd.Parameters.Add("@product_id", SqlDbType.Char, 15);
                    cmd.Parameters["@product_id"].Value = saleInsert.ProductId;

                    cmd.Parameters.Add("@sales", SqlDbType.Decimal);
                    cmd.Parameters["@sales"].Value = saleInsert.Sales;

                    cmd.Parameters.Add("@quantity", SqlDbType.Int);
                    cmd.Parameters["@quantity"].Value = saleInsert.Quantity;

                    cmd.Parameters.Add("@discount", SqlDbType.Decimal);
                    cmd.Parameters["@discount"].Value = saleInsert.Discount;

                    cmd.Parameters.Add("@profit", SqlDbType.Decimal);
                    cmd.Parameters["@profit"].Value = saleInsert.Profit;

                    affectedRows = cmd.ExecuteNonQuery();
                }
                cn.Close();
            }

            return affectedRows;
        }

        public int DeleteSaleById(int id)
        {
            int affectedRows;
            using (SqlConnection cn = new SqlConnection())
            {
                cn.ConnectionString = connection;
                cn.Open();

                var cmdText =
                    "DELETE FROM sale WHERE id = @id";

                using (SqlCommand cmd = new SqlCommand(cmdText, cn))
                {
                    cmd.Parameters.Add("@id", SqlDbType.Int);
                    cmd.Parameters["@id"].Value = id;
                    affectedRows = cmd.ExecuteNonQuery();
                }

                cn.Close();
            }

            return affectedRows;
        }
    }
}

using System.Collections.Generic;
using Utils.Models;

namespace Utils.Interfaces
{
    public interface ISaleRepository
    {
        IList<Sale> GetAllSale();
        Sale GetSaleById(int id);

        int InsertSale(SaleInsert saleInsert);
        int DeleteSaleById(int id);
    }
}

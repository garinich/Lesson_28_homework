using System.Collections.Generic;
using DataAccessADO;
using DataAccessDapper;
using Utils.Interfaces;
using Utils.Models;

namespace BusinessLogicApp
{
    public class SaleManager
    {
        private readonly ISaleRepository _employeeRepository;

        public SaleManager()
        {
            _employeeRepository = new SaleRepoADO();
            // _employeeRepository = new SaleRepoDapper();
        }

        public IList<Sale> GetAllSale()
        {
            return _employeeRepository.GetAllSale();
        }

        public int DeleteSaleById(int id)
        {
            return _employeeRepository.DeleteSaleById(id);
        }

        public Sale GetSaleById(int id)
        {
            return _employeeRepository.GetSaleById(id);
        }

        public int InsertSale(SaleInsert saleInsert)
        {
            return _employeeRepository.InsertSale(saleInsert);
        }
    }
}

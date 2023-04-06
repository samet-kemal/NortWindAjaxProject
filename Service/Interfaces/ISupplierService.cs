using Core.Models;
using Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISupplierService
    {
        Task<CustomResponseViewModel<List<SupplierResponse>>> GetAllSuppliers();
        Task<CustomResponseViewModel<SupplierResponse>> GetSupplierById(int supplierId);
        Task<CustomResponseViewModel<int>> AddSupplier(CreateSupplierViewModel supplier);
        Task<CustomResponseViewModel<int>> UpdateSupplier(SupplierResponse supplier);
        Task<CustomResponseViewModel<int>> DeleteSupplier(int supplierId);



    }
}

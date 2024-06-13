using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Services.DTOs.Sale;
using WMS.Services.DTOs.Supplier;
using WMS.Services.DTOs.Supply;

namespace WMS.Services.Interfaces;

public interface ISupplierService
{
    List<SupplierDto> GetSuppliers();
    SupplierDto GetSupplierById(int id);
    SupplierDto Create(SupplierForCreateDto supplierForCreate);
    void Update(SupplierForUpdateDto supplierForUpdate);
    void Delete(int id);

}

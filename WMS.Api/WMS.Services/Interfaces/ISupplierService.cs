using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMS.Services.Interfaces;

public interface ISupplierService
{
    List<SupplierDto> GetSuppliers();
}

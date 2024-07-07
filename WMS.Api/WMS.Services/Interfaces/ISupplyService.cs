using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMS.Services.DTOs.Supply;

namespace WMS.Services.Interfaces
{
    public interface ISupplyService
    {
        List<SupplyDto> GetSupplies();
        SupplyDto GetSupplyById(int id);
        SupplyDto Create(SupplyForCreateDto supplyForCreate);
        void Update(SupplyForUpdateDto supplyForUpdate);
        void Delete(int id);
    }
}

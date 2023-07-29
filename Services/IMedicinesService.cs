using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineInventory.Models;

namespace MedicineInventory.Services
{
    public interface IMedicinesService
    {
        Task<Medicine> GetMedicineAsync( Guid id);

        Task<Guid> CreateMedicineAsync(string name, int quantity, Prescription prescription);
        Task<IEnumerable<Medicine>> ListMedicinesAsync();
        Task DeleteMedicineAsync(Guid id);

        Task UpdateMedicineAsync(Guid id,string name, int quantity, Prescription prescription);
    }
}
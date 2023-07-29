using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineInventory.Models;

namespace MedicineInventory.Services
{
    public interface IPatientsService
    {
        Task<Patient> GetPatientAsync(int id);

        Task<int> CreatePatientAsync(int patientId, string name, bool hasPrescription, Medicine medicine);

        Task DeletePatientAsync(int patientId);
    }
}
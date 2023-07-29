using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using MedicineInventory.Exceptions;
using MedicineInventory.Models;
using Microsoft.EntityFrameworkCore;

namespace MedicineInventory.Services
{
    public class PatientService :IPatientsService
    {
        private readonly MedicineDbContext _dbContext;

        public PatientService(MedicineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Patient> GetPatientAsync(int id)
        {
            var join = await _dbContext.Patients
                .Where(x => x.PatientId == id)
                .Include(x => x.Medicine)
                .SingleOrDefaultAsync();


            if(join == null) throw  new NotFoundException();

            JsonSerializer.Serialize(join);

            return join;
        }

        public async Task<int> CreatePatientAsync(int patientId, string name, bool hasPrescription, Medicine medicine)
        {
            var newMedicine = await _dbContext.Medicines
                .Where(x => x.Id == medicine.Id)
                .SingleOrDefaultAsync();

            //Make sure the medicine exist
            if(newMedicine == null) throw new NotFoundException();
            
            //Check if the medicine requires prescription
            if(newMedicine.Prescription == Prescription.RequiredPrescription && hasPrescription == false)
                throw new ConflictException();

            var patient = await _dbContext.Patients
                .Where(x => x.PatientId == patientId)
                .SingleOrDefaultAsync();
            
            if(patient != null) throw new ConflictException("Patient alreadt exist");

            var listMedicines = await _dbContext.Medicines.ToListAsync();

            var medResult = listMedicines
                            .Where(x => x.Id == medicine.Id)
                            .SingleOrDefault();
            
            if(medResult == null) throw new NotFoundException();

            if(listMedicines == null) throw new NotFoundException();

            newMedicine.Quantity = newMedicine.Quantity -1;
            
            var newPatient = new Patient
            {
                PatientId = patientId,
                Name = name,
                HasPrescription = hasPrescription,
                Medicine =  medResult
            };

            _dbContext.Patients.Add(newPatient);
            await _dbContext.SaveChangesAsync();

            return newPatient.Id;
        }

        public async Task DeletePatientAsync(int patientId)
        {
            var patient = await _dbContext.Patients
                .Where(x => x.PatientId == patientId)
                .Include(x => x.Medicine)
                .SingleOrDefaultAsync();

            if(patient == null) throw new NotFoundException();

            var addMedicine = await _dbContext.Medicines
                .Where(x => x.Id == patient.Medicine.Id)
                .SingleOrDefaultAsync();

            if(addMedicine == null) throw new NotFoundException();

            addMedicine.Quantity = addMedicine.Quantity +1;
            
            _dbContext.Patients.Remove(patient);
            await _dbContext.SaveChangesAsync();

        }



    }
}
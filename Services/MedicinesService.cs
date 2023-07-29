using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedicineInventory.Models;
using Microsoft.EntityFrameworkCore;
using MedicineInventory.Exceptions;
using System.Text.Json;

namespace MedicineInventory.Services
{
    public class MedicinesService : IMedicinesService
    {
        private readonly MedicineDbContext _dbContext;

        public MedicinesService(MedicineDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Medicine> GetMedicineAsync(Guid id)
        {
            var medicine = await _dbContext.Medicines
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();
            
            if(medicine == null) throw  new NotFoundException();

            JsonSerializer.Serialize(medicine);

            return medicine;
            
        }

        public async Task<Guid> CreateMedicineAsync(string name, int quantity, Prescription prescription)
        {
             // Make sure the name is unique
        if (await _dbContext.Medicines.AnyAsync(x => x.Name.Equals(name)))
            throw new ConflictException(1001, "Name already exists");

            var newMedicine = new Medicine
            {
                Id = Guid.NewGuid(),
                Name = name,
                Prescription = prescription,
                Quantity = quantity
            };

             _dbContext.Medicines.Add(newMedicine);
             await _dbContext.SaveChangesAsync();

             return newMedicine.Id;
        }

        public async Task<IEnumerable<Medicine>> ListMedicinesAsync()
        {
            return await _dbContext.Medicines.ToListAsync();
        }

        public async Task DeleteMedicineAsync(Guid id)
        {
            var foundMedicine = await _dbContext.Medicines
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if(foundMedicine == null) throw new NotFoundException("Medicine is not exist");

            _dbContext.Medicines.Remove(foundMedicine);
            await _dbContext.SaveChangesAsync();

        }

        public async Task UpdateMedicineAsync(Guid id, string name, int quantity, Prescription prescription)
        {
            var foundMedicine = await _dbContext.Medicines
                .Where(x => x.Id == id)
                .SingleOrDefaultAsync();

            if(foundMedicine == null) throw new NotFoundException("Medicine is not exist");

            
            foundMedicine.Name = name;
            foundMedicine.Quantity = quantity;
            foundMedicine.Prescription = prescription;

            await _dbContext.SaveChangesAsync();

        }
    }
}
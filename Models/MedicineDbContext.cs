using Microsoft.EntityFrameworkCore;

namespace MedicineInventory.Models
{
    public class MedicineDbContext : DbContext
    {
        public MedicineDbContext(DbContextOptions<MedicineDbContext> options) : base(options)
        {
        }

        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<Patient> Patients{ get; set; }
    }
}
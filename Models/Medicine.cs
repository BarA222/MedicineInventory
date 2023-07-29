using System.ComponentModel.DataAnnotations;

namespace MedicineInventory.Models
{
    public class Medicine
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public Prescription Prescription { get; set; }
        
    }
}
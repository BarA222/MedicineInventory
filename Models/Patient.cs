using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MedicineInventory.Models
{
    public class Patient
    {
        [Key]
        public int Id { get; set; }
        public int  PatientId { get; set; }
        public string Name { get; set; }
        public bool HasPrescription { get; set; }
        public Medicine Medicine { get; set; } 

    }
}
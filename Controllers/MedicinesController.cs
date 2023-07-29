using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MedicineInventory.Models;
using MedicineInventory.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MedicineInventory.Controllers
{
    [Route("[controller]")]
    public class MedicinesController : Controller
    {
       private readonly IMedicinesService _medicineService;

       public MedicinesController(IMedicinesService medicineService)
       {
        _medicineService = medicineService;
       }

       /// <summary>
        /// Get medicine
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{Id:Guid}")]
        public async Task<Medicine> GetMedicine([FromRoute] Guid id)
        {
            return await _medicineService.GetMedicineAsync(id);
        }

        /// <summary>
        /// Create medicine
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Guid>> CreateMedicine([FromBody] Medicine medicine)
        {
            var result =  await _medicineService.CreateMedicineAsync(medicine.Name, medicine.Quantity, medicine.Prescription);    

            return StatusCode(StatusCodes.Status201Created, new { MedicineId = result });    
        }

        /// <summary>
        /// List medicines
        /// </summary>
        [HttpGet]
        public async Task<IEnumerable<Medicine>> ListMedicines()
        {
            return await _medicineService.ListMedicinesAsync();
        }

        /// <summary>
        /// Delete medicines
        /// </summary>
        [HttpDelete]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> DeleteMedicine([FromRoute] Guid id)
        {
            await _medicineService.DeleteMedicineAsync(id);

            return Ok();
        }

        /// <summary>
        /// Update medicines
        /// </summary>
        [HttpPut]
        [Route("{Id:Guid}")]
        public async Task<IActionResult> UpdateMedicine([FromRoute] Guid id, [FromBody] Medicine medicin)
        {
            await _medicineService.UpdateMedicineAsync(id, medicin.Name, medicin.Quantity, medicin.Prescription);

            return Ok();
        }
    }
}
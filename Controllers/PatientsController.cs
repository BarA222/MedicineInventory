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
    public class PatientsController : Controller
    {
       private readonly IPatientsService _patientsService;

    public PatientsController(IPatientsService patientsService)
    {
        _patientsService = patientsService;
    }

    /// <summary>
    /// Get patients
    /// </summary>
    /// <param name="Id"></param>
    /// <returns></returns>
    [HttpGet]
    [Route("{Id}")]
    public async Task<Patient> GetPatient([FromRoute] int Id)
    {
        return await _patientsService.GetPatientAsync(Id);
    }

    /// <summary>
    /// Create patient
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<ActionResult<int>> CreatePatient([FromBody] Patient patient)
    {
        
        var result = await _patientsService.CreatePatientAsync(patient.PatientId, patient.Name, patient.HasPrescription, patient.Medicine);

         return StatusCode(StatusCodes.Status201Created, new { PatientId = result });   

    }

    /// <summary>
    /// Delete patient
    /// </summary>
    /// <returns></returns>
    [HttpDelete]
    [Route("{patientId}")]
    public async Task<ActionResult> DeletePatient([FromRoute] int patientId)
    {
        await _patientsService.DeletePatientAsync(patientId);

        return Ok();
    }
    }
}
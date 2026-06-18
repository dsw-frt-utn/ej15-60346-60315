using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Entities;

namespace Dsw2026Ej15.Api.Controllers;

[ApiController]
[Route("api")]
public class DoctorsController : AppController
{
    private readonly IPersistence _persistence;
    public DoctorsController(IPersistence persistence)
    {
        _persistence = persistence;
    }

    [HttpPost("doctors")]
    public async Task<IActionResult> CreateDoctor(DoctorModel.Request request)
    {
        if (string.IsNullOrWhiteSpace(request.Name) ||
            string.IsNullOrWhiteSpace(request.LicenseNumbre))
        {
            return BadRequest("el nombre y la matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);

        if (speciality is null)
        {
            return BadRequest("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumbre, true, speciality);

        _persistence.SaveDoctor(doctor);

        return Created();
    }
}

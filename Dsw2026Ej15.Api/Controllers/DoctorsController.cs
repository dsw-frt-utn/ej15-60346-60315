using Dsw2026Ej15.Api.Models;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Dsw2026Ej15.Data;
using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Exceptions;

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
            throw new ValidationException("el nombre y la matricula son requeridos");
        }

        var speciality = _persistence.GetSpecialityById(request.SpecialityId);

        if (speciality is null)
        {
            throw new ValidationException("Especialidad no existe");
        }

        var doctor = new Doctor(request.Name, request.LicenseNumbre, true, speciality);

        _persistence.SaveDoctor(doctor);

        return Created();
    }

    [HttpGet("doctors")]
    public IActionResult GetDoctors()
    {
        var doctors = _persistence.GetDoctors()
            .Where(d => d.IsActive)
            .Select(d => new
            {
                d.Id,
                d.Name,
                d.LicenseNumber,
                SpecialityName = d.Speciality?.Name
            });

        return Ok(doctors);
    }

    [HttpGet("doctors/{id}")]
    public IActionResult GetDoctorById(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor is null || !doctor.IsActive)
        {
            return NotFound();
        }

        return Ok(new
        {
            doctor.Name,
            doctor.LicenseNumber,
            SpecialityName = doctor.Speciality?.Name
        });
    }
    [HttpDelete("doctors/{id}")]
    public IActionResult DeleteDoctor(Guid id)
    {
        var doctor = _persistence.GetDoctorById(id);

        if (doctor is null || !doctor.IsActive)
        {
            return NotFound();
        }

        doctor.SetInactive();

        _persistence.UpdateDoctor(doctor);

        return NoContent();
    }
}

using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private readonly List<Doctor> _doctors;
        private readonly List<Speciality> _specialities;

        public PersistenceInMemory()
        {
            _doctors = new List<Doctor>();
            _specialities = LoadSpecialities();
        }

        public IReadOnlyCollection<Doctor> GetDoctors()
        {
            return _doctors.AsReadOnly();
        }

        public Doctor? GetDoctorById(Guid id)
        {
            return _doctors.FirstOrDefault(doctor => doctor.Id == id);
        }

        public void AddDoctor(Doctor doctor)
        {
            _doctors.Add(doctor);
        }

        public void UpdateDoctor(Doctor doctor)
        {
            var existingDoctor = GetDoctorById(doctor.Id);

            if (existingDoctor is null)
            {
                return;
            }

            _doctors.Remove(existingDoctor);
            _doctors.Add(doctor);
        }

        public IReadOnlyCollection<Speciality> GetSpecialities()
        {
            return _specialities.AsReadOnly();
        }

        public Speciality? GetSpecialityById(Guid id)
        {
            return _specialities.FirstOrDefault(speciality => speciality.Id == id);
        }

        private List<Speciality> LoadSpecialities()
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "specialities.json");

            if (!File.Exists(filePath))
            {
                return new List<Speciality>();
            }

            var json = File.ReadAllText(filePath);

            var specialities = JsonSerializer.Deserialize<List<Speciality>>(
                json,
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return specialities ?? new List<Speciality>();
        }
    }
}

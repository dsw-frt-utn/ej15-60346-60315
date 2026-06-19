using Dsw2026Ej15.Domain.Entities;
using Dsw2026Ej15.Domain.Interfaces;
using Dsw2026Ej15.Data.Dtos;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace Dsw2026Ej15.Data
{
    public class PersistenceInMemory : IPersistence
    {
        private List<Doctor> _doctors= [];
        private List<Speciality> _specialities= [];

        public PersistenceInMemory()
        {
            _doctors = new List<Doctor>();
            _specialities = new List<Speciality>();
            LoadSpecialities();
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
            return _specialities.SingleOrDefault(e => e.Id == id);
        }

        private void LoadSpecialities()
        {
          try
            {
                string jsonPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Sources", "Specialities.Json");
                var json = File.ReadAllText(jsonPath);
                var specialities = JsonSerializer.Deserialize<List<SpecialityDto>>(json, new JsonSerializerOptions()
                {
                    PropertyNameCaseInsensitive = true,
                }) ?? [];
                _specialities = [.. specialities.Select(s => new Speciality(s.Name, s.Description, s.Id))];
            }
            catch(Exception ex)
            {
        }
    }

        public void SaveDoctor(Doctor doctor)
        {
           _doctors.Add(doctor);
        }
    }
}

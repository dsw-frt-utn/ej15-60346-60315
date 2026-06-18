using Dsw2026Ej15.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Interfaces
{
    public interface IPersistence
    {
        IReadOnlyCollection<Doctor> GetDoctors();
        void SaveDoctor(Doctor doctor);

        Doctor? GetDoctorById(Guid id);

        void AddDoctor(Doctor doctor);

        void UpdateDoctor(Doctor doctor);

        IReadOnlyCollection<Speciality> GetSpecialities();

        Speciality? GetSpecialityById(Guid id);
    }
}

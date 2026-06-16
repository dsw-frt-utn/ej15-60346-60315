using System;
using System.Collections.Generic;
using System.Text;

namespace Dsw2026Ej15.Domain.Entities
{
    public class Speciality
    {
        public string Name { get; init; }

        public string Description { get; init; }

        public Speciality(string name, string description)
        {
            Name = name;
            Description = description;
        }
    }
}

using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface IDoctorRepository : IBaseRepository<Doctor>
	{
        public IEnumerable<Doctor> GetBySpec(Specialization specialization);
    }
}


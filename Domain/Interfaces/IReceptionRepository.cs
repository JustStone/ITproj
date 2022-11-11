using System;
using Domain.Models;

namespace Domain.Interfaces
{
	public interface IReceptionRepository : IBaseRepository<Reception>
	{
        bool CheckFreeBySpec(DateTime time, Specialization specialization);

        bool CheckFreeByDoctor(DateTime time, Doctor doctor);

        IEnumerable<DateTime> GetFreeBySpec(Specialization specialization);

        IEnumerable<DateTime> GetFreeByDoctor(Doctor doctor);

        Reception CreateBySpec(DateTime dateTime, Specialization specialization);
    }
}


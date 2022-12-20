using System;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface IReceptionRepository : IBaseRepository<Reception>
    {
        IEnumerable<Reception> GetReceptsByDoctor(Doctor doctor);

        IEnumerable<Reception> GetReceptsBySpec (Specialization spec);

        IEnumerable<DateTime> GetFreeReceptsBySpec(Specialization specialization);

        Reception CreateBySpec(DateTime dateTime, Specialization specialization);
    }
}
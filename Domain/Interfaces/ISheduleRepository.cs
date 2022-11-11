using System;
using Domain.Models;

namespace Domain.Interfaces
{
    public interface ISheduleRepository : IBaseRepository<Shedule>
    {
        IEnumerable<Shedule> GetSheduleByDate(Doctor doctor, DateOnly date);
    }
}


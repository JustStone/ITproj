using System;
using BD_.Convert;
using BD_.Models;
using Domain.Models;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace BD_.Repositories
{
    public class SheduleR : ISheduleRepository
    {
        private readonly ApplicationContext _bd;

        public SheduleR(ApplicationContext bd)
        {
            _bd = bd;
        }

        public Shedule Create(Shedule x)
        {
            _bd.Shedules.Add(x.ToModel());
            return x;
        }

        public Shedule Update(Shedule x)
        {
            _bd.Shedules.Update(x.ToModel());
            return x;
        }

        public Shedule? Get(int id)
        {
            return _bd.Shedules.FirstOrDefault(t => t.Id == id).ToDomain();
        }
        public IEnumerable<Shedule> List()
        {
            return _bd.Shedules.Select(t => t.ToDomain()).ToList();
        }

        public bool Exists(int id)
        {
            return _bd.Shedules.Any(t => t.Id == id);
        }

        public bool Delete(int id)
        {
            var x = _bd.Shedules.FirstOrDefault(t => t.Id == id);
            if (x == default)
            {
                return false;
            }
            _bd.Shedules.Remove(x);
            return true;
        }

        public bool IsValid(Shedule x)
        {
            if (x.Id < 0)
                return false;

            if (x.StartTime >= x.EndTime)
                return false;

            return true;
        }

        public IEnumerable<Shedule> GetSheduleByDate(Doctor doctor, DateOnly date)
        {
            return _bd.Shedules.Where(t => t.DoctorId == doctor.Id &&
                         t.StartTime.Date == date.ToDateTime(new TimeOnly()))
                .Select(t => t.ToDomain());
        }
    }

}
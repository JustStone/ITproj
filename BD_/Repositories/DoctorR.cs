using System;
using BD_.Models;
using BD_.Convert;
using Domain.Interfaces;
using Domain.Models;

namespace BD_.Repositories
{
    public class DoctorR : IDoctorRepository
    {
        private readonly ApplicationContext bd_;

        public DoctorR(ApplicationContext bd)
        {
            bd_ = bd;
        }

        public Doctor Create(Doctor x)
        {
            bd_.Doctors.Add(x.ToModel());
            return x;
        }

        public Doctor Update(Doctor x)
        {
            bd_.Doctors.Update(x.ToModel());
            return x;
        }

        public Doctor? Get(int id)
        {
            var x = bd_.Doctors.FirstOrDefault(t => t.Id == id);
            return x?.ToDomain();
        }

        public bool Exists(int id)
        {
            return bd_.Doctors.Any(t => t.Id == id);
        }

        public bool Delete(int id)
        {
            var x = bd_.Doctors.FirstOrDefault(t => t.Id == id);
            if (x == default)
            {
                return false;
            }
            bd_.Doctors.Remove(x);
            return true;
        }

        public bool IsValid(Doctor x)
        {
            if (x.Id < 0)
                return false;

            if (string.IsNullOrEmpty(x.FullName))
                return false;

            return true;
        }

        public IEnumerable<Doctor?> List()
        {
            return bd_.Doctors.Select(x => x.ToDomain()).ToList();
        }

        public IEnumerable<Doctor> GetBySpec(Specialization x)
        {
            return bd_.Doctors.Where(
                t => t.Specialization == x.ToModel()).Select(t => t.ToDomain());
        }
    }
}

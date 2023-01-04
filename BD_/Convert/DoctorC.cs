using System;
using Domain.Models;
using BD_.Models;
namespace BD_.Convert
{
    public static class DoctorC
    {
        public static Doctor? ToDomain(this DoctorM x)
        {
            return new Doctor(
                x.Id,
                x.FullName,
                x.Specialization.ToDomain()
            );
        }

        public static DoctorM ToModel(this Doctor y)
        {
            return new DoctorM
            {
                Id = y.Id,
                FullName = y.FullName,
                Specialization = y.Specialization.ToModel()
            };
        }
    }
}


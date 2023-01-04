using System;
using Domain.Models;
using BD_.Models;
namespace BD_.Convert
{
    public static class ScheduleC
    {
        public static Shedule? ToDomain(this SheduleM x)
        {
            return new Shedule(
                x.Id,
                x.DoctorId,
                x.StartTime,
                x.EndTime
            );
        }

        public static SheduleM ToModel(this Shedule y)
        {
            return new SheduleM
            {
                Id = y.Id,
                DoctorId = y.DoctorId,
                StartTime = y.StartTime,
                EndTime = y.EndTime
            };
        }

    }
}


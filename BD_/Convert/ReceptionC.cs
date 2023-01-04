using System;
using Domain.Models;
using BD_.Models;
namespace BD_.Convert
{
    public static class AppointmentC
    {
        public static Reception? ToDomain(this ReceptionM x)
        {
            return new Reception(
                x.Id,
                x.StartRecept,
                x.EndRecept,
                x.PatientId,
                x.DoctorId
            );
        }

        public static ReceptionM ToModel(this Reception y)
        {
            return new ReceptionM
            {
                Id = y.Id,
                StartRecept = y.StartRecept,
                EndRecept = y.EndRecept,
                PatientId = y.PatientId,
                DoctorId = y.DoctorId
            };
        }
        

    }
}


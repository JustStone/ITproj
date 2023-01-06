using System;
using Domain.Models;
using BD_.Models;

namespace BD_.Convert
{
    public static class SpecializationC
    {

        public static SpecializationM ToModel(this Specialization y)
        {
            return new SpecializationM
            {
                Id = y.Id,
                Name = y.Name
            };
        }

        public static Specialization? ToDomain(this SpecializationM x)
        {
            if (x == null)
                return new Specialization(0, "stop");

            return new Specialization(
                x.Id,
                x.Name
            );
        }



    }
}


using System;
using Domain.Models;
using BD_.Models;

namespace BD_.Convert
{
    public static class SpecializationC
    {
        public static Specialization? ToDomain(this SpecializationM x)
        {
            return new Specialization(
                x.Id,
                x.Name
            );
        }

        public static SpecializationM ToModel(this Specialization y)
        {
            return new SpecializationM
            {
                Id = y.Id,
                Name = y.Name
            };
        }


    }
}


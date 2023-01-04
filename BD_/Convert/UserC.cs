using System;
using Domain.Models;
using BD_.Models;

namespace BD_.Convert
{
    public static class UserC
    {
        public static User? ToDomain(this UserM x)
        {
            return new User(
                x.Username,
                x.Password,
                x.Id,
                x.PhoneNumber,
                x.FullName,
                x.Role);
        }

        public static UserM ToModel(this User y)
        {
            return new UserM
            {
                Id = y.Id,
                Username = y.Username,
                Password = y.Password,
                PhoneNumber = y.PhoneNumber,
                FullName = y.FullName,
                Role = y.Role

            };
        }
    }
}


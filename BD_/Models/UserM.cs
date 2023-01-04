using System;
using Domain.Models;
namespace BD_.Models
{
	public class UserM
	{
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string FullName { get; set; }
        public Role Role { get; set; }
    }
}


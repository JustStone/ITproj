using System;
namespace Domain
{
    public class User
    {
        public User() { }

        public int id;
        public int number;
        public string fullname;
        public Role role;


        public int ID { get; set; }

        public int Number { get; set; }

        public string FullName { get; set; }

        public Role Role { get; set; }
    }
}


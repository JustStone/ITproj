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


        public int ID
        {
            get { return id; }
        }

        public int Number
        {
            get { return number; }
            set { number = value; }
        }

        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        public Role Role
        {
            get { return role; }
            set { role = value; }
        }

        public void Reg(int id, int number, string fullname, string role)
        {

        }

        public void Auth(int id, int number, string fullname, string role)
        {

        }
    }
}


using System;
namespace Domain
{
    public class Specialization
    {
        public Specialization() { }
        public int id;
        public string name;

        public int ID
        {
            get { return id; }
        }

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        public string[] GetAllSpecs()
        {
            string[] array = new string[100];
            return array;
        }

    }
}


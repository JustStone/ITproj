using System;
namespace Domain
{
    public class Doctor
    {
        public Doctor() { }

        public int id;
        public string fullname;
        public Specialization specialization;

        public int ID
        {
            get { return id; }
        }

        public string FullName
        {
            get { return fullname; }
            set { fullname = value; }
        }

        public Specialization Specialization
        {
            get { return specialization; }
            set { specialization = value; }
        }

        public void Add(int id, string fullname, string specialization)
        {

        }

        public void Delete(int id, string fullname, string specialization)
        {

        }

        public int[] GetAllDocs()
        {
            int[] array = new int[100];
            return array;
        }

        public string FindDoc(int id)
        {
            return "something";
        }

        public string FindDoc(string specialization)
        {
            return "something";
        }
    }
}


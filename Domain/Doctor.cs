using System;
namespace Domain
{
    public class Doctor
    {
        public Doctor() { }

        public int id;
        public string fullname;
        public Specialization specialization;

        public int ID { get; set; }

        public string FullName { get; set; }

        public Specialization Specialization { get; set; }
    }
}


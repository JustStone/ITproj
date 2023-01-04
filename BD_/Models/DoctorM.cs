using System;
namespace BD_.Models
{
	public class DoctorM
	{
        public int Id { get; set; }
        public string FullName { get; set; }
        public SpecializationM Specialization { get; set; }
    }
}


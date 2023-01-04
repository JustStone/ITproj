using System;
namespace BD_.Models
{
	public class ReceptionM
	{
        public int Id { get; set; }
        public DateTime StartRecept { get; set; }
        public DateTime EndRecept { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
    }
}


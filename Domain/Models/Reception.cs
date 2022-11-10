using System;
namespace Domain.Models
{
	public class Reception
	{
        public int Id;
        public DateTime StartRecept;
        public DateTime EndRecept;
        public int PatientId;
        public int DoctorId;

        public Reception(int id, DateTime startRecept, DateTime endRecept, int patientId, int doctorId)
        {
            Id = id;
            StartRecept = startRecept;
            EndRecept = endRecept;
            PatientId = patientId;
            DoctorId = doctorId;
        }
    }
}


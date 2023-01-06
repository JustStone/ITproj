using System;
using System.Text.Json.Serialization;

namespace ITproject.Views
{
    public class ReceptionSearchView
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("start_recept")]
        public DateTime StartRecept { get; set; }

        [JsonPropertyName("end_recept")]
        public DateTime EndRecept { get; set; }

        [JsonPropertyName("patient_id")]
        public int PatientId { get; set; }

        [JsonPropertyName("doctor_id")]
        public int DoctorId { get; set; }
    }
}


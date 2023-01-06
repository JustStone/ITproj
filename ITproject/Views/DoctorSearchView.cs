using System;
using Domain.Models;
using System.Text.Json.Serialization;

namespace ITproject.Views
{
    public class DoctorSearchView
    {
        [JsonPropertyName("id")]
        public int DoctorId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("specialization")]
        public Specialization Specialization { get; set; }
    }
}


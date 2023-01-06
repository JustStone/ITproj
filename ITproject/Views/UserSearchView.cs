using System;
using Domain.Models;
using System.Text.Json.Serialization;

namespace ITproject.Views
{
    public class UserSearchView
    {
        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("phone_number")]
        public string PhoneNumber { get; set; }

        [JsonPropertyName("full_name")]
        public string FullName { get; set; }

        [JsonPropertyName("role")]
        public Role Role { get; set; }
    }
}


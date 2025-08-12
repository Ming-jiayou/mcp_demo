using System;
using System.Text.Json.Serialization;

namespace stdio_mcp_server_demo.Models
{
    public class Student
    {
        [JsonPropertyName("studentId")]
        public string StudentId { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("age")]
        public int Age { get; set; }

        [JsonPropertyName("major")]
        public string Major { get; set; }

        [JsonPropertyName("enrollmentDate")]
        public DateTime EnrollmentDate { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        public override string ToString()
        {
            return $"Student ID: {StudentId}\n" +
                   $"Name: {Name}\n" +
                   $"Age: {Age}\n" +
                   $"Major: {Major}\n" +
                   $"Enrollment Date: {EnrollmentDate:yyyy-MM-dd}\n" +
                   $"Email: {Email}";
        }
    }
}
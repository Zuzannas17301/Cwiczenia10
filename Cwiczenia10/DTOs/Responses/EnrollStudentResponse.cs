using System;

namespace Cwiczenia10.DTOs.Responses
{
    public class EnrollStudentResponse
    {
        public string IndexNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime  BirthDate { get; set; }
        public int Semester { get; set; }
        public string Study { get; set; }
    }
}
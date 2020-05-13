using System.ComponentModel.DataAnnotations;

namespace Cwiczenia10.DTOs.Requests
{
    public class PromoteStudentRequest
    {
        [Required] 
        [MaxLength(100)]
        public string StudiesName { get; set; }
        
        [Required] 
        public int Semester { get; set; }
    }
}
using System.Threading.Tasks;
using Cwiczenia10.DTOs;
using Cwiczenia10.DTOs.Requests;
using Cwiczenia10.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia10.Services
{
    public interface IDbService
    {
        Task<IActionResult> GetStudents();
        Task<IActionResult>  ModifyStudent(ModifyStudentRequest request);
        Task<IActionResult> DeleteStudent(DeleteStudentRequest request);
        Task<IActionResult> EnrollStudent(EnrollStudentRequest request);
        Task<IActionResult> PromoteStudent(PromoteStudentRequest request);

    }
}
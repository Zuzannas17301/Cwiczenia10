using System.Linq;
using System.Threading.Tasks;
using Cwiczenia10.DTOs;
using Cwiczenia10.DTOs.Requests;
using Cwiczenia10.Models;
using Cwiczenia10.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Cwiczenia10.Controllers
{
    [Route("api/students")]
    [ApiController]
        
    public class StudentsController : ControllerBase
    {
        private IDbService service;

        public StudentsController(IDbService service)
        {
            this.service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetStudents()
        {
         return Ok( await service.GetStudents());
        }

        [HttpPut()]
        public async Task<IActionResult> UpdateStudentInfo(ModifyStudentRequest request)
        {
            return Ok(await service.ModifyStudent(request));
        }

       [HttpDelete()]
        public async Task<IActionResult> DeleteStudent(DeleteStudentRequest request)
        {
            return Ok(await service.DeleteStudent(request));

        }

    }
}
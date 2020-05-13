using System.Threading.Tasks;
using Cwiczenia10.DTOs.Requests;
using Cwiczenia10.Services;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia10.Controllers
{
    [ApiController]
    [Route("api/controllers")]
    public class EnrollmentsController : ControllerBase
    {
        private IDbService service;

        public EnrollmentsController(IDbService service)
        {
            this.service = service;
        }

        // [HttpPost]
        // public async Task<IActionResult> EnrollStudent(EnrollStudentRequest request)
        // {
        //     
        // }
        
        
        // [HttpPost("promotions")]
        // public async Task<IActionResult> PromoteStudent(PromoteStudentRequest request)
        // {
        //     
        // }
    }
}
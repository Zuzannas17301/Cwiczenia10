using System.Linq;
using System.Threading.Tasks;
using Cwiczenia10.DTOs;
using Cwiczenia10.DTOs.Requests;
using Cwiczenia10.Models;
using Microsoft.AspNetCore.Mvc;

namespace Cwiczenia10.Services
{
    public class EfDbService : IDbService
    {
        private s17301Context _context;

        public EfDbService(s17301Context context)
        {
            this._context = context;
        }

        public async Task<IActionResult> GetStudents()
        {
            return new OkObjectResult(_context.Student.ToList());
        }

        
        public async Task<IActionResult> ModifyStudent(ModifyStudentRequest request)
        {
            var student = _context.Student.Where(s => s.IndexNumber == request.IndexNumber).SingleOrDefault();

            if (student == null)
                return new BadRequestResult();
            else
            {
                student.FirstName = request.FirstName;
                student.LastName = request.LastName;
                student.BirthDate = request.BirthDate;
                if (request.IdEnrollment != 0)
                    student.IdEnrollment = request.IdEnrollment;

                await _context.SaveChangesAsync();
                return new OkObjectResult(student);
            }
        }

        public async Task<IActionResult> DeleteStudent(DeleteStudentRequest request)
        {
            var student = _context.Student.First(s => s.IndexNumber == request.IndexNumber);
            
            if(student == null)
                return new BadRequestResult();
            else
            {
                _context.Remove(student);
                _context.SaveChanges();
            }
            return new OkObjectResult(student);
        }

        public Task<IActionResult> EnrollStudent(EnrollStudentRequest missing_name)
        {
            throw new System.NotImplementedException();
        }

        public Task<IActionResult> PromoteStudent(PromoteStudentRequest missing_name)
        {
            throw new System.NotImplementedException();
        }
    }
}
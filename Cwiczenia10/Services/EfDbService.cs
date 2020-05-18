using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Principal;
using System.Threading.Tasks;
using Cwiczenia10.DTOs;
using Cwiczenia10.DTOs.Requests;
using Cwiczenia10.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cwiczenia10.Services
{
    public class EfDbService : IDbService
    {
        private readonly s17301Context _context;

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

        public async Task<IActionResult> EnrollStudent(EnrollStudentRequest request)
        {
            try
            {
                // 1. czy istnieją studia 

                var res = Convert.ToInt32(_context.Studies.Where(s => s.Name == request.StudiesName).Select(s => new
                {
                    s.IdStudy
                }).FirstOrDefault());

                if (res == 0)
                    return new BadRequestResult(); //blad 400
                else
                {
                    //odnajdujemy najnowszy wpis w enrollments
                    var res2 = _context.Enrollment.FirstOrDefault(e => e.IdStudy == res && e.Semester == 1);
                    int idEnrollment;

                    if (res2 == null) //dodanie nowego wpisu do enrollment
                    {
                        idEnrollment = _context.Enrollment.Max(e => e.IdEnrollment) + 1;
                        _context.Enrollment.Add(new Enrollment()
                        {
                            IdEnrollment = idEnrollment,
                            Semester = 1,
                            IdStudy = res,
                            StartDate = DateTime.Now
                        });
                    }
                    else
                        idEnrollment = res2.IdEnrollment;

                    //sprawdzenie unikalności indeksu 

                    var res3 = _context.Student.FirstOrDefault(s => s.IndexNumber == request.IndexNumber);

                    if (res3 == null) //dodanie nowego studenta
                    {
                        _context.Student.Add(new Student()
                        {
                            IndexNumber = request.IndexNumber,
                            FirstName = request.FirstName,
                            LastName = request.LastName,
                            BirthDate = request.BirthDate,
                            IdEnrollment = idEnrollment
                        });
                    }
                    else
                        return new BadRequestResult(); //student o takim indeksie istnieje w bazie


                    await _context.SaveChangesAsync();
                    return new AcceptedResult();

                }

            }
            catch (Exception e)
            {
                
                return new BadRequestResult();
            }
        }

        public async Task<IActionResult> PromoteStudent(PromoteStudentRequest request)
        {
            var result = _context.Enrollment.Join(_context.Studies, e => e.IdStudy,
                s => s.IdStudy, ((e, s) => new
                {
                    s.Name,
                    e.Semester
                })).Where(row => row.Name == request.StudiesName && row.Semester == request.Semester);

            if (result.Any())
            {
                _context.Database.ExecuteSqlInterpolated($"EXEC promStudentv{request.StudiesName}, {request.Semester}");
               await _context.SaveChangesAsync();
               return new OkObjectResult(request);
            }else
                return new BadRequestResult();
        }
    }
}
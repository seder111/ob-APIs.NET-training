﻿using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;
using System.Linq;

namespace UniversityApiBackend.Services
{
    public class StudentsService : IStudentsService
    {
        private readonly UniversityDbContext _context;

        public StudentsService(UniversityDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Student> GetStudentsWithCourses()
        {

            var students = _context.Students;

            if (students == null)
            {
                return Enumerable.Empty<Student>();
            }

            var studentsWithCourses = from student in students
                                      where student.Courses.Any()
                                      select student;

            return studentsWithCourses.Include(s => s.Courses);
        }


        public IEnumerable<Student> GetStudentsWithNoCourses()
        {
            throw new NotImplementedException();
        }
    }
}

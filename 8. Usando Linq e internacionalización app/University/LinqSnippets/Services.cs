using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversityApiBackend.DataAccess;
using UniversityApiBackend.Models.DataModels;

namespace UniversityApiBackend
{
    public class Services
    {

        private readonly UniversityDBContext _context;

        public Services(UniversityDBContext context)
        {
            _context = context;
        }
        
        public IEnumerable<User> FindByEmail(string email)
        {
            var users = _context.Users;
            
            var usersWithMail =  from user in users
                                where user.Email == email
                                select user;

            return usersWithMail;
        }

        public IEnumerable<Student> FindOlderStudents()
        {
            var students = _context.Students;

            var olderStudents = from student in students
                                where student.Dob < DateTime.Now.AddYears(-18)
                                select student;

            return olderStudents;
        }

        public IEnumerable<Student> GetAllStudentsWithOneOrMoreCourses()
        {
            var students = _context.Students;

            var studentsWithAtLeastOneCourse = from student in students
                                               where student.Courses.Count() > 0
                                               select student;

            return studentsWithAtLeastOneCourse;
        }

        public IEnumerable<Course> FindCoursesByLevelAtLeastOneStudent(Level level)
        {
            var courses = _context.Courses;

            var coursesWithLevelAtLeastOneCourse = from course in courses
                                                   where course.Level == level && course.Students.Count() > 0
                                                   select course;

            return coursesWithLevelAtLeastOneCourse;
        }

        public IEnumerable<Course> FindCoursesByLevelWithCategoryX(Category category, Level level)
        {
            var courses = _context.Courses;

            var coursesWithLevelAndCategoryX = from course in courses
                                               where course.Level == level && course.Categories == category
                                               select course;

            return coursesWithLevelAndCategoryX;
        }

        public IEnumerable<Student> FindStudentsWithoutCourses()
        {
            var students = _context.Students;

            var studentsWithoutCourses = from student in students
                                         where student.Courses.Count() > 0
                                         select student;

            return studentsWithoutCourses;
        }
    }
}

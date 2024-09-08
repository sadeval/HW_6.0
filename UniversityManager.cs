using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace UniversityManagementSystem
{
    public class UniversityManager
    {
        private UniversityContext _context;

        public UniversityManager(UniversityContext context)
        {
            _context = context;
        }

        public List<Student> GetStudentsByCourse(int courseId)
        {
            return _context.Enrollments
                           .Where(e => e.CourseId == courseId)
                           .Select(e => e.Student)
                           .ToList();
        }

        public List<Course> GetCoursesByInstructor(int instructorId)
        {
            return _context.Courses
                           .Where(c => c.Instructor.InstructorId == instructorId)
                           .ToList();
        }

        public List<Course> GetCoursesWithStudentsByInstructor(int instructorId)
        {
            return _context.Courses
                           .Where(c => c.Instructor.InstructorId == instructorId)
                           .Include(c => c.Enrollments)
                           .ThenInclude(e => e.Student)
                           .ToList();
        }

        public List<Course> GetCoursesWithMoreThanFiveStudents()
        {
            return _context.Courses
                           .Where(c => c.Enrollments.Count > 5)
                           .ToList();
        }

        public List<Student> GetStudentsOlderThan25()
        {
            return _context.Students
                           .Where(s => EF.Functions.DateDiffYear(s.DateOfBirth, DateTime.Now) > 25)
                           .ToList();
        }

        public double GetAverageAgeOfStudents()
        {
            return _context.Students
                           .Average(s => EF.Functions.DateDiffYear(s.DateOfBirth, DateTime.Now));
        }

        public Student GetYoungestStudent()
        {
            return _context.Students
                           .OrderByDescending(s => s.DateOfBirth)
                           .FirstOrDefault()!;
        }

        public int GetCourseCountForStudent(int studentId)
        {
            return _context.Enrollments
                           .Count(e => e.StudentId == studentId);
        }

        public List<string> GetAllStudentNames()
        {
            return _context.Students
                           .Select(s => s.FirstName + " " + s.LastName)
                           .ToList();
        }

        public List<IGrouping<int, Student>> GroupStudentsByAge()
        {
            return _context.Students
                           .GroupBy(s => EF.Functions.DateDiffYear(s.DateOfBirth, DateTime.Now))
                           .ToList();
        }

        public List<Student> GetStudentsSortedByLastName()
        {
            return _context.Students
                           .OrderBy(s => s.LastName)
                           .ToList();
        }

        public List<Student> GetStudentsWithEnrollments()
        {
            return _context.Students
                           .Include(s => s.Enrollments)
                           .ToList();
        }

        public List<Student> GetStudentsNotEnrolledInCourse(int courseId)
        {
            return _context.Students
                           .Where(s => !s.Enrollments.Any(e => e.CourseId == courseId))
                           .ToList();
        }

        public List<Student> GetStudentsEnrolledInTwoCourses(int courseId1, int courseId2)
        {
            return _context.Students
                           .Where(s => s.Enrollments.Any(e => e.CourseId == courseId1) &&
                                       s.Enrollments.Any(e => e.CourseId == courseId2))
                           .ToList();
        }

        public List<(Course, int)> GetStudentCountForEachCourse()
        {
            return _context.Courses
                           .Select(c => new { Course = c, StudentCount = c.Enrollments.Count })
                           .AsEnumerable()
                           .Select(c => (c.Course, c.StudentCount))
                           .ToList();
        }
    }
}

using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace UniversityManagementSystem
{
    public class Student
    {
        public int StudentId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
    }

    public class Course
    {
        public int CourseId { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Enrollment> Enrollments { get; set; }
        public virtual Instructor Instructor { get; set; }
    }

    public class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int StudentId { get; set; }
        public int CourseId { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public virtual Student Student { get; set; }
        public virtual Course Course { get; set; }
    }

    public class Instructor
    {
        public int InstructorId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public virtual ICollection<Course> Courses { get; set; }
    }

    public class UniversityContext : DbContext
    {
        public DbSet<Student> Students { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Instructor> Instructors { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=UniversityDB;Trusted_Connection=True;");
        }
    }
}

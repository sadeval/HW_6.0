using System;
using UniversityManagementSystem;

class Program
{
    static void Main()
    {
        using (var context = new UniversityContext())
        {

            UniversityManager manager = new UniversityManager(context);

            var studentsInCourse = manager.GetStudentsByCourse(1);
            var coursesByInstructor = manager.GetCoursesByInstructor(2);
            var coursesWithStudents = manager.GetCoursesWithStudentsByInstructor(2);
            var coursesWithMoreThanFiveStudents = manager.GetCoursesWithMoreThanFiveStudents();
            var studentsOlderThan25 = manager.GetStudentsOlderThan25();

            Console.WriteLine("Студенты, зачисленные на курс 1:");
            foreach (var student in studentsInCourse)
            {
                Console.WriteLine($"{student.FirstName} {student.LastName}");
            }

        }
    }
}

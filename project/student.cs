using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
    public class Student
    {
        private int studentID;
        private string studentName;
        private Course[] courses;
        public Student()
        {
            studentID = 0;
            studentName = "Unnamed";
            courses = new Course[100];
        }
        public Student(int id, string name, Course[] courses)
        {
            this.studentID = id;
            this.studentName = name;
            this.courses = courses;
        }
        public int StudentID
        {
            get { return studentID; }
            set
            {
                if (value <= 0)
                    Console.WriteLine("Invalid Student ID.");
                else
                    studentID = value;
            }
        }
        public string StudentName
        {
            get { return studentName; }
            set { studentName = value; }

        }
        public Course[] Courses
        {
            get { return courses; }
            set { courses = value; }
        }
        public void DisplayStudentInfo()
        {
            Console.WriteLine("Student ID: " + studentID + ", Name: " + studentName);
            if (courses != null && courses.Length > 0)
            {
                Console.WriteLine("Enrolled Courses:");
                foreach (Course c in courses)
                {
                    c.DisplayCourseInfo();
                }
            }
            else
            {
                Console.WriteLine("No courses enrolled.");
            }
        }
    }
}
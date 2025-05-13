using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student_Project
{
    public class Course
    {
        private int courseID;
        private string courseName;
        public Course()
        {
            courseID = 0;
            courseName = "Unknown";
        }
        public Course(int id, string name)
        {
            courseID = id;
            courseName = name;
        }
        public int CourseID
        {
            get { return courseID; }
            set
            {
                if (value <= 0)
                {
                    Console.WriteLine("Invalid Course ID.");
                }
                else
                    courseID = value;

            }
        }
        public string CourseName
        {
            get { return courseName; }
            set { courseName = value; }
        }
        public void DisplayCourseInfo()
        {
            Console.WriteLine("Course ID: " + courseID + ", Course Name: " + courseName);
        }
    }
}

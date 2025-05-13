using Student_Project;
using System;
using System.Collections.Generic;
using System.IO;

class Program
{
    static Student[] students;
    static int studentCount = 0;
    static int maxStudents;
    const string fileName = "students.txt";

    static void Main(string[] args)
    {
        Console.Write("\t WELCOME IN FCI LUXOR \nEnter the maximum number of students: ");
        maxStudents = int.Parse(Console.ReadLine());
        students = new Student[maxStudents];
        LoadDataFromFile();
        int choice;
        do
        {
            try
            {
                Console.WriteLine("------------------------------\n     Student Management Menu");
                Console.WriteLine("1. Add Student");
                Console.WriteLine("2. Display All Students");
                Console.WriteLine("3. Delete Student by ID");
                Console.WriteLine("4. Search Student by ID");
                Console.WriteLine("5. Sort Students by ID");
                Console.WriteLine("6. Exit and Save");
                Console.Write("Enter your choice: ");
                choice = int.Parse(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;
                    case 2:
                        DisplayAllStudents();
                        break;
                    case 3:
                        DeleteStudentByID();
                        break;
                    case 4:
                        SearchStudentByID();
                        break;
                    case 5:
                        SortStudentsByID();
                        break;
                    case 6:
                        SaveDataToFile();
                        Console.WriteLine("Data saved. Goodbye!");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please choose 1-6.");
                        break;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Invalid input! Please enter a valid number.");
                choice = 0;
            }
        } while (choice != 6);
    }
    static void AddStudent()
    {
        if (studentCount >= maxStudents)
        {
            Console.WriteLine("Cannot add more students. Maximum limit reached.");
            return;
        }
        try
        {
            Student student = new Student();
            Console.Write("Enter Student ID: ");
            student.StudentID = int.Parse(Console.ReadLine());

            Console.Write("Enter Student Name: ");
            student.StudentName = Console.ReadLine();

            Console.Write("Enter number of courses: ");
            int courseCount = int.Parse(Console.ReadLine());

            Course[] courses = new Course[courseCount];
            for (int i = 0; i < courseCount; i++)
            {
                courses[i] = new Course();
                Console.Write($"Enter Course {i + 1} ID: ");
                courses[i].CourseID = int.Parse(Console.ReadLine());

                Console.Write($"Enter Course {i + 1} Name: ");
                courses[i].CourseName = Console.ReadLine();
            }
            student.Courses = courses;
            students[studentCount++] = student;
            Console.WriteLine("Student added successfully.");
        }
        catch (FormatException)
        {
            Console.WriteLine("Invalid input! Please enter numeric values for IDs.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
        }
    }

    static void DisplayAllStudents()
    {
        Console.WriteLine("\n--- All Students ---");
        for (int i = 0; i < studentCount; i++)
        {
            Console.WriteLine($"Student ID: {students[i].StudentID}, Name: {students[i].StudentName}");
            Console.WriteLine("Courses:");
            foreach (var course in students[i].Courses)
            {
                Console.WriteLine($"- Course ID: {course.CourseID}, Course Name: {course.CourseName}");
            }
            Console.WriteLine();
        }
    }

    static void DeleteStudentByID()
    {
        Console.Write("Enter the Student ID to delete: ");
        int studentID = int.Parse(Console.ReadLine());
        int indexToDelete = -1;
        for (int i = 0; i < studentCount; i++)
        {
            if (students[i].StudentID == studentID)
            {
                indexToDelete = i;
                break;
            }
        }
        if (indexToDelete != -1)
        {
            for (int i = indexToDelete; i < studentCount - 1; i++)
            {
                students[i] = students[i + 1];
            }
            studentCount--;
            Console.WriteLine("Student deleted successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void SearchStudentByID()
    {
        Console.Write("Enter the Student ID to search: ");
        int studentID = int.Parse(Console.ReadLine());
        bool found = false;
        foreach (var student in students)
        {
            if (student != null && student.StudentID == studentID)
            {
                Console.WriteLine($"Student ID: {student.StudentID}, Name: {student.StudentName}");
                Console.WriteLine("Courses:");
                foreach (var course in student.Courses)
                {
                    Console.WriteLine($"- Course ID: {course.CourseID}, Course Name: {course.CourseName}");
                }
                found = true;
                break;
            }
        }
        if (!found)
        {
            Console.WriteLine("Student not found.");
        }
    }

    static void SortStudentsByID()
    {
        for (int i = 0; i < studentCount - 1; i++)
        {
            for (int j = 0; j < studentCount - i - 1; j++)
            {
                if (students[j].StudentID > students[j + 1].StudentID)
                {
                    var temp = students[j];
                    students[j] = students[j + 1];
                    students[j + 1] = temp;
                }
            }
        }
        Console.WriteLine("Students sorted by ID.");
    }

    static void SaveDataToFile()
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fileName))
            {
                for (int i = 0; i < studentCount; i++)
                {
                    writer.WriteLine(students[i].StudentID + "," + students[i].StudentName);
                    foreach (var course in students[i].Courses)
                    {
                        writer.Write(course.CourseID + "," + course.CourseName + ";");
                    }
                    writer.WriteLine();
                }
            }
            Console.WriteLine("Data saved to file successfully.");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving data: " + ex.Message);
        }
    }

    static void LoadDataFromFile()
    {
        try
        {
            if (File.Exists(fileName))
            {
                using (StreamReader reader = new StreamReader(fileName))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        string[] studentData = line.Split(',');
                        Student student = new Student();
                        student.StudentID = int.Parse(studentData[0]);
                        student.StudentName = studentData[1];
                        var courses = new System.Collections.Generic.List<Course>();
                        string courseLine = reader.ReadLine();
                        if (courseLine != null)
                        {
                            string[] courseItems = courseLine.Split(';');
                            foreach (var item in courseItems)
                            {
                                if (!string.IsNullOrWhiteSpace(item))
                                {
                                    string[] courseData = item.Split(',');
                                    Course course = new Course();
                                    course.CourseID = int.Parse(courseData[0]);
                                    course.CourseName = courseData[1];
                                    courses.Add(course);
                                }
                            }
                        }
                        student.Courses = courses.ToArray();
                        students[studentCount++] = student;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error loading data: " + ex.Message);
        }
    }
}

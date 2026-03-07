using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    public class Subject
    {
        const int MaxStudents = 100;
        public string Name { get; set; }
        public List<Student> Students { get; set; }

        public Subject() 
        {
            Students = new List<Student>(MaxStudents);
        }
        public Subject(string name)
        {
            Name = name;
            Students = new List<Student>(MaxStudents);
        }
        public Subject(string name, List<Student> students)
        {
            
            Name = name ?? throw new Exception("Name cannot be empty");
            Students = students ?? throw new Exception("Students cannot be null");
            
        }

        public void Enroll(Student student)
        {
            Students.Add(student);
            
        }

        public void NotifyStudents(object sender, ExamEventArgs e)
        {
            foreach (Student student in Students)
            {
                student.OnExamStarted(sender, e);
            }
            Console.Read();
        }
    }
}

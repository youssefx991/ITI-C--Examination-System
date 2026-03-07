using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    public class Student
    {
        public string Name { get; set; }
        public int Id { get; set; }

        public Student(string name, int id)
        {
            Name = name ?? throw new Exception("Name cannot be empty");
            Id = id > 0 ? id : throw new Exception("ID must be greater than zero");
            
        }

        public void OnExamStarted(object sender, ExamEventArgs e)
        {
            Console.WriteLine($"Notification to {Name}: The {e.Subject.Name} exam has started!");
        }



        public override string ToString()
        {
            return $"Student ID: {Id}\nName: {Name}";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    public class Answer : IComparable<Answer>
    {
        public int Id { get; set; }
        public string Text { get; set; }

        public Answer(int id, string text)
        {
            Id = id;
            Text = text ?? throw new Exception("Text cannot be empty");
        }
        public Answer(string text) : this(0, text) { }

        public Answer(List<Answer> answers)
        {
            if (answers == null || answers.Count == 0)
                throw new Exception("Answers array cannot be null or empty");

            Id = answers.Count;
            Text = "";
            foreach (Answer answer in answers)
                Text += answer.Text + " ";
            Text = Text.Trim();
        }





        public override string ToString()
        {
            return $"{Id} - {Text}\n";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            if (obj is Answer a)
            {
                return Text == a.Text;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Text);
        }
        public int CompareTo(Answer? other)
        {
            if (other == null) return 1;

            return Id.CompareTo(other.Id);
        }
    }
}

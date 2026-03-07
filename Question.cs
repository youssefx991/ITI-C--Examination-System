using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Day7
{
    public abstract class Question
    {
        public string Header  {  get; set; }
        public string Body { get; set; }
        public int Marks { get; set; }


        public  AnswerList Answers { get; set; }
        public  Answer CorrectAnswer { get; set; }

        public Question(string header, string body, int marks, AnswerList answers, Answer correctanswer)
        {
            Header = header ?? throw new Exception("Header cannot be empty");
            Body = body ?? throw new Exception("Body cannot be empty");
            Marks = marks > 0 ? marks : throw new Exception("Marks must be greater than zero");
            Answers = answers;
            CorrectAnswer = correctanswer;
        }

        public abstract void Display();
        public abstract bool CheckAnswer(Answer studentAnswer);



        public override string ToString()
        {
            return $"{Header} : {Marks} Marks\n{Body}\n{Answers.ToString()}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            
            if (obj is Question q)
            {
                return Header == q.Header && Body == q.Body;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Header, Body, Marks, Answers, CorrectAnswer);
        }

    }
}

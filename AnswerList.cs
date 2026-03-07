using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Text;

namespace Day7
{
    public class AnswerList 
    {
        const int DefaultSize = 10;
        public List<Answer> Answers { get; set; }


        public AnswerList()
        {
            Answers = new List<Answer>(DefaultSize);
        }
        public AnswerList(List<Answer> answers)
        {
            Answers = answers;
        }

        public AnswerList(int size)
        {
            Answers = new List<Answer>(size);
        }

        public void Add(Answer answer)
        {
            Answers.Add(answer);
        }

        public Answer GetById(int id)
        {
            foreach (Answer answer in Answers)
            {
                if (answer.Id == id)
                    return answer;
            }

            throw new Exception("No answer with the specified ID was found.");
        }

        public Answer this[int index]
        {
            get
            {
                if (index >= 0 && index < Answers.Count)
                    return Answers[index];
                else
                    throw new Exception("Index is out of range.");
            }
            set
            {
                if (index >= 0 && index < Answers.Count)
                    Answers[index] = value;
                else
                    throw new Exception("Index is out of range.");
            }
        }

        public override string ToString()
        {
            string s="";
            foreach (Answer answer in Answers)
            {
                s+=answer.ToString();
            }
            return s;
        }

        

    }
}

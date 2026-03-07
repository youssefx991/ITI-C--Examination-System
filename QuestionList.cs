using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    public class QuestionList : List<Question>
    {
        const string DefaultFileName = "questions.txt";
        public string FileName { get; set; }
        public QuestionList() 
        { 
            FileName = DefaultFileName; 
            if (File.Exists(FileName))
            {
                File.Delete(FileName); 
            }
        }
        public QuestionList(string fileName)
        {
            FileName = fileName;
            if (File.Exists(FileName))
            {
                File.Delete(FileName);
            }
        }
        public new void Add(Question question)
        {
            if (question == null)
                throw new Exception("Question cannot be null.");

            base.Add(question);

            using (StreamWriter writer = new System.IO.StreamWriter(FileName, append: true))
            {
                writer.WriteLine(question.ToString());
                writer.WriteLine("-----------------------");
            }


        }

        public new Question[] ToArray()
        {
            return base.ToArray();
        }
    }

}

using System;
using System.Collections.Generic;
using System.Text;

namespace Day7
{
    public abstract class Exam : IComparable<Exam>, ICloneable
    {
        const string DefaultFileName = "Results.txt";
        public event ExamStartedHandler ExamStarted;
        public int Time { get; set; }
        public int NumberOfQuestions { get; set; }
        public List<Question> Questions { get; set; }
        public Dictionary<Question, Answer> QuestionAnswerDictionary { get; set; }

         Subject Subject { get; set; }
        public ExamMode Mode { get; set; }

        public int TotalStudentMarks { get; set; }
        public int TotalExamMarks { get; set; }

        public string ResultsFile;

        public Exam(int time, int numberOfQuestions, List<Question> questions, Subject subject, ExamMode mode)
        {
            Time = time > 0 ? time : throw new Exception("Time must be greater than zero");
            NumberOfQuestions = numberOfQuestions > 0 ? numberOfQuestions : throw new Exception("Number of questions must be greater than zero");
            Questions = questions ?? throw new Exception("Questions cannot be null");
            Subject = subject;
            Mode = mode;
            QuestionAnswerDictionary = new Dictionary<Question, Answer>();

            foreach (Question question in questions)
            {
                TotalExamMarks += question.Marks;
            }
            ResultsFile = DefaultFileName;
            if (File.Exists(ResultsFile))
            {
                File.Delete(ResultsFile);
            }
        }

        public abstract void ShowExam();

        public virtual void Start()
        {
            Mode = ExamMode.Starting;
            ExamEventArgs e = new ExamEventArgs() { Exam = this, Subject = Subject};
            ExamStarted += Subject.NotifyStudents;

            ExamStarted.Invoke(this, e);

            Console.WriteLine($"Exam started. You have {Time} minutes to complete the exam.");
            for (int i = 0; i < Questions.Count; i++)
            {
                Questions[i].Display();
                if (Questions[i].GetType() == typeof(ChooseOneQuestion))
                    Console.Write("Your answer: ");
                else if (Questions[i].GetType() == typeof(ChooseAllQuestion))
                    Console.Write("Your answers (space separated): ");
                else 
                    Console.Write("Your answer (true / false): ");
                string studentAnswer = Console.ReadLine();

                while (string.IsNullOrEmpty(studentAnswer))
                {
                    Console.Write("Answer cannot be empty. Please enter your answer: ");
                    studentAnswer = Console.ReadLine();
                }

                if (Questions[i].CheckAnswer(new Answer(i, studentAnswer))) // correct answer
                    TotalStudentMarks += Questions[i].Marks;

                QuestionAnswerDictionary[Questions[i]] = new Answer(i, studentAnswer);

                Console.Clear();
            }


        }

        public virtual void Finish()
        {
            Console.WriteLine("Exam finished. Submitting your answers...");
            Mode = ExamMode.Finished;

            using (var writer = new StreamWriter(ResultsFile, true))
            {
                writer.WriteLine($"Exam Result = {TotalStudentMarks} / {TotalExamMarks}");
            }
        }

        public void CorrectExam()
        {
            Console.WriteLine("Correcting Exam...");
            int totalMarks = 0;
            foreach (KeyValuePair<Question, Answer> kvp in QuestionAnswerDictionary)
            {
                if (kvp.Key.CheckAnswer(kvp.Value))
                {
                    totalMarks += kvp.Key.Marks;
                }

            } 
        }

        public override string ToString()
        {
            return $"Exam\nTime:{Time}\n#Questions:{NumberOfQuestions}\nMode:{Mode}";
        }

        public override bool Equals(object? obj)
        {
            if (obj == null)
                return false;
            if (obj is Exam e)
            {
                return Time == e.Time && NumberOfQuestions == e.NumberOfQuestions && Mode == e.Mode;
            }
            return false;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Time, NumberOfQuestions, Mode);
        }

        public int CompareTo(Exam? other)
        {
            if (other == null) return 1;
            int c1 = Time.CompareTo(other.Time);
            int c2 = NumberOfQuestions.CompareTo(other.NumberOfQuestions);
            if (c1 != 0)
                return c1;
            else 
                return c2;

        }

        public object Clone()
        {
            return MemberwiseClone(); // shallow copy of current object 
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Learningdiary_RL
{
    class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletionDate { get; set; }


        //saver user answers to file topics.txt
        public void FileSaving()
        {
            string path = @"C:\Users\Roosa\source\repos\Learningdiary_RL\topics.txt";
   if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    sw.WriteLine(Id.ToString() + " " +
                        Title + " " +
                        Description + " " +
                        EstimatedTimeToMaster.ToString() + " " +
                        TimeSpent.ToString() + " " +
                        Source + " " +
                        StartLearningDate.ToString() + " " +
                        CompletionDate.ToString() + " " +
                        InProgress.ToString());
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(Id.ToString() + " " +
                        Title + " " +
                        Description + " " +
                        EstimatedTimeToMaster.ToString() + " " +
                        TimeSpent.ToString() + " " +
                        Source + " " +
                        StartLearningDate.ToString() + " " +
                        CompletionDate.ToString() + " " +
                        InProgress.ToString());
            }

            using (StreamReader sr = File.OpenText(path))
            {
                string s = "";
                while ((s = sr.ReadLine()) != null)
                {
                    Console.WriteLine(s);
                }
            }
        }
    }

}
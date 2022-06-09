using System;
using System.Collections.Generic;
using System.IO;

namespace Learningdiary_RL
{
    class Program
    {
        static void Main(string[] args)
        {
            Topic topic = new Topic();

            //Asking if user wants a list of topics 
            Console.WriteLine("Do you want a list of all topics? Yes / No ");
            string answer = Console.ReadLine();
            if (answer == "Yes")
            {
                Console.WriteLine(@"
1. Some kind of id
2. Title
3. Description
4. Estimated time to master
5. Time used 
6. Sourses 
7. Start time
8. Completion date
9. Is learning still in progress" + "\n");
            }
            else
                Console.WriteLine();

            //Asking user to enter values to topics
            Console.WriteLine("Write id: ");
            topic.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Title: ");
            topic.Title = Console.ReadLine();

            Console.WriteLine("Description: ");
            topic.Description = Console.ReadLine();

            Console.WriteLine("Estimated time to master: ");
            topic.EstimatedTimeToMaster = double.Parse(Console.ReadLine());

            Console.WriteLine("Time used: ");
            topic.TimeSpent = double.Parse(Console.ReadLine());

            Console.WriteLine("Source: ");
            topic.Source = Console.ReadLine();

            Console.WriteLine("Start time dd/mm/yyyy: ");
            topic.StartLearningDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Is learning still in progress? Yes / No ");
            string progress = Console.ReadLine(topic.InProgress.ToString());
            //topic.InProgress = bool.Parse(Console.ReadLine());
            //string progress = Console.ReadLine();
            //topic.InProgress;
            if (progress == "No")
            {
                topic.InProgress = false;
                Console.WriteLine("Add completion date dd/mm/yyyy: ");
                topic.CompletionDate = DateTime.Parse(Console.ReadLine());
            }
            else
            {
                topic.InProgress = true;
            }

            //saving users answers to file topics.txt
            string path = @"C:\Users\Roosa\source\repos\Learningdiary_RL\topics.txt";

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {

                    sw.WriteLine(topic.Id.ToString() + " " +
                        topic.Title + " " +
                        topic.Description + " " +
                        topic.EstimatedTimeToMaster.ToString() + " " +
                        topic.TimeSpent.ToString() + " " +
                        topic.Source + " " +
                        topic.StartLearningDate.ToString() + " " +
                        topic.CompletionDate.ToString() + " " +
                        topic.InProgress.ToString());
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(topic.Id.ToString() + " " +
                        topic.Title + " " +
                        topic.Description + " " +
                        topic.EstimatedTimeToMaster.ToString() + " " +
                        topic.TimeSpent.ToString() + " " +
                        topic.Source + " " +
                        topic.StartLearningDate.ToString() + " " +
                        topic.CompletionDate.ToString() + " " +
                        topic.InProgress.ToString());
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
    class Topic
    {
        //All topics
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public double EstimatedTimeToMaster { get; set; }
        public double TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime StartLearningDate { get; set; }
        public bool InProgress { get; set; }
        public DateTime CompletionDate { get; set; }

    }
}



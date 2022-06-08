using System;
using System.Collections.Generic;
using System.IO;

namespace Learningdiary_RL
{
    class Program
    {
        static void Main(string[] args)
        {
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
8. Is learning still in progress
9. Completion date");
            }
            else
                Console.WriteLine();
            
            //Empty line
            Console.WriteLine(); 

            //Asking user to enter values to topics
            Console.WriteLine("Write id: ");
            int id = int.Parse(Console.ReadLine());

            Console.WriteLine("Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Description: ");
            string description = Console.ReadLine();

            Console.WriteLine("Estimated time to master: ");
            double estimatedTimeToMaster = double.Parse(Console.ReadLine());

            Console.WriteLine("Time used: ");
            double timeSpent = double.Parse(Console.ReadLine());

            Console.WriteLine("Source: ");
            string source = Console.ReadLine();

            Console.WriteLine("Start time dd/mm/yyyy: ");
            DateTime startLearningDate = DateTime.Parse(Console.ReadLine()); 

            Console.WriteLine("Is learning still in progress? Yes / No ");
            bool inProgress = bool.Parse(Console.ReadLine());
            /*
            string progress = Console.ReadLine();
            bool inProgress = Convert.ToBoolean(progress);
            if (progress == "Yes")
            {
                
                inProgress = true;
            }
                else
            {
                inProgress = false;
            }
                */

            Console.WriteLine("Completion date dd/mm/yyyy: ");
            DateTime completionDate = DateTime.Parse(Console.ReadLine());


            //id int to string
            string idString = id.ToString();

            //saving users answers to file topics.txt
            string path = @"C:\Users\Roosa\source\repos\Learningdiary_RL\topics.txt";
            
            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {
                    // ei antanut laittaa id:tä int muotoisena, miksi kuitenkin hyväksyy doublet ja DateTimet?
                    // ei myöskään lisää listaan kun tuon idString ja myöskin lopussa kirjoittaa vain sen?
                    sw.WriteLine(idString,  
                        title, 
                        description, 
                        estimatedTimeToMaster, 
                        timeSpent, source, 
                        startLearningDate, 
                        inProgress, 
                        completionDate);
                }
            }

            using (StreamWriter sw = File.AppendText(path))
            {
                sw.WriteLine(idString,
                        title,
                        description,
                        estimatedTimeToMaster,
                        timeSpent, source,
                        startLearningDate,
                        inProgress,
                        completionDate);          
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

            public Topic(int id,
                string title,
                string description,
                double estimatedTimeToMaster,
                double timeSpent,
                string source,
                DateTime startLearningDate,
                bool inProgress,
                DateTime completionDate)
            {
                Id = id;
                Title = title;
                Description = description;
                EstimatedTimeToMaster = estimatedTimeToMaster;
                TimeSpent = timeSpent;
                Source = source;
                StartLearningDate = startLearningDate;
                InProgress = inProgress;
                CompletionDate = completionDate;
            }

        }
    }
}

using System;

namespace Learningdiary_RL
{
    class Program
    {
        static void Main(string[] args)
        { 
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

            Console.WriteLine("Start time: ");
            DateTime startLearningDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Is learning still in progress? ");
            bool inProgress = bool.Parse(Console.ReadLine());
            
            Console.WriteLine("Completion date: ");
            DateTime completionDate = DateTime.Parse(Console.ReadLine());

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

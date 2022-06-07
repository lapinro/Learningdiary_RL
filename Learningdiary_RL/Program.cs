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
        int Id;
        string Title;
        string Description;
        double EstimatedTimeToMaster;
        double TimeSpent;
        string Source;
        DateTime StartLearningDate;
        bool InProgress;
        DateTime CompletionDate;

    }
}

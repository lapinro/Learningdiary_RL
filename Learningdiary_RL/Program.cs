using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;

namespace Learningdiary_RL
{
    class Program
    {
        static void Main(string[] args)
        {
            Topic topic = new Topic();

            //asking if user wants a list of topics 
            Console.WriteLine("Do you want a list of all topics? Yes / No ");
            string answer = Console.ReadLine();
            if (answer == "Yes")
            {
                Console.WriteLine(@"
- ID to your diary
- Title
- Description
- Estimated time to master
- Soures 
- Start time
- Is learning still in progress
- Completion date
- Time used" + "\n");
            }
            else
                Console.WriteLine();

            //asking user to enter values to topics

            Console.WriteLine("Write id: ");
            topic.Id = int.Parse(Console.ReadLine());

            Console.WriteLine("Title: ");
            topic.Title = Console.ReadLine();

            Console.WriteLine("Description: ");
            topic.Description = Console.ReadLine();

            Console.WriteLine("Estimated time to master: ");
            topic.EstimatedTimeToMaster = double.Parse(Console.ReadLine());

            Console.WriteLine("Source: ");
            topic.Source = Console.ReadLine();

            Console.WriteLine("Start time dd/mm/yyyy: ");
            topic.StartLearningDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Is learning still in progress? Yes / No ");
            string progress = Console.ReadLine();
            if (progress == "No")
            {
                topic.InProgress = false;
                Console.WriteLine("Add completion date dd/mm/yyyy: ");
                topic.CompletionDate = DateTime.Parse(Console.ReadLine());
                DateTime startedLearning = topic.StartLearningDate;
                DateTime endedLearning = topic.CompletionDate;
                TimeSpan learningTime = endedLearning - startedLearning;
                topic.TimeSpent = Convert.ToDouble(learningTime);
                Console.WriteLine("\nYour learning time was:" + learningTime.Days);

            }
            else
            {
                topic.InProgress = true;
            }

            //saving users answers to file topics.txt
            topic.FileSaving();

            //list of all answers
            List<Topic> list = new List<Topic>();
            list.Add(topic);

            Console.WriteLine("Do you want to find your topic by entering Id or Topic? TOPIC / ID");
            string IdorTopic = Console.ReadLine();

            //user selected ID
            if (IdorTopic == "ID")
            {
                Console.WriteLine("Write topic ID you want to find: ");
                int findID = int.Parse(Console.ReadLine());
                var find = list.Where(findID => findID.Id == topic.Id).FirstOrDefault(); 

                //user can update information 
                Console.WriteLine("Choose what you want to update: ");
                Console.WriteLine("I = ID");
                Console.WriteLine("T = Title");
                Console.WriteLine("D = Description");
                Console.WriteLine("E = Estimated time to master");
                Console.WriteLine("S = Source");
                Console.WriteLine("L = When you started learning");
                Console.WriteLine("P = Is learning still in progress");
                Console.WriteLine("X = Exit");
                string userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "I":
                        Console.WriteLine("Write new id or X to remove it: ");
                        string updateId = Console.ReadLine();
                       if (updateId == "X")
                        {
                            list.Remove(find);
                            Console.WriteLine("Id \"{0}\" is now removed", topic.Id);
                        }
                        else
                        {
                            topic.Id = Convert.ToInt32(updateId);
                            Console.WriteLine("new Id \"{0}\" is now added",topic.Id);
                        } 
                        break;
                    case "T":
                        Console.WriteLine("Write new Title or X to remove it: ");
                        string updateTitle = Console.ReadLine();
                        if (updateTitle == "X")
                        {
                            list.Remove(find);
                            Console.WriteLine("Title \"{0}\" is now removed", topic.Title);
                        }
                        else
                        { 
                            updateTitle = topic.Title;
                            Console.WriteLine("new Title \"{0}\" is now added", topic.Title);
                        }

                        break;
                    case "D":
                        Console.WriteLine("Write new Description or X to remove it:");
                        string updateDesc = Console.ReadLine();
                        if (updateDesc == "X")
                        {
                            list.Remove(find);
                            Console.WriteLine("Description \"{0}\" is now removed", topic.Description);
                        }
                        else
                        {
                            updateDesc = topic.Description;
                            Console.WriteLine("new Description \"{0}\" is now added", topic.Description);
                        }
                        break;
                    case "E":
                        Console.WriteLine("Write new Estimated time to master or X to remove it:");
                        string updateEstimated = Console.ReadLine();
                        if (updateEstimated == "X")
                        {
                            list.Remove(find);
                            Console.WriteLine("Estimated time to master \"{0}\" is now removed", topic.EstimatedTimeToMaster);
                        }
                        else
                        {
                            updateEstimated = topic.Description;
                            Console.WriteLine("new Estimated time to master \"{0}\" is now added", topic.EstimatedTimeToMaster);
                        }
                        break;
                    case "S":
                        Console.WriteLine("Write new Source or X to remove it:");
                        string updateSource = Console.ReadLine();
                        if (updateSource == "X")
                        {
                            list.Remove(find);
                            Console.WriteLine("Source \"{0}\" is now removed", topic.Source);
                        }
                        else
                        {
                            updateSource = topic.Source;
                            Console.WriteLine("new Source \"{0}\" is now added", topic.Source);
                        }
                        break;
                    case "L":
                        Console.WriteLine("Write new Start time (dd/mm/yyyy) or X to remove it:");
                        string updateStartTime = Console.ReadLine();
                        if (updateStartTime == "X")
                        {
                            list.Remove(find);
                            Console.WriteLine("Start time \"{0}\" is now removed", topic.StartLearningDate);
                        }
                        else
                        {
                            topic.StartLearningDate = DateTime.Parse(updateStartTime);
                            Console.WriteLine("new Start time \"{0}\" is now added", topic.StartLearningDate);
                        }
                        break;
                    case "P":
                        Console.WriteLine("Is learning still in progress? Yes / No (you can only change this)");
                        string progress2 = Console.ReadLine();
                        if (progress2 == "No")
                        {
                            topic.InProgress = false;
                            Console.WriteLine("Add completion date dd/mm/yyyy: ");
                            topic.CompletionDate = DateTime.Parse(Console.ReadLine());
                            DateTime startedLearning = topic.StartLearningDate;
                            DateTime endedLearning = topic.CompletionDate;
                            TimeSpan learningTime = endedLearning - startedLearning;
                            topic.TimeSpent = Convert.ToDouble(learningTime);
                            Console.WriteLine("\nYour learning time was:" + learningTime.Days);
                        }
                        else
                        {
                            topic.InProgress = true;
                        }
                        break;
                    case "EXIT":
                        break;
                }
            }
            else //user selected title
            {
                Console.WriteLine("Write topic name you want to find: ");
                string findTitle = Console.ReadLine();
                var findT = list.Where(findID => findID.Title == topic.Title).FirstOrDefault();

                //user can update information 
                Console.WriteLine("Choose what you want to update: ");
                Console.WriteLine("I = ID");
                Console.WriteLine("T = Title");
                Console.WriteLine("D = Description");
                Console.WriteLine("E = Estimated time to master");
                Console.WriteLine("S = Source");
                Console.WriteLine("L = When you started learning");
                Console.WriteLine("P = Is learning still in progress");
                Console.WriteLine("X = Exit");
                string userAnswer = Console.ReadLine();
                switch (userAnswer)
                {
                    case "I":
                        Console.WriteLine("Write new id or X to remove it: ");
                        string updateId = Console.ReadLine();
                        if (updateId == "X")
                        {
                            list.Remove(findT);
                            Console.WriteLine("Id \"{0}\" is now removed", topic.Id);
                        }
                        else
                        {
                            topic.Id = Convert.ToInt32(updateId);
                            Console.WriteLine("new Id \"{0}\" is now added", topic.Id);
                        }
                        break;
                    case "T":
                        Console.WriteLine("Write new Title or X to remove it: ");
                        string updateTitle = Console.ReadLine();
                        if (updateTitle == "X")
                        {
                            list.Remove(findT);
                            Console.WriteLine("Title \"{0}\" is now removed", topic.Title);
                        }
                        else
                        {
                            updateTitle = topic.Title;
                            Console.WriteLine("new Title \"{0}\" is now added", topic.Title);
                        }

                        break;
                    case "D":
                        Console.WriteLine("Write new Description or X to remove it:");
                        string updateDesc = Console.ReadLine();
                        if (updateDesc == "X")
                        {
                            list.Remove(findT);
                            Console.WriteLine("Description \"{0}\" is now removed", topic.Description);
                        }
                        else
                        {
                            updateDesc = topic.Description;
                            Console.WriteLine("new Description \"{0}\" is now added", topic.Description);
                        }
                        break;
                    case "E":
                        Console.WriteLine("Write new Estimated time to master or X to remove it:");
                        string updateEstimated = Console.ReadLine();
                        if (updateEstimated == "X")
                        {
                            list.Remove(findT);
                            Console.WriteLine("Estimated time to master \"{0}\" is now removed", topic.EstimatedTimeToMaster);
                        }
                        else
                        {
                            updateEstimated = topic.Description;
                            Console.WriteLine("new Estimated time to master \"{0}\" is now added", topic.EstimatedTimeToMaster);
                        }
                        break;
                    case "S":
                        Console.WriteLine("Write new Source or X to remove it:");
                        string updateSource = Console.ReadLine();
                        if (updateSource == "X")
                        {
                            list.Remove(findT);
                            Console.WriteLine("Source \"{0}\" is now removed", topic.Source);
                        }
                        else
                        {
                            updateSource = topic.Source;
                            Console.WriteLine("new Source \"{0}\" is now added", topic.Source);
                        }
                        break;
                    case "L":
                        Console.WriteLine("Write new Start time (dd/mm/yyyy) or X to remove it:");
                        string updateStartTime = Console.ReadLine();
                        if (updateStartTime == "X")
                        {
                            list.Remove(findT);
                            Console.WriteLine("Start time \"{0}\" is now removed", topic.StartLearningDate);
                        }
                        else
                        {
                            topic.StartLearningDate = DateTime.Parse(updateStartTime);
                            Console.WriteLine("new Start time \"{0}\" is now added", topic.StartLearningDate);
                        }
                        break;
                    case "P":
                        Console.WriteLine("Is learning still in progress? Yes / No (you can only change this)");
                        string progress2 = Console.ReadLine();
                        if (progress2 == "No")
                        {
                            topic.InProgress = false;
                            Console.WriteLine("Add completion date dd/mm/yyyy: ");
                            topic.CompletionDate = DateTime.Parse(Console.ReadLine());
                            DateTime startedLearning = topic.StartLearningDate;
                            DateTime endedLearning = topic.CompletionDate;
                            TimeSpan learningTime = endedLearning - startedLearning;
                            topic.TimeSpent = Convert.ToDouble(learningTime);
                            Console.WriteLine("\nYour learning time was:" + learningTime.Days);
                        }
                        else
                        {
                            topic.InProgress = true;
                        }
                        break;
                    case "EXIT":
                        break;
                }

            }

        }

    }
}



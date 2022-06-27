using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Learningdiary_RL.Models;
using ClassLibraryLearningDiary;

namespace Learningdiary_RL
{
    class Program
    {
        static void Main(string[] args)
        {

          //  Topic topic = new Topic();
            MethodLibrary useMethods = new MethodLibrary();

            bool menu = true; 
            while (menu)
            {
                menu = MainMenu();
            }
         
            static bool MainMenu()
            {
                
                Console.WriteLine("1 = create new topic");
                Console.WriteLine("2 = edit topic by title");
                Console.WriteLine("3 = print list of your topic");
                Console.WriteLine("4 = exit");
                switch (Console.ReadLine())
                {
                    case "1":
                        NewTopic();
                        return true;
                    case "2":
                        FindAndEdit();
                        return true;
                    case "3":
                        PrintTopic();
                        return true;
                    case "4":
                        return false;                       
                }
                return false;
            }

            //add new topic
            static void NewTopic()
            {
                Topic newtopic = new Topic();

                Console.WriteLine("Title: ");
                newtopic.Title = Console.ReadLine();

                Console.WriteLine("Description: ");
                newtopic.Descriptions = Console.ReadLine();

                Console.WriteLine("Estimated time to master (days): "); 
                newtopic.TimeToMaster = int.Parse(Console.ReadLine());

                Console.WriteLine("Source: ");
                newtopic.Source = Console.ReadLine();

                Console.WriteLine("Start time dd/mm/yyyy: ");
                newtopic.StartLearningDate = DateTime.Parse(Console.ReadLine());

                Console.WriteLine("Is learning still in progress? Yes / No ");
                string progress = Console.ReadLine();
                if (progress == "No")
                {
                    newtopic.InProgress = false;
                    Console.WriteLine("Add completion date dd/mm/yyyy: ");

                   // DateTime startedLearning = Convert.ToDateTime(newtopic.StartLearningDate);
                    //DateTime endedLearning = Convert.ToDateTime(newtopic.CompletionDate);
                    TimeSpan learningTime =  Convert.ToDateTime(newtopic.CompletionDate) - Convert.ToDateTime(newtopic.StartLearningDate);
                    // TimeSpan learningTime = endedLearning - startedLearning;
                    newtopic.TimeSpent = Convert.ToDouble(learningTime);
                    Console.WriteLine("\nYour learning time was:" + learningTime.Days); 
                     
                }
                else
                {
                    newtopic.InProgress = true;
                }
       
                List<Topic> newtopiclist = new List<Topic>();
                newtopiclist.Add(newtopic);

                SaveSQL(newtopiclist); //saves answers to SQL
             
            }

            //find topic by title and edit topics
            static void FindAndEdit()
            {

                    Console.WriteLine("Write title you want to find: ");
                    string find = Console.ReadLine();

                    Console.WriteLine("Choose what you want to update: ");
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
                        case "T":
                            Console.WriteLine("Write new Title (Notice that you cannot remove Title): ");
                            string updateTitle = Console.ReadLine();
                            NewTitle(find, updateTitle); 
                            
                            break;

                        case "D":
                            Console.WriteLine("Write new Description or X to remove it:");
                            string updateDesc = Console.ReadLine();
                            if (updateDesc == "X")
                            {
                             //   list.Remove(find);
                              //  Console.WriteLine("Description \"{0}\" is now removed", topic.Description);
                            }
                            else
                            {
                            NewDescription(find, updateDesc);
                            }
                            break;
                        /*
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
                            break; */
                    }

                   
            }

            static void PrintTopic()
            {
                //etsii jonkin tiedon perusteella topicin ja printtaa sen
            }
 
            //saves answers to SQL
            static void SaveSQL(List<Topic> topics)
            {
                foreach (var item in topics)
                {

                    using (LearningDiaryContext learningD = new LearningDiaryContext())
                    {
                        //Topic table = new Topic()
                        var table = learningD.Topics.Select(item => item);
                        Topic topic1 = new Topic()

                        {
                            // Id = topic.Id,
                            Title = item.Title,
                            Descriptions = item.Descriptions,
                            TimeToMaster = item.TimeToMaster,
                            TimeSpent = item.TimeSpent,
                            Source = item.Source,
                            StartLearningDate = item.StartLearningDate,
                            CompletionDate = item.CompletionDate,
                            InProgress = item.InProgress
                        };
                        
                        learningD.Topics.Add(topic1);
                        learningD.SaveChanges();

                    }
                }

            }
            
            //edit title
            static string NewTitle(string findT, string newTitle)
            {

                using (LearningDiaryContext learningD = new LearningDiaryContext())
                {
                    var findTitle = learningD.Topics.Where(t => t.Title == findT).FirstOrDefault();
                    findTitle.Title = newTitle;
                    learningD.SaveChanges();
                    return findTitle.Title;
                    Console.WriteLine("new Title \"{0}\" is now added", findTitle.Title);
                }
                
            }

            //edit description
            static string NewDescription(string findT, string newD)
            {

                using (LearningDiaryContext learningD = new LearningDiaryContext())
                {
                    var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                    findByTitle.Descriptions = newD;
                    Console.WriteLine("new Description \"{0}\" is now added", findByTitle.Descriptions);
                    learningD.SaveChanges();
                    return findByTitle.Descriptions;

                }

            }
            
            static string RemoveDescription(string findT)
            {
                using (LearningDiaryContext learningD = new LearningDiaryContext())
                {
                    var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                    learningD.Remove(findByTitle.Descriptions);
                    Console.WriteLine("Description \"{0}\" is now removed", findByTitle.Descriptions);
                    learningD.SaveChanges();
                    return findByTitle.Descriptions;
                    
                }
            }
           
        }
            
    }
    
}



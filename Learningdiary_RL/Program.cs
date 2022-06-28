using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using Learningdiary_RL.Models;
using ClassLibraryLearningDiary;
using System.Threading.Tasks;
using System.Threading;

namespace Learningdiary_RL
{
    class Program
    {
        static void Main(string[] args)
        {       
            
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
                Console.WriteLine("4 = delete topic");
                Console.WriteLine("5 = exit");
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
                        RemoveTopic();
                        return true;
                    case "5":
                        return false;
                }
                return false;
            }

        }
        //add new topic
        public static void NewTopic()
        {
            Topic newtopic = new Topic();

            Console.WriteLine("Title: ");
            newtopic.Title = Console.ReadLine();

            Console.WriteLine("Description: ");
            newtopic.Descriptions = Console.ReadLine();

            Console.WriteLine("Estimated time to master (days): ");
            try
            {
                newtopic.TimeToMaster = int.Parse(Console.ReadLine());
            }

            catch (Exception)
            {
                Console.WriteLine("Write only numbers!");
                newtopic.TimeToMaster = int.Parse(Console.ReadLine());
            }

            Console.WriteLine("Source: ");
            newtopic.Source = Console.ReadLine();

            Console.WriteLine("Start time dd/mm/yyyy: ");
            try
            {
                newtopic.StartLearningDate = DateTime.Parse(Console.ReadLine());
            }
            catch (Exception)
            {
                Console.WriteLine("Check that you entered the date in the correct format dd/mm/yyyy");
                newtopic.StartLearningDate = DateTime.Parse(Console.ReadLine());
            }

            Console.WriteLine("Is learning still in progress? Yes / No ");
            string progress = Console.ReadLine();
            if (progress == "No")
            {
                newtopic.InProgress = false;
                Console.WriteLine("Add completion date dd/mm/yyyy: ");
                try
                {
                    newtopic.CompletionDate = DateTime.Parse(Console.ReadLine());
                }
                catch
                {
                    Console.WriteLine("Check that you entered the date in the correct format dd/mm/yyyy");
                    newtopic.CompletionDate = DateTime.Parse(Console.ReadLine());
                }
                TimeSpan learningTime = Convert.ToDateTime(newtopic.CompletionDate) - Convert.ToDateTime(newtopic.StartLearningDate);
                newtopic.TimeSpent = Convert.ToDouble(learningTime); //EI TOIMI AJON AIKANA
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
        public static void FindAndEdit()
        {
            Topic topic2 = new Topic();

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
                    Console.WriteLine("Write new Title: ");
                    topic2.Title = Console.ReadLine();
                    NewTitle(find, topic2.Title);
                    break;

                case "D":
                    Console.WriteLine("Write new Description:");
                    string updateDesc = Console.ReadLine();
                    NewDescription(find, updateDesc);
                    break;
                    
                  case "E":
                    Console.WriteLine("Write new Estimated time to master: ");
                    try
                    {
                        int updateEstimated = Convert.ToInt32(Console.ReadLine());
                        NewTimeToMaster(find, updateEstimated);
                    }
                    catch(Exception)
                    {
                        Console.WriteLine("Write only numbers!");
                        int updateEstimated = Convert.ToInt32(Console.ReadLine());
                        NewTimeToMaster(find, updateEstimated);
                    }
                    break;
                    
                    case "S":
                        Console.WriteLine("Write new Source:");
                        string updateSource = Console.ReadLine();
                        NewSource(find, updateSource);

                        break;
                    
                    case "L":
                        Console.WriteLine("Write new Start time (dd/mm/yyyy):");
                    try
                    {
                        string updateStartTime = Console.ReadLine();
                        NewStartTime(find, Convert.ToDateTime(updateStartTime));
                       
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Check that you entered the date in the correct format dd/mm/yyyy");
                        string updateStartTime = Console.ReadLine();
                        NewStartTime(find, Convert.ToDateTime(updateStartTime));
                    }

                     break;
                    case "P":
                    NewProgress(find);
                    break;
                    case "EXIT":
                        break; 
            }


        }
        
        //print topic user selects
        public static void PrintTopic()
        {


            Console.WriteLine("Write topic ID you want to find: ");
            int findID = Convert.ToInt32(Console.ReadLine());

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var printTopic = learningD.Topics.Where(i => i.Id == findID).FirstOrDefault();
                Console.WriteLine("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}",
                    printTopic.Id,
                    printTopic.Title,
                    printTopic.Descriptions,
                    printTopic.TimeToMaster,
                    printTopic.Source);
               //     StudyingSchedule(Convert.ToDateTime(printTopic.StartLearningDate), Convert.ToInt32(printTopic.TimeToMaster)));// EI TOIMI
            }
            

        }
        
        //remove user selected topic 
        public static void RemoveTopic()
        {
            Console.WriteLine("Write topic ID you want to find: ");
            int findID = Convert.ToInt32(Console.ReadLine());
            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var printTopic = learningD.Topics.Where(i => i.Id == findID).FirstOrDefault();
                learningD.Remove(printTopic);
            }

            Console.WriteLine("Topic is now removed");
            Console.ReadLine();
        }

        //saves answers to SQL
        public static void SaveSQL(List<Topic> topics)
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
        public static void NewTitle(string findT, string newTitle)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findTitle = learningD.Topics.Where(t => t.Title == findT).FirstOrDefault();
                findTitle.Title = newTitle;
                learningD.SaveChanges();
               // return findTitle.Title;
                Console.WriteLine("new Title \"{0}\" is now added", findTitle.Title);
            }

        }

        //edit description
        public static void NewDescription(string findT, string newD)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                findByTitle.Descriptions = newD;

                learningD.SaveChanges();
              //  return findByTitle.Descriptions;

            }
            Console.WriteLine("new Description \"{0}\" is now added", newD);

        }
        
        //edit mastering time
        public static void NewTimeToMaster(string findT, int newT)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                findByTitle.TimeToMaster = newT;

                learningD.SaveChanges();
                // return findByTitle.TimeToMaster;

            }
            Console.WriteLine("new Estimated time to master \"{0}\" is now added", newT);

        }
        
        //edit source
        public static void NewSource(string findT, string newS)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                findByTitle.Source = newS;

                learningD.SaveChanges();
                //  return findByTitle.Source;

            }
            Console.WriteLine("new Source \"{0}\" is now added", newS);

        }
        
        //edit start time
        public static void NewStartTime(string findT, DateTime newDate)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                findByTitle.StartLearningDate = newDate;

                learningD.SaveChanges();
                //  return findByTitle.StartLearningDate;

            }
            Console.WriteLine("new Start Time \"{0}\" is now added", newDate);

        }
       
        //edit progress
        public static void NewProgress(string findT)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByT = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();

                Console.WriteLine("Is learning still in progress? Yes / No ");
                string progress = Console.ReadLine();
                if (progress == "No")
                {
                    bool progressBoolean = false;
                    findByT.InProgress = progressBoolean;

                    Console.WriteLine("Add completion date dd/mm/yyyy: ");
                    try
                    {
                        findByT.CompletionDate = DateTime.Parse(Console.ReadLine());
                    }
                    catch
                    {
                        Console.WriteLine("Check that you entered the date in the correct format dd/mm/yyyy");
                        findByT.CompletionDate = DateTime.Parse(Console.ReadLine());
                    }
                    TimeSpan learningTime = Convert.ToDateTime(findByT.CompletionDate) - Convert.ToDateTime(findByT.StartLearningDate);
                    findByT.TimeSpent = Convert.ToDouble(learningTime); //EI TOIMI AJON AIKANA
                    Console.WriteLine("\nYour learning time was:" + learningTime.Days);

                }
                else
                {
                    findByT.InProgress = true;
                }

                learningD.SaveChanges();
                

            }

        }
        
        //tells user (in PrintTopic) if studying is still in progress
        public static void StudyingSchedule(DateTime date, double days )
        {
            MethodLibrary methods = new MethodLibrary();
            methods.CheckIfLate(date, days);
            if (true)
            {
               Console.WriteLine("This topic is behind schedule");
            }
            else
            {
                Console.WriteLine("This topic is on schedule :)");
            }
            
                
        }
    }

}



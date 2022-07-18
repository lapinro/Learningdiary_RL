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
            Console.WriteLine("***** LEARNING DIARY ***** \n");

            bool menu = true;
            while (menu)
            {
                menu = MainMenu();
            }

            static bool MainMenu()
            {

                Console.WriteLine("1 = create new topic");
                Console.WriteLine("2 = edit topic");
                Console.WriteLine("3 = print a list of your topic");
                Console.WriteLine("4 = delete topic");
                Console.WriteLine("5 = exit program");
                switch (Console.ReadLine())
                {
                    case "1":
                        NewTopic();
                        return true;
                    case "2":
                        EditByTitle();
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
                Console.WriteLine("\nYour learning time was: " + learningTime.Days + "days\n");
                newtopic.TimeSpent = Convert.ToDouble(learningTime.TotalDays);


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
        public static async Task EditByTitle()
        {
            Topic topic2 = new Topic();

            Console.WriteLine("Write title you want to find: ");
            string find = Console.ReadLine();
            await FindTitle(find); // checks if topic exists

            Console.WriteLine("Choose what you want to update: ");
            Console.WriteLine("T = Title");
            Console.WriteLine("D = Description");
            Console.WriteLine("E = Estimated time to master");
            Console.WriteLine("S = Source");
            Console.WriteLine("L = When you started learning");
            Console.WriteLine("P = Is learning still in progress");
            Console.WriteLine("X = Exit");
            string userAnswer = Console.ReadLine();
            switch (userAnswer.ToUpper())
            {
                case "T":
                    Console.WriteLine("Write new Title: ");
                    topic2.Title = Console.ReadLine();
                   await NewTitle(find, topic2.Title);
                    break;

                case "D":
                    Console.WriteLine("Write new Description:");
                    topic2.Descriptions = Console.ReadLine();
                    await NewDescription(find, topic2.Descriptions);
                    break;

                case "E":
                    Console.WriteLine("Write new Estimated time to master: ");
                    try
                    {
                        topic2.TimeToMaster = Convert.ToInt32(Console.ReadLine());
                        await NewTimeToMaster(find, Convert.ToInt32(topic2.TimeToMaster));
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Write only numbers!");
                        topic2.TimeToMaster = Convert.ToInt32(Console.ReadLine());
                        await NewTimeToMaster (find, Convert.ToInt32(topic2.TimeToMaster));
                    }
                    break;

                case "S":
                    Console.WriteLine("Write new Source:");
                    topic2.Source = Console.ReadLine();
                    await NewSource (find, topic2.Source);

                    break;

                case "L":
                    Console.WriteLine("Write new Start time (dd/mm/yyyy):");
                    try
                    {
                        topic2.StartLearningDate = Convert.ToDateTime(Console.ReadLine());
                        await NewStartTime (find, Convert.ToDateTime(topic2.StartLearningDate));

                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Check that you entered the date in the correct format dd/mm/yyyy");
                        topic2.StartLearningDate = Convert.ToDateTime(Console.ReadLine());
                        await NewStartTime (find, Convert.ToDateTime(topic2.StartLearningDate));
                    }

                    break;
                case "P":
                   /* topic2.InProgress = NewProgress(find);
                    if (topic2.InProgress == false)
                    {
                        topic2.CompletionDate = NewCompletionDate(find);
                    }  //SELVITÄ MIKSI TÄMÄ KAATUU  */
                    break;
                case "EXIT":
                    break;
            }

        }

        //print topic user selects
        public static async Task PrintTopic()
        {
            Console.WriteLine("Do you want to find topic by ID or TOPIC?");
            string choose = Console.ReadLine();
            switch (choose.ToUpper())
            {
                case "ID":
                    Console.WriteLine("Write topic ID you want to find: ");
                    int findID = Convert.ToInt32(Console.ReadLine());
                    await FindId(findID);
                    using (LearningDiaryContext learningD = new LearningDiaryContext())
                    {
                        var printTopic = learningD.Topics.Where(i => i.Id == findID).FirstOrDefault();
                        Console.WriteLine("\nId: {0}\nTitle: {1}\nDescription: {2}\nTimeToMaster: {3}\nSource: {4}\n{5}",
                            printTopic.Id,
                            printTopic.Title,
                            printTopic.Descriptions,
                            printTopic.TimeToMaster,
                            printTopic.Source,
                            StudyingSchedule(Convert.ToDateTime(printTopic.StartLearningDate), Convert.ToDouble(printTopic.TimeToMaster)));
                        Console.ReadLine();
                    }
                    break;
                case "TOPIC":
                    Console.WriteLine("Write Title you want to find: ");
                    string findTitle = Console.ReadLine();
                    await FindTitle(findTitle);
                    using (LearningDiaryContext learningD = new LearningDiaryContext())
                    {
                        var printTopic = learningD.Topics.Where(i => i.Title == findTitle).FirstOrDefault();
                        Console.WriteLine("\nId: {0}\nTitle: {1}\nDescription: {2}\nTimeToMaster: {3}\nSource: {4}\n{5}",
                            printTopic.Id,
                            printTopic.Title,
                            printTopic.Descriptions,
                            printTopic.TimeToMaster,
                            printTopic.Source,
                            StudyingSchedule(Convert.ToDateTime(printTopic.StartLearningDate), Convert.ToDouble(printTopic.TimeToMaster)));
                        Console.ReadLine();
                    }
                    break;
            }
        }

        //remove user selected topic 
        public static void RemoveTopic()
        {
            Console.WriteLine("Write topic ID you want to find: ");

            try
            {
                int findID = Convert.ToInt32(Console.ReadLine());
                using (LearningDiaryContext learningD = new LearningDiaryContext())
                {
                    var printTopic = learningD.Topics.Where(i => i.Id == findID).FirstOrDefault();
                    learningD.Remove(printTopic);
                    learningD.SaveChanges();
                    Console.WriteLine("Topic is now removed");
                }
            }
 
            catch (Exception e)
            {
                Console.WriteLine("Not found or check that you used only numbers ");
            }

            Console.ReadLine();
        }

        //saves answers to SQL
        public static void SaveSQL(List<Topic> topics)
        {
            foreach (var item in topics)
            {

                using (LearningDiaryContext learningD = new LearningDiaryContext())
                {

                    var table = learningD.Topics.Select(item => item);
                    Topic topic1 = new Topic()

                    {

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
        
        //checks if topic exists (search by title)
        public static async Task FindTitle(string find)
        {
            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findTitle = learningD.Topics.Where(t => t.Title == find).FirstOrDefault();
                if (findTitle == null)
                {
                    Console.WriteLine("Topic not found\n");
                }
            }
        }
        
        //checks if topic exists (search by ID)
        public static async Task FindId(int find)
        {
            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findTitle = learningD.Topics.Where(t => t.Id == find).FirstOrDefault();
                if (findTitle == null)
                {
                    Console.WriteLine("Topic not found\n");
                }
            }
        }

        //edit title
        public static async Task NewTitle(string findT, string newTitle)
        {
            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                
                var findTitle = learningD.Topics.Where(t => t.Title == findT).FirstOrDefault();
                findTitle.Title = newTitle;
                learningD.SaveChanges();
                Console.WriteLine("new Title \"{0}\" is now added", findTitle.Title);
            }
        }

        //edit description
        public static async Task NewDescription(string findT, string newD)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                findByTitle.Descriptions = newD;

                learningD.SaveChanges();

            }
            Console.WriteLine("new Description \"{0}\" is now added", newD);

        }

        //edit mastering time
        public static async Task NewTimeToMaster(string findT, int newT)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                findByTitle.TimeToMaster = newT;

                learningD.SaveChanges();
                Console.WriteLine("new Estimated time to master \"{0}\" is now added", newT);

            }
            

        }

        //edit source
        public static async Task NewSource(string findT, string newS)
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
        public static async Task NewStartTime(string findT, DateTime newDate)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByTitle = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();
                findByTitle.StartLearningDate = newDate;

                learningD.SaveChanges();

            }
            Console.WriteLine("new Start Time \"{0}\" is now added", newDate);

        }

        //edit progress EI TOIMI 
        public static bool NewProgress(string findT)
        {

            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByT = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();

                Console.WriteLine("Is learning still in progress? YES / NO ");
                string progress = Console.ReadLine();
                if (progress.ToUpper() == "NO")
                {
                    //  bool progressBoolean = false;
                    //  return findByT.InProgress = progressBoolean;
                    return Convert.ToBoolean(findByT.InProgress = false);

                    // NewCompeltionDate(findT);

                    /*
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
                    Console.WriteLine("\nYour learning time was:" + learningTime.Days);
                    findByT.TimeSpent = Convert.ToDouble(learningTime.TotalDays); */

                }
                else
                {
                    return Convert.ToBoolean(findByT.InProgress = true);
                }

                learningD.SaveChanges();

            }

        }
       
        //method to add new completiondate if user set NewProgress to "NO" EI TOIMI
        public static void NewCompletionDate(string findT)
        {
            using (LearningDiaryContext learningD = new LearningDiaryContext())
            {
                var findByT = learningD.Topics.Where(d => d.Title == findT).FirstOrDefault();

                Console.WriteLine("Add completion date dd/mm/yyyy: ");
                try
                {
                    findByT.CompletionDate = DateTime.Parse(Console.ReadLine());
                   // return Convert.ToDateTime(findByT.CompletionDate); Kokeilin myös että medoti palauttaisi datetime mutta ei toiminut
                }
                catch
                {
                    Console.WriteLine("Check that you entered the date in the correct format dd/mm/yyyy");
                    findByT.CompletionDate = DateTime.Parse(Console.ReadLine());

                }
                TimeSpan learningTime = Convert.ToDateTime(findByT.CompletionDate) - Convert.ToDateTime(findByT.StartLearningDate);
                Console.WriteLine("\nYour learning time was:" + learningTime.Days);
                findByT.TimeSpent = Convert.ToDouble(learningTime.TotalDays);
            }
        }
     
        //tells user (in PrintTopic) if studying is still in progress
        public static string StudyingSchedule(DateTime date, double days)
        {
            MethodLibrary methods = new MethodLibrary();
            methods.CheckIfLate(date, days);
            if (true)
            {
                string schedule2 = "This topic is on schedule :)";
                Console.WriteLine(schedule2);
                return schedule2;
            }
            else
            {
                string schedule = "This topic is behind schedule. ";
                Console.WriteLine(schedule);
                return schedule;
            }
            
                
        }
    
    }

}



using System;
using System.Collections.Generic;

#nullable disable

namespace Learningdiary_RL.Models
{
    public partial class Topic
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Descriptions { get; set; }
        public int? TimeToMaster { get; set; }
        public double? TimeSpent { get; set; }
        public string Source { get; set; }
        public DateTime? StartLearningDate { get; set; }
        public DateTime? CompletionDate { get; set; }
        public bool? InProgress { get; set; }
    }
}

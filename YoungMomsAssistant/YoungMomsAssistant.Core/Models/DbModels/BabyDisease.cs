using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class BabyDisease {
        public int Id { get; set; }

        [ForeignKey("Baby")]
        public int Baby_Id { get; set; }
        public Baby Baby { get; set; }

        [ForeignKey("Disease")]
        public int Disease_Id { get; set; }
        public Disease Disease { get; set; }

        public DateTime Begin { get; set; }

        public DateTime? End { get; set; }
    }
}

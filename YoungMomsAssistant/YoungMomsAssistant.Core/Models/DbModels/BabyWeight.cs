using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class BabyWeight {
        public int Id { get; set; }

        [ForeignKey("Baby")]
        public int Baby_Id { get; set; }
        public Baby Baby { get; set; }

        public DateTime Date { get; set; }

        public double Weight { get; set; }
    }
}

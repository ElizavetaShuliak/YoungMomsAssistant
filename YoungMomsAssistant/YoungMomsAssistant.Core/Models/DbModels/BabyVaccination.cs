using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class BabyVaccination {
        public int Id { get; set; }

        [ForeignKey("Baby")]
        public int Baby_Id { get; set; }
        public Baby Baby { get; set; }

        [ForeignKey("Vaccination")]
        public int Vaccination_Id { get; set; }
        public Vaccination Vaccination { get; set; }

        public DateTime Date { get; set; }
    }
}

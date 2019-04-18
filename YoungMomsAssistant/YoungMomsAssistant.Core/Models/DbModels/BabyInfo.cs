using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class BabyInfo {

        public int Id { get; set; }

        [ForeignKey("Baby")]
        public int Baby_Id { get; set; }
        public Baby Baby { get; set; }

        public string Sex { get; set; }

        [Required]
        public string BloodType { get; set; }

        public float CurrentGrowth { get; set; }

        public float CurrentWeight { get; set; }
    }
}

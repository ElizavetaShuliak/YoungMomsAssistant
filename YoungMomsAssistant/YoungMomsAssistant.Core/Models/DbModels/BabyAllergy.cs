using System.ComponentModel.DataAnnotations.Schema;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class BabyAllergy {

        public int Id { get; set; }

        [ForeignKey("Baby")]
        public int Baby_Id { get; set; }
        public Baby Baby { get; set; }

        [ForeignKey("Allergy")]
        public int Allery_Id { get; set; }
        public Allergy Allergy { get; set; }
    }
}

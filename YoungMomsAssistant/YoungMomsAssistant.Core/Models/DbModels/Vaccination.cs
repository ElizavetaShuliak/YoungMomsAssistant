using System.ComponentModel.DataAnnotations;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class Vaccination {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Name { get; set; }
    }
}

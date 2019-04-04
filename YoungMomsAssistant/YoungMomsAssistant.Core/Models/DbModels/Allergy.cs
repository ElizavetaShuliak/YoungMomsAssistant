using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class Allergy {

        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public ICollection<Baby> Babies { get; set; }
    }
}

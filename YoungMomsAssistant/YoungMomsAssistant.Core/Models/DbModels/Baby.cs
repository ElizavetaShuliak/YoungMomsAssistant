using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class Baby {

        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(255)]
        public string LastName { get; set; }

        [Required]
        public DateTime BirthDay { get; set; }

        public ICollection<User> Users { get; set; }

        public ICollection<Allergy> Allergies { get; set; }
    }
}

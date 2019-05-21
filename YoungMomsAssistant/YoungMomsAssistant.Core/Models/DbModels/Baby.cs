using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        [Required]
        public string Sex { get; set; }

        [Required]
        public string BloodType { get; set; }

        [ForeignKey("Image")]
        public int Image_Id { get; set; }
        public Image Image { get; set; }

        public ICollection<UserBaby> UserBabies { get; set; }

        public ICollection<BabyAllergy> BabyAllergies { get; set; }
    }
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class LifeEvent {
        public int Id { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        [MaxLength(1000)]
        public string Summary { get; set; }

        public DateTime Date { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }

        [ForeignKey("Image")]
        public int Image_Id { get; set; }
        public Image Image { get; set; }
    }
}

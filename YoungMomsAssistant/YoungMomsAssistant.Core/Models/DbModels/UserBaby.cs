using System.ComponentModel.DataAnnotations.Schema;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class UserBaby {

        public int Id { get; set; }

        [ForeignKey("User")]
        public int User_Id { get; set; }
        public User User { get; set; }

        [ForeignKey("Baby")]
        public int Baby_Id { get; set; }
        public Baby Baby { get; set; }
    }
}

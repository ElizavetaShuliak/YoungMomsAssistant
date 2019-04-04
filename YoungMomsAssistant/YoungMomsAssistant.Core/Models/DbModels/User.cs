using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace YoungMomsAssistant.Core.Models.DbModels {
    public class User {

        public int Id { get; set; }

        [MaxLength(255)]
        public string Login { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public ICollection<Baby> Babies { get; set; }
    }
}

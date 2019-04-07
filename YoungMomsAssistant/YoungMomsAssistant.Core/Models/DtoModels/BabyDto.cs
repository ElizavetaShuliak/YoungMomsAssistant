using System;

namespace YoungMomsAssistant.Core.Models.DtoModels {
    public class BabyDto {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDay { get; set; }

        public string Sex { get; set; }

        public string BloodType { get; set; }

        public float CurrentGrowth { get; set; }

        public float CurrentWeight { get; set; }
    }
}

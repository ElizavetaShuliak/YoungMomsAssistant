using System;
using System.Collections.Generic;
using System.Text;

namespace YoungMomsAssistant.Core.Models.DtoModels {
    public class LifeEventDto {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public DateTime Date { get; set; }

        public bool IsImageChanged { get; set; }

        public byte[] Image { get; set; }
    }
}

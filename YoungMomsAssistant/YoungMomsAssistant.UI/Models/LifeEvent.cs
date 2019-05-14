using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Models {
    public class LifeEvent {

        public int Id { get; set; }

        public string Title { get; set; }

        public string Summary { get; set; }

        public bool IsImageChanged { get; set; }

        public byte[] Image { get; set; }
    }
}

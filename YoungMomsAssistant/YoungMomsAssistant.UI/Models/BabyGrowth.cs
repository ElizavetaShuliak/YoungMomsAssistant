using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YoungMomsAssistant.UI.Models {
    public class BabyGrowth : ModelBase {
        public int Id { get; set; }

        public int BabyId { get; set; }

        public DateTime Date { get; set; }

        public double Growth { get; set; }
    }
}

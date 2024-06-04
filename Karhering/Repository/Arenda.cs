using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karhering.Repository
{
    internal class Arenda
    {
        public int id_zakaza { get; set; }
        public string beginning_time { get; set; }
        public string end_time { get; set; }
        public string Travel_time { get; set; }
        public float cost { get; set; }
        public int car_id { get; set; }
        public int id_client { get; set; }

    }
}

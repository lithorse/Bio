using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.Models
{
    public class Auditorium
    {
        public int ID { get; set; }
        public int Seats { get; set; }
        public virtual ICollection<Viewing> Viewings { get; set; }
    }
}

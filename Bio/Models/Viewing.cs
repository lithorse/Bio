using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.Models
{
    public class Viewing
    {
        public int ID { get; set; }
        public int SeatsLeft { get; set; }
        public string Time { get; set; }
        public int MovieID { get; set; }
        public int AuditoriumID { get; set; }

        public virtual Movie Movie { get; set; }
        public virtual Auditorium Auditorium { get; set; }
    }
}

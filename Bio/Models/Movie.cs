using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bio.Models
{
    public class Movie
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int Length { get; set; }
        public virtual ICollection<Viewing> Viewings { get; set; }
    }
}

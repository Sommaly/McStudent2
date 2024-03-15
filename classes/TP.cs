using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTP.classes
{
    public class TP
    {
        public int Id {  get; set; }
        public string Titre { get; set; }
        public string Description { get; set; }
        public List<Jalon> Jalons { get; set; }
        public Promotion Promotion { get; set; }

        public TP() { 
            this.Jalons = new List<Jalon>();
        }
    }
}

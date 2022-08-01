using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JP_Core.Entities
{
    public class job_entity
    {
        public string id { get; set; }
        public string title { get; set; }
        public string vacancies { get; set; }
        public string starttime { get; set; }
        public string endtime { get; set; }
        public string type { get; set; }
        public string level { get; set; }
        public string description { get; set; }
        public string status { get; set; }
        public string postTime { get; set; }
    }
}

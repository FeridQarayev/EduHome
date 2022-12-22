using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string City { get; set; }
        public string Image { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime EndTime { get; set; }
    }
}

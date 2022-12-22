using EduHome.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.ViewModels
{
    public class HomeVM
    {
        public List<Slider> Sliders { get; set; }
        public About About { get; set; }
        public List<Courses> Courses { get; set; }
        public List<Notice> Notice { get; set; }
        public NoticeVideo NoticeVideo { get; set; }
        public Testimonial Testimonial { get; set; }
        public List<Blog> Blog { get; set; }
        public List<Event> Events { get; set; }
        public List<Service> Services { get; set; }
    }
}

using EduHome.Dal;
using EduHome.Models;
using EduHome.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _db;
        public HomeController(AppDbContext db)
        {
            _db = db;
        }
        public async Task<IActionResult> Index()
        {
            List < Slider > sliders= await _db.Sliders.Where(x => !x.IsDeactive).ToListAsync();
            About about = await _db.Abouts.FirstAsync();
            List<Courses> courses = await _db.Courses.Take(3).ToListAsync();
            List<Notice> notices = await _db.Notices.ToListAsync();
            NoticeVideo noticeVideo = await _db.NoticeVideos.FirstAsync();
            Testimonial testimonial = await _db.Testimonials.FirstAsync();
            List<Blog> blog = await _db.Blogs.ToListAsync();
            List<Event> events = await _db.Events.ToListAsync();
            List<Service> services = await _db.Services.Where(x=>!x.IsDeactive).ToListAsync();
            HomeVM homeVM = new HomeVM
            {
                Sliders = sliders,
                About = about,
                Courses = courses,
                Notice = notices,
                NoticeVideo = noticeVideo,
                Testimonial = testimonial,
                Blog = blog,
                Events = events,
                Services = services
            };
            return View(homeVM);
        }
        public IActionResult Error()
        {
            return View();
        }
    }
}

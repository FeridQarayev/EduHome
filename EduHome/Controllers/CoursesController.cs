using EduHome.Dal;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Controllers
{
    public class CoursesController : Controller
    {
        private readonly AppDbContext _db;
        public CoursesController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            ViewBag.ProCount = _db.Courses.Count();
            List<Courses> courses = _db.Courses.OrderByDescending(x=>x.Id).Take(6).ToList();
            return View(courses);
        }
        public IActionResult LoadMore(int skip)
        {
            if (_db.Courses.Count()<=skip)
            {
                return Content("I will find you!");
            }
            List<Courses> courses = _db.Courses.OrderByDescending(x => x.Id).Skip(skip).Take(6).ToList();
            return PartialView("_CoursesLoadMorePartial", courses);
        }
    }
}

using EduHome.Dal;
using EduHome.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicesController : Controller
    {
        private readonly AppDbContext _db;
        public ServicesController(AppDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Service> services = _db.Services.ToList();
            return View(services);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Service service)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.Services.AnyAsync(x => x.Title == service.Title);
            if (isExist)
            {
                ModelState.AddModelError("Title", "This Service is already exists!");
                return View();
            }
            await _db.Services.AddAsync(service);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Service dbservice = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbservice==null)
            {
                return BadRequest();
            }
            return View(dbservice);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int? id,Service service)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service dbservice = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbservice == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            bool isExist = await _db.Services.AnyAsync(x => x.Title == service.Title&&x.Id!=id);
            if (isExist)
            {
                ModelState.AddModelError("Title", "This Service already exist!");
                return View();
            }
            dbservice.Title = service.Title;
            dbservice.Description = service.Description;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service dbservice = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbservice == null)
            {
                return BadRequest();
            }
            if (dbservice.IsDeactive)
            {
                dbservice.IsDeactive = false;
            }
            else
            {
                dbservice.IsDeactive = true;
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Detail(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Service dbservice = await _db.Services.FirstOrDefaultAsync(x => x.Id == id);
            if (dbservice == null)
            {
                return BadRequest();
            }
            return View(dbservice);
        }
    }
}

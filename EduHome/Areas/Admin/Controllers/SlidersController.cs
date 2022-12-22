using EduHome.Dal;
using EduHome.Helpers;
using EduHome.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace EduHome.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SlidersController : Controller
    {
        private readonly AppDbContext _db;
        private readonly IWebHostEnvironment _env;
        public SlidersController(AppDbContext db, IWebHostEnvironment env)
        {
            _db = db;
            _env = env;
        }
        public IActionResult Index()
        {
            return View(_db.Sliders.ToList());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(Slider slider)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slider.Photo == null)
            {
                ModelState.AddModelError("Photo", "Please choose photo");
                return View();
            }
            if (!slider.Photo.IsImage())
            {
                ModelState.AddModelError("Photo", "Please choose Image file");
                return View();
            }
            if (slider.Photo.IsOlderTwoMB())
            {
                ModelState.AddModelError("Photo", "Image max 2MB");
                return View();
            }
            string folder = Path.Combine(_env.WebRootPath, "img", "slider");
            slider.Image = await slider.Photo.SaveFileAsync(folder);
            await _db.Sliders.AddAsync(slider);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Update(int? id)
        {
            if (id==null)
            {
                return NotFound();
            }
            Slider slider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (slider==null)
            {
                return BadRequest();
            }
            return View(slider);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int? id,Slider slider)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbslider == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (slider.Photo != null)
            {
                if (!slider.Photo.IsImage())
                {
                    ModelState.AddModelError("Photo", "Please choose Image file");
                    return View();
                }
                if (slider.Photo.IsOlderTwoMB())
                {
                    ModelState.AddModelError("Photo", "Image max 2MB");
                    return View();
                }
                string folder = Path.Combine(_env.WebRootPath, "img", "slider");
                string path = Path.Combine(folder, dbslider.Image);
                if (System.IO.File.Exists(path))
                {
                    System.IO.File.Delete(path);
                }
                dbslider.Image = await slider.Photo.SaveFileAsync(folder);
            }
            dbslider.Title = slider.Title;
            dbslider.SubTitle = slider.SubTitle;
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Activity(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbslider == null)
            {
                return BadRequest();
            }
            if (dbslider.IsDeactive)
            {
                dbslider.IsDeactive = false;
            }
            else
            {
                dbslider.IsDeactive = true;
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
            Slider dbslider = await _db.Sliders.FirstOrDefaultAsync(x => x.Id == id);
            if (dbslider == null)
            {
                return BadRequest();
            }
            return View(dbslider);
        }
    }
}

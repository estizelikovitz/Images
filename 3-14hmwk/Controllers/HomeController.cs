using _3_14hmwk.Models;
using _3_23hmwk;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace _3_14hmwk.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=Images; Integrated Security=true;";

        private readonly IWebHostEnvironment _webHostEnvironment;

        public HomeController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ViewImage(int id)
        {
            var repo = new Repo(_connectionString);
            Image image = repo.GetAll().FirstOrDefault(i => i.Id == id);
            image.Views = HttpContext.Session.GetInt32("Count");
            int count = image.Views.HasValue ? image.Views.Value : 1;
            HttpContext.Session.SetInt32("Count", count + 1);
            return View(image);
        }
        public IActionResult ViewImageRequest(int id)
        {
            var repo = new Repo(_connectionString);
            return View(repo.GetAll().FirstOrDefault(i => i.Id == id));
        }
        public IActionResult Upload(string password, IFormFile image)
        {
            LinkPassword lp = new();
            string fileName = $"{Guid.NewGuid()}-{image.FileName}";

            string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "uploads", fileName);
            using var fs = new FileStream(filePath, FileMode.CreateNew);
            image.CopyTo(fs);
            var repo = new Repo(_connectionString);
            int id = repo.Add(password, Path.Combine("/uploads", fileName));
            lp.ImageId = id;
            lp.Password = password;

            return View(lp);
        }
    }
}

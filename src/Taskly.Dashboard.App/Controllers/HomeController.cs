using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Taskly.Dashboard.App.Data;
using Taskly.Dashboard.App.Services.Interfaces;
using Taskly.Users.Models;

namespace Home.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!_userService.RegisterUser(model))
            {
                ModelState.AddModelError("Email", "Email or Phone number already exists.");
                return View(model);
            }

            TempData["SuccessMessage"] = "Successful!";
            return RedirectToAction("");
        }
    }
}
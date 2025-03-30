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
using Taskly.Dashboard.App.Exceptions;
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
        public async Task<IActionResult> Register(User model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            try
            {
                var userId = await _userService.RegisterUser(model);

                if (userId == null)
                {
                    ModelState.AddModelError("Email", "Registration failed. Please try again.");
                    return View(model);
                }

                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("");
            }
            catch (UserRegistrationException ex)
            {
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }
        }
    }
}
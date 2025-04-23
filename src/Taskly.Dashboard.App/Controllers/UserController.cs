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
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Register()
        {
            _logger.LogInformation("User navigated to Register page at {Time}", DateTime.UtcNow);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(User model)
        {
            _logger.LogInformation("Received registration form submission at {Time}", DateTime.UtcNow);

            if (!ModelState.IsValid)
            {
                _logger.LogWarning("Registration form validation failed at {Time}", DateTime.UtcNow);
                return View(model);
            }

            try
            {
                var userId = await _userService.RegisterUserAsync(model);

                if (userId == null)
                {
                    _logger.LogWarning("User registration failed for email {Email} at {Time}", model.Email, DateTime.UtcNow);
                    ModelState.AddModelError("Email", "Registration failed. Please try again.");
                    return View(model);
                }

                _logger.LogInformation("User registered successfully with ID {UserId} at {Time}", userId, DateTime.UtcNow);
                TempData["SuccessMessage"] = "Registration successful!";
                return RedirectToAction("Register");
            }
            catch (UserRegistrationException ex)
            {
                _logger.LogError(ex, "User registration exception for email {Email} at {Time}", model.Email, DateTime.UtcNow);
                ModelState.AddModelError("Email", ex.Message);
                return View(model);
            }
            catch (Exception ex)
            {
                _logger.LogCritical(ex, "Unexpected error during registration for email {Email} at {Time}", model.Email, DateTime.UtcNow);
                ModelState.AddModelError("Error", "An unexpected error occurred. Please try again later.");
                return View(model);
            }
        }
    }
}
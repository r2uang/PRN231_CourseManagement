﻿using BussinessObject.DTOs;
using CourseManagementWebClientWebClient.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CourseManagementWebClientWebClient.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<AppUser> _userManager;
        private readonly HttpClient client = null;
        private string CourseApiUrl = "";

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            _userManager = userManager;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CourseApiUrl = "https://localhost:7167/api/courses";
        }

        public async Task<IActionResult> Index()
        {
            var userBytes = HttpContext.Session.Get("User");
            if(userBytes == null)
            {
                return View();
            }
            var user = JsonSerializer.Deserialize<AppUser>(userBytes);
            List<String> roles = await GetUserRoles(user);
            return View(roles);
        }
        private async Task<List<string>> GetUserRoles(AppUser user)
        {
            return new List<string>(await _userManager.GetRolesAsync(user));
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
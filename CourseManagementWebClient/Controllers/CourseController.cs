using BussinessObject.DTOs;
using CourseManagementWebClientWebClient.Controllers;
using CourseManagementWebClientWebClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CourseManagementWebClient.Controllers
{
    public class CourseController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string CourseApiUrl = "";

        public CourseController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            CourseApiUrl = "https://localhost:7167/api/courses";
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage reponse = await client.GetAsync(CourseApiUrl);
            var courses = reponse.Content.ReadFromJsonAsync<List<CourseDTO>>().Result;
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
            return View(courses);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CourseDTO courseDTO)
        {
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.PostAsJsonAsync(CourseApiUrl, courseDTO))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return Redirect("/Course/Index");
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            var course = await GetCourseById(id);
            if (course == null)
            {
                return NotFound();
            }
            return View(course);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] CourseDTO courseDTO)
        {
            if (!ModelState.IsValid)
            {
                return View(courseDTO);
            }
            HttpResponseMessage response = await client.PutAsJsonAsync(CourseApiUrl, courseDTO);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            return View(courseDTO);
        }
        public async Task<IActionResult> Details(int id)
        {
            var product = await GetCourseById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        private async Task<CourseDTO?> GetCourseById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            HttpResponseMessage response = await client.GetAsync(CourseApiUrl + "/id?id=" + id);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ReadFromJsonAsync<CourseDTO>().Result;
            }
            return null;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var product = await GetCourseById(id);
            if (product == null)
            {
                return NotFound();
            }
            await client.DeleteAsync(CourseApiUrl + "/id?id=" + id);
            return Redirect("/Course/Index");
        }

        public IActionResult Topic(int? id)
        {
            ViewBag.CaseId = id;
            return View();
        }
    }
}

using BussinessObject.DTOs;
using CourseManagementWebClientWebClient.Controllers;
using CourseManagementWebClientWebClient.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net.Http.Headers;
using System.Text.Json;

namespace CourseManagementWebClient.Controllers
{
    public class TopicController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HttpClient client = null;
        private string TopicApiUrl = "";

        public TopicController(ILogger<HomeController> logger, UserManager<AppUser> userManager)
        {
            _logger = logger;
            client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            client.DefaultRequestHeaders.Accept.Add(contentType);
            TopicApiUrl = "https://localhost:7167/api/topics";
        }

        public async Task<IActionResult> Create(int id)
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] TopicDTO topicDTO, IFormFile formFile, int id)
        {
            using (var httpClient = new HttpClient())
            {
                topicDTO.CourseId = id;
                using (var response = await httpClient.PostAsJsonAsync(TopicApiUrl, topicDTO))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                }
            }
            return Redirect("/Course/Details/" + id);
        }
        [HttpGet("Topic/Edit/{id}")]
        public async Task<IActionResult> Edit(int? id)
        {
            var topic = await GetTopicById(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }
        [HttpPost]
        public async Task<IActionResult> Edit([FromForm] TopicDTO topicDTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return View(topicDTO);
            }
            var topic = await GetTopicById(id);
            topicDTO.CourseId = topic.CourseId;
            HttpResponseMessage response = await client.PutAsJsonAsync(TopicApiUrl, topicDTO);
            return Redirect("/Course/Details/" + topicDTO.CourseId);
        }
        public async Task<IActionResult> Details(int id)
        {
            var topic = await GetTopicById(id);
            if (topic == null)
            {
                return NotFound();
            }
            return View(topic);
        }

        private async Task<TopicDTO?> GetTopicById(int? id)
        {
            if (id == null)
            {
                return null;
            }
            HttpResponseMessage response = await client.GetAsync(TopicApiUrl + "/" +id + "/topic");
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ReadFromJsonAsync<TopicDTO>().Result;
            }
            return null;
        }

        public async Task<IActionResult> Delete(int? id)
        {
            var product = await GetTopicById(id);
            if (product == null)
            {
                return NotFound();
            }
            await client.DeleteAsync(TopicApiUrl + "/id?id=" + id);
            return Redirect("/Product/Index");
        }

        [HttpGet]
        public async Task<IActionResult> UploadMaterial()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UploadMaterial(IFormFile file, int id)
        {
            HttpClient httpClient = new HttpClient();

            // Create a new FormData instance and add the file to it
            var formData = new MultipartFormDataContent();
            
            //Dang loi o day
            //formData.Add(new StreamContent(), "file", file.FileName);

            try
            {
                // Send a POST request to the API endpoint with the file
                HttpResponseMessage response = await httpClient.PostAsync(TopicApiUrl + "upload-meterial" + id, formData);

                // Check if the response was successful
                if (response.IsSuccessStatusCode)
                {
                    // File uploaded successfully
                    ViewBag.Message = "File uploaded successfully";
                }
                else
                {
                    // File upload failed
                    ViewBag.Message = "File upload failed";
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur during the request
                ViewBag.Message = "An error occurred: " + ex.Message;
            }

            // Return the view with the message
            return View();
        }
    }
}

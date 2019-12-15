using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientAppMovieReview.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ClientAppMovieReview.Pages.Users
{
    public class CreateModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl;
        private string _apiKey;
        [BindProperty]
        public User user { get; set; }
        public CreateModel(IConfiguration iConfiguration)
        {
            _apiUrl = iConfiguration.GetSection("ApiUrl").Value;
            _apiKey = iConfiguration.GetSection("ApiKey").Value;
        }
        public void OnGet()
        {

        }
        public async Task<IActionResult> OnPostAsync()
        {
            _client.BaseAddress = new Uri(_apiUrl);
            _client.DefaultRequestHeaders.Accept.Clear();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            _client.DefaultRequestHeaders.Add("x-apikey", _apiKey);
            try
            {
                string json;
                HttpResponseMessage response;
                user.DateCreated = DateTime.Now.Date;
                user.Role = "user";
                json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync("moviereview/api/Users/PostUser", content);

                //   response = await client.PostAsJsonAsync("/api/TodoItems", item);

                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();
                TempData["successmsg"] = "User has been Added Successfully";
                return RedirectToPage("/Users/Login");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                TempData["errormsg"] = e.Message;
                return RedirectToPage("/Users/Create");
            }
        }
    }
}
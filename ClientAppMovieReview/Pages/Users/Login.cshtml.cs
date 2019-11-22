using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using ClientAppMovieReview.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace ClientAppMovieReview.Pages.Users
{
    public class LoginModel : PageModel
    {
        private HttpClient _client = new HttpClient();
        private string _apiUrl; 
        private string _apiKey;
        public User _loggedInUser;

        [BindProperty]
        public User user { get; set; }
        public LoginModel(IConfiguration iConfiguration)
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
                json = JsonConvert.SerializeObject(user);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                response = await _client.PostAsync("moviereview/api/Users/Login", content);
               
                Console.WriteLine($"status from POST {response.StatusCode}");
                response.EnsureSuccessStatusCode();
                Console.WriteLine($"added resource at {response.Headers.Location}");
                json = await response.Content.ReadAsStringAsync();

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    HttpContext.Session.SetString("userString", json);
                    HttpContext.Session.SetString("isLoggedIn", "true");
                    _loggedInUser = JsonConvert.DeserializeObject<User>(HttpContext.Session.GetString("userString"));
                    HttpContext.Session.SetString("userRole", _loggedInUser.Role);
                }
                TempData["successmsg"] = "User has been loggedIn Successfully";
                return RedirectToPage("/Index");
            }
            catch (Exception e)
            {
                TempData["errormsg"] = "Invalid Credentials";
                return RedirectToPage("/Users/Login");
            }
        }
        public IActionResult OnGetLogout() {
            HttpContext.Session.Clear();
            return RedirectToPage("/Users/Login");
        }
    }
}
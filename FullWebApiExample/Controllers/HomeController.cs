using FullWebApiExample.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;


namespace FullWebApiExample.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {

        }
        public async Task<IActionResult> Index()
        {
            List<Employee> employeeList = new List<Employee>();

            using (var client = new HttpClient())
            {
                //[HttpGet] api request
                using (var stream = await client.GetAsync("https://localhost:44346/api/Employee"))
                {

                    string apiResponse = await stream.Content.ReadAsStringAsync();
                    //Debug.Write(apiResponse);
                    
                    employeeList = JsonConvert.DeserializeObject<List<Employee>>(apiResponse);
                    
                }
            }
         
            return View(employeeList);
        }

        public ViewResult GetEmployee() => View();
        [HttpPost]

        public async Task<IActionResult> GetEmployee(int id)
        {
            Employee employee = new Employee();
            using(var client = new HttpClient())
            {
                
                using (var response = await client.GetAsync("https://localhost:44346/api/Employee/" + id))
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //Debug.Write(apiResponse);
                        employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                        
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;

                    }
                }
                return View(employee);
            }
        }


        public ViewResult AddEmployee() => View();
        [HttpPost]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            Employee recievedEmployee = new Employee();
            if(employee.Id == 0)
            {
                
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(employee), Encoding.UTF8, "application/json");
                    using (var response = await client.PostAsync("https://localhost:44346/api/Employee", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        recievedEmployee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                    }
                }
            }
            return View(recievedEmployee);

        }

        
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            using(var client = new HttpClient())
            {
                using(var response = await client.DeleteAsync("https://localhost:44346/api/Employee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();


                }
            }
            return RedirectToAction("index");   

        }

       
        public async Task<IActionResult> Details(int id)
        {

            Employee employeeDetails = new Employee();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:44346/api/Employee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employeeDetails = JsonConvert.DeserializeObject<Employee>(apiResponse);
                }
            }


                return View(employeeDetails);
            
        }


        public async Task<IActionResult> UpdateEmployee(int id)
        {
            Employee employee = new Employee();
            using (var httpClient = new HttpClient())
            {
                using (var response = await httpClient.GetAsync("https://localhost:44324/api/Employee/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    employee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                }
            }
            return View(employee);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateEmployee(Employee employee)
        {
            Employee receivedEmployee = new Employee();
            using (var httpClient = new HttpClient())
            {
                var content = new MultipartFormDataContent();
                content.Add(new StringContent(employee.Id.ToString()), "Id");
                content.Add(new StringContent(employee.Name), "Name");
                content.Add(new StringContent(employee.Email), "Email");
                content.Add(new StringContent(employee.Mobile), "Mobile");
                content.Add(new StringContent(employee.Mobile), "Mobile");
                content.Add(new StringContent(employee.Address), "Address");




                using (var response = await httpClient.PutAsync("https://localhost:44324/api/Reservation", content))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    ViewBag.Result = "Success";
                    receivedEmployee = JsonConvert.DeserializeObject<Employee>(apiResponse);
                }
            }
            return View(receivedEmployee);
        }


    }
}

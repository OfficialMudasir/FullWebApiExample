using FullWebApiExample.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
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
            List<Reservation> reservationList = new List<Reservation>();

            using (var client = new HttpClient())
            {
                //[HttpGet] api request
                using (var stream = await client.GetAsync("https://localhost:44346/api/Reservation"))
                {

                    string apiResponse = await stream.Content.ReadAsStringAsync();
                    //Debug.Write(apiResponse);
                    
                    reservationList = JsonConvert.DeserializeObject<List<Reservation>>(apiResponse);
                    
                }
            }
         
            return View(reservationList);
        }

        public ViewResult GetReservation() => View();
        [HttpPost]

        public async Task<IActionResult>GetReservation(int id)
        {
            Reservation reservation = new Reservation();
            using(var client = new HttpClient())
            {
                
                using (var response = await client.GetAsync("https://localhost:44346/api/Reservation/" + id))
                {
                    if(response.StatusCode == System.Net.HttpStatusCode.OK)
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        //Debug.Write(apiResponse);
                        reservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                        
                    }
                    else
                    {
                        ViewBag.StatusCode = response.StatusCode;

                    }
                }
                return View(reservation);
            }
        }


        public ViewResult AddReservation() => View();
        [HttpPost]
        public async Task<IActionResult> AddReservation(Reservation reservation)
        {
            Reservation recievedReservation = new Reservation();
            if(reservation.Id == 0)
            {
                
                using (var client = new HttpClient())
                {
                    StringContent content = new StringContent(JsonConvert.SerializeObject(reservation), Encoding.UTF8, "application/json");
                    using (var response = await client.PostAsync("https://localhost:44346/api/Reservation", content))
                    {
                        string apiResponse = await response.Content.ReadAsStringAsync();
                        recievedReservation = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                    }
                }
            }
            return View(recievedReservation);

        }

        
        public async Task<IActionResult> DeleteReservation(int id)
        {
            using(var client = new HttpClient())
            {
                using(var response = await client.DeleteAsync("https://localhost:44346/api/Reservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();


                }
            }
            return RedirectToAction("index");

        }

       
        public async Task<IActionResult> Details(int id)
        {
            Reservation reservationDetails = new Reservation();
            using (var client = new HttpClient())
            {
                using (var response = await client.GetAsync("https://localhost:44346/api/Reservation/" + id))
                {
                    string apiResponse = await response.Content.ReadAsStringAsync();
                    reservationDetails = JsonConvert.DeserializeObject<Reservation>(apiResponse);
                }
            }


                return View(reservationDetails);
            
        }
        

    }
}

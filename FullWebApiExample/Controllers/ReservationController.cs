using FullWebApiExample.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FullWebApiExample.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReservationController : ControllerBase
    {
        private readonly IRepository _repository;

        //Dependency injection
        public ReservationController(IRepository repository) => _repository = repository;

        //Get all items
        [HttpGet]
        public IEnumerable<Reservation> Get() => _repository.Reservations;

        //get items by Id
        [HttpGet("{id}")]
        public ActionResult<Reservation> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed to the required body");

            return Ok(_repository[id]);
        }

        //Adding new records to the reservation class
        [HttpPost]
        public Reservation Post(Reservation res) =>
            _repository.AddReservation(new Reservation
            {

                Id = res.Id,
                Name = res.Name,
                StartLocation = res.StartLocation,
                EndLocation = res.EndLocation
            });

       
        // updating records     
        [HttpPut]
        public Reservation Put([FromBody] Reservation res) => _repository.UpdateReservation(res);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody]JsonPatchDocument<Reservation> patch)
        {
            var res = (Reservation)((OkObjectResult)Get(id).Result).Value;
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }


        [HttpDelete("{id}")]

        public void Delete(int id) => _repository.DeleteReservation(id);

    }
}

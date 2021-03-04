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
    public class EmployeeController : ControllerBase
    {
        private readonly IRepository _repository;

        //Dependency injection
        public EmployeeController(IRepository repository) => _repository = repository;

        //Get all items
        [HttpGet]
        public IEnumerable<Employee> Get() => _repository.Employees;

        //get items by Id
        [HttpGet("{id}")]
        public ActionResult<Employee> Get(int id)
        {
            if (id == 0)
                return BadRequest("Value must be passed to the required body");

            return Ok(_repository[id]);
        }

        //Adding new records to the Employee class
        [HttpPost]
        public Employee Post(Employee res) =>
            _repository.AddEmployee(new Employee
            {

                Id = res.Id,
                Name = res.Name,
                Email = res.Email,
                Mobile = res.Mobile,
                Address = res.Address
                
            });

       
        // updating records     
        [HttpPut]
        public Employee Put([FromForm] Employee res) => _repository.UpdateEmployee(res);

        [HttpPatch("{id}")]
        public StatusCodeResult Patch(int id, [FromBody]JsonPatchDocument<Employee> patch)
        {
            var res = (Employee)((OkObjectResult)Get(id).Result).Value;
            if (res != null)
            {
                patch.ApplyTo(res);
                return Ok();
            }
            return NotFound();
        }


        [HttpDelete("{id}")]

        public void Delete(int id) => _repository.DeleteEmployee(id);

    }
}

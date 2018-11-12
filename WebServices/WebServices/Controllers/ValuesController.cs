using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebServices.Controllers
{
    //Route tokens specify current controller/action/area
    [Route("api/[controller]")]
    public class ValuesController : ControllerBase
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromQuery] string query)
        {
            return Ok(new Value { Id = id, Text = "value" + id });
        }

        // GET For optional route values
        [HttpGet("/optional/{id?}")]
        public string OptionalGet(int? id)
        {
            if (id != null)
                return $"value: {id}";
            return "No value specified.";
        }

        // GET For default route values
        [HttpGet("/default/{id=5}")]
        public string DefaultGet(int id)
        {
            if (id != 5)
                return $"Default value: {id}";
            return $"value {id}";
        }

        // GET With type constraints on route values
        [HttpGet("/constraint/{id:int}")]
        public string ConstraintGet(int id)
        {
            return $"value {id}";
        }

        // POST api/<controller>
        [HttpPost]
        public IActionResult Post([FromBody] Value value)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // save the value to the DB

            return CreatedAtAction("Get", new { id = value.Id }, value);
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }

    public class Value
    {
        public int Id { get; set; }
        [MinLength(3)]
        public string Text { get; set; }
    }
}

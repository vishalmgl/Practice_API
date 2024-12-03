using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using test2.Model;

namespace test2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NameController : ControllerBase
    {
        private static List<NameModel> names = new List<NameModel>();

        // Get all names with their ages
        [HttpGet]
        public IActionResult GetAll()
        {
            return Ok(names);
        }

        // Add a new name with age
        [HttpPost]
        public IActionResult AddName([FromBody] NameModel name)
        {
            if (name == null || string.IsNullOrWhiteSpace(name.Name) || name.Age <= 0)
                return BadRequest("Invalid name or age.");

            name.Id = names.Count > 0 ? names.Max(n => n.Id) + 1 : 1;
            names.Add(name);
            return CreatedAtAction(nameof(GetById), new { id = name.Id }, name);
        }

        // Get a specific name by ID
        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            var name = names.FirstOrDefault(n => n.Id == id);
            if (name == null)
                return NotFound("Name not found.");

            return Ok(name);
        }

        // Update either the name or age
        [HttpPut("{id}")]
        [SwaggerOperation(Summary = "Update name or age", Description = "Only the fields provided in the request body will be updated. If no field is provided, it remains unchanged.")]
        [SwaggerResponse(200, "Successfully updated", typeof(NameModel))]
        [SwaggerResponse(404, "Name not found")]
        public IActionResult UpdateName(int id, [FromBody] NameModel updatedName)
        {
            // Find the existing name object by ID
            var name = names.FirstOrDefault(n => n.Id == id);
            if (name == null)
                return NotFound("Name not found.");

            // Update fields only if they are provided
            if (updatedName.Name != "string")
            {
                name.Name = updatedName.Name;
            }
            if (updatedName.Age > 0)
            {
                name.Age = updatedName.Age;
            }

            // Return the updated object, which includes unchanged fields
            return Ok(name);
        }



        // Delete a name by ID
        [HttpDelete("{id}")]
        public IActionResult DeleteName(int id)
        {
            var name = names.FirstOrDefault(n => n.Id == id);
            if (name == null)
                return NotFound("Name not found.");

            names.Remove(name);
            return NoContent();
        }
    }
}

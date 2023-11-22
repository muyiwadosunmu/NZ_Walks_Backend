using Microsoft.AspNetCore.Mvc;

namespace NZWalks_API.Controllers
{
    //GET: https://localhost:portNumber/api/students
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllStudent()
        {
            string[] studentNames = new string[] { "John", "Jane", "Mark", "Emily", "David" };
            return Ok(studentNames);
        }

    }
}
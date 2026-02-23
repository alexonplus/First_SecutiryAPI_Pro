using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace SecutiryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        // In-memory database simulation
        private static readonly List<string> Courses = new() { "C# Pro Mode", "Web Security", "Database Design" };

        /// <summary>
        /// List of all courses (Public)
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetCourses() => Ok(Courses);

        /// <summary>
        /// Create a new course (Protected)
        /// </summary>
        /// <remarks>This is the second POST with a request body as per assignment requirements</remarks>
        [HttpPost]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult CreateCourse([FromBody] CourseModel model)
        {
            Courses.Add(model.Name);
            return CreatedAtAction(nameof(GetCourses), new { name = model.Name });
        }

        /// <summary>
        /// Enroll in a course (Protected)
        /// </summary>
        /// <response code="409">Conflict error: if you are already enrolled</response>
        [HttpPost("{id}/enroll")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public IActionResult Enroll(int id)
        {
            // Demonstration of 409 Conflict error for the assignment
            if (id == 1)
                return Conflict(new { message = "You are already enrolled in this course (Conflict error)" });

            return Ok(new { message = "Enrollment successful!" });
        }

        /// <summary>
        /// Delete a course (Protected)
        /// </summary>
        /// <response code="403">Forbidden error: if you don't have admin rights</response>
        [HttpDelete("{id}")]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public IActionResult DeleteCourse(int id)
        {
            // Demonstration of 403 Forbidden error for the assignment
            // In a real-world scenario, role-based checks would be here
            if (id == 99)
                return Forbid();

            return NoContent();
        }
    }

    public class CourseModel
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}
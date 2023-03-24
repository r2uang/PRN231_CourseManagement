using AutoMapper;
using BussinessObject.DTOs;
using BussinessObject.Models;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace CourseManagmentAPI.Controllers
{
    [Route("api/course")]
    [ApiController]
    public class CourseController : Controller
    {
        private ICourseRepository repository = new CourseRepository();
        private readonly IMapper mapper;

        public CourseController(IMapper mapper)
        {
            this.mapper = mapper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<CourseDTO>> Index()
        {
            List<CourseDTO> courses = repository.getCourses().Select(mapper.Map<Course, CourseDTO>).ToList();
            if (courses.Count == Constants.NUMBER_ZERO)
            {
                return NotFound("NOT FOUND ANY COURSE !!!");
            }
            return Ok(courses);
        }

        [HttpGet("id")]
        public ActionResult <IEnumerable<CourseDTO>> getCourse(int id) {
            Course course = repository.getCourse(id);
            if(course == null)
            {
                return NotFound("COURSE NOT FOUND !!!");
            }
            CourseDTO courseDTO = mapper.Map<CourseDTO>(course);
            return Ok(courseDTO);
        }

        [HttpPost]
        public IActionResult addCourse([FromBody] CourseDTO courseDTO)
        {
            Course course = mapper.Map<Course>(courseDTO);
            repository.addCourse(course);
            return Ok("Create Successfully");
        }

        [HttpPut]
        public IActionResult UpdateProduct([FromBody] CourseDTO courseDTO)
        {
            var course = repository.getCourse(courseDTO.Id);
            if (course == null)
            {
                return NotFound("COURSE NOT FOUND !!!");
            }
            course = mapper.Map<Course>(courseDTO);
            repository.updateCourse(course);
            return Ok("Update Successfully");
        }
        [HttpDelete("id")]
        public IActionResult DeleteProduct(int id)
        {
            var course = repository.getCourse(id);
            if (course == null)
            {
                return NotFound("COURSE NOT FOUND !!!");
            }
            repository.deleteCourse(course.Id);
            return Ok("Delete Successfully");
        }
    }
}

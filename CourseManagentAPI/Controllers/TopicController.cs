using AutoMapper;
using BussinessObject.DTOs;
using BussinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories;

namespace CourseManagmentAPI.Controllers
{
    [Route("api/topics")]
    [ApiController]
    public class TopicController : Controller
    {
        private ITopicRepository repository = new TopicRepository();
        private readonly IMapper mapper;

        public TopicController(IMapper mapper)
        {
            this.mapper = mapper;
        }
        [HttpGet("id")]
        public ActionResult<IEnumerable<TopicDTO>> GetTopics(int id)
        {
            List<TopicDTO> topics = repository.getTopicsByCourseId(id).Select(mapper.Map<Topics, TopicDTO>).ToList();
            if (topics.Count == 0)
            {
                return NotFound("NOT FOUND ANY COURSE !!!");
            }
            return Ok(topics);
        }

        [HttpGet("{id}/topic")]
        public ActionResult<IEnumerable<TopicDTO>> GetTopic(int id)
        {
            Topics topic = repository.getTopic(id);
            if (topic == null)
            {
                return NotFound("TOPIC NOT FOUND !!!");
            }
            TopicDTO topicDTO = mapper.Map<TopicDTO>(topic);
            return Ok(topicDTO);
        }
        [HttpPost]
        public IActionResult AddTopic([FromBody] TopicDTO topicDTO)
        {
            Topics topic = mapper.Map<Topics>(topicDTO);
            repository.addTopic(topic);
            return Ok("Add Successfully");
        }
        [HttpPut]
        public IActionResult UpdateTopic([FromBody] TopicDTO topicDTO)
        {
            var topic = repository.getTopic(topicDTO.Id);
            if (topicDTO == null)
            {
                return NotFound("TOPIC NOT FOUND !!!");
            }
            topic = mapper.Map<Topics>(topicDTO);
            repository.updateTopic(topic);
            return Ok("Update Successfully");
        }
        [HttpDelete("id")]
        public IActionResult DeleteTopic(int id)
        {
            var topic = repository.getTopic(id);
            if (topic == null)
            {
                return NotFound("TOPIC NOT FOUND !!!");
            }
            repository.deleteTopic(topic.Id);
            return Ok("Delete Successfully");
        }

        [HttpPost("upload-meterial")]
        public async Task<ActionResult> PostSingleFile(IFormFile fileData)
        {
            if (fileData == null)
            {
                return BadRequest();
            }
            try
            {
                await repository.addMeterial(fileData);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet("dowload-meterial")]
        public async Task<ActionResult> DownloadFile(int id)
        {
            if (id < 1)
            {
                return BadRequest();
            }

            try
            {
                await repository.dowloadMeterial(id);
                return Ok();
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}


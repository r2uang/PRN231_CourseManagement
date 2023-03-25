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
            List<TopicDTO> topics = repository.getTopicsByCourseId(id).Select(mapper.Map<Topic, TopicDTO>).ToList();
            if (topics.Count == 0)
            {
                return NotFound("NOT FOUND ANY COURSE !!!");
            }
            return Ok(topics);
        }

        [HttpGet("{id}/topic")]
        public ActionResult<IEnumerable<TopicDTO>> GetTopic(int id)
        {
            Topic topic = repository.getTopic(id);
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
            Topic topic = mapper.Map<Topic>(topicDTO);
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
            topic = mapper.Map<Topic>(topicDTO);
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
    }
}


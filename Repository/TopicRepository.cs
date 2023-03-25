using BussinessObject.Models;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class TopicRepository : ITopicRepository
    {
        public void addMeterial()
        {
            throw new NotImplementedException();
        }

        public void addTopic(Topic topic) => TopicDAO.addTopic(topic);

        public void deleteTopic(int id) => TopicDAO.deleteTopic(id);

        public Topic getTopic(int id) => TopicDAO.getTopicById(id);

        public List<Topic> getTopicsByCourseId(int id) => TopicDAO.getTopicsByCourseId(id);

        public void updateTopic(Topic topic) => TopicDAO.updateTopic(topic);
    }
}

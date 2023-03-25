using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface ITopicRepository
    {
        void addTopic(Topic topic);
        void deleteTopic(int id);
        void updateTopic(Topic topic);
        Topic getTopic(int id);
        List<Topic> getTopicsByCourseId(int id);
        void addMeterial();
    }
}

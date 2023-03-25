using BussinessObject.Context;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class TopicDAO
    {
        public static void addTopic(Topic topic)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Topics.Add(topic);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void deleteTopic(int id)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var topic = context.Topics.SingleOrDefault(c => c.Id == id);
                    context.Topics.Remove(topic);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static Topic getTopicById(int id)
        {
            Topic topic = new Topic();
            try
            {
                using (var context = new MyDbContext())
                {
                    topic = context.Topics.SingleOrDefault(x => x.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return topic;
        }

        public static List<Topic> getTopicsByCourseId(int id)
        {
            var topics = new List<Topic>();
            try
            {
                using (var context = new MyDbContext())
                {
                    topics = context.Topics.Where(o => o.CourseId == id).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return topics;
        }

        public static void updateTopic(Topic topic)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Topic>(topic).State =
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

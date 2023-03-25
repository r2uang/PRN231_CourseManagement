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
        public static void addTopic(Topics topic)
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

        public static Topics getTopicById(int id)
        {
            Topics topic = new Topics();
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

        public static List<Topics> getTopicsByCourseId(int id)
        {
            var topics = new List<Topics>();
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

        public static void updateTopic(Topics topic)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Entry<Topics>(topic).State =
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

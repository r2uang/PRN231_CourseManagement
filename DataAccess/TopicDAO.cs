using BussinessObject.Context;
using BussinessObject.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic.FileIO;
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

        public async static Task addMeterial(IFormFile fileData)
        {
            try
            {
                var fileMeterial = new Meterial()
                {
                    Id = 0,
                    FileName = fileData.FileName,
                };
                using (var stream = new MemoryStream())
                {
                    fileData.CopyTo(stream);
                    fileMeterial.FileData = stream.ToArray();
                }
                using (var context = new MyDbContext())
                {
                    var result = context.Meterials.Add(fileMeterial);
                    await context.SaveChangesAsync();
                }
            }
            catch (Exception ex)
            { 
                throw new Exception(ex.Message);
            }
        }

        public async static Task dowloadMeterial(int id)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var file = context.Meterials.Where(x => x.Id == id).FirstOrDefaultAsync();
                    var content = new System.IO.MemoryStream(file.Result.FileData);
                    var path = Path.Combine(
                       @"C:\Users\Khuat Tien Quang\Downloads",
                       file.Result.FileName);
                    await CopyStream(content, path);
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public async static Task CopyStream(Stream stream, string downloadPath)
        {
            using (var fileStream = new FileStream(downloadPath, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
            }
        }
    }
}

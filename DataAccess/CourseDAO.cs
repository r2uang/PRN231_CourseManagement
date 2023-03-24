using BussinessObject.Context;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class CourseDAO
    {
        //todo
        public static List<Course> getCourses()
        {
            var listCourses = new List<Course>();
            try
            {
                using (var context = new MyDbContext())
                {
                    listCourses = context.Course.ToList();
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return listCourses;
        }
        //todo
        public static Course getCourseById(int id)
        {
            Course course = new Course();
            try
            {
                using(var context = new MyDbContext())
                {
                  course = context.Course.SingleOrDefault(c => c.Id == id);
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return course;
        }

        //todo
        public static void addCourse(Course course)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    context.Course.Add(course);
                    context.SaveChanges();
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //todo
        public static void updateCourse(Course course)
        {
            try
            {
                using(var context = new MyDbContext())
                {
                    context.Entry<Course>(course).State = 
                        Microsoft.EntityFrameworkCore.EntityState.Modified;
                    context.SaveChanges();
                        
                }
            }catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        //todo
        public static void deleteCourse(int id)
        {
            try
            {
                using (var context = new MyDbContext())
                {
                    var course = context.Course.SingleOrDefault(c => c.Id == id);
                    context.Course.Remove(course);
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

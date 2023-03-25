﻿using AutoMapper;
using BussinessObject.DTOs;
using BussinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObjects.DTOs
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile() { 
            CreateMap<Course,CourseDTO>();
            CreateMap<CourseDTO, Course>();
            CreateMap<Topics,TopicDTO>();
            CreateMap<TopicDTO,Topics>();
        }
    }
}

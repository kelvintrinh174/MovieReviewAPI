using AutoMapper;
using MovieReviewAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieReviewAPI.DTOModels
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
 
            CreateMap<Movie, MovieDto>();
            CreateMap<MovieComment, MovieDto>();


        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using VidlyApp.Models;
using VidlyApp.Dtos; //importing models and dtos

namespace VidlyApp.App_Start
{
    public class MappingProfile: Profile //deriving it from the namespace 
    {
      public MappingProfile()
        {
            //uses reflection
            //checks properties types and maps them 
            Mapper.CreateMap<Customer, CustomerDto>();
            Mapper.CreateMap<CustomerDto, Customer>().ForMember(c => c.Id, opt => opt.Ignore());
            Mapper.CreateMap<MembershipType, MembershipTypeDto>(); 

            //maps teh properties of the movie model 
            Mapper.CreateMap<Movies, MovieDto>().ForMember(m => m.Id, opt => opt.Ignore());
            Mapper.CreateMap<MovieDto, Movies>();
            
        }
    }
}
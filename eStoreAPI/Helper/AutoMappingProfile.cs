using AutoMapper;
using BusinessObject;
using eStoreAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eStoreAPI.Helper
{
    public class AutoMappingProfile : Profile
    {
        public AutoMappingProfile()
        {
            CreateMap<MemberDTO, Member>().ReverseMap();
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<OrderDTO, Order>().ReverseMap();
            CreateMap<OrderDetailDTO, OrderDetail>().ReverseMap();
            CreateMap<LoginDTO, MemberDTO>().ReverseMap();
            
        }
    }
}

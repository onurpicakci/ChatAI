using AutoMapper;
using ChatAI.Application.Dto;
using ChatAI.Domain.Entity;

namespace ChatAI.Application.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<AddUserDto, User>();
    }
}
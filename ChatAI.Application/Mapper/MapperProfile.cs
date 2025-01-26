using AutoMapper;
using ChatAI.Application.Dto.Chats;
using ChatAI.Application.Dto.User;
using ChatAI.Domain.Entity.Chat;
using ChatAI.Domain.Entity.User;

namespace ChatAI.Application.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<UserDto, User>().ReverseMap();
        CreateMap<AddUserDto, User>();
        
        CreateMap<ChatMessageDto, ChatMessage>();
        
        CreateMap<ChatSessionDto, ChatSession>();
    }
}
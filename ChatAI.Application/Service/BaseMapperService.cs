using AutoMapper;

namespace ChatAI.Application.Service;

public abstract class BaseMapperService
{
    protected readonly IMapper _mapper;

    protected BaseMapperService(IMapper mapper)
    {
        _mapper = mapper;
    }

    protected TDestination Map<TSource, TDestination>(TSource source)
    {
        return _mapper.Map<TDestination>(source);
    }
    
    protected IEnumerable<TDestination> MapList<TSource, TDestination>(IEnumerable<TSource> source)
    {
        return source?.Select(item => _mapper.Map<TDestination>(item));
    }
}
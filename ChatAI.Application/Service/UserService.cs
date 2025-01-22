using AutoMapper;
using ChatAI.Application.Dto;
using ChatAI.Application.Interface;
using ChatAI.Domain.Entity;
using ChatAI.Persistence.Cache;
using ChatAI.Persistence.Interface;

namespace ChatAI.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly RedisCache _redisCache;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, RedisCache redisCache, IMapper mapper)
    {
        _userRepository = userRepository;
        _redisCache = redisCache;
        _mapper = mapper;
    }

    public async Task<UserDto> GetByNameAsync(string name)
    {
        var cachedUser = await _redisCache.GetAsync<User>(name);
        if (cachedUser == null)
        {
            var user = await _userRepository.GetByNameAsync(name);
            await _redisCache.SetAsync(name, user);
            return _mapper.Map<UserDto>(user);
        }
        
        return _mapper.Map<UserDto>(cachedUser);
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var cachedUser = await _redisCache.GetAsync<User>(email);
        if (cachedUser == null)
        {
            var user = await _userRepository.GetByEmailAsync(email);
            await _redisCache.SetAsync(email, user);
            return _mapper.Map<UserDto>(user);
        }
        
        return _mapper.Map<UserDto>(cachedUser);
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var cachedUser = await _redisCache.GetAsync<User>(id.ToString());
        if (cachedUser == null)
        {
            var user = await _userRepository.GetByIdAsync(id);
            await _redisCache.SetAsync(id.ToString(), user);
            return _mapper.Map<UserDto>(user);
        }
        
        return _mapper.Map<UserDto>(cachedUser);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        foreach (var user in users)
        {
            await _redisCache.SetAsync(user.Id.ToString(), user);
        }
        
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task AddAsync(AddUserDto entity)
    {
        await _userRepository.AddAsync(_mapper.Map<User>(entity));
        await _redisCache.SetAsync(entity.Id.ToString(), entity);
    }

    public async Task UpdateAsync(UserDto entity)
    {
        await _userRepository.UpdateAsync(_mapper.Map<User>(entity));
    }

    public async Task DeleteAsync(UserDto entity)
    {
        await _userRepository.DeleteAsync(_mapper.Map<User>(entity));
    }
}
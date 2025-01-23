using AutoMapper;
using ChatAI.Application.Dto.User;
using ChatAI.Application.Helper;
using ChatAI.Application.Interface;
using ChatAI.Domain.Entity.User;
using ChatAI.Persistence.Cache;
using ChatAI.Persistence.Interface;

namespace ChatAI.Application.Service;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly RedisCache _redisCache;
    private readonly JwtHelper _jwtHelper;

    public UserService(IUserRepository userRepository, RedisCache redisCache, IMapper mapper, JwtHelper jwtHelper)
    {
        _userRepository = userRepository;
        _redisCache = redisCache;
        _mapper = mapper;
        _jwtHelper = jwtHelper;
    }

    public async Task<UserDto> GetByNameAsync(string name)
    {
        var cachedUser = await _redisCache.GetAsync<User>($"user:name:{name}");
        if (cachedUser != null)
        {
            return _mapper.Map<UserDto>(cachedUser);
        }

        var user = await _userRepository.GetByNameAsync(name);
        if (user != null)
        {
            await _redisCache.SetAsync($"user:name:{name}", user);
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var cachedUser = await _redisCache.GetAsync<User>($"user:email:{email}");
        if (cachedUser != null)
        {
            return _mapper.Map<UserDto>(cachedUser);
        }

        var user = await _userRepository.GetByEmailAsync(email);
        if (user != null)
        {
            await _redisCache.SetAsync($"user:email:{email}", user);
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var cachedUser = await _redisCache.GetAsync<User>($"user:id:{id}");
        if (cachedUser != null)
        {
            return _mapper.Map<UserDto>(cachedUser);
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            await _redisCache.SetAsync($"user:id:{id}", user);
        }

        return _mapper.Map<UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task AddAsync(AddUserDto entity)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(entity.Password);

        var userEntity = new User(entity.Name, entity.Email, passwordHash);
        await _userRepository.AddAsync(userEntity);
        
        // Add to Cache
        await _redisCache.SetAsync($"user:id:{userEntity.Id}", userEntity);
        await _redisCache.SetAsync($"user:email:{userEntity.Email}", userEntity);
        await _redisCache.SetAsync($"user:name:{userEntity.Name}", userEntity);
    }

    public async Task UpdateAsync(UserDto entity)
    {
        var userEntity = _mapper.Map<User>(entity);
        await _userRepository.UpdateAsync(userEntity);

        // Update Cache
        await _redisCache.SetAsync($"user:id:{userEntity.Id}", userEntity);
        await _redisCache.SetAsync($"user:email:{userEntity.Email}", userEntity);
        await _redisCache.SetAsync($"user:name:{userEntity.Name}", userEntity);
    }

    public async Task DeleteAsync(UserDto entity)
    {
        var userEntity = _mapper.Map<User>(entity);
        await _userRepository.DeleteAsync(userEntity);

        // Remove cache
        await _redisCache.RemoveAsync($"user:id:{userEntity.Id}");
        await _redisCache.RemoveAsync($"user:email:{userEntity.Email}");
        await _redisCache.RemoveAsync($"user:name:{userEntity.Name}");
    }

    public async Task<string> Login(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return null; 
        }

        if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
        {
            return null; 
        }

        return _jwtHelper.GenerateJwtToken(user.Id, user.Email);
    }


}

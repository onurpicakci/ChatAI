using AutoMapper;
using ChatAI.Application.Dto.User;
using ChatAI.Application.Interface;
using ChatAI.Domain.Entity.User;
using ChatAI.Helper.Jwt;
using ChatAI.Helper.Redis;
using ChatAI.Persistence.Abstract;

namespace ChatAI.Application.Service;

public class UserService : BaseMapperService, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly RedisCache _redisCache;
    private readonly JwtHelper _jwtHelper;

    public UserService(IUserRepository userRepository, RedisCache redisCache, IMapper mapper, JwtHelper jwtHelper) : base(mapper)
    {
        _userRepository = userRepository;
        _redisCache = redisCache;
        _jwtHelper = jwtHelper;
    }

    public async Task<UserDto> GetByNameAsync(string name)
    {
        var cachedUser = await _redisCache.GetAsync<User>($"user:name:{name}");
        if (cachedUser != null)
        {
            return Map<User, UserDto>(cachedUser);
        }

        var user = await _userRepository.GetByNameAsync(name);
        if (user != null)
        {
            await _redisCache.SetAsync($"user:name:{name}", user);
        }

        return Map<User, UserDto>(user);
    }

    public async Task<UserDto> GetByEmailAsync(string email)
    {
        var cachedUser = await _redisCache.GetAsync<User>($"user:email:{email}");
        if (cachedUser != null)
        {
            return Map<User, UserDto>(cachedUser);
        }

        var user = await _userRepository.GetByEmailAsync(email);
        if (user != null)
        {
            await _redisCache.SetAsync($"user:email:{email}", user);
        }

        return Map<User, UserDto>(user);
    }

    public async Task<UserDto> GetByIdAsync(Guid id)
    {
        var cachedUser = await _redisCache.GetAsync<User>($"user:id:{id}");
        if (cachedUser != null)
        {
            return Map<User, UserDto>(cachedUser);
        }

        var user = await _userRepository.GetByIdAsync(id);
        if (user != null)
        {
            await _redisCache.SetAsync($"user:id:{id}", user);
        }

        return Map<User, UserDto>(user);
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return MapList<User, UserDto>(users);
    }

    public async Task AddAsync(AddUserDto entity)
    {
        var passwordHash = BCrypt.Net.BCrypt.HashPassword(entity.Password);

        var userEntity = Map<AddUserDto, User>(entity);
        await _userRepository.AddAsync(userEntity);
        
        // Add to Cache
        await _redisCache.SetAsync($"user:id:{userEntity.Id}", userEntity);
        await _redisCache.SetAsync($"user:email:{userEntity.Email}", userEntity);
        await _redisCache.SetAsync($"user:name:{userEntity.Name}", userEntity);
    }

    public async Task UpdateAsync(UserDto entity)
    {
        var userEntity = Map<UserDto, User>(entity);
        await _userRepository.UpdateAsync(userEntity);

        // Update Cache
        await _redisCache.SetAsync($"user:id:{userEntity.Id}", userEntity);
        await _redisCache.SetAsync($"user:email:{userEntity.Email}", userEntity);
        await _redisCache.SetAsync($"user:name:{userEntity.Name}", userEntity);
    }

    public async Task DeleteAsync(UserDto entity)
    {
        var userEntity = Map<UserDto, User>(entity);
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

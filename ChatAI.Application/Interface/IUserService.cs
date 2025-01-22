using ChatAI.Application.Dto;
using ChatAI.Domain.Entity;

namespace ChatAI.Application.Interface;

public interface IUserService
{
    Task<UserDto> GetByNameAsync(string name);
    Task<UserDto> GetByEmailAsync(string email);
    Task<UserDto> GetByIdAsync(Guid id);
    Task<IEnumerable<UserDto>> GetAllAsync();
    Task AddAsync(AddUserDto entity);
    Task UpdateAsync(UserDto entity);
    Task DeleteAsync(UserDto entity);
}
using AutoMapper;
using ChatAI.Application.Dto;
using ChatAI.Application.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatAI.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }
        
        [HttpGet("{name}")]
        public async Task<IActionResult> GetByNameAsync(string name)
        {
            var user = await _userService.GetByNameAsync(name);
            if(user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }
        
        [HttpGet("{email}")]
        public async Task<IActionResult> GetByEmailAsync(string email)
        {
            var user = await _userService.GetByEmailAsync(email);
            if(user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(Guid id)
        {
            var user = await _userService.GetByIdAsync(id);
            if(user == null)
            {
                return NotFound();
            }
            
            return Ok(user);
        }
        
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        
        [HttpPost]
        public async Task<IActionResult> AddAsync(AddUserDto user)
        {
            await _userService.AddAsync(user);
            return Ok();
        }
        
        [HttpPut]
        public async Task<IActionResult> UpdateAsync(UserDto user)
        {
            await _userService.UpdateAsync(user);
            return Ok();
        }
    }
}

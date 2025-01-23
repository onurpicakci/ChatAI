using AutoMapper;
using ChatAI.Application.Dto.User;
using ChatAI.Application.Interface;
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
        
        [HttpPost("login")]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var token = await _userService.Login(email, password);
                if (string.IsNullOrEmpty(token))
                {
                    return StatusCode(StatusCodes.Status401Unauthorized, "Invalid email or password");
                }
                return Ok(token);
            }
            catch (UnauthorizedAccessException e)
            {
                return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
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

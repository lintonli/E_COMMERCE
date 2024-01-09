using AUTHSERVICE.Models.Dtos;
using AUTHSERVICE.Service.IService;
using AutoMapper;
using Azure;
using EcommMessageBus;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AUTHSERVICE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUser _userService;
        private readonly ResponseDto _responseDto;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public UserController(IUser userService, IConfiguration configuration,IMapper mapper)
        {
            _userService = userService;
            _responseDto = new ResponseDto();
            _configuration = configuration;
            _mapper = mapper;
        }
        [HttpPost]
        public async Task<ActionResult<ResponseDto>> RegisterUser(RegisterUserDto registerUserDto)
        {
            var res = await _userService.RegisterUser(registerUserDto);
            if (string.IsNullOrWhiteSpace(res))
            {
                _responseDto.Result = "User registered successfully";

                var message = new UserMessageDto()
                {
                    Name = registerUserDto.Name,
                    Email = registerUserDto.Email,
                };
                var mb = new MessageBus();
                await mb.PublishMessage(message, _configuration.GetValue<string>("ServiceBus:Register"));
                return Created("", _responseDto);
            }
            _responseDto.ErrorMessage = res;
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
        [HttpPost("login")]

        public async Task<ActionResult<ResponseDto>> LoginUser(LoginRequestDto loginRequestDto)
        {
            var res = await _userService.Login(loginRequestDto);
            if (res.user != null)
            {
                _responseDto.Result = res;
                return Created("", _responseDto);
            }
            _responseDto.ErrorMessage = "Invalid Credentials";
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
        [HttpPost("AssignRole")]
        public async Task<ActionResult<ResponseDto>> AssignRole(AssignRoleDto role)
        {
            var res = await _userService.AssignUserRoles(role.Email, role.Role);
            if (res)
            {
                _responseDto.Result = res;
                return Ok(_responseDto);
            }
            _responseDto.ErrorMessage = "Error Occured";
            _responseDto.Result = res;
            _responseDto.IsSuccess = false;
            return BadRequest(_responseDto);
        }
        [HttpGet("{Id}")]
        public async Task<ActionResult<ResponseDto>> GetUser(string Id)
        {
            var res = await _userService.GetUserById(Id);
            var user = _mapper.Map<UserDto>(res);

            if (res != null)
            {
                //this was success
                _responseDto.Result = user;
                return Ok(_responseDto);
            }

            _responseDto.ErrorMessage = "User Not found ";
            _responseDto.IsSuccess = false;
            return NotFound(_responseDto);
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserManager.Data;
using UserManager.Entity;
using UserManager.Models.Dto;
using UserManager.repository.IRepository;

namespace UserManager.Controllers
{
    [Route("api/FuckShitTOTAMMATLON")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _db;
        private readonly IMapper _mapper;
        public UserController(IUserRepository fuckthisuser,IMapper mapper) {
            _db = fuckthisuser; // injection
            _mapper = mapper;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> getFuckingApi(int id) {
            var user = await _db.GetAsync(u=>u.Id == id);
            if (user == null) {
                return NotFound();
            }
            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }
        [HttpPost]
        public async Task<ActionResult<UserDto>> createfuckingUser([FromBody] CreateUserDto createUserDto) {
            if (createUserDto == null)
            {
                return BadRequest();
            }
            else {
                Users newUser = _mapper.Map<Users>(createUserDto);
                
                var userDb = await _db.CreateAsync(newUser);
                var shit = _mapper.Map<UserDto>(userDb);
                return CreatedAtAction(nameof(getFuckingApi), new { id = userDb.Id }, shit);

            }

        }
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> getFuckingList() {
            var user = await _db.GetAllAsync();

            return Ok(_mapper.Map<List<UserDto>>(user));
        }
        
 
    }
}

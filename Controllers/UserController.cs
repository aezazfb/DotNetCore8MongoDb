using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using System;
using testProjectApis.Models;
using testProjectApis.services;

namespace testProjectApis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMongoCollection<User>? _users;
        private readonly TokenService _tokenService;

        public UserController(MongoDbService mongoDbService, TokenService tokenService)
        {
            _users = mongoDbService.Database?.GetCollection<User>("users");
            _tokenService = tokenService;
        }

        [HttpPost("login")]
        public async Task<ActionResult> UserLogin([FromBody] RequestLogin requestLogin)
        {
            if (requestLogin == null) { return NotFound("User Not found"); }

            //Uncomment for non static login

            //var filter = Builders<User>.Filter.Eq(i => i.Email, requestLogin.Email);
            //var userr = _users.Find(filter).FirstOrDefault();
            //if (userr is null)
            //{
            //    return NotFound("No user found!");
            //}
            //if (userr.Password != requestLogin.Password)
            //{
            //    return Unauthorized("Invalid Credentials");
            //}

            User userr = new User() 
            {
                Email = requestLogin.Email,
                FirstName = "Az",
                LastName = "fb",
                Password = requestLogin.Password,
                Role = "player",
                UserId = "2312312"
            };

            var tokenn = _tokenService.CreateToken(userr);
            return Ok(tokenn);
        }

        //[HttpPost("register")]
        //public async Task<IActionResult> Register([FromBody] RequestRegistration request)
        //{
        //    var salt = CryptoHelper.GenerateSalt();
        //    //qvar saltedPassword = request.Password + salt;

        //    var user = new User
        //    {
        //        FirstName = request.Firstname,
        //        LastName = request.Lastname,
        //        Email = request.Email,
        //        Password = request.Password,    // Null is because the user is not created yet, normally this is where the user object is.
        //        Role = Enums.UserRoles.User.ToString()
        //    };

        //    await _userService.CreateUser(user);
        //    var token = _tokenService.CreateToken(user);

        //    return Ok(new AuthResponse { Token = token });
        //    return Ok(0);
        //}
        [HttpGet]
        public async Task<IEnumerable<User>> GetAll() 
        {
            return await _users.Find(FilterDefinition<User>.Empty).ToListAsync();
        }
        [Authorize(Roles ="Player")]
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetbyId(string id)
        {
            var filter = Builders<User>.Filter.Eq(i => i.UserId, id);
            var userr = _users.Find(filter).FirstOrDefault();
            return userr is null ? NotFound() : Ok(userr);

        }
        [HttpPost("Register")]
        public async Task<ActionResult> SaveUser(User user)
        {
            await _users.InsertOneAsync(user);
            var token = _tokenService.CreateToken(user);

            //return Ok(new { Token = token });
            return CreatedAtAction(nameof(GetbyId), new {id = user.UserId}, user);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateUser(User user)
        {
            var filter = Builders<User>.Filter.Eq(i => i.UserId, user.UserId);
            //var theupdate = Builders<User>.Update
            //    .Set(x => x.FirstName, user.FirstName)
            //    .Set(x => x.LastName, user.LastName)
            //    .Set(x => x.Email, user.Email);
            //await _users.UpdateOneAsync(filter, theupdate);

            await _users.ReplaceOneAsync(filter, user);
            return Ok();
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteUser(string id)
        {
            var filter = Builders<User>.Filter.Eq(i => i.UserId, id);
            await _users.DeleteOneAsync(filter);
            return Ok();
        }
    }
}

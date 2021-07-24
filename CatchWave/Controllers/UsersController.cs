using System;
using BLL;
using BLL.Models;
using Microsoft.AspNetCore.Mvc;

namespace CatchWave.Controllers
{
    [ApiController]
    [Route("Users")]
    public class UsersController : ControllerBase
    {

        [HttpPut("token")]
        public IActionResult GetToken(User user)
        {
            try
            {
                user = UsersManageService.Instance.CreateUser(user);
                return Ok(new 
                {
                    token = AuthTools.CreateToken(user.Id, user.Username)
                });
            }
            catch (Exception e)
            {
                return StatusCode(500, new {Message = "ERROR occured."});
            }
        }
    }
}
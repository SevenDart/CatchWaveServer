using System;
using System.Collections.Generic;
using System.Security.Claims;
using BLL;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CatchWave.Controllers
{
    [ApiController]
    [Route("rooms")]
    public class RoomsController: ControllerBase
    {
        [HttpGet("{roomId}")]
        public IActionResult GetRoom(int roomId)
        {
            try
            {
                return Ok(RoomsManageService.Instance.GetRoom(roomId));
            }
            catch (Exception e)
            {
                return StatusCode(500, new {Message = "There is no such room!"});
            }
        }
        
        [HttpPut("{roomId}")]
        [Authorize]
        public IActionResult EnterRoom(int roomId)
        {
            try
            {
                var user = GetUserFromIdentity();
                RoomsManageService.Instance.EnterRoom(user, roomId);
                return Ok();
            }
            catch (KeyNotFoundException e)
            {
                return StatusCode(404, new {e.Message});
            }
            catch (Exception e)
            {
                return StatusCode(500, new {Message = "ERROR occured."});
            }
        }
        
        [HttpPost]
        [Authorize]
        public IActionResult CreateRoom(Room room)
        {
            try
            {
                var user = GetUserFromIdentity();
                return Ok(RoomsManageService.Instance.CreateRoom(user, room.Name));
            }
            catch (Exception e)
            {
                return StatusCode(500, new {Message = "ERROR occured."});
            }
        }



        private User GetUserFromIdentity()
        {
            var user = new User()
            {
                Id = Int32.Parse(User.FindFirst(ClaimTypes.Sid).Value),
                Username = User.Identity.Name
            };
            return user;
        }
    }
}
using System;
using System.Threading.Tasks;
using CatchWave.Hubs.HubInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace CatchWave.Hubs
{
    [Authorize]
    public class GameHub : Hub<IGameClient>
    {
        public void EnterGame()
        {
            Console.Write("Work!");
            Console.Write(Context.User.FindFirst("Username"));
        }
    }
}
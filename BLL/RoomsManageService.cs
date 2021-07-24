using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BLL.Models;

namespace BLL
{
    public class RoomsManageService
    {
        private static readonly Mutex Mutex = new Mutex();
        private static RoomsManageService _instance;
        public static RoomsManageService Instance
        {
            get
            {
                if (_instance == null)
                {
                    Mutex.WaitOne();
                    _instance ??= new RoomsManageService();
                    Mutex.ReleaseMutex();
                }
                return _instance;
            }
        }

        private List<Room> Rooms { get; set; } = new List<Room>();

        public Room CreateRoom(User user, string name)
        {
            var room = new Room()
            {
                Id = GetRandomRoomId(),
                Name = name,
                Owner = user
            };
            room.Users.Add(user);
            Rooms.Add(room);

            return room;
        }

        public void EnterRoom(User user, int roomId)
        {
            var room = Rooms.SingleOrDefault(r => r.Id == roomId);
            if (room == null)
            {
                throw new KeyNotFoundException("There is no such room.");
            }
            room.Users.Add(user);
        }

        public Room GetRoom(int roomId)
        {
            return Rooms.Single(r => r.Id == roomId);
        }

        private int GetRandomRoomId()
        {
            var random = new Random();
            while (true)
            {
                int id = random.Next(0, Int32.MaxValue);
                if (Rooms.SingleOrDefault(r => r.Id == id) == null)
                {
                    return id;
                }
            }
        }
    }
}
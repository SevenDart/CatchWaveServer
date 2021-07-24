using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BLL.Models;

namespace BLL
{
    public class UsersManageService
    {
        private static readonly Mutex Mutex = new Mutex();
        private static UsersManageService _instance;
        public static UsersManageService Instance
        {
            get
            {
                if (_instance == null)
                {
                    Mutex.WaitOne();
                    _instance ??= new UsersManageService();
                    Mutex.ReleaseMutex();
                }
                return _instance;
            }
        }

        public User CreateUser(User user)
        {
            user.Id = GetRandomUserId();
            Users.Add(user);
            return user;
        }
        
        private List<User> Users { get; set; } = new List<User>();
        private int GetRandomUserId()
        {
            var random = new Random();
            while (true)
            {
                int id = random.Next(0, Int32.MaxValue);
                if (Users.SingleOrDefault(r => r.Id == id) == null)
                {
                    return id;
                }
            }
        }
    }
}
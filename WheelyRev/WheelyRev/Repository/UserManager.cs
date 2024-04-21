using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WheelyRev.Models;

namespace WheelyRev.Repository
{
    public class UserManager
    {
        private BaseRepository<Users> _users;


        public UserManager()
        {
            _users = new BaseRepository<Users>();
        }

        public Users GetUserById(int Id)
        {
            return _users.Get(Id);
        }
        public Users GetUserByUserId(String userId)
        {
            return _users._table.Where(m => m.userId == int.Parse(userId)).FirstOrDefault();
        }
        public Users GetUserByUsername(String username)
        {
            return _users._table.Where(m => m.username == username).FirstOrDefault();
        }
        public Users GetUserByEmail(String email)
        {
            return _users._table.Where(m => m.email == email).FirstOrDefault();
        }
    }
}
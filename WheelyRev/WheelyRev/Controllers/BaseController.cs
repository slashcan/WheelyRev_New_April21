using ShopApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheelyRev.Models;
using WheelyRev.Repository;

namespace WheelyRev.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        public WheelyRevEntities _db;  //Connection
        public BaseRepository<Users> _table;   //Users table
        public BaseRepository<UserRoles> _tableUR; //User Role table
        public BaseRepository<Shops> _tableShop; //Shop table
        public BaseRepository<Products> _tableProduct;
        public BaseRepository<Transaction> _tableTransaction;
        public BaseRepository<Cart> _tableCart;
        public BaseRepository<Status> _tableStatus;

        public UserManager _userManager;

        public String Username { get { return User.Identity.Name; } }
        public int UserId { get { return _userManager.GetUserByUsername(Username).userId; } }
        public BaseController()
        {
            _db = new WheelyRevEntities();
            _table = new BaseRepository<Users>();
            _tableUR = new BaseRepository<UserRoles>();
            _tableShop = new BaseRepository<Shops>();
            _tableProduct = new BaseRepository<Products>();
            _tableTransaction = new BaseRepository<Transaction>();
            _tableCart = new BaseRepository<Cart>();
            _tableStatus = new BaseRepository<Status>();

            _userManager = new UserManager();
        }

        public void IsUserLoggedSession()
        {
            UserLogged userLogged = new UserLogged();
            if (User != null)
            {
                if (User.Identity.IsAuthenticated)
                {
                    userLogged.UserAccount = _userManager.GetUserByUsername(User.Identity.Name);
                }
            }
            Session["User"] = userLogged;
        }
    }
}
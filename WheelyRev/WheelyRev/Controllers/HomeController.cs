using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WheelyRev.Models;

namespace WheelyRev.Controllers
{
    [Authorize(Roles = "Admin,Customer,Store owner")]
    public class HomeController : BaseController
    {
        // GET: Home
        public ActionResult Index()
        {
            var GetProducts = _tableProduct.GetAll().Where(m => m.productQty != 0).ToList();
            return View(GetProducts);
        }
        [AllowAnonymous]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Login(Users u)
        {
            var user = _db.Users.Where(m => m.username == u.username && m.password == u.password).FirstOrDefault();

            if (user != null)//Check if the user is nag exist or same og value
            {
                //if true
                FormsAuthentication.SetAuthCookie(u.username, false); //Set Cookie
                Session["UserId"] = user.userId; //Store the userId to Session para magamit nato sa lain lain nga Action

                var user_role = _db.vw_UserRoles.Where(m => m.username == u.username).Select(m => m.roleId).FirstOrDefault();
                if (user_role == 1)
                {
                    return RedirectToAction("AdminHome");
                }
                else
                {
                    return RedirectToAction("Index");
                }
            }
            ViewData["ErrorMessage"] = "Incorrect username or password.";
            return View(u);
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut(); //Para mawagtang ang authentication nga gikan ni login.
            return RedirectToAction("Login");
        }
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Register(Users u)
        {
            _table.Create(u);
            setDefaultRole(u.userId);
            TempData["msg"] = $"Register successfully!";
            return View();
        }
        private void setDefaultRole(int id)
        {
            UserRoles Default_Role = new UserRoles
            {
                roleId = 3, //Default role 3 = Customer
                userId = id
            };
            _tableUR.Create(Default_Role);
            Session["UserRole_ID"] = Default_Role.UserRoles_ID;
        }

        [Authorize(Roles = "Admin,Customer,Store owner")]
        [HttpPost]
        public ActionResult ViewProducts()
        {
            return View(_db.Products.ToList());
        }

        public JsonResult GetCartCount()
        {
            IsUserLoggedSession();
            //_db.Cart.Where(m => m.userId == UserId).Count()
            var res = new { count = _db.vw_CartItem.Where(m => m.userId == UserId).Count() };
            Console.Write(res);
            return Json(res, JsonRequestBehavior.AllowGet);
        }

        //ADMIN SIDE

        [Authorize(Roles = "Admin")]
        public ActionResult AdminHome()
        {
            return View();
        }

        // MANAGE USER
        [Authorize(Roles = "Admin")]
        public ActionResult AdminManageUser()
        {
            return View(_table.GetAll());
        }

        public ActionResult AdminCreate()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AdminCreate(Users u)
        {
            _table.Create(u);
            TempData["Msg"] = $"User {u.username} added!";

            return RedirectToAction("AdminManageUser");
        }

        public ActionResult AdminDetails(int id)
        {
            return View(_table.Get(id));
        }

        public ActionResult AdminEdit(int id)
        {
            return View(_table.Get(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AdminEdit(Users u)
        {
            _table.Update(u.userId, u);
            TempData["Msg"] = $"User {u.username} updated!";

            return RedirectToAction("AdminManageUser");
        }

        public ActionResult AdminDelete(int id)
        {
            _table.Delete(id);
            TempData["Msg"] = $"User deleted!";
            return RedirectToAction("AdminManageUser");
        }

        //MANAGE SHOP
        [Authorize(Roles = "Admin")]
        public ActionResult AdminManageShop()
        {
            return View(_tableShop.GetAll());
        }

        public ActionResult AdminShopDetails(int id)
        {
            return View(_tableShop.Get(id));
        }

        public ActionResult AdminShopEdit(int id)
        {
            return View(_tableShop.Get(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AdminShopEdit(Shops u)
        {
            _tableShop.Update(u.userId, u);
            TempData["Msg"] = $"User {u.shopId} updated!";

            return RedirectToAction("AdminManageUser");
        }

        public ActionResult AdminShopDelete(int id)
        {
            _tableShop.Delete(id);
            TempData["Msg"] = $"Shop deleted!";
            return View();
        }
    }
}
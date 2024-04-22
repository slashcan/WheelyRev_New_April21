using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheelyRev.Models;

namespace WheelyRev.Controllers
{
    public class CartController : BaseController
    {
        // GET: Cart
        public ActionResult Cart()
        {
            IsUserLoggedSession();

            var cartItem = _db.vw_CartItem.Where(m => m.userId == UserId);
            return View(cartItem);
        }
        [Authorize(Roles = "Admin,Customer,Store owner")]
        public ActionResult AddToCart(int quantity, int? productID, int? shopId)
        {
            try
            {
                if(productID != null && shopId != null)
                {
                    Cart cart = new Cart
                    {
                        username = Username,
                        productID = productID,
                        shopId = shopId,
                        userId = UserId,
                        quantity = quantity,
                        statusId = 2
                    };

                    _tableCart.Create(cart);

                    return Json(new { success = true, message = "Product added to cart successfully." });
                }
                else
                {
                    return Json(new { success = false, message = "Error adding product to cart"});
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Error adding product to cart: " + ex.Message });
            }
        }
        public ActionResult CheckOutAjax(List<vw_CartItem> items, string contactNumber, string address, decimal cash)
        {
            try
            {
                IsUserLoggedSession();

                if (items != null && items.Any())
                {
                    foreach (var item in items)
                    {
                        //var productQuantity = _db.sp_MergeQuantity(UserId).FirstOrDefault();
                        decimal totalSum = (decimal)item.totalQuantity * item.productPrice;

                        Transaction transact = new Transaction
                        {
                            productID = item.productID,
                            shopID = item.shopId,
                            userID = UserId,
                            shopName = item.shopName,
                            productName = item.productName,
                            productQuantity = item.totalQuantity,
                            productPrice = item.productPrice,
                            customerContact = contactNumber,
                            customerAddress = address,
                            Total = totalSum,
                            customerCash = cash
                        };

                        _tableTransaction.Create(transact);
                        _db.sp_UpdateCart(UserId);
                    }

                    return Json(new { success = true, message = "Thank you!" });
                }
                else
                {
                    return Json(new { error = true, message = "No items to checkout." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { error = true, message = "Error: " + ex.Message });
            }
        }
    }
}
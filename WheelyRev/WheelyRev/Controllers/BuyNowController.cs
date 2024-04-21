using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheelyRev.Models;

namespace WheelyRev.Controllers
{
    [Authorize(Roles = "Admin,Customer,Store owner")]
    public class BuyNowController : BaseController
    {
        // GET: BuyNow
        public ActionResult BuyNow(int? productID, int? shopId)
        {
            if (productID != null && shopId != null)
            {
                var productDetails = _db.sp_BuyNow(productID).FirstOrDefault(); // Check if the product and shop actually exist and belong to the current user
                if (productDetails != null && productDetails.shopId == shopId)
                {
                    Session["productId"] = productID;
                    Session["shopId"] = shopId;

                    return View(productDetails);
                }
                else
                {
                    return RedirectToAction("Index", "Error"); // Example redirection to an error page
                }
            }
            else
            {
                return RedirectToAction("Index"); // if tang tangan og ID sa user
            }
        }


        [Authorize(Roles = "Admin,Customer,Store owner")]
        public ActionResult Purchase(int productID, int shopId, string contactNumber, string address, int quantity, int cash)
        {
            try
            {
                var productName = _db.Products.Where(m => m.productID == productID && m.shopId == shopId).Select(m => m.productName).FirstOrDefault();
                var productPrice = _db.Products.Where(m => m.productID == productID && m.shopId == shopId).Select(m => m.productPrice).FirstOrDefault();
                var shopName = _db.Shops.Where(m => m.shopId == shopId).Select(m => m.shopName).FirstOrDefault();
                
                Transaction transaction = new Transaction
                {
                    productID = productID,
                    userID = UserId,
                    shopID = shopId,
                    shopName = shopName,
                    productName = productName,
                    productPrice = (int)productPrice,
                    customerContact = contactNumber,
                    customerAddress = address,
                    customerCash = cash,
                    productQuantity = quantity,
                    Total = quantity * (int)productPrice,
                    datePurchase = DateTime.Now
                };

                _tableTransaction.Create(transaction);
                UpdateQty(productID, quantity);

                return Json(new { success = true, message = "Thank you!" }); // Return success status
            }
            catch (Exception ex)
            {
                // Log the exception for debugging purposes
                // Log.Error("Error occurred during transaction: " + ex.Message);
                return Json(new { success = false, message = "Error: Please contact the administrator for the help.\n" + ex.Message });
            }
        }

        private void UpdateQty(int productId, int quantity)
        {
            _db.sp_UpdateQty(quantity, productId);
        }
    }
}
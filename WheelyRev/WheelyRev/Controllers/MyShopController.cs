using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WheelyRev.Models;

namespace WheelyRev.Controllers
{
    [Authorize(Roles = "Admin,Customer,Store owner")]
    public class MyShopController : BaseController
    {
        // GET: MyShop
        public ActionResult MyShop()
        {
            return View();
        }

        [Authorize(Roles = "Store owner")]
        [HttpPost]
        public ActionResult MyShop(HttpPostedFileBase file, Products product)
        {
            int userId = (int)TempData["UserId"];
            var shopID = _db.Shops.Where(m => m.userId == userId).Select(m => m.shopId).FirstOrDefault();

            if (file != null && file.ContentLength > 0)
            {
                try
                {
                    // Check file type (example: allow only image files)
                    string[] allowedFileTypes = { "image/jpeg", "image/png", "image/gif", "image/jpg", "image/jfif" };
                    if (!allowedFileTypes.Contains(file.ContentType))
                    {
                        TempData["msg"] = "Invalid file type. Please upload a valid image file.";
                        return View();
                    }

                    // Resize image to 750x750 pixels
                    using (var image = Image.FromStream(file.InputStream))
                    {
                        var resizedImage = new Bitmap(image, new Size(750, 750));

                        // Get the path to the "Images" folder
                        string imagesFolderPath = Server.MapPath("~/Images");

                        // Create the folder if it doesn't exist
                        if (!Directory.Exists(imagesFolderPath))
                        {
                            Directory.CreateDirectory(imagesFolderPath);
                        }

                        // Combine the folder path and the filename
                        string fileName = Path.GetFileName(file.FileName);
                        string filePath = Path.Combine(imagesFolderPath, fileName);

                        // Handle file name conflicts
                        if (System.IO.File.Exists(filePath))
                        {
                            fileName = Guid.NewGuid().ToString() + "_" + fileName;
                            filePath = Path.Combine(imagesFolderPath, fileName);
                        }

                        // Save the resized image to the specified path
                        resizedImage.Save(filePath);

                        var imageModel = new Products
                        {
                            ImagePath = fileName,
                            productName = product.productName,
                            productDescription = product.productDescription,
                            productPrice = product.productPrice,
                            productQty = product.productQty,
                            shopId = shopID
                        };

                        _db.Products.Add(imageModel);
                        _db.SaveChanges();

                        TempData["msg"] = "File uploaded successfully";
                    }
                }
                catch (Exception ex)
                {
                    TempData["msg"] = "ERROR: " + ex.Message.ToString();
                }
            }
            else
            {
                TempData["msg"] = "You have not specified a file.";
            }
            return RedirectToAction("MyShop");
        }
    }
}

namespace AfricanShopLviv.PL.Controllers
{
    using System;
    using System.IO;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using Microsoft.AspNet.Identity;
    using AfricanShopLviv.PL.Models;
    using AfricanShopLviv.BLL.Services;
    using Microsoft.AspNet.Identity.Owin;
    using AfricanShopLviv.BLL.DTO;

    public enum ViewPartType
    {
        UsersInfoPart,
        AllOrdersPart,
        NewPersonsPart,
        MessagesPart,
        ProductsPart,
        AddProductPart,
        EditProductPart,
        EditUserPart,
        MyDataPart,
        NoN
    };

    public class AdminController : Controller
    {
        private static ApplicationUser CurrentAdmin { get; set; }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ServiceAfricanShop db;
        private static bool isError = false, _isPartView = false;
        private static ProductDto SelectedProduct { get; set; }
        private static string path;
        public static int CategoryId { get; set; }
        private static string PhotoPath { get; set; }


        public AdminController()
        {
            db = new ServiceAfricanShop(Init.ConnectionStr);
        }

        #region SignInManager & UserManager:
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }
        #endregion

        #region Login to Admin:
        [HttpGet]
        public ActionResult Admin()
        {
            ViewBag.IsError = isError;
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Admin(LoginViewModel admin)
        {
            if (!ModelState.IsValid)
            {
                ViewBag.IsError = isError = true;
                if (admin.Email == null && admin.Password == null) ViewBag.Error = "Login and Password Fields have to be Filled out!";
                else ViewBag.Error = "Some of the fields are EMPTY!";
                return View(admin);
            }
            else
            { 
                ApplicationUser signedUser = UserManager.FindByEmail(admin.Email);

                if (signedUser == null) // if login and password doesn't exists
                {
                    ViewBag.Error = "Your Login and/or Password is Incorrect!";
                    ViewBag.IsError = isError = true;
                    return View(admin);
                }

                SignInStatus result = await SignInManager.PasswordSignInAsync(signedUser.Email, admin.Password, admin.RememberMe, shouldLockout: false);

                switch (result)
                {
                    case SignInStatus.Success:
                        switch (signedUser.Role)
                        {
                            case "Admin":
                                CurrentAdmin = signedUser;
                                break;
                            default: break;
                        }
                        return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", new { token = CurrentAdmin.Id });
                    case SignInStatus.Failure:
                    default:
                        ViewBag.Error = "This Email or Password does not exist or the user is not confirmed!";
                        return View(admin);
                }
            }
        }
        #endregion

        #region Main Admin Panel:
        static bool isAddProduct = false;
        public async Task<ActionResult> _829528a_441d_484m_862i_22475963ffdn(
            string token = null, bool isPartView = false, string partViewName = null, string categId = null)
        {
            if (isPartView)
            {
                _isPartView = isPartView;
                { ViewBag.PartialView = partViewName; }
                ViewBag.IsMenuPart = isPartView;
                if (categId != null)
                {
                    isAddProduct = true;
                    var products = await db.ReadProductsAsync();
                    var prodBy = products.Where(p => p.CategoryId == int.Parse(categId)).ToList();
                    CategoryId = int.Parse(categId);
                    { ViewBag.Products = prodBy; }
                    { ViewBag.CategoryId = CategoryId; }
                    { ViewBag.Category = db.ReadCategories().FirstOrDefault(c => c.Id == CategoryId).Name; }
                }
                else
                {
                    ViewBag.Users = UserManager.Users.ToList();
                    ViewBag.CurrentUser = CurrentAdmin;
                }
            }
            if (Guid.TryParse(token, out Guid ui) || CurrentAdmin != null)
            {
                { ViewBag.TotalOrders = await db.ReadOrdersAsync(); }
                { ViewBag.Users = UserManager.Users.ToList(); }
                { ViewBag.AdminName = $"{CurrentAdmin.FirstName} {CurrentAdmin.LastName}"; }
                { ViewBag.Categories = await db.ReadCategoriesAsync(); }
                { ViewBag.IsPartialView = isPartView; }
                { ViewBag.IsStock = new SelectList(new string[] { "In Stock", "Out of Stock"}); }
                return View(UserManager.Users);
            }

            { ViewBag.IsPartialView = isPartView; }
            return RedirectToAction("Admin");
        }

        [HttpPost]
        public async Task<ActionResult> _829528a_441d_484m_862i_22475963ffdn(ProductDto product)
        {
            var button =
                Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First().Remove(0, 1);
            if (button != null)
            {
                var arr = button.Split(new char[] { '-' }); // 0 - action, 1 - Product Id
                switch (arr[0])
                {
                    case "AddProduct":
                        ViewBag.PartialView = ViewPartType.AddProductPart.ToString();
                        ViewBag.IsMenuPart = _isPartView = true;
                        break;
                    case "AddProductToDB":
                        product.Date = DateTime.Now;
                        product.CategoryId = CategoryId;
                        product.Photo = PhotoPath == null ? "" : PhotoPath;
                        await db.Insert(product); // Put the new product to datgabase
                        break;
                    case "Edit":
                        ViewBag.SelectedProduct = SelectedProduct = db.ReadProducts().FirstOrDefault(f => f.Id == int.Parse(arr[1]));
                        ViewBag.PartialView = ViewPartType.EditProductPart.ToString();
                        ViewBag.IsMenuPart =  _isPartView = true;
                        break;
                    case "EditProduct":
                        //product.Id = SelectedProduct.Id;
                        product.Date = DateTime.Now;
                        product.CategoryId = CategoryId;
                        product.Photo = PhotoPath == null && PhotoPath == "" ? SelectedProduct.Photo : PhotoPath;
                        db.Update(product);
                        break;
                    case "Details":
                        break;
                    case "Delete":
                        if (int.TryParse(arr[1], out int id))
                        {
                            var prod = db.ReadProducts().FirstOrDefault(i => i.Id == int.Parse(arr[1]));
                            db.DeleteProduct(int.Parse(arr[1]));
                            try
                            {
                                string path = Path.Combine(Server.MapPath($"~/Images/Products/{prod.Photo}"));
                                System.IO.File.Delete(path);
                            }
                            catch { }
                        }
                        ViewBag.IsMenuPart = _isPartView = true;
                        break;

                }
            }

            { ViewBag.TotalOrders = await db.ReadOrdersAsync(); }
            { ViewBag.Users = UserManager.Users.ToList(); }
            { ViewBag.CategoryId = CategoryId; }
            { ViewBag.Users = UserManager.Users.ToList(); }
            { ViewBag.AdminName = $"{CurrentAdmin.FirstName} {CurrentAdmin.LastName}"; }
            { ViewBag.Categories = await db.ReadCategoriesAsync(); }
            { ViewBag.IsPartialView = _isPartView; }
            return View(UserManager.Users);
        }
        #endregion

        #region Upload product photo[Post]:
        [HttpPost]
        public ActionResult PhotoUpload()
        {
            if (Request.Files.Count > 0)
            {
                try
                {
                    HttpFileCollectionBase files = Request.Files;
                    for (int i = 0; i < files.Count; i++)
                    {
                        HttpPostedFileBase file = files[i];
                        string fname;

                        if (Request.Browser.Browser.ToUpper() == "IE" || Request.Browser.Browser.ToUpper() == "INTERNETEXPLORER")
                        {
                            string[] testfiles = file.FileName.Split(new char[] { '\\' });
                            PhotoPath = fname = testfiles[testfiles.Length - 1];
                        }
                        else fname = PhotoPath = file.FileName;

                        path = Path.Combine(Server.MapPath($"~/Images/Products/{fname}"));
                        file.SaveAs(path);
                    }

                    return Json("Upload!");
                }
                catch (Exception ex)
                {
                    return Json("Error occurred. Error details: " + ex.Message);
                }
            }
            else return Json("U should to upload image!");
        }
        #endregion

        public ActionResult SelectPartView(string type)
        {
            
            return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { isPartView = true });
        }

        #region Auxiliary methods:
        private object GetPartialView(ViewPartType vPart)
        {
            return new object();
        }
        #endregion
    }
}



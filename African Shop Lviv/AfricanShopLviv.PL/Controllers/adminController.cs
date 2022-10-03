namespace AfricanShopLviv.PL.Controllers
{
    #region Namespaces:
    using System;
    using System.IO;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using AfricanShopLviv.BLL.DTO;
    using Microsoft.AspNet.Identity;
    using AfricanShopLviv.PL.Models;
    using AfricanShopLviv.BLL.Services;
    using Microsoft.AspNet.Identity.Owin;
    #endregion

    #region View Parts [enum]:
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
        TotalIncomePart,
        AddCategoryPart,
        CategoryPart,
        EditCategoryPart,
        NoN
    };
    #endregion

    public class AdminController : Controller
    {
        #region Fields:
        private static string ObjectType { get; set; }
        private static ApplicationUser CurrentAdmin { get; set; }
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private readonly ServiceAfricanShop db;
        private static bool isError = false, _isPartView = false;
        private static ProductDto SelectedProduct { get; set; }
        private static CategoryDto SelectedCategory { get; set; }
        private static string path;
        public static int CategoryId { get; set; }
        private static string PhotoPath { get; set; }
        #endregion

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
                            default:
                                return View();
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
        static bool isAddProduct = false, isNewMsg = false, isShowAllMsg = false;
        public async Task<ActionResult> _829528a_441d_484m_862i_22475963ffdn(
            string token = null, bool isPartView = false, string partViewName = null,
            bool isShowMsg = false, string categId = null)
        {
            if (isShowMsg) isShowAllMsg = isShowMsg;
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
                var messages = await db.ReadMessagesAsync();
                if (messages.Count != 0)
                    isNewMsg = true;
                else isNewMsg = false;
                { ViewBag.IsShownAllMsg = isShowAllMsg; }
                { ViewBag.IsNewMsg = isNewMsg; }
                { ViewBag.Messages = messages; }
                { ViewBag.Orders = await db.ReadOrdersAsync(); }
                { ViewBag.Users = UserManager.Users.ToList().OrderByDescending(d => d.DateRegister).ToList(); }
                try
                {
                    { ViewBag.AdminName = $"{CurrentAdmin.FirstName} {CurrentAdmin.LastName}"; }
                }
                catch { return RedirectToAction("Admin", "Admin"); }
                { ViewBag.Categories = await db.ReadCategoriesAsync(); }
                { ViewBag.IsPartialView = isPartView; }
                { ViewBag.IsStock = new SelectList(new string[] { "In Stock", "Out of Stock" }); }
                return View(UserManager.Users);
            }

            { ViewBag.IsShownAllMsg = isShowAllMsg; }
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
                        ObjectType = "Product";
                        ViewBag.PartialView = ViewPartType.AddProductPart.ToString();
                        ViewBag.IsMenuPart = _isPartView = true;
                        break;
                    case "AddCategory":
                        ObjectType = "Category";
                        ViewBag.PartialView = ViewPartType.AddCategoryPart.ToString();
                        ViewBag.IsMenuPart = _isPartView = true;
                        break;
                    case "AddCategoryToDB":
                        var photo = PhotoPath ==
                           null || PhotoPath == "" ? "" : PhotoPath;
                        var tag = string.Join("", product.Name.Split(default(string[]), StringSplitOptions.RemoveEmptyEntries));
                        var categ = new CategoryDto
                        {
                            Name = product.Name,
                            Photo = photo,
                            TagName = tag
                        };
                        db.Insert(categ); // Put the new category to datgabase
                        return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { isPartView = true, partViewName = "CategoryPart" });
                    case "AddProductToDB":
                        product.Date = DateTime.Now;
                        product.CategoryId = CategoryId;
                        product.Photo = PhotoPath == null ? "" : PhotoPath;
                        await db.Insert(product); // Put the new product to datgabase
                        return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { isPartView = true, partViewName = "ProductsPart", categId = CategoryId, });
                    case "Edit":
                        ObjectType = "Product";
                        ViewBag.SelectedProduct = SelectedProduct = db.ReadProducts().FirstOrDefault(f => f.Id == int.Parse(arr[1]));
                        ViewBag.PartialView = ViewPartType.EditProductPart.ToString();
                        ViewBag.IsMenuPart = _isPartView = true;
                        break;
                    case "EditCateg":
                        ObjectType = "Category";
                        ViewBag.SelectedCategory
                            = SelectedCategory = db.ReadCategories().FirstOrDefault(f => f.Id == int.Parse(arr[1]));
                        ViewBag.PartialView = ViewPartType.EditCategoryPart.ToString();
                        ViewBag.IsMenuPart = _isPartView = true;
                        break;
                    case "EditCategory":
                        var phto = PhotoPath == 
                            null || PhotoPath == "" ? SelectedCategory.Photo : PhotoPath;
                        var c = new CategoryDto
                        {
                            Id = SelectedCategory.Id,
                            Name = product.Name,
                            Photo = phto,
                            TagName = SelectedCategory.TagName
                        };
                        db.Update(c);
                        return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { isPartView = true, partViewName = "CategoryPart"});
                    case "EditProduct":
                        product.Id = SelectedProduct.Id;
                        product.Date = DateTime.Now;
                        product.CategoryId = CategoryId;
                        product.Photo = PhotoPath == null || PhotoPath == "" ? SelectedProduct.Photo : PhotoPath;
                        db.Update(product);
                        return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { isPartView = true, partViewName = "ProductsPart", categId = CategoryId });
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
                        { ViewBag.IsShownAllMsg = isShowAllMsg; }
                        ViewBag.IsMenuPart = _isPartView = true;
                        return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { isPartView = true, partViewName = "ProductsPart", categId = CategoryId, });
                    case "DeleteCateg":
                        if (int.TryParse(arr[1], out int Id))
                        {
                            var categ1 = db.ReadCategories().FirstOrDefault(i => i.Id == int.Parse(arr[1]));
                            try
                            {
                                db.DeleteCategory(int.Parse(arr[1]));     
                                string path = Path.Combine(Server.MapPath($"~/Images/Categories/{categ1.Photo}"));
                                System.IO.File.Delete(path);
                            }
                            catch { }
                        }
                        { ViewBag.IsShownAllMsg = isShowAllMsg; }
                        ViewBag.IsMenuPart = _isPartView = true;
                        return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { isPartView = true, partViewName = "CategoryPart" });
                }
            }
            { ViewBag.IsShownAllMsg = isShowAllMsg; }
            { ViewBag.IsNewMsg = isNewMsg; }
            { ViewBag.Messages = await db.ReadMessagesAsync(); }
            { ViewBag.Orders = await db.ReadOrdersAsync(); }
            { ViewBag.Users = UserManager.Users.ToList(); }
            { ViewBag.CategoryId = CategoryId; }
            { ViewBag.Users = UserManager.Users.ToList(); }
            { ViewBag.AdminName = $"{CurrentAdmin.FirstName} {CurrentAdmin.LastName}"; }
            { ViewBag.Categories = await db.ReadCategoriesAsync(); }
            { ViewBag.IsPartialView = _isPartView; }
            return View(UserManager.Users);
        }
        #endregion

        [HttpPost]
        public async Task<ActionResult> OnReadMessage(int? msgId)
        {
            try
            {
                var msgs = await db.ReadMessagesAsync();
                var found = msgs.FirstOrDefault(m => m.Id == msgId);
                found.IsReviwed = true;
                var resultMsg = db.UpdateADO(found);
            }
            catch { }
            return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn");
        }

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
                        path = ObjectType == "Product" ?
                            Path.Combine(Server.MapPath($"~/Images/Products/{fname}")) :
                            Path.Combine(Server.MapPath($"~/Images/Categories/{fname}"));
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

        #region Partial Views Selector:
        public ActionResult SelectPartView(string type)
        {

            return RedirectToAction("_829528a_441d_484m_862i_22475963ffdn", "Admin", new { partViewName = type, isPartView = true });
        }
        #endregion

        #region Auxiliary methods:
        private object GetPartialView(ViewPartType vPart)
        {
            return new object();
        }
        #endregion
    }
}



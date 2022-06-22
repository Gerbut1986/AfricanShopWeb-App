namespace AfricanShopLviv.PL.Controllers
{
    using System;
    using System.Linq;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using AfricanShopLviv.BLL.DTO;
    using AfricanShopLviv.PL.Models;
    using System.Collections.Generic;
    using AfricanShopLviv.BLL.Services;

    public class HomeController : Controller
    {
        #region Fields:       
        private readonly ServiceAfricanShop db;
        private Guid AccessToken { get; set; }
        private ApplicationUser CurrentLocalUser;
        private static bool isTopMenu = false, isCategoryPressed = false;
        #endregion

        #region Constructor:
        public HomeController()
        {
            db = new ServiceAfricanShop(Init.ConnectionStr);
        }
        #endregion

        #region Interrupt when we try to enter via 'url-route'(not usage yet):
        public object CheckAlreadyUser()
        {
            try
            {
                CurrentLocalUser = GetCurrentUser();
                return AccessToken = Guid.Parse(CurrentLocalUser.Id);
            }
            catch
            {
                string unAuthMsg = "First you should to SignIn or go to Register Form!";
                return RedirectToAction($"../Account/Login/{unAuthMsg}");
            }
        }

        public ApplicationUser GetCurrentUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));
        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> MainPage(string param)
        {
            var files = System.IO.Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Videos", "*");
            var list = new List<string[]>();
            var fnames = new List<string>();
            for (int i = 0; i < files.Length; i++)
                list.Add(files[i].Split(new char[] { '\\' })); // [9] - file name
            foreach (var l in list)
                for (var i = 0; i < l.Length;)
                {
                    fnames.Add(l[9]);
                    break;
                }
            ViewBag.Videos = fnames;

            await CheckFillOutTbl(StaticTables.Categories);

            if (param != null)
            {
                ViewBag.PartialView = TopMenuClicked(param);
            }

            { ViewBag.CategoryPressed = isCategoryPressed; }
            { ViewBag.TopMenu = isTopMenu; }
            return View(StaticTables.Categories);
        }

        [HttpPost]
        public async Task<ActionResult> MainPage(object param)
        {
            var btnCategory =
               Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First();

            if (btnCategory != null)
            {
                isCategoryPressed = true;
                var d = DateTime.Now;
                btnCategory = btnCategory.Remove(0, 1); 
                await CheckFillOutTbl(StaticTables.Products);
                lock (new object())
                {
                    ViewBag.PartialView = TopMenuClicked("ProductsById");
                    {
                        ViewBag.Products = StaticTables.Products.Where(p => p.CategoryId ==
                      StaticTables.Categories.FirstOrDefault(c => c.TagName == btnCategory).Id);
                    }
                    { ViewBag.CategoryName = StaticTables.Categories.FirstOrDefault(c => c.TagName == btnCategory).Name; }
                }

            }

            { ViewBag.CategoryPressed = isCategoryPressed; }
            { ViewBag.TopMenu = isTopMenu; }
            return View(StaticTables.Categories);
        }

        public ActionResult Cart()
        {
            //if (CheckAlreadyUser() is ActionResult) return CheckAlreadyUser() as ActionResult;
            return View();
        }

        public ActionResult CategoryType(int idCateg)
        {
            return View();
        }

        #region Auxiliary methods:
        private object TopMenuClicked(string param)
        {
            if (!param.Equals("ProductsById"))
                isTopMenu = true;
            switch (param)
            {
                case "Contacts":
                    return "ContactPart";
                case "Delivery":
                    return "DeliveryPart";
                case "Payments":
                    return "PaymentsPart";
                case "Videos":
                    return "VideosPart";
                case "ProductsById":
                    return "ProductsPart";
                default: return "This top menu doesn't exists..";
            }
        }

        private async Task CheckFillOutTbl(IEnumerable<BLL.Interfaces.IModel> table)
        {
            //if (table == null || table.Count() == 0)
            //{
            switch (table.GetType().GetGenericArguments()[0].Name)
            {
                case nameof(CategoryDto):
                    StaticTables.Categories = await db.ReadCategoriesAsync();
                    break;
                case nameof(ProductDto):
                    StaticTables.Products = await db.ReadProductsAsync();
                    break;
                case nameof(CartDto):
                    StaticTables.Carts = await db.ReadCartsAsync();
                    break;
            }
            //}
        }
        #endregion
    }
}
namespace AfricanShopLviv.PL.Controllers
{
    using System;
    using PagedList;
    using System.IO;
    using System.Web;
    using System.Linq;
    using System.Web.Mvc;
    using System.Threading.Tasks;
    using AfricanShopLviv.BLL.DTO;
    using AfricanShopLviv.PL.Models;
    using System.Collections.Generic;
    using AfricanShopLviv.BLL.Services;
    using Microsoft.AspNet.Identity.Owin;

    public class HomeController : Controller
    {
        #region Fields:       
        private static bool isOrder = false;
        private Guid AccessToken { get; set; }
        private readonly ServiceAfricanShop db;
        static string nameCategoryPressed = null;
        public static double TotalSum { get; set; } = 0;
        public static bool IsCart { get; set; } = false;
        private static bool IsEmptyCart { get; set; } = false;
        private string CurrentUserEmail { get; set; } = null;
        private ApplicationUser CurrentLocalUser { get; set; } = null;
        private static bool isTopMenu = false, isCategoryPressed = false;
        #endregion

        #region Constructor:
        public HomeController()
        {
            db = new ServiceAfricanShop(Init.ConnectionStr);
        }
        #endregion

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
        }

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
                return RedirectToAction($"../Home/MainPage");
            }
        }

        public ApplicationUser GetCurrentUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));
        #endregion

        public ActionResult Index() // For testing..
        {
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> MainPage(int? page, string param, string isShopCart)
        {
            var files = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Videos", "*");
            var list = new List<string[]>();
            ViewBag.Videos = FillVideoNames(files, list);
            await CheckFillOutTbl(StaticTables.Categories);
            await CheckFillOutTbl(StaticTables.Products);

            // If have an order:
            if (isShopCart == "cart")
            {
                IsCart = true;
                if (StaticTables.Carts.Count == 0)
                    IsEmptyCart = true;
                { ViewBag.Cart = StaticTables.Carts; }
            }
            if (param != null)
                ViewBag.PartialView = TopMenuClicked(param);

            { ViewBag.CategoryName = nameCategoryPressed; }
            { ViewBag.RegistredUser = CurrentUserEmail; }
            { ViewBag.IsEmptyCart = IsEmptyCart; }
            { ViewBag.IsOrder = isOrder; }
            { ViewBag.IsCart = IsCart; }
            { ViewBag.Categories = StaticTables.Categories; }
            { ViewBag.Products = StaticTables.Products; }
            { ViewBag.CategoryPressed = isCategoryPressed; }
            { ViewBag.TopMenu = isTopMenu; }

            int pageSize = 8;
            int pageNumber = (page ?? 1);
            return View(StaticTables.Products.ToPagedList(pageNumber, pageSize));
        }

        [HttpPost]
        public async Task<ActionResult> MainPage(object page)
        {
            var button =
               Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First().Remove(0, 1);
            if (char.IsDigit(button[0]))  // If we Added a Product to the Cart
            {
                await CheckFillOutTbl(StaticTables.Carts);
                var selectedProd = StaticTables.Products.FirstOrDefault(c => c.Code == button);

                int userId = 0;
                if (CurrentLocalUser == null)
                    userId = 0;
                else userId = CurrentLocalUser.UserId;
                PlaceOrder(selectedProd, userId);
                { ViewBag.Cart = StaticTables.Carts; }
            }
            else if (button != null)     // If We pressed any Category button
            {
                isCategoryPressed = true;
                var d = DateTime.Now;
                await CheckFillOutTbl(StaticTables.Products);
                StaticTables.Products = StaticTables.Products.Where(p => p.CategoryId ==
                StaticTables.Categories.FirstOrDefault(c => c.TagName == button).Id);
                { nameCategoryPressed = StaticTables.Categories.FirstOrDefault(c => c.TagName == button).Name; }
            }

            { ViewBag.CategoryName = nameCategoryPressed; }
            { ViewBag.RegistredUser = CurrentUserEmail; }
            { ViewBag.IsEmptyCart = IsEmptyCart; }
            { ViewBag.IsOrder = isOrder; }
            { ViewBag.IsCart = IsCart; }
            { ViewBag.Products = StaticTables.Products; }
            { ViewBag.Categories = StaticTables.Categories; }
            { ViewBag.CategoryPressed = isCategoryPressed; }
            { ViewBag.TopMenu = isTopMenu; }

            int pageSize = 8;
            int pageNumber = (page as int? ?? 1);
            return View(StaticTables.Products.ToPagedList(pageNumber, pageSize));
        }

        public ActionResult CategoryType(int idCateg)
        {
            return View();
        }

        [HttpPost]
        public ActionResult SendMessage(BLL.DTO.EmailEntity.EmailData mailInfo)
        {
            mailInfo.SendingDate = DateTime.UtcNow;
            if (Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First().Remove(0, 1) == "registred")
            {
                TotalSum = 0;
                CurrentLocalUser = UserManager.Users.FirstOrDefault(e => e.Email == CurrentUserEmail);
                var arr = MakeOrderFile(CurrentLocalUser.FirstName + CurrentLocalUser.LastName, CurrentLocalUser.PhoneNumber, CurrentLocalUser.Email);
                mailInfo.UserName = CurrentLocalUser.FirstName + "  " + CurrentLocalUser.LastName;
                mailInfo.Email = CurrentLocalUser.Email;
                mailInfo.Body = CreateOrderBody(StaticTables.Carts, mailInfo, TotalSum.ToString());
                return Json(new EmailSender(mailInfo, mailInfo.Body).SendMessage(MessageType.NewOrderHtmlBody, arr[0], "andriygerbut@gmail.com"));
            }
            if (mailInfo.Message.Equals("MakeOrder")) // If client making order as not registered
            {
                TotalSum = 0;
                var arr = MakeOrderFile(mailInfo.UserName, mailInfo.Phone, mailInfo.Email);
                return Json(new EmailSender(mailInfo).SendMessage(MessageType.NewOrder, arr[0]));
            }
            return Json(new EmailSender(mailInfo).SendMessage(MessageType.WriteUs));
        }

        [HttpGet]
        public ActionResult DeleteItem(string cartSelected)
        {
            var found = StaticTables.Carts.FirstOrDefault(f => f.ProductName == cartSelected);
            TotalSum -= found.ItemsPrice;
            StaticTables.Carts.Remove(StaticTables.Carts.FirstOrDefault(f => f.ProductName == cartSelected));
            return RedirectToAction("MainPage");
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
            if (table == null || table.Count() == 0)
            {
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
            }
        }

        private IEnumerable<string> FillVideoNames(string[] files, List<string[]> list)
        {
            var fNames = new List<string>();
            for (int i = 0; i < files.Length; i++)
                list.Add(files[i].Split(new char[] { '\\' })); // [9] - file name
            foreach (var l in list)
                for (var i = 0; i < l.Length;)
                {
                    fNames.Add(l[10]);
                    break;
                }
            return fNames;
        }

        private static string PlaceOrder(ProductDto product, int userId)
        {
            try
            {
                var order = new CartDto
                {
                    Price = product.Price,
                    OrderDate = DateTime.Now,
                    Quantity = 1,
                    ItemsPrice = product.Price * 1,
                    UserId = userId,
                    ProductId = product.Id,
                    ProductName = product.Name,
                    ProductPhoto = product.Photo
                };
                var repead = StaticTables.Carts.FirstOrDefault(f => f.ProductName == order.ProductName);
                if (repead != null) // if product already exist
                {
                    order.Quantity = ++repead.Quantity;
                    order.ItemsPrice = order.Price * repead.Quantity;
                    StaticTables.Carts.Remove(StaticTables.Carts.SingleOrDefault(del => del.ProductName == order.ProductName));
                }
                TotalSum += product.Price;
                isOrder = true;
                IsEmptyCart = false;
                StaticTables.Carts.Add(order);
                return "Successfuly! The Product was added to Cart.";
            }
            catch (Exception ex) { return ex.Message + "\n" + ex.Message; }
        }

        /// <summary>
        /// The method build an Attachment File
        /// </summary>
        /// <param name="order"> List of an orders </param>
        /// <param name="userName"> Client Name </param>
        /// <returns> 1st - path to attachment, 2nd - Success message </returns>
        private string[] MakeOrderFile(string userName, string phone, string email, bool isCreateFile = true)
        {
            var returnArr = new string[2];
            returnArr[0] = AppDomain.CurrentDomain.BaseDirectory + $"OrdersFiles\\{userName}'s Order.txt";
            if (isCreateFile)
            {
                using (var sw = new StreamWriter(returnArr[0], true))
                {
                    if (System.IO.File.Exists(returnArr[0]))
                    {
                        sw.WriteLine(new string('-', 100));
                        sw.WriteLine($"Order № '{Guid.NewGuid()}':\n{DateTime.UtcNow}");
                    }
                    sw.WriteLine(new string('-', 100) + "\n");
                    sw.WriteLine($"Hi, I'm \t{userName}\nI placed an order with you:\n");
                    for (var i = 0; i < StaticTables.Carts.Count; i++)
                    {
                        sw.WriteLine($"Product Name:\t{StaticTables.Carts[i].ProductName}");
                        sw.WriteLine($"Quantity:\t{StaticTables.Carts[i].Quantity}");
                        sw.WriteLine($"Price:\t{StaticTables.Carts[i].ItemsPrice}");
                        sw.WriteLine($"Date place product:\t{StaticTables.Carts[i].OrderDate}");
                        sw.WriteLine(new string('-', 100) + "\n");
                        TotalSum += StaticTables.Carts[i].ItemsPrice;
                    }
                    sw.WriteLine($"\n\n\t\t\t\t\t\t\t\t\t\tTotal: {TotalSum}грн.");
                    sw.WriteLine("---------------------------------------------");
                    sw.WriteLine("My Contacts:");
                    sw.WriteLine($"My Email:\t{email}\nPhone:\t{phone}");
                    sw.WriteLine("---------------------------------------------");
                    sw.WriteLine(new string('-', 100));
                    sw.WriteLine(new string('-', 100) + "\n\n");
                    returnArr[1] = "Successfully! Order info sent to Owners.";
                }
            }
            else returnArr[1] = "Successfully! Order info sent to Owners.";
            return returnArr;
        }


        private string CreateOrderBody(List<CartDto> carts, BLL.DTO.EmailEntity.EmailData data, string totalPrice)
        {
            string prods = string.Empty;
            for (var i = 0; i < carts.Count; i++)
            {
                prods = $"<td class='p-4'>" +
                         $"<div class='media align-items-center'>" +
                         $"<div class='media-body'><a href='#' style='text-decoration:none'>{carts[i].ProductName}</a>" +
                         $"<div><small><span class='text-muted'>SKY: {new Random().Next(new Random().Next(10, 99), new Random().Next(100, 999))}</span></small>" +
                         $"</div></div></div></td>" +
                         $"<td class='text-right font-weight-semibold align-middle p-4'>{carts[i].Price} грн.</td>" +
                         $"<td class='text-right font-weight-semibold align-middle p-4'>{carts[i].Quantity}</td>" +
                         $"<td class='text-right font-weight-semibold align-middle p-4'>{carts[i].ItemsPrice} грн.</td>";
            }
            return "<h1>h1</h1> <h2>h2</h2> <h3>h3</h3> <h4>h4</h4>";
                //"<!DOCTYPE html>" +
                //        "<html> <head> <meta charset=\"utf-8\" /> <link href=\"https://cdn.jsdelivr.net/npm/bootstrap@5.2.0-beta1/dist/css/bootstrap.min.css\" rel=\"stylesheet\" integrity=\"sha384-0evHe/X+R7YkIZDRvuzKMRqM+OrBnVFBL6DOitfPri4tjfHxaWutUpFmBp4vmVor\" crossorigin=\"anonymous\">" +
                //        $"</head><body><div class=\"width:100%\"><div class=\"card\"> <div class=\"card-header\"> " +
                //        $"<h2 style=\"text-align:center;font-family:'Times New Roman'\">New Order № 1 [{data.SendingDate}]</h2>" +
                //        "</div>     <div class=\"card-body\"><div class=\"table-responsive\"><h6>Hello Marko Okoye!</h6><h6>Incoming order on the site <a href=\"africanshoplviv.com\">africanshoplviv.com </a></h6><table class=\"table table-bordered m-0\"><thead><tr><th class=\"text-center py-3 px-4\" style=\"min-width: 400px;\">" +
                //        "Product</th><th class=\"text-right py-3 px-4\" style=\"width: 100px;\">Price</th>" +
                //        "<th class=\"text-center py-3 px-4\" style=\"width: 120px;\">Quantity</th><th class=\"text-right py-3 px-4\" style=\"width: 100px\">" +
                //        "Total</th></tr></thead><tbody>" +
                //        $"<tr>{prods}</tr>" +
                //        "</tbody> </table> </div> <div  class=\"\">      " +
                //        "<div style=\"background-image:linear-gradient(transparent, yellow, transparent, yellow);padding:10px;border:1px solid black;float:right;width:100%>" +
                //        $"<h3 class=\"\" style=\"font-weight:bold;text-align:right\">Total price:</h3> <h4 style=\"text-align:right;font-weight:bold\">{totalPrice} грн.</h4>" +
                //       " </div>  </div> </div>  </div> </div> <style>" +
                //       "body { width:50%; margin-top: 20px;  background: #eee; } .ui-w-40 { width: 40px !important; height: auto; }.card { box-shadow: 0 1px 15px 1px rgba(52,40,104,.08); }" +
                //       ".ui-product-color { display: inline-block; overflow: hidden;  margin: .144em; width: .875rem;  height: .875rem;  border-radius: 10rem;" +
                //       "-webkit-box-shadow: 0 0 0 1px rgba(0,0,0,0.15) inset;  box-shadow: 0 0 0 1px rgba(0,0,0,0.15) inset; vertical-align: middle;  }  </style></body></html>";
        }
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUserEmail = filterContext.HttpContext.User.Identity.Name;
            }
            else // When User Log off
            {
                CurrentUserEmail = null;
                CurrentLocalUser = null;
            }
        }
    }
}

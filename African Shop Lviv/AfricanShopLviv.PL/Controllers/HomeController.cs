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
        private static string FooterText { get; set; }
        private static bool isOrder = false;
        private Guid AccessToken { get; set; }
        private readonly ServiceAfricanShop db;
        static string nameCategoryPressed = null;
        public static int CntAdd { get; set; } = 0;
        public static double TotalSum { get; set; } = 0;
        public static bool IsCart { get; set; } = false;
        private static bool IsEmptyCart { get; set; } = false;
        private string CurrentUserEmail { get; set; } = null;
        private static string[] VideoPath { get; set; } = null;
        private ApplicationUser CurrentLocalUser { get; set; } = null;
        public static bool isTopMenu = false;
        private static bool isCategoryPressed = false;
        #endregion

        #region Constructor:
        public HomeController()
        {
            db = new ServiceAfricanShop(Init.ConnectionStr);
            try
            {
                {
                    //FooterText = 
                }
                StaticTables.Categories = db.ReadCategories();
                StaticTables.Carts = db.ReadCarts().ToList();
                if (VideoPath == null)
                    VideoPath = Directory.GetFiles(AppDomain.CurrentDomain.BaseDirectory + "Videos", "*");
            }
            catch { }
        }
        #endregion

        #region SignInManager & UserManager:
        private ApplicationSignInManager _signInManager;
        public ApplicationSignInManager SignInManager
        {
            get => _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            private set => _signInManager = value;
        }

        private ApplicationUserManager _userManager;
        public ApplicationUserManager UserManager
        {
            get => _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            private set => _userManager = value;
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
                return RedirectToAction($"../Home/MainPage");
            }
        }

        public ApplicationUser GetCurrentUser() => AccountController.AllUsers.FirstOrDefault(n => n.Email.Equals(User.Identity.Name));
        #endregion

        [HttpGet]
        public async Task<ActionResult> MainPage(int? page, string param = "AllCategoryPart", string isShopCart = "")
        {
            var list = new List<string[]>();
            VideoPath = VideoPath == null ? new List<string>().ToArray() : VideoPath;
            try
            {
                ViewBag.Videos = FillVideoNames(VideoPath, list);
            }
            catch { }

            // If we selected page pagination - reset on default categories view:
            if(page != null)
            param = null;

            // If have an order or clicked Cart btn:
            if (isShopCart == "cart")
            {
                await CurrentOrDefaultUser(CurrentLocalUser);
                StaticTables.Carts = StaticTables.Carts.Where(u => u.UserId == CurrentLocalUser.UserId).ToList();
                CntAdd = StaticTables.Carts.Count;
                isTopMenu = false;
                TotalSum = StaticTables.Carts.Sum(t => t.ItemsPrice); // View cart's total sum 
                IsCart = true;
                isOrder = true;
                if (StaticTables.Carts.Count() == 0)
                    IsEmptyCart = true;
                { ViewBag.Cart = StaticTables.Carts; }
            }
            if (param != null)
                ViewBag.PartialView = TopMenuClicked(param);
            else isTopMenu = false;

            { ViewBag.IsModal = isModal; }
            { ViewBag.ModalMsg = modalMsg; }
            { ViewBag.Cart = StaticTables.Carts; }
            { ViewBag.CurrentUser = CurrentLocalUser; }
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
            List<ProductDto> productsByCateg = default;
            var button =
               Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First().Remove(0, 1);
            if (char.IsDigit(button[0]))  // If we Added a Product to the Cart
            {
                ++CntAdd;
                await CurrentOrDefaultUser(CurrentLocalUser);
                var selectedProd = StaticTables.Products.FirstOrDefault(c => c.Code == button);
                StaticTables.Carts = StaticTables.Carts.Where(u => u.UserId == CurrentLocalUser.UserId).ToList();
                // CurrentOrDefaultUser(CurrentLocalUser);
                PlaceOrder(selectedProd, CurrentLocalUser.UserId, db);
                { ViewBag.Cart = StaticTables.Carts; }
            }
            else if (button != null)      // If We pressed any Category button
            {
                isCategoryPressed = true;
                isTopMenu = false;
                StaticTables.Products = new List<ProductDto>();
                StaticTables.Products = db.ReadProducts();
                StaticTables.Categories = await db.ReadCategoriesAsync();
                productsByCateg = StaticTables.Products.Where(p => p.CategoryId ==
                StaticTables.Categories.FirstOrDefault(c => c.TagName == button).Id).ToList();
                StaticTables.Products = productsByCateg;
                { nameCategoryPressed = StaticTables.Categories.FirstOrDefault(c => c.TagName == button).Name; }
            }

            { ViewBag.IsModal = isModal; }
            { ViewBag.ModalMsg = modalMsg; }
            { ViewBag.CurrentUser = CurrentLocalUser; }
            { ViewBag.CategoryName = nameCategoryPressed; }
            { ViewBag.RegistredUser = CurrentUserEmail; }
            { ViewBag.IsEmptyCart = IsEmptyCart; }
            { ViewBag.IsOrder = isOrder; }
            { ViewBag.IsCart = IsCart; }
            { ViewBag.Cart = StaticTables.Carts; }
            { ViewBag.Categories = StaticTables.Categories; }
            { ViewBag.CategoryPressed = isCategoryPressed; }
            { ViewBag.TopMenu = isTopMenu; }

            int pageSize = 8;
            int pageNumber = (page as int? ?? 1);
            return View(StaticTables.Products.ToPagedList(pageNumber, pageSize));
        }

        public static string modalMsg = string.Empty;
        static bool isModal = false;
        public ActionResult CloseAlerts()
        {
            isModal = false;
            return RedirectToAction("../Home/MainPage");
        }

        public ActionResult Cart()
        {
            ViewBag.Cart = StaticTables.Carts.Where(u => u.UserId == CurrentLocalUser.UserId).ToList();
            return View();
        }

        [HttpPost]
        public ActionResult CartUpdate(CartDto cart)
        {
            try
            {
                db.Update(cart);
            }
            catch { }
            return RedirectToAction("MainPage", "Home", new { isShopCart = "cart"});
        }

        [HttpPost]
        public async Task<ActionResult> SendMessage(BLL.DTO.EmailEntity.EmailData mailInfo)
        {
            isModal = true;
            mailInfo.SendingDate = DateTime.UtcNow;
            #region Old f-ty:
            //var param = string.Empty;
            //try
            //{
            //    param = Request.Params.Cast<string>().Where(p => p.StartsWith("btn")).Select(p => p.Substring("btn".Length)).First().Remove(0, 1);
            //}
            //catch { }
            //if (param == "registred")
            //{
            //    var orders = StaticTables.Carts.Where(u => u.UserId == CurrentLocalUser.UserId).ToList();
            //    CurrentLocalUser = UserManager.Users.FirstOrDefault(e => e.Email == CurrentUserEmail);
            //    var arr = MakeOrderFile(CurrentLocalUser.FirstName + CurrentLocalUser.LastName, CurrentLocalUser.PhoneNumber, CurrentLocalUser.Email);
            //    mailInfo.UserName = CurrentLocalUser.FirstName + "\t" + CurrentLocalUser.LastName;
            //    mailInfo.Email = CurrentLocalUser.Email;
            //    mailInfo.Body = CreateOrderBody(orders, mailInfo, TotalSum.ToString());

            //    return Json(new EmailSender(mailInfo, mailInfo.Body).SendMessage(MessageType.NewOrderHtmlBody, arr[0], "africanshoplviv@gmail.com"));
            //}
            #endregion
            if (mailInfo.Message.Equals("MakeOrder"))
            {
                TotalSum = 0;
                var arr = MakeOrderFile(mailInfo.UserName, mailInfo.Phone, mailInfo.Email);
                var carts = StaticTables.Carts.Where(u => u.UserId == CurrentLocalUser.UserId).ToList();
                var productsStr = string.Empty;
                foreach (var pr in carts)
                    productsStr += pr.ProductName + ", ";
                var htmlB = CreateOrderBody(carts, mailInfo);
                db.Insert(new OrderDto
                {
                    OrderDate = DateTime.Now,
                    TotalAmount = carts.Sum(t => t.ItemsPrice),
                    UserId = CurrentLocalUser.UserId,
                    Products = productsStr
                }); ;
                modalMsg = new EmailSender(mailInfo, htmlB)
                    .SendMessage(MessageType.NewOrder, arr[0]);
                return RedirectToAction("../Home/MainPage");
            }
            // Else someone clicked on Call Back or Write Us button:
            // if user still doesn't exist
            if (CurrentLocalUser == null)
            {
                await CurrentOrDefaultUser(CurrentLocalUser);
            }
            db.Insert(new MessageDto 
            { 
                TypeMessage = "Write Us",
                TextMessage = mailInfo.Comment,
                DateMessage = DateTime.Now,
                IsReviwed = false, 
                RecipientId = 1,
                SenderId = CurrentLocalUser.UserId,
                Title = "'Write Us' from africanshoplviv.com"
            });
            modalMsg = new EmailSender(mailInfo).SendMessage(MessageType.WriteUs);
            return RedirectToAction("../Home/MainPage");
        }

        [HttpGet]
        public ActionResult DeleteItem(int? cartSelected)
        {
            if (cartSelected != null)
            {
                --CntAdd;
                var cart = StaticTables.Carts.FirstOrDefault(i => i.Id == cartSelected);
                TotalSum -= cart.ItemsPrice;
                var id = int.Parse(cartSelected.ToString());
                db.DeleteCart(id);
                StaticTables.Carts.Remove(cart);
            }
            return RedirectToAction("MainPage", "Home", new { isShopCart = "cart" }); // ..on 3rd params pass 'cart' to left cart deployed
        }

        #region Auxiliary methods:
        private async Task CurrentOrDefaultUser(ApplicationUser user)
        {
            if (user == null)
                CurrentLocalUser = await new Init().InitDefaultUser(UserManager, SignInManager);
            else CurrentLocalUser.UserId = CurrentLocalUser.UserId;
        }

        private object TopMenuClicked(string param)
        {
            if (param != "ProductsById")
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
                case "AllCategoryPart":
                    return "AllCategoryPart";
                case "PersonDataPart":
                    return "PersonDataPart";
                default:
                    return "NoN Partial";
                    //return "This top menu doesn't exists..";
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
                    var concreteCarts = await db.ReadCartsAsync();
                    // Get cart only for Current user:
                    try
                    {
                        if (CurrentLocalUser == null) { }
                        //CurrentLocalUser = await new Init().InitDefaultUser(UserManager, SignInManager);
                        else
                            StaticTables.Carts = concreteCarts.Where(user => user.UserId == CurrentLocalUser.UserId).ToList();
                    }
                    catch { }
                    break;
            }
            // }
        }

        private IEnumerable<string> FillVideoNames(string[] files, List<string[]> list)
        {
            var fNames = new List<string>();
            if (files.Length == 0)
                return fNames;
            for (int i = 0; i < files.Length; i++)
                list.Add(files[i].Split(new char[] { '\\' })); // [9] - file name
            foreach (var l in list)
                for (var i = 0; i < l.Length;)
                {
                    fNames.Add(l[l.Length - 1]);
                    break;
                }
            return fNames;
        }

        private static string PlaceOrder(ProductDto product, int userId, ServiceAfricanShop db)
        {
            try
            {
                var orderItem = new CartDto
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
                //var repead = StaticTables.Carts.FirstOrDefault(f => f.ProductName == orderItem.ProductName);
                //if (repead != null) // if product already exist
                //{
                //    orderItem.Quantity = ++repead.Quantity;
                //    orderItem.ItemsPrice = orderItem.Price * repead.Quantity;
                //    //CartDto dubl = StaticTables.Carts.FirstOrDefault(del => del.ProductName == orderItem.ProductName);
                //    StaticTables.Carts.Remove(repead);
                //    db.DeleteCart(repead.Id);
                //}
                isOrder = true;
                IsEmptyCart = false;
                StaticTables.Carts.Add(orderItem);
                db.Insert(orderItem);
                TotalSum = StaticTables.Carts.Sum(t => t.ItemsPrice);

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
            var randNum = new Random();
            var dateFile = DateTime.UtcNow;
            returnArr[0] = AppDomain.CurrentDomain.BaseDirectory +
                $"OrdersFiles\\Order - {dateFile.Day + "." + dateFile.Month + "." + dateFile.Year + "  " + dateFile.Hour + "." + dateFile.Minute + "." + dateFile.Second + "." + dateFile.Millisecond}.txt";
            var carts = StaticTables.Carts.Where(u => u.UserId == CurrentLocalUser.UserId).ToList();
            if (isCreateFile)
            {
                using (var sw = new StreamWriter(returnArr[0]))
                {
                    if (System.IO.File.Exists(returnArr[0]))
                    {
                        sw.WriteLine(new string('-', 100));
                        sw.WriteLine($"Order № '{Guid.NewGuid()}':\n{DateTime.UtcNow}");
                    }
                    sw.WriteLine(new string('-', 100) + "\n");
                    sw.WriteLine($"Hi, I'm {userName}\nI placed an order with you:\n");
                    for (var i = 0; i < carts.Count; i++)
                    {
                        sw.WriteLine($"{i + 1}.");
                        sw.WriteLine($"Product Name:\t{carts[i].ProductName}");
                        sw.WriteLine($"Quantity:\t{carts[i].Quantity}");
                        sw.WriteLine($"Price:\t{carts[i].ItemsPrice}");
                        sw.WriteLine($"Date place product:\t{carts[i].OrderDate}");
                        sw.WriteLine(new string('-', 100) + "\n");
                        //TotalSum = carts[i].ItemsPrice;
                    }
                    sw.WriteLine($"\n\n\t\t\t\t\t\t\t\t\t\tTotal: {carts.Sum(a => a.ItemsPrice)}грн.");
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

        private static string htmlBody = string.Empty;
        private string CreateOrderBody(List<CartDto> carts, BLL.DTO.EmailEntity.EmailData data)
        {
            string subHtml = string.Empty;
            foreach (var c in carts)
            {
                subHtml +=
                    "<tr style=\"text-align:center\">" +
                     $"<td scope = \"row\"><a href = \"#\">{c.ProductName}</a></td>" +
                     $"<td scope = \"row\">{c.Quantity}</td> " +
                     $"<td scope = \"row\">{c.Price}</td> " +
                     $"<td scope = \"row\">{c.ItemsPrice}</td> " +
                    "</tr> ";
            }
            htmlBody =
"<!DOCTYPE html>" +
"<html>" +
"<head>" +
   "<meta charset = \"utf-8\" /> " +
    "<meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">" +
    "<link href = \"https://cdn.jsdelivr.net/npm/bootstrap@5.0.2/dist/css/bootstrap.min.css\" rel=\"stylesheet\">" +
"</head>" +
"<body>" +
    $"<h3 style = \"text-align:center;color:blue\" > Order № {carts.Count} {DateTime.Now}</h3>" +
    "<h5>Hello, Marko Okoye!</h5>" +
    "<h5>" +
        "Incoming order on the site&nbsp;&nbsp;<a href = \"http://www.africanshopukraine.com\">" +
            "africanshopukraine.com" +
        "</a> " +
    "</h5> " +
    "<h1 style=\"text-align:center\">Ordering Information</h1>" +
    "<table style=\"padding:3px;text-align:center\" class=\"table table-bordered\">" +
        "<tr style = \"text-align:center;padding:3px;background-color:whitesmoke\"> " +
           " <th style=\"padding:5px\" scope=\"col\">Product</th>" +
           " <th style=\"padding:5px\" scope = \"col\" > Quantity </ th > " +
           " <th style=\"padding:5px\" scope=\"col\">Price</th>" +
           " <th style=\"padding:5px\" scope = \"col\" > Total Price</th>" +
       " </tr>" +
      subHtml +
    "</table>" +
    "<div style = \"border: thin solid sandybrown; background-color: sandybrown;padding:8px\"> " +
        $"<h2 style=\"text-align:right\">Tatal:&nbsp;&nbsp;{carts.Sum(a => a.ItemsPrice)}.00 ГРН</h2>" +
    "</div>" +
    "<div>" +
        "<h3 style = \"color:orange\"> Payment details</h3>" +
        "<table style=\"padding:3px\" class=\"table table-bordered\">" +
            "<tr style=\"padding:3px\">" +
                "<th style = \"background-color:silver;padding:3px\" > Payment method</th>" +
                "<th style = \"background-color:orange;padding:3px\">Payment in cash</th>" +
            "</tr>" +
            "<tr style=\"padding:3px\">" +
                "<th style = \"background-color:silver;padding:3px\" > Payment status</th>" +
                "<th style = \"background-color:orange;padding:3px\">Unpaid</th>" +
            "</tr>" +
      "  </table>" +
       " <h3 style = \"color:orange\"> Customer's info</h3>" +
        "<table style=\"padding:3px\" class=\"table table-bordered\">" +
            "<tr style=\"padding:3px\">" +
               " <th style = \"background-color:silver;padding:3px\" > Name and Surname</th>" +
               $"<th style = \"background-color:yellow;padding:3px\">{data.UserName}</th>" +
           " </tr>" +
            "<tr style=\"padding:3px\">" +
                "<th style = \"background-color:silver;padding:3px\" > Phone number</th>" +
               $"<th style = \"background-color:yellow;padding:3px\">{data.Phone}</th>" +
           " </tr>" +
            "<tr style=\"padding:3px\">" +
                "<th style = \"background-color:silver;padding:3px\" > Nova Pochta Address Or Home Address</th>" +
               $"<th style = \"background-color:yellow;padding:3px\">{data.Comment}</th>" +
            "</tr>  <tr style=\"padding:3px\">" +
               " <th style = \"background-color:silver;padding:3px\"> E-Mail </th>" +
               $"<th style = \"background-color:yellow;padding:3px\">{data.Email}</th>" +
               " </tr>" +
       " </table>    </div></body></html>";

            return htmlBody;
        }
        #endregion

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.User.Identity.IsAuthenticated)
            {
                CurrentUserEmail = filterContext.HttpContext.User.Identity.Name;
                try
                {
                    CurrentLocalUser = UserManager.Users.FirstOrDefault(u => u.Email == CurrentUserEmail);
                }
                catch { }
            }
            else // When User Log off, or still doesn't enter 
            {
                CurrentUserEmail = null;
                CurrentLocalUser = null;
            }
        }
    }
}

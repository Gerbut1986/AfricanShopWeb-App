namespace AfricanShopLviv.PL.Controllers
{
    using System.Web;
    using System.Web.Mvc;
    using AfricanShopLviv.PL.Models;
    using Microsoft.AspNet.Identity.Owin;

    [Authorize]
    public class adminController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

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

        [HttpGet]
        public ActionResult admin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult admin(ApplicationUser admin)
        {
            return View();
        }

        public ActionResult _829528a_441d_484m_862i_22475963ffdn()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}



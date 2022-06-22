using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AfricanShopLviv.PL.Startup))]
namespace AfricanShopLviv.PL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

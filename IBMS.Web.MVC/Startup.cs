using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IBMS.Web.MVC.Startup))]
namespace IBMS.Web.MVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

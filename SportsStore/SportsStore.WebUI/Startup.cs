using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SportsStore.WebUI.Startup))]
namespace SportsStore.WebUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

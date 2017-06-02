using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JehovaJirehWebApp.Startup))]
namespace JehovaJirehWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

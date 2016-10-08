using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Quartett.Web.Startup))]

namespace Quartett.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}

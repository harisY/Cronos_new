using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cronos.Startup))]
namespace Cronos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}

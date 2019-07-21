using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Inventory.Startup))]
namespace Inventory
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
            //app.UseSession();
            //app.UseAuthentication();
        }

      
    }
}

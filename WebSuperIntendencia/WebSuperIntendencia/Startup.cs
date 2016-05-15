using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebSuperIntendencia.Startup))]
namespace WebSuperIntendencia
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

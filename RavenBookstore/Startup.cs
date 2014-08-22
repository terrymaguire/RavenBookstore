using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RavenBookstore.Startup))]
namespace RavenBookstore
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RedSocial.Startup))]
namespace RedSocial
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

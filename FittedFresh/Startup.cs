using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FittedFresh.Startup))]
namespace FittedFresh
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

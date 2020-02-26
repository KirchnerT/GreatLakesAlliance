using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(GreatLakesAlliance.Startup))]
namespace GreatLakesAlliance
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

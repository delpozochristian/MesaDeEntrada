using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mailroom.Startup))]
namespace Mailroom
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

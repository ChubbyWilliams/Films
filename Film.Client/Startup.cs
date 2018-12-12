using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Film.Client.Startup))]
namespace Film.Client
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

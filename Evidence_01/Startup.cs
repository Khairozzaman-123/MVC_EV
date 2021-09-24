using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Evidence_01.Startup))]
namespace Evidence_01
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

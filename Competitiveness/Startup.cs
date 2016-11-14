using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Competitiveness.Startup))]
namespace Competitiveness
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

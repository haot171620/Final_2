using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhuTungXeMay2019.Startup))]
namespace PhuTungXeMay2019
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

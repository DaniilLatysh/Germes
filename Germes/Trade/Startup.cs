using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Trade.Startup))]
namespace Trade
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AspNet.BoardGameMall.Startup))]
namespace AspNet.BoardGameMall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

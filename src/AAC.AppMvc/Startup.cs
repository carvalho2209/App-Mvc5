using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(AAC.AppMvc.Startup))]
namespace AAC.AppMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

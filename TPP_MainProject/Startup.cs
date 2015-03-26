using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TPP_MainProject.Startup))]
namespace TPP_MainProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

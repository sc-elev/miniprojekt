using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SC_MiniProject.Startup))]
namespace SC_MiniProject
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
           // ConfigureAuth(app);
        }
    }
}

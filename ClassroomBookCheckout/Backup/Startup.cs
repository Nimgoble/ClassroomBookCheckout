using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ClassroomBookCheckout.Startup))]
namespace ClassroomBookCheckout
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

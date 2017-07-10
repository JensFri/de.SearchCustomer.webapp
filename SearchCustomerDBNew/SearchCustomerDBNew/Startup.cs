using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchCustomerDBNew.Startup))]
namespace SearchCustomerDBNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

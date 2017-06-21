using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SearchCustomerDatabase.Startup))]
namespace SearchCustomerDatabase
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

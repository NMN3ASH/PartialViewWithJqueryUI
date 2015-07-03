using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AddressBookUsingJqueryUIValidation.Startup))]
namespace AddressBookUsingJqueryUIValidation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PayrollUsingJqueryUIWithValidation.Startup))]
namespace PayrollUsingJqueryUIWithValidation
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

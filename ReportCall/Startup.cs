using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReportCall.Startup))]
namespace ReportCall
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

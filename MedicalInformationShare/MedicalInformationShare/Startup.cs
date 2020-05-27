using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MedicalInformationShare.Startup))]
namespace MedicalInformationShare
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SNSRi.Web.Startup))]
namespace SNSRi.Web {

    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
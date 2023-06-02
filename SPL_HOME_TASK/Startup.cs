using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SPL_HOME_TASK.Startup))]
namespace SPL_HOME_TASK
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}

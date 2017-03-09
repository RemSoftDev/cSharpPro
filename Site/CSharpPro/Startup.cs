using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSharpPro.Startup))]
namespace CSharpPro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
        }
    }
}

using Pictures.Controllers;
using System.IO;
using System.Web.Http;

namespace Pictures
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            Directory.CreateDirectory(PicturesController.DIRECTORY);
        }
    }
}

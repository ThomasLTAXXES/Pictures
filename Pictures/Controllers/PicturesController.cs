using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;

namespace Pictures.Controllers
{
    public class PicturesController : ApiController
    {
        // GET: api/Pictures/5
        public HttpResponseMessage Get(string id)
        {
            string filePath = string.Format(@"C:\temp\pictures\{0}.jpg", id);

            var result = new HttpResponseMessage(HttpStatusCode.OK);

            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                Image image = Image.FromStream(fileStream);
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    image.Save(memoryStream, ImageFormat.Jpeg);

                    result.Content = new ByteArrayContent(memoryStream.ToArray());
                    result.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpeg");
                    return result;
                }
            }

        }
    }
}

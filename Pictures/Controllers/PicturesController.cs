using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Http;

namespace Pictures.Controllers
{
    public class PicturesController : ApiController
    {
        // TODO: move to config

        internal const string DIRECTORY = @"C:\temp\pictures\";
        internal const string FILE_PATH = DIRECTORY + @"{0}.jpg";

        // GET: api/Pictures/5
        public HttpResponseMessage Get(string id)
        {
            string filePath = string.Format(FILE_PATH, id);

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

        public async Task<IHttpActionResult> Post([FromUri]string fileName)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            var provider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(provider);
            foreach (var file in provider.Contents)
            {
                var buffer = await file.ReadAsByteArrayAsync();
                File.WriteAllBytes(string.Format(FILE_PATH, fileName), buffer);
            }

            return Ok();
        }
    }
}

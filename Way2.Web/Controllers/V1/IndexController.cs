using Microsoft.AspNetCore.Mvc;

namespace Way2.Presentation.WebApi.Controllers.V1
{
    [ApiController]
    [ApiVersionNeutral]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class IndexController : Controller
    {

        [Route("/{filename}")]
        public IActionResult GetFile(string filename)
        {

            if (string.IsNullOrEmpty(filename))
            {
                return this.BadRequest();
            }

            if (!filename.StartsWith("loaderio-"))
            {
                return this.BadRequest();
            }

            if (!System.IO.File.Exists(filename))
            {
                return this.NotFound();
            }

            var fileStream = System.IO.File.OpenRead(filename);
            return this.File(fileStream, "application/octet-stream");

        }

    }
}

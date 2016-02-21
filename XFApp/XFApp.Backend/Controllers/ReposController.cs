using System;
using System.Web.Http;

namespace XFApp.Backend.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("repos")]
    public class ReposController : ApiController
    {
        [HttpPost]
        [Route("togglewatch")]
        public IHttpActionResult PostToggleWatch(Guid deviceId)
        {
            return Ok("Hello world");
        }        

    }
}
using System;
using System.Web.Http;

namespace XFApp.Backend.Controllers
{
    [AllowAnonymous]
    [RoutePrefix("device")]
    public class DeviceController : ApiController
    {
        [HttpGet]
        [Route("register")]
        public IHttpActionResult GetRegistered()
        {
            return Ok(Guid.NewGuid());
        }
    }
}
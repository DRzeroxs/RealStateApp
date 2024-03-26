using Microsoft.AspNetCore.Mvc;

namespace RealStateApp.Api.Controllers
{

    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseApiController : ControllerBase
    {
      
    }
}

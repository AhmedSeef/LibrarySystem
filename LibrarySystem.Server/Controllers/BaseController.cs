using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Server.Controllers
{
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class BaseController : ControllerBase
    {

    }
}

using Microsoft.AspNetCore.Mvc;

namespace MediAgent.Api.Controllers;

[ApiController]
[Route("/")]
public class FallbackController : ControllerBase
{
    [HttpGet]
    public IActionResult Index()
    {
        return Redirect("/swagger");
    }
}

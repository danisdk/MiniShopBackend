using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MiniShop.Controllers;

[ApiController]
[Route("error")]
public class ErrorController : Controller
{
    private readonly ILogger<ErrorController> _logger;

    public ErrorController(ILogger<ErrorController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public IActionResult HandleError()
    {
        var exception = HttpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
        _logger.LogError(exception, "Необработанная ошибка");

        return Problem("Внутренняя ошибка сервера"); // 500 с ProblemDetails
    }
}
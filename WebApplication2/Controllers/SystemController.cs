using System.Linq.Expressions;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Dto;
using WebApplication2.Services;

namespace WebApplication2.Controllers;


[ApiController]
[Route("api/[controller]")]
public class SystemController : ControllerBase
{
    private readonly ISystemService _systemService;

    public SystemController(ISystemService systemService)
    {
        _systemService = systemService;
    }

    [HttpPost("/api/Sbscription")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> RegisterSubscriptionInDatabaseAsync([FromBody]SubscriptionDTO subscribtionDto)
    {
        var state = await _systemService.RegisterSubscriptionInDatabaseAsync(subscribtionDto);
        if (state == 200) return Ok();
        if (state == 404) return NotFound();
        if (state == 404) return Conflict();

        return NotFound();
    }
    
    
    
}
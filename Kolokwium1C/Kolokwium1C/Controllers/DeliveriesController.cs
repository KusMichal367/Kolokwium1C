using Kolokwium1C.Models;
using Kolokwium1C.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;

namespace Kolokwium1C.Controllers;

[Route("api/[controller]")]
[ApiController]

public class DeliveriesController : ControllerBase
{
    private readonly IDeliveriesService _ideliveriesService;

    public DeliveriesController(IDeliveriesService ideliveriesService)
    {
        _ideliveriesService = ideliveriesService;
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDelivery(int id)
    {
        try
        {
            var delivery = await _ideliveriesService.GetDeliveryAsync(id);
            return Ok(delivery);
        }
        catch (ArgumentException ex)
        {
            return NotFound(ex.Message);
        }
        catch (ApplicationException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (SqlException ex)
        {
            return StatusCode(500, ex.Message);
        }

    }



}
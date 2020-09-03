using CB.AplicationCore.Interfaces;
using CB.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CuentaAhorroController : ControllerBase
    {
        readonly ICuentaAhorroService cuentaAhorroService;

        public CuentaAhorroController(ICuentaAhorroService cuentaAhorroService)
        {
            this.cuentaAhorroService = cuentaAhorroService;
        }

        [HttpGet("{productoId}")]
        public IActionResult GetCuentaAhorroByProductoId(int productoId)
        {
            var result = cuentaAhorroService.GetCuentaAhorroByProductoId(productoId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

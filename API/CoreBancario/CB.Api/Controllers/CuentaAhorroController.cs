using CB.AplicationCore.Interfaces;
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

            return Ok(result);
        }
    }
}

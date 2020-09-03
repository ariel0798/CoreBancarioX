using CB.AplicationCore.Interfaces;
using CB.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarjetaCreditoController : ControllerBase
    {
        readonly ITarjetaCreditoService tarjetaCreditoService;

        public TarjetaCreditoController(ITarjetaCreditoService tarjetaCreditoService)
        {
            this.tarjetaCreditoService = tarjetaCreditoService;
        }

        [HttpGet("{productoId}")]
        public IActionResult GetTarjetaCreditoByProductoId(int productoId)
        {
            var result = tarjetaCreditoService.GetTarjetaCreditoByProductoId(productoId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

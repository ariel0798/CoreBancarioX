using CB.AplicationCore.Interfaces;
using CB.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        readonly IProductoService productoService;

        public ProductoController(IProductoService productoService)
        {
            this.productoService = productoService;
        }
        [HttpGet("{productoId}")]
        public IActionResult GetProductoByProductoId(int productoId)
        {
            var result = productoService.GetProductoByProductoId(productoId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
        [HttpGet("cliente/{clienteId}/tipo-producto/{tipoProducto}")]
        public IActionResult GetProductoByProductoId(int clienteId,int tipoProducto)
        {
            var result = productoService.GetListProductosByClienteIdAndTipoProducto(clienteId, tipoProducto);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

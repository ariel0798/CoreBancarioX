using CB.AplicationCore.Interfaces;
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

            return Ok(result);
        }
        [HttpGet("cliente/{clienteId}/tipo-producto/{tipoProducto}")]
        public IActionResult GetProductoByProductoId(int clienteId,int tipoProducto)
        {
            var result = productoService.GetListProductosByClienteIdAndTipoProducto(clienteId, tipoProducto);

            return Ok(result);
        }
    }
}

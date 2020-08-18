using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.AplicationCore.Interfaces;
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

            return Ok(result);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.AplicationCore.Interfaces;
using CB.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HistorialTransaccionController : ControllerBase
    {
        readonly IHistorialTransaccionService historialTransaccionService;

        public HistorialTransaccionController(IHistorialTransaccionService historialTransaccionService)
        {
            this.historialTransaccionService = historialTransaccionService;
        }

        [HttpGet("{historialTransaccionId}")]
        public IActionResult GetHistorialTransaccionByHistorialTransaccionId(int historialTransaccionId)
        {
            var result = historialTransaccionService.GetHistorialTransaccionByHistorialTransaccionId(historialTransaccionId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("producto/{productoId}")]
        public IActionResult GetListHistorialTransaccionesByProductoId(int productoId)
        {
            var result = historialTransaccionService.GetListHistorialTransaccionesByProductoId(productoId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

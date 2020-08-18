using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CB.AplicationCore.Interfaces;
using CB.Common.DTOs.DtoIn;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransaccionController : ControllerBase
    {
        readonly ITransaccionService transaccionService;

        public TransaccionController(ITransaccionService transaccionService)
        {
            this.transaccionService = transaccionService;
        }

        [HttpGet("{transaccionId}")]
        public IActionResult GetTransaccionByTransaccionId(int transaccionId)
        {
            var result = transaccionService.GetTransaccionByTransaccionId(transaccionId);

            return Ok(result);
        }

        [HttpGet("producto-origen/{productoOrigenId}")]
        public IActionResult GetListTransaccionesByProductoOrigenId(int productoOrigenId)
        {
            var result = transaccionService.GetListTransaccionesByProductoOrigenId(productoOrigenId);

            return Ok(result);
        }

        [HttpPost]
        public IActionResult CreateTransaccion(TransaccionDtoIn transaccionDto)
        {
            var result = transaccionService.CreateTransaction(transaccionDto);

            return Ok(result);
        }
    }
}

using System;
using CB.AplicationCore.Interfaces;
using CB.Common.DTOs.DtoIn;
using CB.Common.Enums;
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

        [HttpPost]
        public IActionResult CreateTransaccion(TransaccionDtoIn transaccionDto)
        {
            var result = transaccionService.CreateTransaction(transaccionDto);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpPost("rowuid/{rowUidTransaccion}/clave/{clave}")]
        public IActionResult EjecutarTransaccion(Guid rowUidTransaccion, int clave)
        {
            var result = transaccionService.EjecutarTransaccion(rowUidTransaccion, clave);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

using CB.AplicationCore.Interfaces;
using CB.Common.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
    {
        readonly IClienteService clienteService;

        public ClienteController(IClienteService clienteService)
        {
            this.clienteService = clienteService;
        }

        [HttpGet("{clienteId}")]
        public IActionResult GetClienteByClienteId(int clienteId)
        {
            var result = clienteService.GetClienteByClienteId(clienteId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("cedula/{cedula}")]
        public IActionResult GetClienteByCedula(string cedula)
        {
            var result = clienteService.GetClienteByCedula(cedula);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

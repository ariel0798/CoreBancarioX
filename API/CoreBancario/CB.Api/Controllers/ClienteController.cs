using CB.AplicationCore.Interfaces;
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

            return Ok(result);
        }

        [HttpGet("cedula/{cedula}")]
        public IActionResult GetClienteByCedula(string cedula)
        {
            var result = clienteService.GetClienteByCedula(cedula);

            return Ok(result);
        }
    }
}

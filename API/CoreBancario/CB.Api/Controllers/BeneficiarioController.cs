using CB.AplicationCore.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CB.Common.Enums;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiarioController : ControllerBase
    {
        readonly IBeneficiarioService beneficiarioService;

        public BeneficiarioController(IBeneficiarioService beneficiarioService)
        {
            this.beneficiarioService = beneficiarioService;
        }

        [HttpGet("{clienteBeneficiarioId}")]
        public IActionResult GetBeneficiarioByBeneficiarioId(int clienteBeneficiarioId)
        {
            var result = beneficiarioService.GetBeneficiarioByClienteBeneficiarioId(clienteBeneficiarioId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }

        [HttpGet("cliente/{clienteId}")]
        public IActionResult GetListBeneficiariosByClienteId(int clienteId)
        {
            var result = beneficiarioService.GetListBeneficiariosByClienteId(clienteId);

            if (result.Code == ResponseCode.Ok)
                return Ok(result);

            else if (result.Code == ResponseCode.Warning)
                return BadRequest(result);

            else
                return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}

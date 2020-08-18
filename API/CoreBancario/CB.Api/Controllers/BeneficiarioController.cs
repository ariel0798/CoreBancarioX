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
    public class BeneficiarioController : ControllerBase
    {
        readonly IBeneficiarioService beneficiarioService;

        public BeneficiarioController(IBeneficiarioService beneficiarioService)
        {
            this.beneficiarioService = beneficiarioService;
        }

        [HttpGet("{beneficiarioId}")]
        public IActionResult GetBeneficiarioByBeneficiarioId(int beneficiarioId)
        {
            var result = beneficiarioService.GetBeneficiarioByBeneficiarioId(beneficiarioId);

            return Ok(result);
        }

        [HttpGet("cliente/{clienteId}")]
        public IActionResult GetListBeneficiariosByClienteId(int clienteId)
        {
            var result = beneficiarioService.GetListBeneficiariosByClienteId(clienteId);

            return Ok(result);
        }
    }
}

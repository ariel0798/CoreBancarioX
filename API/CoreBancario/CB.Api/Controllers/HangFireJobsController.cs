using CB.AplicationCore.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Hangfire;

namespace CB.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HangFireJobsController : ControllerBase
    {
        readonly IHangFireJobsService hangFireJobsService;

        public HangFireJobsController(IHangFireJobsService hangFireJobsService)
        {
            this.hangFireJobsService = hangFireJobsService;
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult GenerarCargoInteresTarjetaAndGenerarCorte()
        {
            RecurringJob.AddOrUpdate(() => hangFireJobsService.GenerarCargoInteresTarjetaAndGenerarCorte(), Cron.Daily);

            return Ok("Job inicializado");
        }

        [HttpPost]
        [Route("[action]")]
        public IActionResult EliminarTransaccionesPendientes()
        {
            RecurringJob.AddOrUpdate(() => hangFireJobsService.EliminarTransaccionesPendientes(), Cron.MinuteInterval(5));

            return Ok("Job inicializado");
        }
    }
}

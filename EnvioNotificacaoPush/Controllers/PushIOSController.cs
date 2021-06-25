using EnvioNotificacaoPush.Model;
using EnvioNotificacaoPush.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvioNotificacaoPush.Controllers
{
    [Route("api/[controller]")]
    [ValidacaoTokenFixo]
    [ApiController]
    public class PushIOSController : ControllerBase
    {
        [HttpPost]
        [Route("enviar")]
        public ActionResult Enviar(PushIOSModel pushIOSModel)
        {
            var enviado = new PushIOSService().EnviarPush(pushIOSModel);
            return Ok(new { enviado = enviado });
        }
    }
}

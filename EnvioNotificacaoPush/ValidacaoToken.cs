using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace EnvioNotificacaoPush
{

    public class ValidacaoTokenFixo : ActionFilterAttribute
    {
        public static string _chave = "hdjksncu6723jkG623kfdkjfh2hjkhblkm57463";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var error = JsonConvert.SerializeObject(new { error = "Token invalida" });
            if (!filterContext.HttpContext.Request.Headers.ContainsKey("Token"))
            {
                filterContext.Result = new UnauthorizedObjectResult(error);
                return;
            }

            string tokenString = filterContext.HttpContext.Request.Headers["Token"].FirstOrDefault();
            if (_chave != tokenString)
            {
                filterContext.Result = new UnauthorizedObjectResult(error);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}

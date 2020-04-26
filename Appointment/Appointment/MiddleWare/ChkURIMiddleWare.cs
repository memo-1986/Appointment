using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace appointmentLast.MiddleWare
{
    public class ChkURIMiddleWare
    {

        private readonly RequestDelegate _next;  
        public ChkURIMiddleWare(RequestDelegate   next) {
            _next = next;
        }

        private async Task Invoke(HttpContext  contxt) {

          var  aa=contxt.Request.Body;
            await _next(contxt);
        } 
    }
}

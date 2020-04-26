using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
 using Newtonsoft.Json;
 using Serilog;
using Serilog.Events;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using appointmentLast.Utiltes;
using appoimntlastlq.Models.DB;
namespace appoimntlastlq.Middlewares
{
    public class ResponseLogMiddleware
    {
        private readonly RequestDelegate next;
        const string MessageTemplate =
         "{ApplicationName} {RemoteIpAddress} {TraceId} {RequestMethod} {RequestPath} {StatusCode} {@ErrorMsg} {Elapsed:0.0000}";
        private static readonly ILogger Log = Serilog.Log.ForContext<ResponseLogMiddleware>();
        private static string AppName;

        public ResponseLogMiddleware(RequestDelegate next, IConfiguration conf)
        {
            this.next = next;
            AppName = conf["Application:Name"];
        }

        public async Task Invoke(HttpContext context)
        {
            object Msg = null;
            var sw = Stopwatch.StartNew();
            Stream originalBody = context.Response.Body;
            try
            {
                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await next(context);


                    var statusCode = context.Response?.StatusCode;

                    var level = statusCode > 200 ? LogEventLevel.Warning : LogEventLevel.Information;

                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();
                    memStream.Position = 0;

                    await memStream.CopyToAsync(originalBody);

                    if (level == LogEventLevel.Warning)
                    {
                        ErrorMsg em = GetObject(responseBody);
                        if (em != null)
                            Msg = new { title = em.Title, message = em.Message };
                    }
                    sw.Stop();
                    var log = level == LogEventLevel.Error ? LogForErrorContext(context) : Log;
                    string xTrace = context.Request.Headers["TraceIdReq"];
                    log.Write(level, MessageTemplate,
                               AppName,
                               context.Connection.RemoteIpAddress,
                               xTrace,
                               context.Request.Method,
                               context.Request.Path,
                               statusCode,
                               Msg,
                               sw.Elapsed.TotalMilliseconds);
                }
            }
            catch (Exception ex) when (LogException(context, sw, ex)) { }
            finally { context.Response.Body = originalBody; }

        }

        private static bool LogException(HttpContext httpContext, Stopwatch sw, Exception ex)
        {
            sw.Stop();
            string xTrace = httpContext.Request.Headers["TraceIdReq"];
            LogForErrorContext(httpContext)
                .Error(MessageTemplate,
                            AppName,
                            httpContext.Connection.RemoteIpAddress,
                            xTrace,
                            httpContext.Request.Method,
                            httpContext.Request.Path,
                            500,
                            ex.Message,
                            sw.Elapsed.TotalMilliseconds);

            return false;
        }

        private static ILogger LogForErrorContext(HttpContext httpContext)
        {
            var request = httpContext.Request;

            var result = Log
                .ForContext("RequestHeaders", request.Headers.ToDictionary(h => h.Key, h => h.Value.ToString()), destructureObjects: true)
                .ForContext("RequestHost", request.Host)
                .ForContext("RequestProtocol", request.Protocol);

            if (request.HasFormContentType)
                result = result.ForContext("RequestForm", request.Form.ToDictionary(v => v.Key, v => v.Value.ToString()));

            return result;
        }

        private static ErrorMsg GetObject(string str)
        {
            ErrorMsg error = JsonConvert.DeserializeObject<ErrorMsg>(str);
            return error;
        }
    }
}

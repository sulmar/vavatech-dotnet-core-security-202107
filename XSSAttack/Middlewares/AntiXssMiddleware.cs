using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XSSAttack.Middlewares
{
    public class AntiXssMiddleware
    {
        private readonly RequestDelegate next;

        public AntiXssMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            // Check XSS in URL
            string url = context.Request.Path.Value;

            if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            // Check XSS in Query String
            string queryString = context.Request.QueryString.Value;
            
            if (CrossSiteScriptingValidation.IsDangerousString(queryString, out _))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            // Check XSS in Body
            string body = await context.Request.PeekBodyAsync();

            if (CrossSiteScriptingValidation.IsDangerousString(body, out _))
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }

            await next(context);

        }
    }

    public static class HttpRequestExtensions
    {
        public static async Task<string> PeekBodyAsync(this HttpRequest request)
        {
            try
            {
                request.EnableBuffering();

                var buffer = new byte[Convert.ToInt32(request.ContentLength)];

                await request.Body.ReadAsync(buffer, 0, buffer.Length);

                return Encoding.UTF8.GetString(buffer);
            }
            finally
            {
                request.Body.Position = 0;
            }
        }
    }
}

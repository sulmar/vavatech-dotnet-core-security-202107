using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;

namespace XSSAttack.Middlewares
{
    public class AntiDOSMiddleware
    {
        private readonly RequestDelegate next;
        private readonly int limit;

        private readonly TimerDictionary<IPAddress, int> counters;

        public AntiDOSMiddleware(RequestDelegate next, int limit, int period)
        {
            this.next = next;
            this.limit = limit;
            counters = new TimerDictionary<IPAddress, int>(period);
        }

        public async Task Invoke(HttpContext context)
        {
            var ipAddress = context.Connection.RemoteIpAddress;

            if (counters.TryGetValue(ipAddress, out int counter))
            {
                counters[ipAddress] = ++counter;

                Trace.WriteLine($"ipAddress {ipAddress} counter {counter}");

                if (counter > limit)
                {
                    context.Response.StatusCode = StatusCodes.Status400BadRequest;
                    await context.Response.WriteAsync($"Przekroczono limit {limit} zapytań");
                }
                
            }
            else
            {
                counters.Add(ipAddress, 1);
            }

            await next(context);

        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace fakegame.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HelloController : ControllerBase
    {
        IHostApplicationLifetime applicationLifetime;
        public HelloController(IHostApplicationLifetime appLifetime)
        {
            applicationLifetime = appLifetime;
        }

        [HttpGet]
        public string Get()
        {
            Console.WriteLine($"GET /hello at {DateTime.UtcNow}");
            return $"Hello from {Dns.GetHostName()}";
        }

        [HttpGet("terminate")]
        public void Terminate()
        {
            Console.WriteLine($"GET /hello/terminate at {DateTime.UtcNow}");
            applicationLifetime.StopApplication();
        }

        [HttpGet("crash")]
        public void Crash()
        {
            Console.WriteLine($"GET /hello/crash at {DateTime.UtcNow}");
            System.Environment.FailFast("An Expected Error Occurred");
        }

        [HttpGet("morecpu")]
        public string MoreCpu()
        {
            Console.WriteLine($"GET /hello/morecpu at {DateTime.UtcNow}");
            Task.Run(() => { while(true){}});
            return $"Cpu Usage Increased on {Dns.GetHostName()}";
        }

        [HttpGet("morememory")]
        public string MoreMemory()
        {
            Console.WriteLine($"GET /hello/morememory at {DateTime.UtcNow}");
            byte[] data = new byte[1024*1024*1024];
            Array.Clear(data, 0, data.Length);
            return $"Memory Usage Increased on {Dns.GetHostName()}";

        }
    }
}
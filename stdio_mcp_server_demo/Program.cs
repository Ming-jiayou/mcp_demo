using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using stdio_mcp_server_demo.Tools;
using System.Net.Http.Headers;

namespace stdio_mcp_server_demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddMcpServer()
                .WithStdioServerTransport()
                .WithTools<TimeTools>()
                .WithTools<SearchTools>()
                .WithTools<EchoTools>();
         

            builder.Logging.AddConsole(options =>
            {
                options.LogToStandardErrorThreshold = LogLevel.Trace;
            });

            builder.Services.AddSingleton(_ =>
            {
                var client = new HttpClient();
                return client;
            });

            //SearchTools searchTools = new SearchTools();
            //await searchTools.SearchWeb("mingupup是谁？");

            await builder.Build().RunAsync();
            
        }
    }
}

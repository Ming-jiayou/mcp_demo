using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using stdio_mcp_server_demo.Tools;

namespace stdio_mcp_server_demo
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var builder = Host.CreateApplicationBuilder(args);

            builder.Services.AddMcpServer()
                .WithStdioServerTransport()
                .WithTools<TimeTools>();

            builder.Logging.AddConsole(options =>
            {
                options.LogToStandardErrorThreshold = LogLevel.Trace;
            });

            await builder.Build().RunAsync();
        }
    }
}

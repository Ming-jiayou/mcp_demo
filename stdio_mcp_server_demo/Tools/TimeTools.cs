using Microsoft.Extensions.Logging;
using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace stdio_mcp_server_demo.Tools
{
    [McpServerToolType]
    public sealed class TimeTools
    {
        private readonly ILogger<TimeTools> _logger;
        
        public TimeTools(ILogger<TimeTools> logger)
        {
            _logger = logger;
        }

        [McpServerTool, Description("获取当前时间。")]
        public DateTime GetCurrentTime()
        {
            _logger.LogInformation("调用工具: GetCurrentTime - 获取当前时间被触发");
            var now = DateTime.Now;
            _logger.LogInformation($"返回时间: {now:yyyy-MM-dd HH:mm:ss}");
            return now;
        }
    }
}

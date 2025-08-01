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
        [McpServerTool, Description("获取当前时间")]
        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }
    }
}

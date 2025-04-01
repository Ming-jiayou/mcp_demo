using CommunityToolkit.Mvvm.ComponentModel;
using MCP_Studio.ViewModels;
using ModelContextProtocol.Client;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace MCP_Studio.Models
{
    public class ServerConfig
    {
        [JsonPropertyName("command")]
        public string Command { get; set; }

        [JsonPropertyName("args")]
        public string Args { get; set; }
    }

    public partial class MCPServerConfig:ViewModelBase
    {
        [ObservableProperty]
        private string name;

        [ObservableProperty]
        private string command;
        [ObservableProperty]
        private string args;
        [ObservableProperty]
        private IList<McpClientTool> tools;
    }

    public class McpServerDictionary
    {
        [JsonPropertyName("mcpServers")]
        public Dictionary<string, ServerConfig> Servers { get; set; } = new Dictionary<string, ServerConfig>();
    }
}

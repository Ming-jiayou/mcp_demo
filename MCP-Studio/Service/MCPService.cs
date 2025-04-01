using Microsoft.Extensions.Logging.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MCP_Studio.Models;
using System.Text.Json;
using System.IO;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using ModelContextProtocol;
using ModelContextProtocol.Protocol.Transport;

namespace MCP_Studio.Service
{
    public static class MCPService
    {      
        public static async Task<List<McpClientTool>?> GetToolsAsync()
        {
            var serverList = new List<MCPServerConfig>();
            try
            {
                string jsonString = File.ReadAllText("mcp_settings.json");
                var mcpServerDictionary = JsonSerializer.Deserialize<McpServerDictionary>(jsonString);

                if (mcpServerDictionary?.Servers != null)
                {
                    foreach (var server in mcpServerDictionary.Servers)
                    {
                        MCPServerConfig serverConfig = new MCPServerConfig();
                        serverConfig.Name = server.Key;
                        serverConfig.Command = server.Value.Command;
                        serverConfig.Args = server.Value.Args;
                        serverList.Add(serverConfig);                    
                    }
                }

                McpClientOptions options = new()
                {
                    ClientInfo = new() { Name = "AIE-Studio", Version = "1.0.0" }
                };

                List<McpServerConfig> mcpServerConfigs = new List<McpServerConfig>();

                foreach (var server in serverList)
                {
                    McpServerConfig config = new()
                    {
                        Id = server.Name,
                        Name = server.Name,
                        TransportType = TransportTypes.StdIo,
                        TransportOptions = new()
                        {
                            ["command"] = server.Command,
                            ["arguments"] = server.Args
                        }
                    };
                    mcpServerConfigs.Add(config);
                }

                List<McpClientTool> mappedTools = new List<McpClientTool>();

                foreach (var config in mcpServerConfigs)
                {
                    var client = await McpClientFactory.CreateAsync(config);
                    var listToolsResult = await client.ListToolsAsync();
                    mappedTools.AddRange(listToolsResult);
                }

                return mappedTools;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error parsing settings: {ex.Message}");
                return null;
            }          
        }
    }
}

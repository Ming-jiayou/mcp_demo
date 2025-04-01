using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Text.Json.Serialization;
using MCP_Studio.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using Avalonia.Styling;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.AI;
using ModelContextProtocol.Client;
using ModelContextProtocol;
using ModelContextProtocol.Protocol.Transport;
using ModelContextProtocol.Protocol.Types;

namespace MCP_Studio.ViewModels;

public partial class MCPSettingsViewModel : ViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<MCPServerConfig> serverList;
    

    public  MCPSettingsViewModel()
    {
        ServerList = new ObservableCollection<MCPServerConfig>();
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        LoadSettings();
        await LoadAvailableTools();
    }


    private void LoadSettings()
    {
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
                    ServerList.Add(serverConfig);
                }              
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing settings: {ex.Message}");
        }
    }

    private async Task LoadAvailableTools()
    {
        McpClientOptions options = new()
        {
            ClientInfo = new() { Name = "AIE-Studio", Version = "1.0.0" }
        };

        List<McpServerConfig> mcpServerConfigs = new List<McpServerConfig>();

        foreach (var server in ServerList)
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

        foreach (var config in mcpServerConfigs)
        {
            var client = await McpClientFactory.CreateAsync(config);
            var listToolsResult = await client.ListToolsAsync();
            ServerList.Where(s => s.Name == config.Name).First().Tools = listToolsResult;
        }         
    }

    [RelayCommand]
    private async Task Test()
    {       
        
    }


}

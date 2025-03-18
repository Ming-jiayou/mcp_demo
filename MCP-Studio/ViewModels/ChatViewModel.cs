using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MCP_Studio.Models;
using System.Text.Json;
using System;
using System.IO;
using Microsoft.Extensions.AI;
using OpenAI.Chat;
using OpenAI;
using System.ClientModel;
using System.Collections.Generic;
using Tmds.DBus.Protocol;
using System.Threading.Tasks;
using MCP_Studio.Service;
using System.Linq;

namespace MCP_Studio.ViewModels;

public partial class ChatViewModel : ViewModelBase
{
    public ChatViewModel()
    {
        
    }

    [RelayCommand]
    private async Task Test()
    {
        try
        {
            string jsonString = File.ReadAllText("ChatModelSettings.json");
            var chatModelConfig = JsonSerializer.Deserialize<ChatModelConfig>(jsonString);

            ApiKeyCredential apiKeyCredential = new ApiKeyCredential(chatModelConfig.ApiKey);

            OpenAIClientOptions openAIClientOptions = new OpenAIClientOptions();
            openAIClientOptions.Endpoint = new Uri(chatModelConfig.BaseURL);

            IChatClient openaiClient = new OpenAIClient(apiKeyCredential, openAIClientOptions)
                .AsChatClient(chatModelConfig.ModelID);

            // Note: To use the ChatClientBuilder you need to install the Microsoft.Extensions.AI package
            var chatClient = new ChatClientBuilder(openaiClient)
                .UseFunctionInvocation()
                .Build();
            List<Microsoft.Extensions.AI.ChatMessage> messages = new();
            messages =
            [
               // Add a system message
               new(ChatRole.System, "You are a helpful assistant, helping us test MCP server functionality."),
            ];

            string query = "��ȡhttps://github.com/Ming-jiayou/mcp_demo���ı�����";
            // Add a user message
            messages.Add(new(ChatRole.User, query));

            var tools = await MCPService.GetToolsAsync();
            var response = await chatClient.GetResponseAsync(
                   messages,
                   new() { Tools = tools });

            // �޸�������������

            messages.AddMessages(response);
            var toolUseMessage = response.Messages.Where(m => m.Role == ChatRole.Tool);
            if (response.Messages[0].Contents.Count > 1)
            {
                //var functionMessage = response.Messages.Where(m => (m.Role == ChatRole.Assistant && m.Text == "")).Last();
                var functionCall = (FunctionCallContent)response.Messages[0].Contents[1];
                Console.ForegroundColor = ConsoleColor.Green;
                string arguments = "";
                if (functionCall.Arguments != null)
                {
                    foreach (var arg in functionCall.Arguments)
                    {
                        arguments += $"{arg.Key}:{arg.Value};";
                    }
                    Console.WriteLine($"���ú�����:{functionCall.Name};������Ϣ��{arguments}");
                    foreach (var message in toolUseMessage)
                    {
                        var functionResultContent = (FunctionResultContent)message.Contents[0];
                        Console.WriteLine($"���ù��߽����{functionResultContent.Result}");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("���ù��߲���ȱʧ");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("����û�е��ù���");
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error parsing settings: {ex.Message}");
        }

    }
} 
﻿using Microsoft.Extensions.AI;
using Microsoft.Extensions.Logging.Abstractions;
using OpenAI;
using OpenAI.Chat;
using System.ClientModel;
using System.Linq;
using dotenv.net;
using ModelContextProtocol.Client;
using ModelContextProtocol;
using ModelContextProtocol.Protocol.Transport;


namespace mcp_client_demo
{
    internal class ChatDemo
    {
        public ChatDemo() 
        {
            InitIChatClient();
        }

        public IChatClient ChatClient;
        public IList<Microsoft.Extensions.AI.ChatMessage> Messages;
        private void InitIChatClient()
        {
            DotEnv.Load();
            var envVars = DotEnv.Read();
            ApiKeyCredential apiKeyCredential = new ApiKeyCredential(envVars["API_KEY"]);

            OpenAIClientOptions openAIClientOptions = new OpenAIClientOptions();
            openAIClientOptions.Endpoint = new Uri(envVars["BaseURL"]);

            IChatClient openaiClient = new OpenAIClient(apiKeyCredential, openAIClientOptions)
                .AsChatClient(envVars["ModelID"]);

            // Note: To use the ChatClientBuilder you need to install the Microsoft.Extensions.AI package
            ChatClient = new ChatClientBuilder(openaiClient)
                .UseFunctionInvocation()
                .Build();

            Messages =
            [
                // Add a system message
                new(ChatRole.System, "You are a helpful assistant, helping us test MCP server functionality."),
            ];
        }

        public async Task<string> ProcessQueryAsync(string query, IList<McpClientTool> tools)
        {
            if(Messages.Count == 0)
            {
                Messages =
                [
                 // Add a system message
                new(ChatRole.System, "You are a helpful assistant, helping us test MCP server functionality."),
                ];
            }
            
            // Add a user message
            Messages.Add(new(ChatRole.User, query));

            var options = new ChatOptions
            {               
                Tools = [.. tools]
            };

            var response = await ChatClient.GetResponseAsync(Messages,options);
            Messages.AddMessages(response);
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
                    Console.WriteLine($"调用函数名:{functionCall.Name};参数信息：{arguments}");
                    foreach (var message in toolUseMessage)
                    {
                        var functionResultContent = (FunctionResultContent)message.Contents[0];
                        Console.WriteLine($"调用工具结果：{functionResultContent.Result}");
                    }

                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.WriteLine("调用工具参数缺失");
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("本次没有调用工具");               
            }
            Console.ForegroundColor = ConsoleColor.White;
            return response.Text;
        }
    }
    internal class Program
    {
        private static async Task<IMcpClient> GetMcpClientAsync()
        {
            DotEnv.Load();
            var envVars = DotEnv.Read();
            McpClientOptions options = new()
            {
                ClientInfo = new() { Name = "SimpleToolsConsole", Version = "1.0.0" }
            };

            var config = new McpServerConfig
            {
                Id = "test",
                Name = "Test",
                TransportType = TransportTypes.StdIo,
                TransportOptions = new Dictionary<string, string>
                {
                    ["command"] = envVars["MCPCommand"],
                    ["arguments"] = envVars["MCPArguments"],
                }
            };

            var client = await McpClientFactory.CreateAsync(config);

            return client;
        }
 
        async static Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;  // 设置输出编码
            Console.InputEncoding = System.Text.Encoding.UTF8;   // 设置输入编码
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Initializing MCP 'fetch' server");
            var client = await GetMcpClientAsync();
            Console.WriteLine("MCP 'everything' server initialized");          
            Console.WriteLine("Listing tools...");
            var listToolsResult = await client.ListToolsAsync();
            //var mappedTools = listToolsResult.Tools.Select(t => t.ToAITool(client)).ToList();
            Console.WriteLine("Tools available:");
            foreach (var tool in listToolsResult)
            {
                Console.WriteLine("  " + tool);
            }
            Console.WriteLine("\nMCP Client Started!");
            Console.WriteLine("Type your queries or 'quit' to exit.");

            ChatDemo chatDemo = new ChatDemo();

            while (true)
            {
                try
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.Write("\nQuery: ");
                    string query = Console.ReadLine()?.Trim() ?? string.Empty;

                    if (query.ToLower() == "quit")
                        break;
                    if (query.ToLower() == "clear")
                    {
                        Console.Clear();
                        chatDemo.Messages.Clear();                    
                    }
                    else 
                    {
                        string response = await chatDemo.ProcessQueryAsync(query, listToolsResult);
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine($"AI回答：{response}");
                        Console.ForegroundColor = ConsoleColor.White;
                    }                      
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nError: {ex.Message}");
                }
            }
        }
    }
}

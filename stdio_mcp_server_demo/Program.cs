using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using stdio_mcp_server_demo.Tools;
using stdio_mcp_server_demo.Models;
using System.Net.Http.Headers;
using System.Text.Json;
using System.Collections.Generic;

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

            await builder.Build().RunAsync();
            
        }

        public async static Task<Student> TestClient(string studentId)
        {
            try
            {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                
                string url = $"https://localhost:7220/api/Student/{studentId}";
                Console.WriteLine($"Sending GET request to: {url}");
                
                HttpResponseMessage response = await httpClient.GetAsync(url);
                
                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("JSON Response received successfully:");
                    Console.WriteLine(jsonResponse);
                    
                    // Deserialize JSON to Student object
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };
                    
                    Student student = JsonSerializer.Deserialize<Student>(jsonResponse, options);
                    
                    Console.WriteLine("\nDeserialized Student object:");
                    Console.WriteLine("================================");
                    Console.WriteLine(student);
                    Console.WriteLine();
                    
                    return student;
                }
                else
                {
                    Console.WriteLine($"Request failed with status code: {(int)response.StatusCode} {response.StatusCode}");
                    string errorContent = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(errorContent))
                    {
                        Console.WriteLine($"Error details: {errorContent}");
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return null;
            }
        }
    }
}

using dotenv.net;
using ModelContextProtocol.Server;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace stdio_mcp_server_demo.Tools
{
    [McpServerToolType]
    public sealed class SearchTools
    {
        public string BraveSearchApiKey { get; set; }

        public SearchTools()
        {
            // Load .env file
            DotEnv.Load();

            // Get environment variables from .env file
            var envVars = DotEnv.Read();

            BraveSearchApiKey = envVars["BraveSearchApiKey"];
        }

        [McpServerTool, Description("从网络搜索获取答案。")]
        public async Task<string> SearchWeb([Description("提问的问题")] string query)
        {
            string apiKey = BraveSearchApiKey;

            // Set the request URL
            string url = $"https://api.search.brave.com/res/v1/web/search?q={query}";

            // Create an HttpClient instance
            using (HttpClient client = new HttpClient())
            {
                // Set request headers
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("x-subscription-token", apiKey);

                // Send GET request
                HttpResponseMessage response = await client.GetAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    // Request succeeded, parse the returned JSON data
                    string jsonResponse = await response.Content.ReadAsStringAsync();
                    using JsonDocument doc = JsonDocument.Parse(jsonResponse);
                    JsonElement root = doc.RootElement;
                    JsonElement results = root.GetProperty("web").GetProperty("results");

                    // Build the results string
                    string resultsStr = "";
                    foreach (JsonElement result in results.EnumerateArray())
                    {
                        string title = result.GetProperty("title").GetString() ?? "";
                        string resultUrl = result.GetProperty("url").GetString() ?? "";
                        string description = result.GetProperty("description").GetString() ?? "";

                        resultsStr += $"Title: {title}\nURL: {resultUrl}\nDescription: {description}\n\n";
                    }
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine(resultsStr);
                    Console.ForegroundColor = ConsoleColor.Green;
                    return resultsStr;
                }
                else
                {
                    // Request failed, print error message
                    Console.WriteLine($"Request failed with status code: {response.StatusCode}");
                    return $"Error: Search request failed with status code {response.StatusCode}";
                }
            }
        }
    }
}

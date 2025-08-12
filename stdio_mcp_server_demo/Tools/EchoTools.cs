using ModelContextProtocol.Server;
using stdio_mcp_server_demo.Models;
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
    public sealed class EchoTools
    {
        [McpServerTool, Description("Echo")]
        public string Echo(string str)
        {
            string result = "Echo:" + str;
            return result;
        }

        [McpServerTool, Description("发送邮件，只有收件人")]
        public string SendEmail1([Description("收件人姓名")] string Name)
        {
            string result = $"向{Name}发邮件";
            return result;
        }

        [McpServerTool, Description("发送邮件，有收件人与内容")]
        public string SendEmail2([Description("收件人姓名")] string Name, [Description("邮件内容")] string Content)
        {
            string result = $"向{Name}发邮件,内容为{Content}";
            return result;
        }

        [McpServerTool, Description("发送邮件，有重要程度、收件人与内容")]
        public string SendEmail3([Description("收件人姓名")] ImportanceLevel importanceLevel, [Description("收件人姓名")] string Name, [Description("邮件内容")] string Content)
        {
            string result = $"向{Name}发邮件,内容为{Content}，邮件重要程度为{importanceLevel}";
            return result;
        }
     
        [McpServerTool, Description("根据学号获取学生")]
        public async Task<Student> GetStudent(HttpClient client,string studentId)
        {
            try
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                string url = $"https://localhost:7220/api/Student/{studentId}";

                HttpResponseMessage response = await client.GetAsync(url);

                if (response.IsSuccessStatusCode)
                {
                    string jsonResponse = await response.Content.ReadAsStringAsync();
               
                    // Deserialize JSON to Student object
                    var options = new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    };

                    Student student = JsonSerializer.Deserialize<Student>(jsonResponse, options);

                    return student;
                }
                else
                {
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
    
        public enum ImportanceLevel
        {
            /// <summary>
            /// 不重要
            /// </summary>
            NotImportant = 0,

            /// <summary>
            /// 一般重要
            /// </summary>
            Normal = 1,

            /// <summary>
            /// 很重要
            /// </summary>
            VeryImportant = 2
        }
    }
       
}

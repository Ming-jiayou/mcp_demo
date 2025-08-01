using CommunityToolkit.Mvvm.Input;
using MCP_Studio.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MCP_Studio.ViewModels
{
    public partial class TTSViewModel : ViewModelBase
    {
        [RelayCommand]
        private async Task Test()
        {
            using (var client = new HttpClient())
            {
                // 设置请求的 URL
                string url = "https://api.siliconflow.cn/v1/audio/speech";
                // 设置请求头
                client.DefaultRequestHeaders.Add("Authorization", "Bearer sk-bjcffjvmlsbkaduhgpleszxqszmbvsxkmqzxobodpefwfsfw");
                // 构建请求体
                string requestBody = "{\"input\": \"你好啊\", \"response_format\": \"opus\", \"stream\": false, \"speed\": 1, \"gain\": 0, \"model\": \"FunAudioLLM/CosyVoice2-0.5B\", \"voice\": \"FunAudioLLM/CosyVoice2-0.5B:alex\"}";
                // 将请求体转换为 HTTP 内容，并设置 Content-Type
                var content = new StringContent(requestBody, Encoding.UTF8, "application/json");
                // 发送 POST 请求
                using (var response = await client.PostAsync(url, content))
                {
                    // 检查响应状态
                    if (response.IsSuccessStatusCode)
                    {
                        var result = await response.Content.ReadAsByteArrayAsync();
                        NAudioService audioService = new NAudioService();
                        audioService.PlayOpusData(result);
                    }
                    else
                    {
                        Console.WriteLine("Request failed with status code: " + response.StatusCode);
                    }
                }
            }
        }
    }
}

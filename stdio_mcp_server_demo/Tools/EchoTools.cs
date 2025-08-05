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

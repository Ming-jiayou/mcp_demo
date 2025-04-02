# Building a Simple Cross-Platform MCP Client using Avalonia/C#

## Introduction

A few days ago, I introduced building a MCPclient in C#.

Recently, I've been learning Avalonia, so I wanted to use Avalonia to implement a simple cross-platform MCP client.

By connecting to someone else's or my own MCP server, I can leverage AI to do many interesting things.

Next, when I have time, I will also share some fun MCP servers with everyone.

## Effect

Tools possessed by the connected MCP server:

![image-20250318174336737](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318174336737.png)

Utilizing these MCP servers:

duckduckgo_mcp

![image-20250318174522899](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318174522899.png)

fetch-mcp

![image-20250318175233774](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318175233774.png)

sqlite-mcp

![image-20250318175711695](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318175711695.png)

Due to the model, it may not be possible to succeed at once sometimes.

Ask AI the question: "Retrieve all product information from the products table where the shelf life is greater than 30 days".

![image-20250318180038096](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318180038096.png)

![image-20250318180054915](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318180054915.png)

The Chinese display still has issues, but the data has indeed been retrieved from the database.

## Practice

```cmd
git clone https://github.com/Ming-jiayou/mcp_demo.git
```

Enter the mcp_demo\MCP-Studio folder, rename ChatModelSettings.json.example to ChatModelSettings.json, and fill in the large model information, for example, with Silicon Flow:

![image-20250318181015554](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318181015554.png)

Open mcp_settings.json to configure your MCP server, my example is as follows:

![image-20250318181133621](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318181133621.png)

Run the program.

If the MCP server tools can be displayed on the MCP Settings page, it indicates that the server connection is successful.

![image-20250318181230749](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318181230749.png)

Now you can play with these MCP servers, but remember to use a model with tool use capabilities!

The complete code has been uploaded to GitHub, located at: https://github.com/Ming-jiayou/mcp_demo.

**Recommended Reading**

[Creating an MCP Client using C#](https://mp.weixin.qq.com/s/Jd6irZiwKuRn3IselQAhNQ)

[Let's play with mcp_server_sqlite and have AI help you with CRUD (Create, Read, Update, Delete) operations!](https://mp.weixin.qq.com/s/dASBZQfC3aWsw_85V2tn-g)

[Using fetch_mcp to enable Cline to fetch and retrieve web content.](https://mp.weixin.qq.com/s/iG2cFhYf0tAqQTxfKtlsQw)

[Create an MCP server and use it in Cline to enhance custom functionality.](https://mp.weixin.qq.com/s/nkJ3pqvsBX7HQEkTVI0Fvw)




简体中文|[English](./README.md) 

# 使用Avalonia/C#构建一个简易的跨平台MCP客户端

## 前言

前几天介绍了在C#中构建一个MCP客户端。

最近正在学习Avalonia，所以就想用Avalonia实现一个简易的跨平台MCP客户端。接入别人写的或者自己写的MCP服务器就可以利用AI做很多有意思的事情。

接下来我有时间也会和大家继续分享一些好玩的MCP服务器。

## 效果

展示连接的MCP服务器的工具：

![image-20250318174336737](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318174336737.png)

使用这些MCP服务器：

duckduckgo_mcp

![image-20250318174522899](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318174522899.png)

fetch-mcp

![image-20250318175233774](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318175233774.png)

sqlite-mcp

![image-20250318175711695](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318175711695.png)

由于模型的原因有时候可能没法一次就成功。

问AI这个问题：“获取products表中所有保质期大于30天的商品信息”。

![image-20250318180038096](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318180038096.png)

![image-20250318180054915](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318180054915.png)

中文显示还有问题，但是数据确实是从数据库中读取出来的了。

## 实践

```cmd
git clone https://github.com/Ming-jiayou/mcp_demo.git
```

进入mcp_demo\MCP-Studio文件夹，将ChatModelSettings.json.example修改为ChatModelSettings.json，填入大模型信息，以硅基流动为例：

![image-20250318181015554](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318181015554.png)

打开mcp_settings.json设置你的MCP服务器，我的示例如下所示：

![image-20250318181133621](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318181133621.png)

运行程序。

在MCPSettings页如果能显示MCP服务器的工具，说明服务器连接成功。

![image-20250318181230749](https://mingupupup.oss-cn-wuhan-lr.aliyuncs.com/imgs/image-20250318181230749.png)

现在就可以玩耍这些MCP服务器咯，不过要注意得用一个有工具调用能力的模型哦！！

全部代码已经放到GitHub，地址：https://github.com/Ming-jiayou/mcp_demo。

**推荐阅读**

[使用C#创建一个MCP客户端](https://mp.weixin.qq.com/s/Jd6irZiwKuRn3IselQAhNQ)

[一起来玩mcp_server_sqlite，让AI帮你做增删改查！！](https://mp.weixin.qq.com/s/dASBZQfC3aWsw_85V2tn-g)

[通过fetch_mcp，让Cline能够获取网页内容。](https://mp.weixin.qq.com/s/iG2cFhYf0tAqQTxfKtlsQw)

[创建一个MCP服务器，并在Cline中使用，增强自定义功能。](https://mp.weixin.qq.com/s/nkJ3pqvsBX7HQEkTVI0Fvw)

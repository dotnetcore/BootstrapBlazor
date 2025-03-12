<h1 align="center">Bootstrap Blazor 组件库</h1>

<div align="center">
<h2>Bootstrap Blazor 是一套基于 Bootstrap 和 Blazor 的企业级组件库</h2>
<h4>.NET 基金会成员项目</h4>

[![License](https://img.shields.io/github/license/dotnetcore/BootstrapBlazor.svg?logo=git&logoColor=red)](https://github.com/dotnetcore/BootstrapBlazor/blob/main/LICENSE)
[![Nuget](https://img.shields.io/nuget/v/BootstrapBlazor.svg?color=red&logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Nuget](https://img.shields.io/nuget/dt/BootstrapBlazor.svg?logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Repo Size](https://img.shields.io/github/repo-size/dotnetcore/BootstrapBlazor.svg?logo=github&logoColor=green&label=repo)](https://github.com/dotnetcore/BootstrapBlazor)
[![Commit Date](https://img.shields.io/github/last-commit/dotnetcore/BootstrapBlazor/main.svg?logo=github&logoColor=green&label=commit)](https://github.com/dotnetcore/BootstrapBlazor)
[![Github build](https://img.shields.io/github/actions/workflow/status/dotnetcore/BootstrapBlazor/build.yml?branch=main&?label=main&logo=github)](https://github.com/dotnetcore/BootstrapBlazor/actions?query=workflow%3A%22Build+Project%22+branch%3Amain)
[![codecov](https://codecov.io/gh/dotnetcore/BootstrapBlazor/branch/main/graph/badge.svg?token=5SXIWHXZC3)](https://codecov.io/gh/dotnetcore/BootstrapBlazor)
</div>

<a href="README.md">English</a> | <span>中文</span>

---
## .NET Foundation
[<img align="right" src="https://github.com/dotnet-foundation/swag/blob/main/logo/dotnetfoundation_v4.png?raw=true" width="68px" />](https://dotnetfoundation.org/projects/project-detail/bootstrap-blazor)
<p>该项目属于 <a href="https://www.dotnetfoundation.org/">.NET 基金会</a> ，并根据其 <a href="https://www.dotnetfoundation.org/code-of-conduct">行为准则</a> 运作。</p>

## 社区与项目
目前 `BootstrapBlazor` 团队由六名热爱开源的技术达人组成，其中有四名 **微软最有价值专家(Microsoft MVP)** ；成立了大约 2000 人的 `Blazor 中文社区` 积极响应，只讨论 Blazor 相关技术

本组件库内置 **200** 多个组件，作者所在几家公司的项目均在重度使用，大多数组件都是在实际项目中提炼出来，非常适合国人操作习惯，大大节约开发时间，不像有些开源作品甚至作者本人都不使用，遇到问题从根本上无法解决，提交 Issue 也是让其自行解决并帮忙 PR 到其仓库

作者与团队积极处理 Issue 社区中积极回答问题，绝对不会一个开源仓库有几百上千 Issue 不予处理，当需求合理，即使是新功能也会积极响应并尽快提供新版本不会出现让提交者自己实现功能并提交 PR 的行为

针对个别白嫖党、伸手党会亮出杀手锏口号（付费提供远程支持），其实给钱也没时间搭理，逼不得已只能在这里明确一下，一些黑粉跑去其他社区说我们有卖课广告

微软 MVP 列表（按字母排序）

- Argo Zhang [链接地址](https://mvp.microsoft.com/en-us/PublicProfile/5004174)
- Alex Chow [链接地址](https://mvp.microsoft.com/en-us/PublicProfile/5005078)
- Guohao Wang [链接地址](https://mvp.microsoft.com/en-us/PublicProfile/5005089)
- Xiang Ju [链接地址](https://mvp.microsoft.com/en-us/PublicProfile/5005108)

## 生态伙伴
WTM 快速开发框架，设计的核心理念就是 "尽一切可能提高开发效率"。WTM框架把常规编码结构化，重复编码自动化，它不仅是一个框架，它是强有力的生产力工具！目前 WTM 快速开发框架已深度集成 Blazor 欢迎大家使用 [传送门](https://wtmdoc.walkingtec.cn)

<a href="https://wtmdoc.walkingtec.cn" target="_blank"><img src="http://images.gitee.com/uploads/images/2021/0718/194451_5b6cff04_554725.png" width="100px" /></a>

WTM 快速开发框架，设计的核心理念就是 "尽一切可能提高开发效率"。WTM框架把常规编码结构化，重复编码自动化，它不仅是一个框架，它是强有力的生产力工具！目前 WTM 快速开发框架已深度集成 Blazor 欢迎大家使用 [传送门](https://wtmdoc.walkingtec.cn)

## 开发环境搭建
1. 安装 .net core sdk 最新版 [官方网址](http://www.microsoft.com/net/download)
2. 安装 Visual Studio 2022 最新版 [官方网址](https://visualstudio.microsoft.com/vs/getting-started/)
3. 获取本项目代码 [BootstrapBlazor](https://github.com/dotnetcore/BootstrapBlazor?wt.mc_id=DT-MVP-5004174)

### 克隆代码
```shell
git clone https://github.com/dotnetcore/BootstrapBlazor.git
cd BootstrapBlazor/src/BootstrapBlazor.Server
dotnet run
```

## 快速安装指南

### 安装包
```shell
dotnet add package BootstrapBlazor
```

### 添加默认命名空间到 `_Imports.razor`
```razor
@using BootstrapBlazor.Components
```

### 添加 `BootstrapBlazorRoot` 到 `MainLayout.razor`
```razor
<BootstrapBlazorRoot>
    @Body
</BootstrapBlazorRoot>
```

### 添加样式到 HTML Head 中
具体是 **index.html** 或者 **_Layout.cshtml/_Host.cshtml/App.razor** 取决项目类型 `WebAssembly` 还是 `Server`
```razor
<link rel="stylesheet" href="_content/BootstrapBlazor/css/bootstrap.blazor.bundle.min.css" />
```

### 增加脚本到 Html Body 结尾处
```razor
<script src="_content/BootstrapBlazor/js/bootstrap.blazor.bundle.min.js"></script>
```

### 增加服务 `Program.cs`
```csharp
builder.Services.AddBootstrapBlazor();
```

## 用法示例
```razor
<Display Value="@_text"></Display>
<Button Text="Button" OnClick="@ClickButton"></Button>

@code {
    private string? _text;
    private void ClickButton(MouseEventArgs e)
    {
        _text = DateTime.Now.ToString();
    }
}
```

## 安装项目模板
```cscharp
dotnet new install Bootstrap.Blazor.Templates::*
```

## 更多文档
- [项目模板](https://www.blazor.zone/template)
- [快速上手](https://www.blazor.zone/install-server)

## 相关资源
- [Blazor 官方文档](https://docs.microsoft.com/zh-cn/aspnet/core/blazor/?WT.mc_id=DT-MVP-5004174)
- [生成 Blazor Web 应用](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/?WT.mc_id=DT-MVP-5004174)
- [什么是 Blazor](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/2-understand-blazor-webassembly?WT.mc_id=DT-MVP-5004174)
- [练习 - 配置开发环境](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/3-exercise-configure-enviromnent?WT.mc_id=DT-MVP-5004174)
- [Blazor 组件](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/4-blazor-components?WT.mc_id=DT-MVP-5004174)
- [练习 - 添加组件](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/5-exercise-add-component?WT.mc_id=DT-MVP-5004174)
- [数据绑定和事件](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/6-csharp-razor-binding?WT.mc_id=DT-MVP-5004174)
- [练习 - 数据绑定和事件](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/7-exercise-razor-binding?WT.mc_id=DT-MVP-5004174)
- [总结](https://docs.microsoft.com/zh-cn/learn/modules/build-blazor-webassembly-visual-studio-code/8-summary?WT.mc_id=DT-MVP-5004174)

## QQ交流群

[![QQ](https://img.shields.io/badge/QQ-795206915-greenlight.svg?logo=tencent%20qq&logoColor=red)](https://qm.qq.com/cgi-bin/qm/qr?k=1-jF9-5WA_3GFiJgXem2U_AQfqbdyOlV&jump_from=webapi) [![QQ](https://img.shields.io/badge/QQ-675147445-greenlight.svg?logo=tencent%20qq&logoColor=red)](https://qm.qq.com/cgi-bin/qm/qr?k=Geker7hCXK0HC-J8_974645j_n6w0OE0&jump_from=webapi)

## 视频教程

B 站视频集锦 [传送门](https://space.bilibili.com/660853738/channel/index)

## 演示地址
[![website](https://img.shields.io/badge/online-https://www.blazor.zone-success.svg?color=greenlight&logo=buzzfeed&logoColor=green)](https://www.blazor.zone)

## GVP 奖杯
![项目奖杯](https://images.gitee.com/uploads/images/2021/0112/120620_e596ac3c_554725.png "GVP.png")

## 开源协议
[![Gitee license](https://img.shields.io/github/license/dotnetcore/BootstrapBlazor.svg?logo=git&color=&logoColor=green)](https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/master/LICENSE)

## 特别鸣谢

### 上海智通建设发展股份有限公司

特别鸣谢对本项目的鼎力赞助 **10000** 元

<img src="https://gitee.com/Longbow/Pictures/raw/master/BootstrapBlazor/Donate@x2.png" width="552px;" />

## 参与贡献

1. Fork 本项目
2. 新建 Feat_xxx 分支
3. 提交代码
4. 新建 Pull Request

## 行为准则

本项目采用了《贡献者公约》所定义的行为准则，以明确我们社区的预期行为。
更多信息请见 [.NET Foundation Code of Conduct](https://dotnetfoundation.org/code-of-conduct).

## 捐助

如果这个项目对您有所帮助，请扫下方二维码打赏一杯咖啡。

<img src="https://gitee.com/Longbow/Pictures/raw/master/WeChat/BarCode@2x.png" width="382px;" />

## 赞助商
感谢 [JetBrains](https://jb.gg/OpenSourceSupport) 提供的免费开源 License
<img src="https://resources.jetbrains.com/storage/products/company/brand/logos/jb_beam.svg" width="100px" align="right" />

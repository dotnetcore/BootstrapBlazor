---
name: bb-api
description: 随 BootstrapBlazor NuGet 包提供的组件 API 参考。编写或修改 Blazor/Razor 组件标记和代码、查询组件参数、事件、方法或可注入服务，以及修复 BootstrapBlazor 组件相关的编译错误时使用。涵盖核心组件和服务。
---

# BootstrapBlazor API 参考

此 skill 随 `BootstrapBlazor` NuGet 包分发，文档与安装的版本一致。组件文档从源码生成，正文为中文。

不要凭记忆或网络搜索猜测 API。如果这些文件中没有某个参数、事件回调或方法，应说明文档未收录，不要编造。

## 核心组件和服务

1. 阅读 [references/llms.txt](references/llms.txt)，其中按类别列出核心组件、服务及相关接口。
2. 阅读所需组件或服务对应的 `references/components/<Name>.txt`。服务文档也位于 `components/` 目录。

始终优先读取当前项目实际引用的 `BootstrapBlazor` 版本，不要选择全局缓存中的最高版本。本地文档不可用时，可使用 `bb-llms get <ComponentName> --base-url https://www.blazor.zone/llms --no-cache` 查询官网，并说明官网版本可能与项目引用的版本不同；命令或官网不可用时报告原因，不要编造 API。

## 使用约定

- 参数名称、类型和事件回调签名必须与文档完全一致。
- 对于可注入服务，按照服务文档中的代码示例注册。
- 此 skill 是只读参考资料，不包含脚本；读取本地文档无需运行命令。

---
name: bb-api
description: 随 BootstrapBlazor NuGet 包提供的组件 API 参考。编写或修改 Blazor/Razor 组件标记和代码、查询组件参数、事件、方法或可注入服务，以及修复 BootstrapBlazor 组件相关的编译错误时使用。涵盖核心组件和已安装的 BootstrapBlazor.* 扩展包。
---

# BootstrapBlazor API 参考

此 skill 随 `BootstrapBlazor` NuGet 包分发，文档与安装的版本一致。组件文档从源码生成，正文为中文。

不要凭记忆或网络搜索猜测 API。如果这些文件中没有某个参数、事件回调或方法，应说明文档未收录，不要编造。

## 核心组件和服务

1. 阅读 [references/llms.txt](references/llms.txt)，其中按类别列出所有核心组件和可注入服务。
2. 阅读所需组件或服务对应的 `references/components/<Name>.txt`。服务文档也位于 `components/` 目录。

## 扩展组件

此 skill **不包含**扩展组件文档。每个 `BootstrapBlazor.*` 扩展包在包内的 `docs/llms/` 目录中提供自己的文档（一个 `llms.txt` 索引和每个类型对应的 `<Name>.txt`），并且不包含 `SKILL.md`。

查找扩展类型时，先使用消费项目的还原数据：

1. 阅读 `references/extensions/llms.txt`。每行将类型名称映射到所属的包 ID 和包内文档路径。
2. 打开**当前项目的** `obj/project.assets.json`：
   - `packageFolders` 列出 NuGet global-packages 根目录。
   - `libraries` 中以 `<PackageId>/<Version>` 为键的条目，其 `path` 是相对于 global-packages 根目录的包目录，已包含解析出的版本；`files` 数组列出该包的 `docs/llms/*.txt` 文件。
3. 拼接绝对路径 `<global-packages root>/<libraries[...].path>/docs/llms/<Name>.txt` 并读取。可以先查看该包的 `docs/llms/llms.txt`，了解其提供的所有文档。

如果 `project.assets.json` 中没有该包 ID，说明扩展包未安装。请用户添加对应的 `BootstrapBlazor.*` 包引用，不要猜测其 API。

始终优先读取项目实际解析出的版本（包含在 `libraries[...].path` 中），不要选择全局缓存中的最高版本。本地文档不可用时，可使用 `bb-llms get <ComponentName> --base-url https://www.blazor.zone/llms --no-cache` 查询官网，并说明官网版本可能与项目引用的版本不同；命令或官网不可用时报告原因，不要编造 API。

## 使用约定

- 参数名称、类型和事件回调签名必须与文档完全一致。
- 对于可注入服务，按照服务文档中的代码示例注册。
- 如果某个类型同时出现在核心索引和扩展路由表中，优先使用该类型的扩展文档。
- 此 skill 是只读参考资料，不包含脚本；读取本地文档无需运行命令。

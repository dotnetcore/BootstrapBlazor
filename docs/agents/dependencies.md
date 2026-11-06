# 依赖说明

排查依赖源码、添加或更新依赖前必须读取本文件，并遵守根 `AGENTS.md`。

## 依赖源码

BootstrapBlazor.Server 项目依赖项源码大部分位于 BootstrapBlazor.Extensions：

- GitHub：https://github.com/BootstrapBlazor/BootstrapBlazor.Extensions
- 国内镜像：https://gitee.com/LongbowEnterprise/BootstrapBlazor.Extensions

排查依赖问题时先确认所属项目，再查阅对应源码，不要仅根据调用方式猜测 API。

## 依赖变更约束

- 不主动引入新 NuGet 包，优先使用项目已有能力。
- 不修改 `Directory.Packages.props`，除非说明原因。
- 必要的依赖变更应说明用途、兼容性及对组件工程、示例工程的影响。

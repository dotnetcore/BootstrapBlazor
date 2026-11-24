---
name: update-extension-dependency
description: 按组件名将 BootstrapBlazor.Server 的 BB.Extensions NuGet 引用同步到包含最新稳定依赖的已发布版本。用于更新 dom2image 的 SnapDOM、driverjs 的 DriverJs、tableexport 的 MiniExcel，或通过 upstream 模式在 BB.Extensions 中更新依赖源码和组件版本。
---

# 更新 BB.Extensions 组件依赖

开始前读 [根 AGENTS.md](../../../AGENTS.md) 和 [依赖说明](../../../docs/agents/dependencies.md)。组件名和模式大小写不敏感。默认同步 BB 主仓库的示例站点引用。添加 `upstream` 时，在 BB.Extensions 中执行上游依赖更新。缺少组件名、组件未知或模式无效时，列出可用调用方式。

```text
$update-extension-dependency dom2image
$update-extension-dependency driverjs
$update-extension-dependency tableexport
$update-extension-dependency driverjs upstream
```

## 组件映射

在 BB.Extensions 中，以下项目位于 `src/components/<项目名>/<项目名>.csproj`，项目名也是发布的 NuGet 包 ID。

- `dom2image` 对应 `BootstrapBlazor.Dom2Image`，上游是 npm 的 `@zumer/snapdom`。npm 归档的 `package/dist/snapdom.mjs` 对应组件内的 `wwwroot/lib/snapdom.mjs`。检查 `.nupkg` 中 `staticwebassets/lib/snapdom.mjs` 的文件头版本。
- `driverjs` 对应 `BootstrapBlazor.DriverJs`，上游是 npm 的 `driver.js`。npm 归档的 `package/dist/driver.js.mjs` 和 `package/dist/driver.css` 对应组件 `wwwroot/` 下的同名文件。以依赖更新记录定位候选版本，再将 `.nupkg` 的 `staticwebassets/driver.js.mjs` 和 `staticwebassets/driver.css` 与同一个 npm 归档的产物比对。可以统一 CRLF 和 LF 后再比较。
- `tableexport` 对应 `BootstrapBlazor.TableExport`，上游是 NuGet 的 `MiniExcel`。组件项目通过 `MiniExcelVersion` 设置 `net8.0` 至 `net11.0` 的依赖版本，`net6.0` 和 `net7.0` 使用各自固定的版本。读取 `.nupkg` 的 `.nuspec`，核对 BB 示例站点实际目标框架对应的 `MiniExcel` 依赖声明。

扩展支持时，按同一格式登记组件项目、上游包和可验证的版本证据。

## 同步 BB 主仓库

1. 在 BB 仓库根目录查看 `git status --short` 和近期同组件提交。读取 [示例站点项目](../../../src/BootstrapBlazor.Server/BootstrapBlazor.Server.csproj) 中该组件现有的 `PackageReference` 以及项目使用的目标框架。
2. 按组件映射查询上游最新稳定版和 NuGet 上已列出的稳定组件包。npm 可用 `npm view "<包名>@latest" version dist.tarball --json --registry https://registry.npmjs.org/`，并核实 `latest` 指向稳定版。NuGet 可用 [V3 服务索引](https://api.nuget.org/v3/index.json) 中的查询服务和 `PackageBaseAddress` 获取版本与 `.nupkg`。按对应包生态的版本规则比较版本。
3. 核对当前引用及候选组件包内的版本证据和目标框架兼容性。选择已包含目标上游版本的最新兼容稳定组件包。若最新版上游依赖还没有可用的组件包，报告目标版本、当前可用的组件包及其依赖版本。若发布源或包内容无法核实，说明缺少的证据。只有确认存在比当前引用更新的目标组件包时才编辑项目文件。
4. 仅修改所选组件的 `PackageReference` 版本。运行 `dotnet build src/BootstrapBlazor.Server` 和 `git diff --check`，核对项目文件的差异。TableExport 构建后同时核对示例站点 `obj/project.assets.json` 中该目标框架实际解析的 MiniExcel 版本。

## 在 BB.Extensions 更新上游依赖

1. 优先使用用户指定的 BB.Extensions 检出目录，其次查找已确认远端的本地检出。需要建立独立检出时，使用 [BB.Extensions 官方仓库](https://github.com/BootstrapBlazor/BootstrapBlazor.Extensions)。在目标仓库中读取适用的项目规则，检查工作区状态和近期同组件提交，保留已有改动。
2. 如果目标仓库有 `.agents/skills/update-dependency/SKILL.md`，复用其中对应组件的版本查询、依赖更新和构建步骤。否则按上面的映射核对当前项目文件和上游稳定版，确认上游有更新后再编辑文件。更换 npm 产物时，从官方同版本归档提取映射指定的文件，保留许可证和现有组件依赖的模块导出。TableExport 更新共享版本属性前应检查目标框架兼容性，保留旧框架的固定版本。跨大版本时，检查发布说明和现有调用点，按构建结果处理必要的 API 适配。
3. 实际完成依赖更新后，核对组件项目版本已按近期惯例递增一次。构建该组件项目声明的目标框架，并用 `git diff --check` 和目标文件差异确认改动。返回更新结果和组件包发布状态。待组件包实际发布后，可通过默认模式同步示例站点引用。

## 交付结果

报告组件、上游依赖和组件包的版本变化，以及实际执行的验证。需要提交或发布时，按用户已授权的范围和目标仓库约定执行。BB 主仓库涉及 Issue 或 PR 时，使用 [github-issue-pr](../github-issue-pr/SKILL.md)。上游依赖实际升级时，标题沿用近期格式，例如 `feat(Dom2Image): update SnapDOM to vX.Y.Z`，版本号取上游依赖版本。

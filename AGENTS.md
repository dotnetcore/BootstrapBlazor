# BootstrapBlazor Agent Rules

> AI 生成代码必须遵守本文件。

## 项目与命令

- 解决方案为 `BootstrapBlazor.slnx`。组件源码在 `src/BootstrapBlazor/Components/`，可运行示例在 `src/BootstrapBlazor.Server/Components/Samples/`，组件单元测试在 `test/UnitTest/Components/`。
- 使用与 [CI 构建流程](.github/workflows/build.yml) 一致的 .NET 11 SDK（`11.0.x`），从仓库根目录执行下列命令。
- 构建组件库用 `dotnet build src/BootstrapBlazor`。
- 启动示例站点用 `dotnet run --project src/BootstrapBlazor.Server`。
- 按下方测试约定运行组件单元测试时，用 `dotnet test --project test/UnitTest`。

## 编码规范

### 术语
- BootstrapBlazor 组件库：简称 BB
- BootstrapBlazor 组件：简称 BB 组件
- BootstrapBlazor.Extensions 简称 BB.Extensions

### C#

- 异步方法加 `Async` 后缀
- 优先早期返回

### Razor

- `@code` 放末尾
- 业务逻辑不写 UI 层
- WASM 中的 JSInterop 使用异步 API

## AI 约束

- 不使用 bootstrapblazor skill
- 改前先读相关源码和相近实现，沿用现有模式，只做任务所需的改动并保持公开 API 兼容。
- 不主动引入 NuGet 包
- 不修改 `Directory.Packages.props`（除非说明理由）
- 禁止事项：
  - 不改 Bootstrap 原始 CSS（覆盖除外）
  - 不直接操作 DOM（用 JSInterop）
  - 不提交 `Console.WriteLine` 到生产代码（C#）
  - 不提交 `console.log` 到生产代码（Javascript）

## 测试约定

- 普通开发和缺陷修复中，AI 默认不新增、修改或运行单元测试。用户明确要求时，按任务范围执行。
- 快捷代码审查按 [bb-review](.agents/skills/bb-review/SKILL.md) 执行。需要补充单元测试时，优先覆盖行为和边界条件。
- 所有 AI 任务均不得针对组件 JavaScript 脚本逻辑新增、修改或运行测试，也不得在 GitHub Actions 中增加 JavaScript 测试环节。涉及 JavaScript 的改动仍可按上述约定验证相关 C# 组件逻辑。
- 根据改动执行相关构建或其他验证，报告实际结果和未执行的检查。用户要求预览或验证 UI 确有必要时，可启动站点或打开浏览器。
- CI 按仓库工作流执行，PR 合并前须通过适用的 CI 检查。

## 参考文档（按需读取）

- 排查或更新依赖前，读 [依赖说明](docs/agents/dependencies.md)。
- 建分支、提交、推送或创建、更新 Issue / PR 前，读 [提交与 PR 指南](docs/agents/pr-guide.md)。
- 修改组件、参数、样式、本地化或组件示例前，读 [组件规范与自检](docs/agents/component-checklist.md)。
- 用户提示去除首尾空白后恰为 `review`、`代码审查` 或 `审查代码` 时，按 [快捷代码审查流程](.agents/skills/bb-review/SKILL.md) 执行。
- 生成、翻译或校对 release notes 时，按 [发布说明 skill](.agents/skills/release-notes/SKILL.md) 执行。
- 按仓库约定创建或更新 Issue 和关联 PR 时，按 [GitHub Issue 和 PR skill](.agents/skills/github-issue-pr/SKILL.md) 执行。
- 配置新的 AI 工具或仓库级 skill 时，读 [各工具接入方式](docs/agents/skills.md)。

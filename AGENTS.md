# BootstrapBlazor Agent Rules

> AI 生成代码必须遵守本文件。

## 编码规范

### C#

- 异步方法加 `Async` 后缀
- 优先早期返回

### Razor

- `@code` 放末尾
- 业务逻辑不写 UI 层

## AI 约束

- 不使用 bootstrapblazor skill
- 改前先读相关源码
- 不主动引入 NuGet 包
- 不修改 `Directory.Packages.props`（除非说明理由）
- 禁止事项：
  - 不改 Bootstrap 原始 CSS（覆盖除外）
  - 不直接操作 DOM（用 JSInterop）
  - 不提交 `Console.WriteLine` 到生产代码（C#）
  - 不提交 `console.log` 到生产代码（Javascript）

## 参考文档（按需读取）

- 依赖：`docs/agents/dependencies.md`
- PR 规范：`docs/agents/pr-guide.md`
- 新组件流程：`docs/agents/component-checklist.md`
- 发布说明：涉及生成、翻译或校对 release notes 时，读取并遵循 `.agents/skills/release-notes/SKILL.md`；这是仓库级 skill 的唯一副本。各工具接入方式见 `docs/agents/skills.md`。

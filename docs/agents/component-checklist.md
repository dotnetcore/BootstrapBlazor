# 组件规范与自检

修改组件、参数、样式、本地化或组件示例前必须读取本文件，并遵守根 `AGENTS.md`。

## 参数与 API

- 修改前阅读相关组件源码，遵循现有设计，避免破坏现有 API。
- 新增公开类型和成员的 XML 注释使用中英文双语。`[Parameter]` 的注释也按此格式编写，分别用 `<para lang="zh">中文</para>` 和 `<para lang="en">English</para>`。
- 在每个新增公开声明的 XML `<summary>` 中添加 `<para><version>正式版版本号</version></para>`。使用计划发布的正式版版本号，`<version>` 须为 `<para>` 的直接子元素。
- XML 注释必须完整，以支持文档生成。

## 样式与本地化

- 样式基于 Bootstrap 5，优先使用 CSS Variables，兼容现有主题。
- 所有用户可见文本必须使用 `IStringLocalizer`，禁止硬编码中文/英文 UI 文本。
- 增删资源键时同步检查 `src/BootstrapBlazor/Locales/` 和 `localization/`，保留其他组件仍使用的资源。

## 文档与示例

- 新组件必须更新 `src/BootstrapBlazor.Server/docs.json` 并提供可运行示例。
- 组件行为或 API 变化时同步相关文档和示例。

## 修改后自检

- [ ] 符合 BootstrapBlazor 架构，未破坏现有 API。
- [ ] 新增公开 API 的双语注释和版本标记完整。
- [ ] 不引入新依赖；必要的依赖变更已说明原因。
- [ ] 样式兼容主题，无硬编码 UI 字符串，本地化资源已同步。
- [ ] 文档、示例已更新或确认无需更新。
- [ ] 按根 [AGENTS.md 的测试约定](../../AGENTS.md#测试约定) 完成适用验证，准确记录未执行的检查。

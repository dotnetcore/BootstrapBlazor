---
name: bb-review
description: Review and fix BootstrapBlazor changes using the repository's quick review workflow. Use when explicitly invoked or when the entire user prompt, after trimming whitespace, is review, 代码审查, or 审查代码.
---

# BootstrapBlazor 快捷代码审查

遵守根 `AGENTS.md` 和 `.github/copilot-instructions.md` 中的通用约束，按以下流程审查。用户明确要求只报告或禁止修改时，遵守用户限制。

## 审查范围

- 比较 `main...HEAD`，同时纳入暂存、未暂存及相关未跟踪改动。
- 实际取得并核对完整 diff，不能仅凭工作区状态判断；对相关未跟踪文件读取内容。
- `main` 不存在或无法确定比较基准时，先向用户确认，不自行切换基准。
- 只处理本次改动引入或暴露的问题，不扩大到无关历史问题。

## 审查重点

- 审查组件或公开 API 改动前，读取 `docs/agents/component-checklist.md`。
- 检查公开 API 兼容性、资源释放、本地化和行为测试覆盖；涉及 JavaScript 时仍须审查相关代码，但不得测试组件 JavaScript 脚本逻辑。
- 检查组件代码重写方法是否使用 `<inheritdoc/>` 注释。
- 检查文件是否为 UTF-8 无 BOM；无法确认时明确说明。
- 逐项检查新增公开类型和成员（包括非 `[Parameter]` 的公开属性）是否有中英文 XML 注释，以及版本标记。每个新增声明自身的 `<summary>` 内须包含 `<para><version>正式版版本号</version></para>`，且 `<version>` 须是 `<para>` 的直接子元素。
- 已有版本标记保持原样；仅缺少标记时才补充版本号。
- 版本号缺乏可靠依据时先向用户确认，不猜测。

## 处理与报告

- 对明确可执行的问题进行最小修复，并按需补充或更新测试；不得针对组件 JavaScript 脚本逻辑新增、修改或运行测试。完成后复查本轮改动并运行相关验证。
- 遇到 API 设计或兼容性取舍、需求歧义、需要新增或删除公开成员等情况，先向用户确认。
- 报告审查基准与实际范围、发现和处理的问题、复查结论、未解决问题及验证结果；未发现问题时明确说明。
- 新增公开 API 时，逐项报告新增公开类型和成员的版本标记核对结果。

# 提交与 PR 指南

创建分支、提交、推送、创建或更新 Issue / PR 前必须读取本文件，并遵守根 `AGENTS.md`。

## 分支与提交

- 分支命名：`feature/xxx`、`fix/xxx`、`docs/xxx`。
- 提交格式：`docs/agents/commit_msg_template.md`
- 提交前检查差异，仅包含当前任务相关改动，保留用户已有改动。

## Issue 与 PR

- 先检查仓库近期提交、Issue、PR 惯例及 `.github/ISSUE_TEMPLATE/`，避免重复创建。
- 创建或更新 PR 正文前，必须读取 `.github/pull_request_template.md`；若存在 `.github/PULL_REQUEST_TEMPLATE/`，也应检查适用模板。
- 保留模板的标题、注释、清单和顺序，按实际改动填写。
- PR 必须关联 Issue、描述修改点；UI 变更提供截图。
- 记录实际执行的验证命令及结果；未运行的检查明确说明，不勾选尚未完成的项目。
- 单元测试是否编写和执行遵循根 `AGENTS.md` 的测试约定，不因提交 PR 自动运行。
- 合并前必须通过 CI；创建 PR 不代表 CI 已通过。

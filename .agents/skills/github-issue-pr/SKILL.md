---
name: github-issue-pr
description: 根据当前分支的已推送改动创建 GitHub Issue 和关联的 PR。
---

# 创建 GitHub Issue 和 PR

遵守根 [AGENTS.md](../../../AGENTS.md) 和 [提交与 PR 指南](../../../docs/agents/pr-guide.md)。直接调用本 skill 即授权为当前分支创建一个新 Issue 和关联的 PR。

本 skill 只创建 Issue 和 PR，不修改代码或现有 Issue、PR，不创建或切换分支，不提交、推送、合并，也不运行构建或测试。无法直接创建时停止并说明原因。

1. 确认目标仓库及其默认分支，使用 `gh` 检查登录状态。当前分支必须是非默认分支，且已推送到目标仓库或其 Fork；远端分支必须与本地 `HEAD` 一致。未提交的改动不属于 PR，存在影响本次发布的未提交改动时停止。
2. 只读核对当前分支相对默认分支的提交和差异，确认有改动可发布。查询已有 Issue 和 PR，避免重复；若已有匹配的 Issue 或同一来源分支和目标分支的打开 PR，停止并返回链接。
3. 读取 [Issue 表单](../../../.github/ISSUE_TEMPLATE/)、[PR 模板](../../../.github/pull_request_template.md)及 `.github/PULL_REQUEST_TEMPLATE/` 中的适用模板，参考近期同类 Issue 和 PR 的惯例。根据实际差异创建 Issue，使用 `gh issue create --repo <目标仓库>`。
4. 创建以当前远端分支为 head、默认分支为 base 的 PR，使用 `gh pr create --repo <目标仓库>`。Fork 的 head 使用 `<owner>:<branch>`。保留 PR 模板的标题、注释、清单和顺序，在 `Link issues` 中填写 `fixes #<编号>`；跨仓库 Issue 使用 `fixes <owner>/<repo>#<编号>`。按实际改动和已知验证结果填写正文，不勾选未完成的项目。
5. 回读 Issue 和 PR，确认链接、head、base 和 Issue 关联并返回链接。创建请求报错时先查询远端，确认未创建后才能重试；状态无法确认时如实报告。

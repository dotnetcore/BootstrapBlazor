---
name: github-issue-pr
description: 根据当前分支的已推送改动复用或创建 GitHub Issue，并创建关联的新 PR。
---

# 创建 GitHub Issue 和 PR

遵守根 [AGENTS.md](../../../AGENTS.md) 和 [提交与 PR 指南](../../../docs/agents/pr-guide.md)。直接调用本 skill 即授权为当前分支复用或创建 Issue，并创建关联的新 PR。

本 skill 只创建 Issue 和 PR，不修改代码或现有 Issue、PR，不创建或切换分支，不提交、推送、合并，也不运行构建或测试。无法直接创建时停止并说明原因。

1. 确认目标仓库及其默认分支，使用 `gh` 检查登录状态。当前分支必须是非默认分支，且已推送到目标仓库或其 Fork；远端分支必须与本地 `HEAD` 一致。未提交的改动不属于 PR，存在影响本次发布的未提交改动时停止。
2. 只读核对当前分支相对默认分支的提交和差异，确认有改动可发布。搜索是否已有匹配的 Issue；无论 Issue 是否已关闭，匹配时均可复用并记录编号，没有匹配项则在步骤 3 创建。不要更新已有 Issue。
3. 读取 [Issue 表单](../../../.github/ISSUE_TEMPLATE/)、[PR 模板](../../../.github/pull_request_template.md)及 `.github/PULL_REQUEST_TEMPLATE/` 中的适用模板，参考近期同类 Issue 和 PR 的惯例。需要新建 Issue 时，根据实际差异创建，使用 `gh issue create --repo <目标仓库>`。
4. 每次都创建新的 PR，不复用或更新已有 PR。只检查同一 head 和 base 是否已有未关闭的 PR；若有则停止并说明无法新建，不要改为复用或更新。已关闭的 PR 不复用，也不阻止创建新 PR。使用 `gh pr create --repo <目标仓库>`，以当前远端分支为 head、默认分支为 base。分支名可能包含 `/`，始终将完整分支名作为一个值传递；Fork 的 head 格式为 `<owner>:<完整分支名>`，冒号用于分隔仓库所有者与分支名，不要按斜杠拆分分支名。保留 PR 模板的标题、注释、清单和顺序，在 `Link issues` 中填写 `fixes #<编号>`；跨仓库 Issue 使用 `fixes <owner>/<repo>#<编号>`。按实际改动和已知验证结果填写正文，不勾选未完成的项目。
5. 回读 Issue 和 PR，确认链接、head、base 和 Issue 关联并返回链接。创建请求报错时先查询远端，确认未创建后才能重试；状态无法确认时如实报告。

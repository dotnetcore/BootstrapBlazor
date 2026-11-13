## 使用自定义 Agent

以下路由仅供尚未进入专用角色的主 agent 使用。当前已经是 `bb-dev`、`bb-review` 或 `bb-test` 时，直接执行自身职责，不再套用本节路由，不得委派给自己。

根据任务类型委派给 `.github/agents/` 中对应的角色：
- 新组件开发、功能实现或 Bug 修复：委派给 `bb-dev`。
- 用户要求审查，或实现完成后需要检查 diff 时：委派给 `bb-review`。
- 需要新增或更新单元测试时：由 `bb-review` 审查后判定并委派给 `bb-test`；用户明确要求时也可直接委派。`bb-test` 只写测试、不运行测试。

`bb-review` 按 `.github/agents/bb-review.agent.md` 决定审查范围并处理委派与复查。

## 任务

- 执行任务完成后，显示任务耗时

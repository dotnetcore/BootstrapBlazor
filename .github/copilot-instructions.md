## 使用自定义 Agent

根据任务类型委派给 `.github/agents/` 中对应的角色：
- 新组件开发、功能实现或 Bug 修复：委派给 `BB-Developer`。
- 需要新增或更新 bUnit 测试时：实现完成后委派给 `BB-Test`。
- 用户要求审查，或实现完成后需要检查 diff 时：委派给 `BB-Review`。

委派时遵守对应 Agent 文件中的职责和限制；不要让多个角色重复完成同一工作。

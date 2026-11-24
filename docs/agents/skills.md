# 仓库级 Agent Skills

配置 AI agent 的 skill 目录，或需要让某个工具使用本仓库的 skill 前必须读取本文件，并遵守根 `AGENTS.md`。

## 唯一来源

仓库级 skill 统一放在 `.agents/skills/<skill-name>/SKILL.md`，随仓库分发，且每个 skill 只有一份：

- `.agents/skills/release-notes/SKILL.md`：生成、翻译、校对中英文版本发布说明。
- `.agents/skills/bb-review/SKILL.md`：快捷代码审查、修复和复查。
- `.agents/skills/github-issue-pr/SKILL.md`：按仓库约定创建 GitHub Issue 和关联 PR。
- `.agents/skills/update-extension-dependency/SKILL.md`：按组件名同步 BB.Extensions NuGet 引用或更新上游依赖。

`.agents/skills/` 是 Agent Skills 开放标准的跨工具目录。`SKILL.md` 的 frontmatter 只保留 `name` 和 `description`，不写任何工具专有字段；新增 skill 时同样遵守。

## 各工具接入方式

### 加载项目规则

- Codex 和 Cursor 支持根目录的 `AGENTS.md`。 [Codex 指南](https://developers.openai.com/codex/guides/agents-md) · [Cursor 指南](https://cursor.com/docs/rules#agentsmd)
- GitHub Copilot 可从 [.github/copilot-instructions.md](../../.github/copilot-instructions.md) 找到根 `AGENTS.md`。各客户端直接加载 `AGENTS.md` 的能力见 [Copilot 指令支持说明](https://docs.github.com/en/copilot/reference/custom-instructions-support)。
- Gemini CLI 默认加载 `GEMINI.md`。可将 `AGENTS.md` 加入 `.gemini/settings.json` 中的 `context.fileName`，例如 `{"context":{"fileName":["GEMINI.md","AGENTS.md"]}}`。已有设置时只合并该字段。 [Gemini CLI 指南](https://geminicli.com/docs/cli/gemini-md/)
- Claude Code 自 v2.1.277 起支持直接加载 `AGENTS.md`。默认优先读取项目路径上的 `CLAUDE.md` 或 `CLAUDE.local.md`。需要兼容旧版或已有 `CLAUDE.md` 时，可在 `CLAUDE.md` 中用 `@AGENTS.md` 引用根文件。 [Claude Code 指南](https://code.claude.com/docs/en/memory#agents-md)

### 发现仓库级 skill

- Codex、Cursor、Gemini CLI 和支持 Agent Skills 的 GitHub Copilot 客户端可以发现仓库的 `.agents/skills/`。 [Codex 指南](https://developers.openai.com/codex/skills) · [Cursor 指南](https://cursor.com/docs/skills) · [Gemini CLI 指南](https://geminicli.com/docs/cli/skills/) · [Copilot 指南](https://docs.github.com/en/copilot/concepts/customizations/agent-skills)
- Copilot CLI 的 `/bb-review` 调用本仓库的快捷审查，`/review` 调用内置审查流程。
- Claude Code 可按根 `AGENTS.md` 的指引直接读取仓库里的 `SKILL.md`。如需斜杠调用，可以将所需 skill 复制到用户级目录 `~/.claude/skills/`，并在仓库 skill 更新后同步本地副本。 [Claude Code Skills 指南](https://code.claude.com/docs/en/skills)

## 约束

- 不要在 `.github/skills/`、`.claude/skills/` 等目录复制第二份 `SKILL.md`，避免内容漂移。
- 保留 `.gitignore` 对个人工具目录 `.claude` 的忽略规则。
- 不要用软链共享 skill。Git 的 `core.symlinks` 为 `false` 时，软链会被检出为普通文本文件。
- `.github/agents/*.agent.md` 是 Copilot 自定义 agent（`target: github-copilot`），与 skill 无关，不要一并迁移。
- skill 中的 `gh api` 需要 `gh` CLI、已登录的 GitHub 账号和网络；不可用时按 `SKILL.md` 内的降级说明处理。
- 本仓库不使用 `bootstrapblazor` skill。

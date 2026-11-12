# 仓库级 Agent Skills

配置 AI agent 的 skill 目录，或需要让某个工具使用本仓库的 release notes skill 前必须读取本文件，并遵守根 `AGENTS.md`。

## 唯一来源

仓库级 skill 统一放在 `.agents/skills/<skill-name>/SKILL.md`，随仓库分发，且每个 skill 只有一份：

- `.agents/skills/release-notes/SKILL.md`：生成、翻译、校对中英文版本发布说明。

`.agents/skills/` 是 Agent Skills 开放标准的跨工具目录。`SKILL.md` 的 frontmatter 只保留 `name` 和 `description`，不写任何工具专有字段；新增 skill 时同样遵守。

## 各工具接入方式

| 工具 | 接入方式 |
| --- | --- |
| GitHub Copilot（CLI、云端 agent、VS Code / VS / JetBrains） | 自动发现，无需配置 |
| Codex、Cursor、Gemini CLI | 自动发现，无需配置 |
| Claude Code、WorkBuddy 等只读 `AGENTS.md` 的工具 | 按根 `AGENTS.md` 的指路读取该文件；不会自动加载，也不能用斜杠命令调用 |

需要斜杠调用时，可在自己的用户级目录做一次性安装（这些目录被 `.gitignore` 忽略，不要提交）：

```sh
mkdir -p ~/.claude/skills ~/.workbuddy/skills
cp -r .agents/skills/release-notes ~/.claude/skills/
cp -r .agents/skills/release-notes ~/.workbuddy/skills/
```

Windows PowerShell 可用 `Copy-Item -Recurse .agents/skills/release-notes $HOME\.workbuddy\skills\`。

## 约束

- 不要在 `.github/skills/`、`.claude/skills/` 等目录复制第二份 `SKILL.md`，避免内容漂移。
- 不要修改 `.gitignore` 放行 `.claude`、`.workbuddy`；这些是个人工具目录，不属于仓库配置。
- 不要用软链共享 skill：本仓库 `core.symlinks = false`，Windows 上会被当作普通文本文件。
- `.github/agents/*.agent.md` 是 Copilot 自定义 agent（`target: github-copilot`），与 skill 无关，不要一并迁移。
- skill 中的 `gh api` 需要 `gh` CLI、已登录的 GitHub 账号和网络；不可用时按 `SKILL.md` 内的降级说明处理。
- 本仓库不使用 `bootstrapblazor` skill

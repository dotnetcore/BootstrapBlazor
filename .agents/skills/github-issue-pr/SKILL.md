---
name: github-issue-pr
description: 按当前仓库的历史约定创建 GitHub Issue 和关联的 Pull Request。用户要求创建 Issue 和 PR、提交并发起 PR，或提出“在 GitHub 中创建 Issue PR”“创建 Issue 和 PR”“提交并提 PR”“按仓库格式发 PR”等请求时使用。
---

# GitHub Issue 和 Pull Request

为当前仓库创建 GitHub Issue 和关联的 Pull Request。先学习并遵循仓库自身约定，不要套用通用标题或正文格式。

## 预期结果

除非用户限制了范围，否则应端到端完成以下流程：

1. 检查仓库和当前改动。
2. 学习近期 Commit、Issue 和 PR 的约定。
3. 创建或复用一个 Issue。
4. 按需提交并推送改动。
5. 创建或复用一个与 Issue 关联的 PR。
6. 返回 Issue 和 PR 链接。

用户明确要求创建 GitHub 资源时，不要只起草文案就停止。

## 文案语言和长度

- 除非用户明确要求其他语言，Issue 和 PR 的标题、正文均使用英文。
- 在保留必要模板章节、链接和验证信息的前提下，尽量简洁。

## 工具

- 优先使用 `git` 检查仓库和历史。
- GitHub 操作优先使用已认证的 `gh` CLI。
- 使用 `git` 时禁用分页器（`git --no-pager`）。
- 使用非交互命令。
- Issue 或 PR 正文的临时文件只能放在会话临时目录，不要放进仓库。

## 1. 操作前先检查

确定以下信息：

- 仓库根目录及 `origin` 的 owner/repository。
- 当前分支及其 upstream。
- 从 GitHub 查询默认分支/基准分支，不要自行假定为 `main` 或 `master`。
- 已暂存、未暂存和未跟踪的改动。
- 当前分支独有的提交。
- 当前分支是否已有打开或已合并的 PR。

比较历史前先获取 `origin`：

```powershell
git --no-pager status --short
git branch --show-current
git remote -v
git fetch origin --quiet
gh repo view --json nameWithOwner,defaultBranchRef
git rev-list --left-right --count "origin/<base>...HEAD"
git --no-pager log --oneline "origin/<base>..HEAD"
gh pr list --head "<branch>" --state all --json number,state,title,url,mergedAt
```

### 分支安全

PR 只能包含与当前任务相关的改动。

- 如果分支只包含相关提交，继续使用当前分支。
- 如果分支包含无关提交、已合并 PR 的历史，或其他会污染新 PR 的内容，不要从该分支创建 PR。
- 保留所有用户改动。未经明确许可，不要重置、丢弃、覆盖、amend、rebase、强推或 stash。
- 如果无法在不移动或隐藏用户改动的情况下创建干净分支，先停止并向用户确认。
- 不要把无关的工作区改动放入提交。

## 2. 学习仓库约定

先查证已有先例，不要臆造格式。检查：

- 最近 10–15 条提交，尤其是相关组件或范围的提交。
- 同一作者或同一组件最近 10 个 Issue。
- 同一作者或同一组件最近 10 个 PR。
- `.github/ISSUE_TEMPLATE/**`。
- `.github/pull_request_template.md` 及其他 PR 模板。
- 仓库中适用的贡献指南或指令文件。

可使用以下命令：

```powershell
git --no-pager log -15 --pretty=format:"%h`t%s%n%b%x00"
gh issue list --state all --limit 10 --json number,title,body,labels,author
gh pr list --state all --limit 10 --json number,title,body,headRefName,baseRefName,author
```

归纳并沿用：

- Conventional Commit 类型和 scope。
- 大小写风格和术语。
- Issue 模板章节和标签。
- PR 标题和正文章节。
- `fixes #123` 等关闭 Issue 的关键词写法。
- 版本更新和打包惯例。

优先参考同一组件、同一作者的案例，而不是全仓库的通用案例。

## 3. 创建或复用 Issue

创建 Issue 前，按拟定标题、组件、版本和关键词搜索现有的打开 Issue。

```powershell
gh issue list --state open --search "<distinctive query> in:title" --json number,title,url
```

- 明确匹配的打开 Issue 应予以复用。
- 除非用户明确要求，不要复用已关闭的 Issue。
- 遵循仓库的 Issue 模板和历史标题格式。
- 只使用仓库已有标签，不要创建新标签。
- 功能或依赖更新在适用时使用仓库历史上的 enhancement 标签。
- 缺陷修复在适用时使用仓库历史上的 bug 标签。
- 如果仓库历史使用简洁的生成式模板，Issue 正文也应保持简洁。
- 记录创建后的 Issue 编号和 URL。

## 4. 提交并推送

如果相关改动尚未提交：

- 只暂存属于本任务的文件。
- 遵循近期提交信息格式。
- 按当前指令加入必需的 commit trailer。
- 除非用户明确要求，不要 amend 现有提交。

如果不需要新提交，确认所有预期改动已存在于分支提交中。

安全推送：

```powershell
git push -u origin HEAD
```

除非用户明确要求，否则不要强推。

## 5. 创建或复用 PR

创建 PR 前，先检查当前 head 分支是否已有打开的 PR。

```powershell
gh pr list --head "<branch>" --state open --json number,title,url
```

- 复用已存在的打开 PR，仅在必要时更新。
- 除非用户指定其他基准，否则以已确认的仓库默认分支为目标。
- 遵循仓库历史上的 PR 标题格式和正文模板。
- 使用仓库惯用的关闭关键词关联 Issue，通常为 `fixes #<number>`。
- 只描述 `origin/<base>...HEAD` 中实际包含的改动。
- 如实填写风险、回归、验证、打包、文档和示例相关章节；未执行验证时明确说明。
- 未完成的事项不要勾选。
- 不要添加与仓库模板冲突的通用章节。

创建 PR 时明确指定仓库、基准分支和 head 分支：

```powershell
gh pr create --repo "<owner/repo>" --base "<base>" --head "<branch>" --title "<title>" --body "<body>"
```

## 6. 最终验证

创建后：

- 确认 Issue 和 PR 的 URL。
- 确认 PR 的 base 和 head 分支。
- 确认 PR 正文引用了 Issue。
- 确认 PR 的提交和文件列表不包含无关改动。
- 确认本地工作区没有因本流程产生的非预期改动。

简洁地返回 Issue 和 PR 链接。只有流程无法完成时才说明阻塞原因。

## 禁止的捷径

- 不要假定分支名称、标签、模板、标题格式或版本规则。
- 不要创建重复的 Issue 或 PR。
- 检查分支污染前不要创建 PR。
- 不要声称执行过未实际运行的验证。
- 存在无关改动时，不要使用 `git add .` 等宽泛暂存方式。
- 不要推送密钥或生成的临时正文文件。
- 除非用户明确要求，否则不要合并 PR。

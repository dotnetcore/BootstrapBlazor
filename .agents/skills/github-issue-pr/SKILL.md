---
name: github-issue-pr
description: 按 BootstrapBlazor 仓库约定将当前分支的改动发布为新 Issue 和关联 PR，也可按明确要求单独处理 Issue 或 PR。
---

# GitHub Issue 和 Pull Request

开始前先读 [根 AGENTS.md](../../../AGENTS.md) 和 [提交与 PR 指南](../../../docs/agents/pr-guide.md)。涉及提交时，按 [提交格式](../../../docs/agents/commit_msg_template.md) 和近期同类提交拟定消息。

## 确认任务范围

- 直接调用本 skill 且未指定其他 Issue 或 PR 操作时，按当前分支相对目标仓库默认分支的待发布差异，先创建新 Issue，再创建指向默认分支并关联该 Issue 的新 PR。直接调用即授权完成这套发布流程。
- 仅处理 Issue 时，确认仓库并按任务创建、复用或更新 Issue。
- 单独发布 PR 时，必须关联 Issue，优先使用用户指定或已有的匹配 Issue。如缺少 Issue，请用户指定 Issue 或授权新建。
- 更新现有 PR 时，按用户提供的编号或 URL 核对目标。仅更新文案时，用 `gh pr view` 和 `gh pr diff` 核对远端 PR 的提交和差异，直接更新请求涉及的字段。只有需要更新分支内容时，才执行本地提交与推送的步骤。
- 用户明确要求发布时，完成授权范围内的操作并返回结果。合并需要用户明确授权，并遵守仓库 CI 要求。

## 1. 确认仓库并检查当前分支

根据用户指定的仓库、Issue 或 PR 链接及本地远端确认目标。若本地连接的是 Fork，核对其上游。按任务选用以下 PowerShell 示例，将仓库、远端、标题、查询词和文件路径替换为实际值。`$targetRepo` 是接收 PR 的仓库。仅处理 Issue 时，它是 Issue 所在仓库。`$issueRepo` 默认与 `$targetRepo` 相同，关联用户指定的跨仓库 Issue 时设为该 Issue 所在仓库。`gh issue` 和 `gh pr` 命令均显式传入对应的 `--repo`。

确定目标仓库后，先只读查询当前分支和 GitHub 返回的默认分支。当前分支是默认分支时，立即结束本次调用并告知用户，跳过后续所有步骤，包括拉取、切换或创建分支、提交、推送和创建或更新 Issue / PR。即使任务指定了其他 PR 基准分支，也先执行这项检查。

```powershell
$targetRepo = 'owner/repo'
$issueRepo = $targetRepo
git --no-pager rev-parse --show-toplevel
git --no-pager remote -v
$currentBranch = git --no-pager branch --show-current
if ($LASTEXITCODE -ne 0) { throw '无法读取当前分支。' }
$repoInfo = gh repo view "$targetRepo" --json 'nameWithOwner,defaultBranchRef,isFork,parent,viewerPermission'
if ($LASTEXITCODE -ne 0) { throw '无法查询目标仓库。' }
$repo = $repoInfo | ConvertFrom-Json -ErrorAction Stop
$defaultBranch = $repo.defaultBranchRef.name
if (-not $defaultBranch) { throw '无法确定目标仓库的默认分支。' }
if ($currentBranch -ceq $defaultBranch) { throw "当前分支 $currentBranch 是默认分支，已停止。" }
gh auth status
```

GitHub 操作优先使用已认证的 `gh`，发布时传入非交互参数。按任务检查网络和操作权限。发布 PR 时，先确认有本次任务的改动可发布，且基准分支、可写的推送目标和 PR 创建方式均已确定，再创建配套的新 Issue。上游无推送权限时，优先使用可写且与上游关联的 Fork。

## 2. 核对本地分支和现有 PR

发布分支或更新 PR 的提交时，确认当前分支及其跟踪分支。新 PR 以用户指定的分支为基准，未指定时使用 GitHub 返回的默认分支。更新现有 PR 时使用该 PR 的基准分支，除非用户要求变更。`$baseRemote` 指向接收 PR 的仓库，可用已核实的远端名或 URL。获取指定分支后，将 `FETCH_HEAD` 解析为固定的提交 ID。

```powershell
$baseRemote = 'upstream'
$baseBranch = $defaultBranch  # 更新现有 PR 或用户指定基准时，按上文设置
if (-not $currentBranch) { throw '未检出本地分支。' }
git --no-pager status --short --branch
if (-not $baseBranch) { throw '无法确定 PR 的基准分支。' }
git --no-pager fetch "$baseRemote" "refs/heads/$baseBranch"
if ($LASTEXITCODE -ne 0) { throw '无法获取 PR 的基准分支。' }
$baseRef = git --no-pager rev-parse --verify 'FETCH_HEAD^{commit}'
if ($LASTEXITCODE -ne 0) { throw '无法解析 PR 的基准提交。' }
git --no-pager log --oneline "$baseRef..HEAD"
git --no-pager diff "$baseRef...HEAD"
```

查看已暂存、未暂存和未跟踪的改动。当前分支适合作为 PR head，且分支独有的提交和差异均属于本次任务时，可以复用。当前分支是基准分支或包含无关提交时，从基准提交建立符合仓库命名约定的独立工作区，只迁移本次任务的提交和改动，保留原工作区和暂存区。重置、覆盖、amend、rebase、强推和 stash 均需明确授权。改动无法安全隔离时，说明具体冲突。

确定要推送的分支后，在推送前查找现有 PR。`$headRepo` 是接收推送的 GitHub 仓库，使用 Fork 时设为该 Fork 的 `owner/repo`。核对 `$pushRemote` 的所有推送 URL 均指向该仓库。更新现有 PR 的提交时，使用已核对的 PR head 仓库和分支。

```powershell
$pushRemote = 'origin'
$headRepo = $targetRepo  # 使用 Fork 时改为 Fork 的 owner/repo
$headBranch = git --no-pager branch --show-current  # 推送到不同分支名时填实际的远端分支名
if ($LASTEXITCODE -ne 0 -or -not $headBranch) { throw '无法确定要推送的分支。' }
git --no-pager remote get-url --push --all "$pushRemote"
if ($LASTEXITCODE -ne 0) { throw '无法查询推送远端。' }
gh pr list --repo "$targetRepo" --head "$headBranch" --state open --limit 100 --json number,title,url,headRefName,headRepository,baseRefName
if ($LASTEXITCODE -ne 0) { throw '无法查询该分支的现有 PR。' }
```

`gh pr list --head` 只接收分支名。用 `headRepository.nameWithOwner`、`headRefName` 和 `baseRefName` 核对来源及目标。若返回 100 条，将同一命令的 `--limit` 提高到 200。`gh` 会自动翻页，结果仍达到上限时继续提高，直到返回条数低于上限。默认流程发现同一 head 和 base 已有打开的 PR 时，返回该 PR 链接并在创建 Issue 前结束。其他任务可复用与当前任务匹配的打开 PR，若其基准分支与初次比较时不同，按实际基准重新比较。若同一 head 已用于其他打开的 PR，选择新的任务分支并重新检查。

## 3. 查模板和近期先例

- 处理 Issue 时，读取 [Issue 表单](../../../.github/ISSUE_TEMPLATE/)。创建或更新 PR 正文前，读取 [PR 模板](../../../.github/pull_request_template.md)。如果存在 `.github/PULL_REQUEST_TEMPLATE/`，也检查其中适用的模板。
- 查看近期同组件或同作者的提交、Issue 和 PR。可用 `gh issue list` 或 `gh pr list` 的 `--search`、`--author` 缩小范围，核对标题格式、标签、关联和打包惯例。
- 文案语言以用户指定为准。未指定时参考近期同类案例，默认使用英文。将完整 Markdown 正文以 UTF-8 写入会话临时目录，并通过 `--body-file` 传入。

## 4. 创建、复用或更新 Issue

默认流程根据第 2 节核对的待发布差异拟定新 Issue 的标题和正文，说明这些改动要解决的问题或实现的目标。需要复用 Issue 时，用 `gh issue view` 核对用户指定或查重找到的 Issue 的正文和状态，优先复用明确匹配的打开 Issue。已关闭的 Issue 只有在用户明确指定时复用。

```powershell
$issueId = '123'  # 换成实际编号或 URL
gh issue view "$issueId" --repo "$issueRepo"
if ($LASTEXITCODE -ne 0) { throw '无法读取指定的 Issue。' }
```

需要查重时，按标题、组件、版本和关键词搜索。

```powershell
$issueSearch = '组件名 关键词'
gh issue list --repo "$issueRepo" --state open --search "$issueSearch" --json number,title,url
if ($LASTEXITCODE -ne 0) { throw '无法查询现有 Issue。' }
```

创建或编辑前，按任务准备标题及正文文件。

```powershell
$issueTitle = '已确认的 Issue 标题'
$issueBodyFile = 'C:\Temp\session\issue.md'  # 已写入正文的临时文件
```

新建 Issue 时，按适用表单和仓库先例填写必填项。需要标签时，显式传入适用的已有标签，例如 `--label bug` 或 `--label enhancement`。

```powershell
gh issue create --repo "$issueRepo" --title "$issueTitle" --body-file "$issueBodyFile"
if ($LASTEXITCODE -ne 0) { throw '创建 Issue 报错，须先查证远端结果。' }
```

创建命令报错时，先在 `$issueRepo` 核对最近创建的 Issue 及标题搜索结果，回读匹配项的正文。只有确认未创建且故障已排除时才重试。查询受阻时报告创建状态待确认。

更新已有 Issue 时，先读取原内容，再按需执行下例。仅保留任务要求修改的选项，例如只改标题时省略 `--body-file`。

```powershell
gh issue edit "$issueId" --repo "$issueRepo" --title "$issueTitle" --body-file "$issueBodyFile"
if ($LASTEXITCODE -ne 0) { throw '更新 Issue 失败。' }
```

记录 Issue 编号和 URL。

## 5. 提交并推送

需要提交时，逐一指定本次任务的文件。文件混有无关改动时，对该文件用 `git add -p` 选取相关差异块。检查完整的暂存差异，包括原先已暂存的改动。暂存区存在无关内容时，使用独立工作区隔离。如果相关改动均已提交，直接核对分支提交。

```powershell
$taskFiles = @('.agents/skills/github-issue-pr/SKILL.md')  # 替换为从仓库根目录确认的文件路径
git --no-pager --literal-pathspecs add -- @taskFiles
if ($LASTEXITCODE -ne 0) { throw '暂存失败，请核查文件路径和暂存区。' }
git --no-pager diff --cached
if ($LASTEXITCODE -ne 0) { throw '无法检查暂存差异。' }
```

确认暂存区只包含本次要提交的差异后，将按仓库格式拟定的提交消息写入会话临时文件，并包含当前指令要求的 trailer。

```powershell
$commitMessageFile = 'C:\Temp\session\commit-message.txt'  # 已写入提交消息的临时文件
git --no-pager commit -F "$commitMessageFile"
if ($LASTEXITCODE -ne 0) { throw '提交失败，暂停推送。' }
```

提交报错时，查看 `git status --short --branch` 和 `git log -1 --oneline`，确认预期提交是否已生成以及哪些改动仍在暂存区。若提交未生成，按报错处理提交消息、Git 作者配置或钩子失败等具体原因，复查暂存差异后再重试。原因无法解决时，报告阻塞。

推送前再次对照 `$baseRef` 检查提交和文件，确认 `HEAD` 包含所有要发布的改动。使用已核实的远端和分支。需要建立本地跟踪关系时才加 `--set-upstream`。

```powershell
git --no-pager push "$pushRemote" "HEAD:refs/heads/$headBranch"
if ($LASTEXITCODE -ne 0) { throw '推送失败。' }
```

推送报错后，先用实际推送 URL 查远端分支。`git ls-remote` 成功且无输出表示该分支不存在。有输出时，将返回的提交 ID 与 `git rev-parse HEAD` 比较。两者一致时，按已推送处理。

```powershell
$pushUrls = @(git --no-pager remote get-url --push --all "$pushRemote")
if ($LASTEXITCODE -ne 0 -or -not $pushUrls) { throw '无法确定推送 URL。' }
$pushUrl = $pushUrls[0]  # 有多个 URL 时，选用发生错误的 URL
git --no-pager ls-remote --refs "$pushUrl" "refs/heads/$headBranch"
if ($LASTEXITCODE -ne 0) { throw '无法读取远端分支，请按实际错误继续排查。' }
```

- 被远端拒绝时，若远端存在同名分支，先用 `git fetch` 获取该分支，再比较两侧提交并核对分支归属。新 PR 遇到其他任务占用分支时，改用新分支。更新现有 PR 时，保留远端提交并解决分叉后再推送。若远端提示分支保护或仓库规则，按规则处理或报告所需权限。
- 认证失败时，按推送 URL 使用的协议核对凭据。HTTPS 检查凭据管理器中的账号和仓库写权限。若由 `gh` 提供凭据，用 `gh auth status` 核对登录。SSH 检查密钥是否对应有写权限的账号。凭据失效时，提示用户重新认证，再查询远端状态。
- 遇到超时、DNS 或连接错误时，检查到推送主机的网络和代理配置。连接恢复后，重新查询远端状态，确认仍需推送再重试。

## 6. 创建或更新 PR

- 保留模板的标题、注释、清单和顺序。描述实际 PR 差异，UI 变更按指南提供截图。记录实际验证命令和结果，说明未运行的检查，按实际完成情况勾选。
- PR 以默认分支为目标时，在 `Link issues` 中用 `fixes #编号` 关联同仓库 Issue，跨仓库用 `fixes owner/repo#编号`。以其他分支为目标时，在同一节填写 Issue 链接，并在有写权限时手动关联。同仓库可从 PR 侧栏操作，跨仓库从 Issue 侧栏操作。若权限不足，报告正文已引用 Issue 以及手动关联仍待完成。 [GitHub 关联说明](https://docs.github.com/en/issues/tracking-your-work-with-issues/using-issues/linking-a-pull-request-to-an-issue)
- 更新已有 PR 时保留任务未涉及的内容、模板和 Issue 关联。

按任务准备标题及正文文件。

```powershell
$prTitle = '已确认的 PR 标题'
$prBodyFile = 'C:\Temp\session\pr.md'  # 已写入正文的临时文件
```

更新已有 PR 的正文时，按实际编号或 URL 执行。

```powershell
$prId = '123'  # 换成实际编号或 URL
gh pr edit "$prId" --repo "$targetRepo" --body-file "$prBodyFile"
if ($LASTEXITCODE -ne 0) { throw '更新 PR 正文失败。' }
```

确认 head 分支已推送且没有可复用的 PR 后，按 head 仓库选择一种创建方式。同仓库或个人 Fork 使用 `gh pr create`。

```powershell
$headSpec = $headBranch
if ($headRepo -ne $targetRepo) {
    $forkOwner = ($headRepo -split '/', 2)[0]
    $headSpec = "${forkOwner}:$headBranch"
}
gh pr create --repo "$targetRepo" --base "$baseBranch" --head "$headSpec" --title "$prTitle" --body-file "$prBodyFile"
if ($LASTEXITCODE -ne 0) { throw '创建 PR 报错，须先查证远端结果。' }
```

组织拥有的 Fork 使用 REST API。两个仓库同属一个组织时，提交 `head_repo` 指定 head 仓库名。`-F "body=@$prBodyFile"` 从临时文件读取完整正文。 [创建 PR 接口](https://docs.github.com/en/rest/pulls/pulls#create-a-pull-request)

```powershell
$forkOwner, $forkName = $headRepo -split '/', 2
$headRepoArgs = @()
if ($forkOwner -eq ($targetRepo -split '/', 2)[0]) {
    $headRepoArgs = @('-f', "head_repo=$forkName")
}
gh api "repos/$targetRepo/pulls" --method POST -f "head=${forkOwner}:$headBranch" -f "base=$baseBranch" -f "title=$prTitle" -F "body=@$prBodyFile" @headRepoArgs --jq '.html_url'
if ($LASTEXITCODE -ne 0) { throw '创建 PR 报错，须先查证远端结果。' }
```

## 7. 验证并返回结果

回读操作涉及的 Issue 和 PR，确认 URL 和实际更新。对 PR 核对 head 仓库、head 分支、base 分支、Issue 关联、提交和文件，确保只包含本次任务改动。检查本地工作区和暂存区是否符合预期。

PR 创建请求报错后，先按第 2 节在目标仓库查询匹配的 head 和 base，确认尚未创建且故障已排除后再重试。返回已创建、复用或更新的链接。流程受阻时，说明已完成的操作和受阻步骤。

你正在维护 BootstrapBlazor 源码仓库内嵌的 AI Skill 文件。

目标：
根据当前仓库源码和官方 Sample，为本次变更涉及的组件生成或更新同名 Skill 文件：

`src/BootstrapBlazor/Components/<ComponentName>/<ComponentName>.md`

本次对比基准为 `<BASE_REF>`。如果没有明确指定，优先使用 `HEAD~1`。

禁止：

- 禁止根据模型记忆生成内容
- 禁止根据互联网示例生成内容
- 禁止使用历史版本 BootstrapBlazor API
- 禁止猜测参数、事件、模板、废弃 API
- 禁止覆盖无关组件 Skill

组件知识优先级：

1. 当前仓库组件源码是 API 和行为的最终依据
2. 如果官方 Sample 存在，示例用法优先来自 Sample
3. 如果 Sample 不存在，再使用 Skill 中的示例，但必须先用源码验证

执行步骤：

1. 定位本次变更组件

   使用 git 对比 `<BASE_REF>` 和当前工作树/HEAD：

   - 查找 `src/BootstrapBlazor/Components/**` 下被新增或修改的文件
   - 将路径归并到组件目录
   - 如果有新增组件目录，必须创建同名 `<ComponentName>.md`
   - 如果组件源码修改，必须同步检查并更新同名 Skill

2. 对每个组件读取必要文件

   必须读取：

   - `src/BootstrapBlazor/Components/<ComponentName>/<ComponentName>.razor`
   - `src/BootstrapBlazor/Components/<ComponentName>/<ComponentName>.razor.cs`
   - 组件目录下相关 `.cs`、`.scss`、`.js` 文件
   - 当前已有的 `<ComponentName>.md`，如果存在
   - `skill-index.json`

   如果 `skill-index.json` 中该组件存在 `sample`：

   - 必须读取对应 Sample
   - Sample 可能是目录，也可能是 `.razor` 文件
   - 示例代码优先从 Sample 中提炼

3. 更新 Skill 文件

   每个 Skill 文件头部必须包含：

   ```yaml
   ---
   component: <ComponentName>
   namespace: BootstrapBlazor.Components
   skillVersion: 1
   lastUpdated: <当前日期，格式 yyyy-MM-dd>
   ---
   ```

   必须包含以下章节：

   - `## Component Purpose`
   - `## Usage Scenarios`
   - `## Parameters`
   - `## Events And Callbacks`
   - `## Templates And Child Content`
   - `## Cascading Parameters`
   - `## Implementation Notes`
   - `## Sample Mapping`
   - `## Examples`
   - `## Common Mistakes`
   - `## Obsolete Members`
   - `## Agent Rules`

   内容要求：

   - Skill 是给 AI Agent 的开发指南，不是用户文档
   - 参数、事件、模板必须来自当前源码
   - 示例必须来自官方 Sample；没有 Sample 时，示例必须能被当前源码支持
   - 如果源码里没有某个参数或事件，不得写入 Skill
   - 如果成员标记 `[Obsolete]`，必须写入 `Obsolete Members`
   - `Sample Mapping` 必须记录实际 Sample 路径；没有 Sample 时明确写 `No official sample found in skill-index.json.`
   - `Agent Rules` 必须明确要求生成代码前读取源码、Sample、Skill

4. 更新索引

   如果新增组件、删除组件、移动 Sample 或新增 Sample，执行：

   ```powershell
   powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\generate-skill-index.ps1
   ```

5. 自查

   必须执行：

   ```powershell
   powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\generate-skill-index.ps1 -Check
   powershell -NoProfile -ExecutionPolicy Bypass -File .\scripts\check-skill-sync.ps1 -BaseRef <BASE_REF> -WarnAllMissingSkills
   ```

   还要人工抽查本次更新的每个 Skill：

   - metadata 是否正确
   - 章节是否完整
   - 示例是否来自 Sample 或已被源码验证
   - 参数/事件是否真实存在
   - 是否误用了历史 API
   - Markdown 代码块是否闭合

6. 输出结果

   最终回复必须包含：

   - 更新了哪些组件 Skill
   - 是否更新了 `skill-index.json`
   - 执行了哪些校验命令
   - 是否存在无法确认的组件或 Sample

请基于 `<BASE_REF>` 到当前仓库状态的组件变更，完成以上 Skill 同步维护。中途无需询问；如遇到无法从源码或 Sample 确认的信息，保守省略并在最终结果说明。

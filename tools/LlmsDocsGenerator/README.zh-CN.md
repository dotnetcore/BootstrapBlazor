# LlmsDocsGenerator

自动为 BootstrapBlazor 组件生成 LLM 友好文档的工具。

## 目的

AI 编程助手（Claude Code、Cursor、GitHub Copilot）经常因为缺乏准确的组件 API 信息而生成错误的 UI 代码。本工具通过以下方式解决这个问题：

1. **使用 Roslyn 自动生成参数表** - 从源代码提取
2. **提供 GitHub 源码链接** - 方便深入查阅
3. **集成 CI/CD** - 确保文档与代码同步

## 架构

```
┌─────────────────────────────────────────────────────────────┐
│                    LlmsDocsGenerator                         │
├─────────────────────────────────────────────────────────────┤
│  ComponentAnalyzer     → 基于 Roslyn 的源码解析器            │
│  MarkdownBuilder       → 生成 Markdown 文档                  │
│  DocsGenerator         → 协调生成流程                        │
└─────────────────────────────────────────────────────────────┘
                              │
                              ▼
┌─────────────────────────────────────────────────────────────┐
│              输出目录: wwwroot/llms/                        │
├─────────────────────────────────────────────────────────────┤
│  llms.txt              → 索引文件，包含快速入门指南           │
│  llms-table.txt        → 表格组件文档                        │
│  llms-input.txt        → 输入组件文档                        │
│  llms-select.txt       → 选择组件文档                        │
│  llms-button.txt       → 按钮组件文档                        │
│  llms-dialog.txt       → 对话框组件文档                      │
│  llms-nav.txt          → 导航组件文档                        │
│  llms-card.txt         → 容器组件文档                        │
│  llms-treeview.txt     → 树形组件文档                        │
│  llms-form.txt         → 表单组件文档                        │
│  llms-other.txt        → 其他组件文档                        │
│  llms-example-project.txt → 用户项目模板                    │
└─────────────────────────────────────────────────────────────┘
```

## 工作原理

### 1. 源码分析

`ComponentAnalyzer` 使用 Roslyn 解析 C# 源文件：

```csharp
// 扫描 [Parameter] 特性
var parameters = classDeclaration.DescendantNodes()
    .OfType<PropertyDeclarationSyntax>()
    .Where(p => HasAttribute(p, "Parameter"));

// 提取 XML 文档注释
var summary = ExtractXmlSummary(property);
```

### 2. 文档生成

`MarkdownBuilder` 生成结构化的 Markdown，包含：

- 参数表（名称、类型、默认值、描述）
- 事件回调部分
- 公共方法
- GitHub 源码链接

### 3. 组件分类

组件自动分组到以下类别：

| 类别 | 组件示例 |
|------|----------|
| table | Table, SelectTable, TableToolbar |
| input | BootstrapInput, Textarea, OtpInput |
| select | Select, AutoComplete, Cascader |
| button | Button, PopConfirmButton |
| dialog | Modal, Drawer, Toast |
| nav | Menu, Tab, Breadcrumb |
| card | Card, Collapse, GroupBox |
| treeview | TreeView, Tree |
| form | ValidateForm, EditorForm |
| other | 其他所有组件 |

## 安装dotnet tool install --global --add-source ./tools/LlmsDocsGenerator/bin/Debug BootstrapBlazor.LlmsDocsGenerator

### 作为全局工具安装

```bash
dotnet pack tools/LlmsDocsGenerator
dotnet tool install --global --add-source ./tools/LlmsDocsGenerator/bin/Release BootstrapBlazor.LlmsDocsGenerator
```

或从 NuGet 安装（发布后）：

```bash
dotnet tool install --global BootstrapBlazor.LlmsDocsGenerator
```

### 更新工具

```bash
dotnet tool update --global BootstrapBlazor.LlmsDocsGenerator
```

### 卸载工具

```bash
dotnet tool uninstall --global BootstrapBlazor.LlmsDocsGenerator
```

## 使用方法

安装为全局工具后，使用 `bbllmsdocs` 命令：

### 生成所有文档

```bash
bbllmsdocs
```

或从源代码运行：

```bash
dotnet run --project tools/LlmsDocsGenerator
```

### 生成特定组件

```bash
bbllmsdocs --component Table
```

### 仅生成索引

```bash
bbllmsdocs --index-only
```

### 检查文档是否过期（CI/CD）

```bash
bbllmsdocs --check
```

如果文档过期，返回退出码 1。

### 自定义输出目录

```bash
bbllmsdocs --output ./docs
```

### 显示帮助

```bash
bbllmsdocs --help
```

## CI/CD 集成

### 构建工作流 (build.yml)

每次推送到 main 分支时检查文档是否最新：

```yaml
- name: Check LLM Documentation
  run: dotnet run --project tools/LlmsDocsGenerator -- --check
```

### Docker 工作流 (docker.yml)

构建文档站点前重新生成文档：

```yaml
- name: Generate LLM Documentation
  run: dotnet run --project tools/LlmsDocsGenerator
```

### Dockerfile

容器构建时生成文档：

```dockerfile
WORKDIR /tools/LlmsDocsGenerator
RUN dotnet run
```

## 输出格式

每个组件的文档包含：

```markdown
## 组件名称

来自 XML 注释的描述

### 类型参数
- `TItem` - 泛型类型参数

### 参数
| 参数 | 类型 | 默认值 | 描述 |
|------|------|--------|------|
| Items | `List<TItem>` | - | 数据源 |
| ShowToolbar | `bool` | false | 显示工具栏 |

### 事件回调
| 事件 | 类型 | 描述 |
|------|------|------|
| OnClick | `EventCallback` | 点击处理器 |

### 公共方法
- `Task RefreshAsync()` - 刷新数据

### 源码
- 组件: [src/.../Component.razor.cs](GitHub 链接)
- 示例: [src/.../Samples/Components.razor](GitHub 链接)
```

## 库用户使用指南

用户可以在自己的项目中创建 `llms.txt` 来引用本文档：

```markdown
# 我的项目

## 依赖

### BootstrapBlazor
- 文档: https://www.blazor.zone/llms/llms.txt
- 表格: https://www.blazor.zone/llms/llms-table.txt
```

完整模板请参考 `llms-example-project.txt`。

## 设计理念

### 为什么需要这个工具？

| 问题 | 解决方案 |
|------|----------|
| AI 生成错误的组件代码 | 提供准确的参数文档 |
| 手动维护文档容易过期 | 自动从源码生成 |
| 文档太大占用上下文 | 按类别分割，按需加载 |
| 用户不知道如何引用 | 提供项目模板 |

### 混合文档策略

```
AI 代理工作流程：
1. 读取 llms.txt (5KB) → 快速了解组件分类
2. 按需读取 llms-{component}.txt → 获取详细参数
3. 不确定时查阅 GitHub 源码 → 获取准确信息
4. 参考 Samples 目录 → 学习官方用法
```

# BootstrapBlazor Agent Rules

本文件用于指导 AI 编程助手在 BootstrapBlazor 仓库中的行为。

> ⚠️ 所有 AI 生成的代码、文档、测试用例 **必须** 遵守本文件规则。

---

## 一、项目基本信息

- 仓库：dotnetcore/BootstrapBlazor
- 技术栈：.NET 6/7/8/9/10/11、Blazor（Server / WebAssembly / Auto）
- UI 基础：Bootstrap 5
- 主要语言：C# / Razor
- 组件工程：src/BootstrapBlazor/BootstrapBlazor.csproj
- 文档示例工程：src/BootstrapBlazor.Server/BootstrapBlazor.Server.csproj
- 测试工程：test/UnitTest/UnitTest.csproj

---

## 二、架构与代码边界

### 1. 组件设计原则

- 参数 `[Parameter]` 注释需要中英文双语 使用 `<para lang="zh">中文</para><para lang="en">English</para>` 标记
- 新组件，新参数 `[Parameter]` 必须增加 `<para><version></version></para>` 版本信息为将要发布的正式版版本号

### 2. 样式与主题

- 样式基于 Bootstrap 5
- 优先使用 CSS Variables

### 3. 国际化

- 所有用户可见文本必须使用 `IStringLocalizer`
- 禁止硬编码中文/英文 UI 文本

---

## 三、编码规范（C# / Razor）

### C#

- 异步方法必须加 `Async` 后缀
- 优先使用早期返回，避免深层嵌套
- XML 注释必须完整（用于文档生成）

### Razor

- `@code` 块放在组件末尾
- 避免复杂业务逻辑写在 UI 层

---

## 四、测试约定

- 为提高开发效率，新组件在未明确要求编写单元测试的情况下，不要编写单元测试，也不要运行现有单元测试
- 测试项目：`UnitTest`
- 测试框架：xUnit + bUnit
- 测试命名：`MethodName_State_ExpectedResult`

---

## 五、Git / PR 规范

- 分支命名：`feature/xxx`、`fix/xxx`、`docs/xxx`
- Commit 格式：

  ```
  type(scope): description
  ```

- PR 要求：
  - 关联 Issue
  - 描述修改点
  - 提供截图（UI 变更）
  - 通过 CI

---

## 六、AI 行为约束

- 修改前先阅读相关组件源码
- 不主动引入新 NuGet 包
- 不修改 `Directory.Packages.props` 除非说明原因
- 生成代码后必须自检：
  - 是否违反 BootstrapBlazor 设计原则
  - 是否破坏现有 API
  - 是否影响主题/本地化
- 本仓库不加载 `bootstrapblazor` skill
- BootstrapBlazor.Server 项目依赖项源码大部分都在 [Github](https://github.com/BootstrapBlazor/BootstrapBlazor.Extensions) 仓库中，国内镜像站 [Gitee](https://gitee.com/LongbowEnterprise/BootstrapBlazor.Extensions) 有同步

---

## 七、文档与示例

- 新组件必须更新 `docs.json` 文件
- 示例必须可运行

---

## 八、禁止事项（红线）

- 禁止修改 Bootstrap 原始 CSS（覆盖除外）
- 禁止直接操作 DOM（除非 JSInterop）
- 禁止提交包含 `Console.WriteLine` 的生产代码

---

## 九、Agent 自检清单（每次生成后）

- [ ] 符合 BootstrapBlazor 架构
- [ ] 不引入新依赖
- [ ] 文档/示例已更新
- [ ] 无硬编码字符串

---

> 本文件由 BootstrapBlazor 维护团队维护  
> 最后更新：2026-11  

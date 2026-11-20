# Bootstrap 6 与 BootstrapBlazor 对比分析

> 信息核对日期：2026-11-20。Bootstrap 6 仍处于 Alpha 阶段，以下结论基于官方 Alpha 1 公告和迁移指南；后续版本可能调整。

## 结论摘要

Bootstrap 6 不只是 Bootstrap 5 的小幅升级，而是一次涉及 CSS 类名、主题体系、Sass 构建方式、浏览器支持范围和 JavaScript 模块加载方式的破坏性升级。BootstrapBlazor 当前内置的是 **Bootstrap 5.3.8**，并在多个组件的 C# 和 JS 代码中直接依赖 Bootstrap 5 的类名及 `window.bootstrap` 全局对象。因此，不能只替换 Bootstrap 静态资源并期待现有组件和主题继续正常工作。

建议先做**隔离式兼容性验证**，保留现有 Bootstrap 5 兼容面；待 Bootstrap 6 API 和 CSS 稳定后，再决定是升级默认资源、提供并行版本，还是维持 Bootstrap 5 支持。最先需要验证的两个技术点是：**v6 ESM 插件与现有 JS 互操作的衔接**，以及**v5 类名/主题 API 与 v6 新类名/主题 Token 的映射**。

## Bootstrap 6 公开进展

- Bootstrap 官方于 **2026-10-08** 发布 Bootstrap 6 Alpha 1，并明确说明 Alpha 阶段仍可能有实质变化，后续还会有更多 Alpha，之后才进入 Beta 和 RC。
- 官方 Alpha 公告提供的 v6 安装版本为 `6.0.0-alpha.1`。截至本文核对日，官方 GitHub Releases 页面显示最新正式发布为 **v5.3.9**；v6 的 Alpha 2 里程碑可见，但里程碑本身不代表已发布。
- Bootstrap 6 提高了最低浏览器版本：Chrome / Edge 130、Firefox 132、Safari 18；不为这些版本以下的浏览器提供回退、前缀或 polyfill。官方称 Bootstrap 5 仍可继续使用，且提及计划发布 v5.4.0，但未在 Alpha 公告中给出 Bootstrap 5 的 EOL 日期。

## 主要变化及对 BB 的影响

| 领域 | Bootstrap 6 变化 | 对 BootstrapBlazor 的影响 |
|---|---|---|
| 响应式类名 | 断点从 infix 改为前缀，例如 `d-md-none` → `md:d-none`、`col-lg-6` → `lg:col-6`；覆盖工具类、网格和组件变体 | BB 组件自身生成的类名、示例代码、用户自定义 CSS/HTML 都可能需要迁移。冒号类名还需检查 HTML、Razor 和构建工具是否按预期处理。 |
| 组件颜色与主题 | 颜色变体转为 `.theme-*` 组合模式；例如原有 `.btn-primary`、`.alert-primary` 等不再是 v6 的主要变体方案 | BB 的 Button 和 Alert 按颜色枚举拼接 `btn-{color}`、`alert-{color}`。只替换 CSS 会使这些变体失效，必须设计映射或更新 BB 的输出，并评估旧主题变量的兼容。 |
| 断点与容器 | `lg`、`xl`、`2xl` 断点和容器宽度调整；`xxl` 改名为 `2xl`；部分响应式行为改用容器查询 | 可能改变现有 Layout、Navbar、Table 等页面的折叠时机和宽度。组件与消费端均需做不同视口、不同容器宽度下的视觉检查。 |
| CSS 与 Sass 定制 | 使用 Sass `@use` / `@forward` 模块系统，放弃 `@import` 定制模式；视觉定制转向 CSS Token Map 和 CSS Variables；引入 CSS cascade layers、`oklch()`、`color-mix()`、`light-dark()` 等 | BB 的自定义 Sass、生成的主题 CSS，以及使用 `--bs-*` 变量的组件样式需要逐项核对。层叠层也可能改变 Bootstrap 与 BB 样式之间原有的覆盖优先级。 |
| JavaScript 插件 | 插件只提供 ESM，不再提供 UMD 或 `window.bootstrap` 全局对象 | 这是高风险项。BB 多个组件 JS 直接调用 `bootstrap.Tooltip`、`bootstrap.Modal`、`bootstrap.Dropdown` 等全局 API。必须改为可被组件 JS 互操作加载的 ESM 模块或实现兼容适配层，并验证初始化、事件、销毁和重复渲染生命周期。 |
| 原生浏览器能力 | Accordion 使用 `<details>/<summary>`；新增基于原生 `<dialog>` 的 Dialog；更多功能采用 CSS 原生能力，减少 JS | 这为简化交互提供机会，但 BB 当前 Modal、Collapse 等组件具有既有 Razor API 和 JS 生命周期。应先判断是否只换底层实现，保持公开 API 与行为兼容，不宜直接照搬 Bootstrap 的新标记结构。 |
| 浏览器基线 | 最低浏览器版本显著提高，且无低版本回退 | 这是面向 BB 所有用户的产品兼容性选择，不只是构建问题；特别要评估老企业浏览器、内嵌 WebView 和用户统计。 |

## 与 BootstrapBlazor 组件的关系

BB 是建立在 Bootstrap 样式与交互之上的 **Blazor 组件库**，并非对 Bootstrap 原生组件的一对一封装。项目有百余个组件目录，既包含 Bootstrap 同类组件的 Blazor 化封装，也包含数据展示、表单、树形结构和业务交互等 BB 自有能力。

| Bootstrap 能力 / 组件族 | BB 对应组件示例 | 对比 |
|---|---|---|
| Button、Badge、Alert、Breadcrumb、Card、Carousel | `Button`、`Badge`、`Alert`、`Breadcrumb`、`Card`、`Carousel` | 复用 Bootstrap 基础样式并增加 Blazor 参数、回调、异步状态、图标、关闭行为等。它们最容易受到类名与 Token 变化影响。 |
| Forms、Inputs、Select、Checkbox、Radio、Switch | `Input`、`InputNumber`、`Select`、`Checkbox`、`Radio`、`Switch`、`DateTimePicker`、`Upload` | BB 提供绑定、校验、泛型、复杂选择、日期时间等 .NET 组件能力；Bootstrap 6 表单样式更新可借用，但不能替代 BB 的数据绑定与验证逻辑。 |
| Modal、Collapse、Dropdown、Popover、Tooltip、Toast、Tab | `Modal`、`Collapse`、`Dropdown`、`Popover`、`Tooltip`、`Toast`、`Tab` | BB 使用 Razor 生命周期与 JS interop 管理交互。Bootstrap 插件从全局对象转为 ESM 会直接影响其 JS 初始化和实例管理。 |
| Grid、Navbar、Pagination、Progress、Spinner、Table | `Row`、`Navbar`、`Pagination`、`Progress`、`Spinner`、`Table` | 部分组件依赖 Bootstrap 栅格、断点和响应式类；Table 等还包含大量 BB 自己的交互能力，改 CSS 需防止影响数据操作行为。 |
| Bootstrap 未覆盖或不等同的能力 | `TreeView`、`QueryBuilder`、`Table` 的筛选/排序/虚拟化、`AutoComplete`、`Transfer`、`Calendar`、`Dialog`、`Layout` 等 | 是 BB 的差异化价值。Bootstrap 6 不能视为 BB 的替代品；升级重点应放在底层样式与插件兼容，而非重做全部组件。 |

## 仓库现状与兼容风险证据

以下为本文在仓库中核对到的实现依据：

- `src/BootstrapBlazor/wwwroot/core/bootstrap/css/bootstrap.css` 标记为 **Bootstrap v5.3.8**；同目录 `js/bootstrap.bundle.js` 也标记为 v5.3.8。官方 Releases 页面在核对日显示 v5.3.9 为最新发布，意味着当前 vendored 资源也落后于当时最新 v5 补丁版本；这是独立于 v6 迁移的维护项。
- `src/BootstrapBlazor/bundler.json` 将 vendored Bootstrap CSS 与 BB 自有 CSS 合并为 `bootstrap.blazor.bundle.min.css`，因此 CSS 资源版本和 BB 样式构建是耦合的。
- `src/BootstrapBlazor/Components/Button/ButtonBase.cs` 按颜色生成 `btn-{color}` / `btn-outline-{color}`；`src/BootstrapBlazor/Components/Alert/Alert.razor.cs` 生成 `alert-{color}`；`src/BootstrapBlazor/Components/Navbar/Navbar.razor.cs` 生成 `navbar-expand-{breakpoint}`。
- 多个组件脚本直接使用全局 `bootstrap` 对象，包括 Button、Modal、Popover、Tooltip、Toast、Carousel、Menu、Table 等，参见 `src/BootstrapBlazor/Components/*/*.razor.js`。
- 默认安装说明在 `README.md` 和 `README.zh-CN.md` 中引用 BB 合并后的 CSS 与 JS bundle。变更底层 Bootstrap 时，需要同步考虑包内 bundle、服务端模板、主题样式、文档示例和用户自定义样式。
- 组件规范 `docs/agents/component-checklist.md` 明确组件样式基于 Bootstrap 5，并要求优先使用 CSS Variables、兼容现有主题。这是评估升级兼容策略时应保留的现有约束。

## 建议的评估与迁移顺序

1. **先确定支持策略**：明确是否继续支持 Bootstrap 5 用户、是否要支持低于 v6 浏览器基线，以及目标是无感升级还是显式选择 v5/v6。
2. **建立最小验证分支/样例**：只替换隔离样例中的 v6 CSS/JS，不先改主分支的默认 bundle。至少覆盖 Button、Alert、Navbar、Modal、Dropdown、Tooltip、Toast、Table、RTL、深色主题和服务端渲染/静态 HTML 场景。
3. **先打通 JS 互操作**：验证 ESM 导入、Blazor 组件初始化和销毁、多实例以及交互事件；统计所有 `bootstrap.*` 全局调用并确定统一适配方案。
4. **再做 CSS 兼容清单**：盘点 BB 生成的旧变体类、响应式类、断点、颜色变量和用户可见示例；确定 v6 新类映射、主题变量策略及旧 API 的过渡方式。
5. **执行视觉与浏览器回归**：重点检查折叠断点、容器查询、CSS cascade layers、暗色模式、`oklch()` 与 RTL；使用真实支持范围内的浏览器验证，并单独决定是否接受 v6 的最低浏览器要求。
6. **待稳定后再决定发布形式**：在 Alpha/Beta 阶段避免承诺不可逆的公开 API 变更。若需要提前试用，应明确标记实验性并隔离资源/构建产物；正式支持前补齐迁移说明和兼容性矩阵。

## 参考资料

### Bootstrap 官方

- [Bootstrap 6 Alpha 公告（2026-10-08）](https://blog.getbootstrap.com/2026/10/08/bootstrap-6-alpha/)
- [Bootstrap 6 v5-to-v6 Migration Guide](https://getbootstrap.com/docs/6.0/guides/migration/)
- [Bootstrap GitHub Releases](https://github.com/twbs/bootstrap/releases)
- [Bootstrap 6 Alpha 2 milestone](https://github.com/twbs/bootstrap/milestone/42)（开发里程碑，不等同已发布版本）

### 本仓库

- [BootstrapBlazor 项目文件](../src/BootstrapBlazor/BootstrapBlazor.csproj)
- [vendored Bootstrap CSS](../src/BootstrapBlazor/wwwroot/core/bootstrap/css/bootstrap.css)
- [vendored Bootstrap JavaScript](../src/BootstrapBlazor/wwwroot/core/bootstrap/js/bootstrap.bundle.js)
- [CSS bundle 配置](../src/BootstrapBlazor/bundler.json)
- [组件规范](agents/component-checklist.md)
- [Button 颜色类生成](../src/BootstrapBlazor/Components/Button/ButtonBase.cs)
- [Alert 颜色类生成](../src/BootstrapBlazor/Components/Alert/Alert.razor.cs)
- [Navbar 响应式类生成](../src/BootstrapBlazor/Components/Navbar/Navbar.razor.cs)

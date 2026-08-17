# BootstrapBlazor × Fluent UI 审查报告

本文档合并三份审查结果：

- **第一部分：组件匹配度审查**——全组件库（`src/BootstrapBlazor/Components/` 120 个组件目录）与微软 Fluent UI Blazor 的组件覆盖对照，回答"组件层面对得上多少"。
- **第二部分：Fluent UI 主题完善计划**——针对 `lee/feat-fluent-ui-theme` 分支的主题审查结论，回答"主题层面对得上多少、如何补齐"。
- **第三部分：弹出层（Flyout）差异统计与 Fluent 适配**——Select 等组件的弹出层与原生 Fluent 的形态差异盘点，及已落地的 CSS 适配。

两部分的关联：第一部分指出的语义错位（Badge/CounterBadge、Anchor 等）同样适用于第二部分第一阶段的 token 对齐工作——对照 Fluent 官方 token 时需按语义而非名字匹配。

---

## 第一部分：组件匹配度审查

### 审查范围与方法

- **本库侧**：`src/BootstrapBlazor/Components/` 下全部 120 个组件目录（含其中的子组件，如 `Button/` 下的 `PopConfirmButton`、`DialButton` 等），逐一核对实际组件类。
- **Fluent 侧**：以微软官方 Blazor 组件库 [microsoft/fluentui-blazor](https://github.com/microsoft/fluentui-blazor)（dev 分支，`src/Core/Components`，约 66 个面向用户的组件）为基准。"Fluent UI" 在此指该 Blazor 组件库，而非 React 版 Fluent UI。
- **匹配等级**：
  - ✅ 直接对应：功能定位一致，可平替
  - 🟡 部分对应：概念相近但能力/粒度/语义有差异，需要改造或组合实现
  - ❌ 无对应：对方没有同类组件
- 卫星扩展包（`src/Extensions/Components/` 下 Chart、Markdown、DockView、Gantt 等 39 个独立 NuGet 包）不计入主表，见本部分附录。

### 总体结论

以本库全部 119 个统计项为基准：

| 匹配度 | 数量 | 占比 |
|---|---|---|
| ✅ 直接对应 | 44 | 37% |
| 🟡 部分对应 | 19 | 16% |
| ❌ 无对应（本库独有/Fluent 缺口） | 56 | 47% |

反向以 Fluent UI Blazor 约 66 个用户组件为基准：

| 本库覆盖情况 | 数量 | 占比 |
|---|---|---|
| ✅ 有对应 | ~42 | 64% |
| 🟡 可组合/近似实现 | ~10 | 15% |
| ❌ 本库缺失 | ~14 | 21% |

**解读**：两套库的"核心 60%"（布局、基础表单、表格、弹窗、反馈组件）高度重合，可以互相迁移；差异主要在两端的特色组件上——本库偏企业应用/国产化场景（Transfer、Cascader、QueryBuilder、Captcha、水印、IP 定位等），Fluent 偏设计系统一致性（设计令牌、NavMenu、Wizard、SortableList、Persona 等）。本库的 ❌ 大多是"本库有、Fluent 没有"，即功能广度上本库明显更大；真正的反向缺口（Fluent 有、本库没有）只有约 14 个。

### 详细对照表

#### 1. 布局组件

| BootstrapBlazor | Fluent UI Blazor | 匹配 | 说明 |
|---|---|---|---|
| Layout / Header / Footer / Side / Main / LayoutSplitBar | FluentMainLayout / FluentLayout / FluentHeader / FluentFooter / FluentMain / FluentBodyContent | ✅ | 一一对应 |
| Row（栅格） | FluentGrid / FluentGridItem | ✅ | 同为 12/24 栅格思路 |
| Stack | FluentStack | ✅ | |
| Split | FluentSplitter / FluentMultiSplitter | ✅ | |
| Toolbar / ToolbarSpace | FluentToolbar / FluentSpacer | ✅ | |
| Waterfall（瀑布流） | — | ❌ | Fluent 无 |
| GroupBox | — | ❌ | 可用 Card/fieldset 替代 |
| Scroll（自定义滚动条） | — | ❌ | |

#### 2. 导航组件

| BootstrapBlazor | Fluent UI Blazor | 匹配 | 说明 |
|---|---|---|---|
| Menu / SideMenu / SubMenu / MenuLink | FluentNavMenu / FluentNavLink / FluentNavGroup / FluentNavMenuTree | ✅ | 侧边导航完全对应 |
| TopMenu（横向菜单） | — | 🟡 | FluentNavMenu 无横向模式，需自建 |
| Dropdown（菜单场景） | FluentMenu / FluentMenuButton | ✅ | |
| ContextMenu | FluentMenu | 🟡 | Fluent 无专用右键菜单，可用 Menu+定位组合 |
| Breadcrumb | FluentBreadcrumb | ✅ | |
| Tab / TabLink | FluentTabs / FluentTab | ✅ | |
| Nav | — | 🟡 | 纯样式化导航链接组，Fluent 用 Tabs/NavMenu 代替 |
| Navbar | — | ❌ | Fluent 无导航条组件 |
| Anchor / AnchorLink | FluentAnchor | 🟡 | 语义不同：本库是页面锚点滚动导航，FluentAnchor 只是超链接样式 |
| Pagination / GotoNavigator | FluentPaginator | 🟡 | FluentPaginator 依附 DataGrid 设计，独立分页能力弱 |
| Step（步骤条） | FluentWizard | 🟡 | FluentWizard 是带内容联动的向导；本库 Step 仅展示步骤 |
| RibbonTab | — | ❌ | |

#### 3. 表单组件

| BootstrapBlazor | Fluent UI Blazor | 匹配 | 说明 |
|---|---|---|---|
| BootstrapInput | FluentTextField | ✅ | |
| BootstrapInputGroup | — | 🟡 | FluentTextField 有 start/end 插槽可部分实现 |
| FloatingLabel | — | ❌ | Fluent 采用固定顶部标签 |
| OtpInput | — | ❌ | |
| Textarea | FluentTextArea | ✅ | |
| InputNumber | FluentNumberField | ✅ | |
| Search | FluentSearch | ✅ | |
| AutoComplete | FluentAutocomplete | ✅ | |
| Select / SelectGeneric | FluentSelect / FluentListbox / FluentCombobox | ✅ | |
| MultiSelect / MultiSelectGeneric | FluentAutocomplete（多选模式） | ✅ | |
| SelectTree / SelectTable / SelectObject | — | ❌ | 树/表格/对象选择器是 Fluent 空白 |
| Cascader | — | ❌ | 级联选择 Fluent 无 |
| Checkbox | FluentCheckbox | ✅ | |
| CheckboxList | — | 🟡 | Fluent 无列表式复选组，需循环组合 |
| Radio / RadioList | FluentRadio / FluentRadioGroup | ✅ | |
| Switch | FluentSwitch | ✅ | |
| NullSwitch（三态） | FluentCheckbox（indeterminate） | 🟡 | |
| Toggle | FluentSwitch | 🟡 | 语义相近、形态不同 |
| Slider | FluentSlider | ✅ | |
| Rate | FluentRating | ✅ | |
| DateTimePicker | FluentDatePicker + FluentTimePicker | ✅ | 本库日期时间一体，Fluent 拆分两个组件 |
| TimePicker | FluentTimePicker | ✅ | |
| ClockPicker（模拟表盘） | — | ❌ | |
| DateTimeRange | — | ❌ | Fluent 知名的长期缺口 |
| ColorPicker | — | ❌ | Fluent 同样没有 |
| Upload 族（Button/Input/Avatar/Card/DropUpload） | FluentInputFile | ✅ | FluentInputFile 支持拖拽与预览；本库形态更多 |
| AutoFill | — | ❌ | |
| Captcha | — | ❌ | |
| IpAddress | — | ❌ | |
| Handwritten（手写签名） | — | ❌ | |
| ValidateForm / EditorForm | FluentEditForm / FluentValidationMessage / FluentValidationSummary / FluentField | 🟡 | 验证能力对等；但本库可按模型自动生成编辑器，Fluent 需手写表单 |
| SearchForm | — | ❌ | |
| QueryBuilder | — | ❌ | |
| Display | — | 🟡 | 只读展示可用只读输入框/文本替代 |
| Segmented | — | ❌ | |
| Transfer | — | ❌ | |
| Button | FluentButton | ✅ | |
| PopConfirmButton | Fluent MessageBox（服务） | 🟡 | 气泡确认 vs 模态确认 |
| DialButton / PulseButton / SlideButton | — | ❌ | |
| SwitchButton / ToggleButton | — | 🟡 | 可用 FluentSwitch 或自建 |

#### 4. 数据展示组件

| BootstrapBlazor | Fluent UI Blazor | 匹配 | 说明 |
|---|---|---|---|
| Table | FluentDataGrid | ✅ | 双方旗舰组件；本库内置 CRUD/工具栏/筛选/导出，FluentDataGrid 更轻量、周边能力需自建 |
| Tree / TreeView | FluentTreeView / FluentTreeItem | ✅ | |
| Calendar | FluentCalendar | ✅ | |
| Card | FluentCard | ✅ | |
| Collapse | FluentAccordion / FluentCollapsibleRegion | ✅ | |
| Skeleton 族（Avatar/Paragraph/Table/Tree/Editor） | FluentSkeleton | ✅ | 本库预设形态更多 |
| Badge（计数角标） | FluentCounterBadge | ✅ | 注意：两侧"Badge"语义错位，见下 |
| Tag | FluentBadge | 🟡 | 本库 Tag ≈ FluentBadge；本库 Badge ≈ FluentCounterBadge |
| ShieldBadge（缎带徽章） | — | ❌ | |
| Avatar | FluentPersona | 🟡 | Fluent 无独立 Avatar，Persona = 头像+姓名+状态 |
| ListView | — | ❌ | |
| ListGroup | — | ❌ | |
| Timeline | — | ❌ | |
| Empty | — | ❌ | |
| Carousel | — | ❌ | FluentFlipper 只是翻页箭头元件 |
| ImageViewer / ImagePreviewer | — | ❌ | |
| FileIcon | — | ❌ | |
| Console | — | ❌ | |
| CountUp | — | ❌ | |
| FlipClock | — | ❌ | |
| Marquee | — | ❌ | |
| Typed（打字机） | — | ❌ | |
| Light（指示灯） | — | ❌ | |
| Watermark | — | ❌ | |
| SpeechWave | — | ❌ | |

#### 5. 反馈组件

| BootstrapBlazor | Fluent UI Blazor | 匹配 | 说明 |
|---|---|---|---|
| Alert | FluentMessageBar | ✅ | |
| Message（全局消息） | FluentMessageBarProvider + MessageService | ✅ | |
| Toast | FluentToast / FluentToastProvider / ToastService | ✅ | Fluent 另有 Communication/Confirmation/Progress 三类预置 Toast |
| Modal / ModalDialog | FluentDialog | ✅ | |
| Dialog 服务族（EditDialog / SearchDialog / ResultDialog） | Fluent DialogService | ✅ | 本库内置编辑/搜索/结果对话框模板，Fluent 需自建内容组件 |
| SweetAlert | Fluent MessageBox | ✅ | |
| Popover | FluentPopover | ✅ | |
| Tooltip | FluentTooltip | ✅ | |
| Progress | FluentProgress | ✅ | |
| Circle（环形进度） | FluentProgressRing | ✅ | |
| Spinner | FluentProgressRing（indeterminate） | 🟡 | |
| Mask | FluentOverlay | ✅ | |
| Drawer | — | ❌ | Fluent 无抽屉组件 |

#### 6. 工具 / 服务 / 其他

| BootstrapBlazor | Fluent UI Blazor | 匹配 | 说明 |
|---|---|---|---|
| Icon / SvgIcon / BootstrapBlazorIcon | FluentIcon | ✅ | Fluent 图标体系（Fluent System Icons）是独立资产包 |
| ThemeProvider | FluentDesignTheme / FluentDesignSystemProvider | 🟡 | 都能切明暗主题；Fluent 的设计令牌体系更完整 |
| Dropzone（DragDrap） | FluentDragContainer / FluentDropZone | 🟡 | 都是拖放容器，交互模型不同 |
| Logout / LogoutLink | FluentProfileMenu | 🟡 | |
| FullScreen / GoTop / Affix / Transition | — | ❌ | |
| Clipboard / Download / Print | — | ❌ | 浏览器工具服务 |
| Geolocation / IPLocator / NetworkMonitor | — | ❌ | 设备/网络服务 |
| Camera | — | ❌ | |
| WebSpeech / Speech 服务 | — | ❌ | |
| Title | — | ❌ | Blazor 内置 PageTitle 可替代 |
| Timer / Block / Reconnector / ErrorLogger | — | ❌ | 框架层设施 |
| Redirect / AutoRedirect | — | ❌ | |
| IntersectionObserver / LoadMore / LazyLoad | — | ❌ | |
| Ajax / ConnectionHub | — | ❌ | |
| IFrame | — | ❌ | |

### Fluent UI Blazor 有、本库缺失的组件（反向缺口，约 14 项）

| Fluent UI Blazor | 本库现状 | 说明 |
|---|---|---|
| FluentWizard / FluentWizardStep | 🟡 Step 仅步骤展示 | 无"步骤+内容联动+上一步/下一步"的向导容器 |
| FluentSortableList | ❌ 仅有 `ISortableList` 接口 | 本库没有独立的可排序列表组件（排序能力集成在 Table 内） |
| FluentAppBar / FluentAppBarItem | ❌ | 移动风格底部应用栏 |
| FluentAnchoredRegion | ❌ | 锚点定位容器（本库 Anchor 是另一概念） |
| FluentHorizontalScroll | ❌ | |
| FluentOverflow / FluentOverflowItem | ❌ | 空间不足自动折叠到溢出菜单 |
| FluentHighlighter | ❌ | 搜索关键字高亮 |
| FluentKeyCode / FluentKeyCodeProvider | ❌ | 全局快捷键 |
| FluentPullToRefresh | ❌ | 移动端下拉刷新 |
| FluentPersona | 🟡 Avatar 近似 | 头像+姓名+在线状态一体 |
| FluentPresenceBadge | ❌ | 在线状态徽标 |
| FluentProfileMenu | 🟡 Logout 近似 | 用户资料菜单 |
| FluentEmoji | ❌ | Fluent Emoji 资产组件 |
| FluentSplashScreen / FluentAccessibilityStatus / FluentPageScript | ❌ | 启动屏/无障碍状态/页脚本，多为模板与框架层设施 |

### 结论要点

1. **核心组件高度重合**：布局、基础输入、选择器、日期时间、表格、树、选项卡、对话框、Toast、Tooltip 等约 44 项可一一平替，占本库组件的 37%；加上可组合实现的 19 项，超过一半的组件在 Fluent 侧有着落。
2. **两库的设计取向不同**：本库是"全家桶"——表格自带 CRUD/筛选/导出、表单可自动生成、大量企业场景组件（Transfer、Cascader、QueryBuilder、Captcha、Handwritten、Watermark）开箱即用；Fluent UI Blazor 走"设计系统 + 轻内核"路线，能力依赖 Fluent 设计令牌与自行组合。
3. **若从本库迁往 Fluent，主要障碍**（Fluent 无对应且常用）：DateTimeRange、Drawer、Transfer、Cascader、Segmented、Timeline、Empty、Carousel、ImageViewer、Upload 多形态预览、EditorForm 自动生成表单、Table 内置 CRUD 工具链。
4. **若从 Fluent 迁往本库，缺口很小**：真正需要补的只有 Wizard 向导容器、SortableList 独立组件、Overflow、HorizontalScroll、Highlighter、KeyCode、AppBar、PullToRefresh 等约 14 项，且多数是边缘场景。
5. **注意语义错位**：`Badge`（本库=计数角标 ↔ FluentCounterBadge；本库 Tag ↔ FluentBadge）、`Anchor`（本库=滚动锚点导航 ↔ FluentAnchor=超链接样式），迁移时需按语义而非名字对应。

### 附：卫星扩展包（未计入主表）

`src/Extensions/Components/` 下另有 39 个独立组件/服务包：Chart、Markdown、CherryMarkdown、SummerNote、CodeEditor、Dock/DockView、Gantt、Player、Topology、WinBox、BarCode、MouseFollower、Live2DDisplay、Html2Pdf、SVGEditor、DriverJs、Splitting、FloatingUI、TableExport、Holiday、各图标包（FontAwesome/Bootstrap/MaterialDesign/AntDesign/Element/IconPark）、Azure/Baidu AI 服务包等。Fluent 侧对应的补充是官方图标/Emoji 资产包与 DataGrid 生态，这些扩展包在 Fluent 侧基本全部无对应，属于本库独有生态。

*数据来源：本仓库 `src/BootstrapBlazor/Components/` 目录实测；[fluentui-blazor dev 分支组件目录](https://github.com/microsoft/fluentui-blazor/tree/dev/src/Core/Components)（GitHub API，2026-08 抓取）。*

---

## 第二部分：Fluent UI 主题完善计划

> 针对 `lee/feat-fluent-ui-theme` 分支（即当前工作分支）。现状核对（2026-08，已实测）：
>
> - 组件库主题文件：`src/BootstrapBlazor/wwwroot/css/fluent.min.css`、`src/BootstrapBlazor/wwwroot/css/motronic.min.css`
> - 官网未压缩源文件：`src/BootstrapBlazor.Server/wwwroot/css/fluent.css`（其文件头注明主题本体随组件库分发）
> - Motronic 目前确为全局固定加载：`src/BootstrapBlazor.Server/Components/App.razor:28`
> - Fluent 主题已在站点主题列表中注册：`src/BootstrapBlazor.Server/appsettings.json:120-121`
>
> 以下计划内容按原审查结论收录。

### 目标

完善 `lee/feat-fluent-ui-theme` 分支，使 Fluent UI 主题具备完整的明暗模式支持、稳定的主题切换机制和可复用的发布质量。

### 第一阶段：补齐主题 Token

- 补充 Fluent light/dark 模式下的 Bootstrap 语义变量：
  - `--bs-emphasis-color`
  - `--bs-secondary-color`
  - `--bs-tertiary-color`
  - `--bs-secondary-bg`
  - `--bs-tertiary-bg`
  - 对应 RGB 变量
- 统一正文、边框、链接、禁用态、焦点态和阴影颜色。
- 检查 Fluent token 与 Microsoft Fluent UI 官方 token 的对应关系。

验收标准：常用 BootstrapBlazor 组件不再出现 Bootstrap 默认灰色或 Motronic 残留色。

### 第二阶段：调整主题加载机制

- 将 `motronic.min.css` 从全局固定加载改为可选主题（现状：固定加载于 `src/BootstrapBlazor.Server/Components/App.razor:28`）。
- 保留 Bootstrap 基础 CSS 作为唯一固定样式。
- 主题切换时只加载当前主题 CSS。
- 确保切换 Fluent、Motronic、Bootstrap 时不会残留上一个主题的变量。

验收标准：主题之间可以互相切换，页面刷新后样式一致，没有旧主题颜色残留。

### 第三阶段：完善组件覆盖

按组件类别逐项检查和补充：

- Layout、Menu、Tabs
- Button、Input、Form、Validation
- Table、ListView、TreeView
- Modal、Drawer、Popover
- Calendar、Picker、Upload
- Switch、Captcha、Transfer
- Hover、Focus、Active、Disabled 状态

验收标准：所有核心组件的默认态、悬浮态、激活态、禁用态和校验态均符合 Fluent UI 风格。

### 第四阶段：建设主题预览与测试

- 扩展主题页面，增加核心组件展示。
- 分别验证：
  - Fluent Light
  - Fluent Dark
  - Bootstrap
  - Motronic
- 增加 CSS 变量检查或基础视觉回归测试。
- 验证移动端主题选择器和滚动行为。

验收标准：主题页面可以覆盖主要组件和状态，明暗模式切换无明显布局或颜色问题。

### 第五阶段：整理发布文件

- 将 `fluent.min.css` 改为真正压缩产物。
- 保留未压缩的 `fluent.css` 作为源码版本（现状：未压缩版在 `src/BootstrapBlazor.Server/wwwroot/css/fluent.css`，压缩版随组件库分发）。
- 明确组件库主题 CSS 与官网专用 CSS 的边界。
- 更新主题使用文档，说明加载顺序和 `data-bs-theme` 用法。

验收标准：主题可以独立复制到其他 BootstrapBlazor 项目使用，文档示例可直接运行。

### 第六阶段：最终验证

执行以下检查：

```powershell
git diff --check
dotnet build
dotnet test
```

并手动验证：

- 主题切换
- 明暗模式切换
- 页面刷新
- 移动端显示
- 主题选择器展开、滚动、关闭
- 组件库静态资源路径
- NuGet 包内是否包含 `fluent.min.css`

### 建议优先级

1. 补齐语义变量，解决颜色混用问题。
2. 修正 Motronic 永久加载问题。
3. 完善核心组件覆盖。
4. 增加主题预览和视觉验证。
5. 最后处理压缩、文档和发布整理。


---

## 第三部分：弹出层（Flyout）差异统计与 Fluent 适配

BootstrapBlazor 的弹出层基于 Bootstrap 的 `.dropdown-menu` / `.popover` / `.tooltip` 三种原语，与原生 Fluent 的 Menu / Popover / Tooltip 形态不同。本部分盘点全部弹出组件的差异，并记录已落地的适配。

### 弹出原语差异总览

| 原语 | BootstrapBlazor（Bootstrap 风格） | 原生 Fluent UI | 核心差异 |
|---|---|---|---|
| `.dropdown-menu` 菜单浮层 | 可带小三角箭头（`.dropdown-menu-arrow`）、圆角偏大、选中项蓝底白字 | Menu/Listbox 浮层：**无箭头**、圆角 4px（borderRadiusMedium）、shadow16、1px 中性描边、选中项左侧 ✓（品牌色）+ 中性灰底 | 箭头、圆角、阴影、选中态、内边距 |
| `.popover` 卡片浮层 | 带箭头、圆角偏大、头部有分隔线 | Popover（v9）：**保留箭头**、圆角 4px、shadow16、标题半粗无分隔线 | 圆角、阴影、头部分隔线 |
| `.popover-dropdown` 面板容器（picker 类） | 带箭头 | DatePicker 等面板浮层在 Fluent 中属 Menu 类：**无箭头** | 箭头 |
| `.tooltip` 提示 | 带箭头、黑色底、半透明 | Tooltip：**无箭头**、深灰面（grey14）、圆角 4px、不透明 | 箭头、背景、不透明度 |

### 弹出层组件逐项统计

**A. `.dropdown-menu` 系（菜单浮层）**

| 组件 | 箭头 | 备注 |
|---|---|---|
| Select / SelectGeneric | 有（`.dropdown-menu-arrow`） | 选中项 `.dropdown-item.active` 蓝底；支持搜索框 |
| MultiSelect / MultiSelectGeneric | 无 | 项内自带复选框，无需 ✓ 适配 |
| SelectTree | 有 | 树形下拉 |
| Cascader | 有 | 级联面板，项带右侧子菜单箭头 |
| SelectTable / SelectObject | 无（dropdown 模式）/ 走 popover 模式 | 面板宽度自定义 |
| AutoComplete / AutoFill / Search | 无 | 候选列表 |
| Dropdown / DropdownWidget | 无 | 通用下拉菜单 |
| TopMenu（SubMenu） | 无 | 顶部菜单下拉 |
| Logout | 无 | 用户菜单 |
| Tab（溢出菜单） | 无 | |
| Table 工具栏 / TableToolbar / FilterButton | 无 | 表格列菜单、筛选面板 |

**B. `.popover` 系（卡片浮层）**

| 组件 | 原生 Fluent 对应 | 箭头处理 |
|---|---|---|
| Popover | FluentPopover（有箭头） | 保留 |
| PopConfirmButton | Fluent Popover 确认 / MessageBox | 保留 |
| DateTimePicker / DateTimeRange（`.picker-panel`，经 `popover-dropdown` 容器弹出） | FluentDatePicker 日历浮层（无箭头） | 隐藏 |
| SelectObject / SelectTable（popover 模式） | 无直接对应 | 隐藏 |
| TableColumnFilter（`popover-dropdown`） | FluentDataGrid 列筛选浮层（无箭头） | 隐藏 |

**C. `.tooltip` 系**

| 组件/使用处 | 适配 |
|---|---|
| Tooltip 组件、GoTop、Layout 侧栏折叠按钮、BootstrapLabel 标签提示 | 去箭头、深灰面、圆角 4px |

**D. 不纳入适配**

| 组件 | 原因 |
|---|---|
| ColorPicker | 使用浏览器原生 `<input type="color">` 弹层，无法用 CSS 主题化；Fluent 侧也无对应组件 |
| IpAddress | 纯行内输入框，无浮层 |
| Modal / Dialog / Drawer / SweetAlert | 属对话框层面，Fluent 无菜单式浮层差异；Modal 已适配 shadow64 |

### 已落地的适配（`src/BootstrapBlazor/wwwroot/css/fluent.min.css` 新增 "Flyout surfaces" 段）

| 适配项 | 实现 | Fluent token 来源 |
|---|---|---|
| 浮层面板 | `--bs-dropdown-bg/border-color/border-radius(4px)/box-shadow` 重指向主题变量 | colorNeutralBackground1、colorNeutralStroke1、borderRadiusMedium、shadow16 |
| 去除菜单箭头 | `.dropdown-menu-arrow { display: none }`；`.popover-dropdown .popover-arrow` 隐藏 | Fluent Menu 无箭头 |
| 浮层间距 | `--bb-select-dropdown-menu-margin-top: 8px → 4px` | spacingVerticalXS |
| 菜单项 | padding 4px 容器 / 6px 12px 项；hover `#f5f5f5`、选中 `#ebebeb`（dark 均 `#3d3d3d`） | colorNeutralBackground1Hover / Selected |
| 选中项 ✓ | `.select:not(.multi-select)` 项预留 28px 左槽，`::before` 品牌色 ✓（绝对定位不影响布局） | Fluent Listbox Option 选中态 |
| Popover 卡片 | 圆角 4px、shadow16、标题半粗去分隔线（箭头保留） | Fluent Popover v9 |
| Tooltip | 去箭头、`#242424` 深灰面、不透明、圆角 4px | Fluent Tooltip |
| 菜单内搜索框分隔线 | `--bb-select-search-border-color` 指向中性描边 | colorNeutralStroke2 |

### 待验证（建议并入第二部分第四阶段）

- 站点主题页切至 Fluent，逐项打开上述弹出组件，核对明暗两种模式。
- 重点核对：Select 选中 ✓ 与分组标题（Divider）的混排、SelectTable/SelectObject 宽面板的定位、TableColumnFilter 去箭头后的偏移、Tooltip 在 dark 模式下的描边可读性。

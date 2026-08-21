# Fluent UI 主题全面审查与组件库兼容性评估（2026-08 轮）

本文档是对 `src/BootstrapBlazor/wwwroot/css/fluent.css`（含 2026-08 新增的"尺寸与间距层"）的第三轮全面审查，覆盖：① 与微软 Fluent 2 官方源（`microsoft/fluentui` master，`@fluentui/tokens` webLightTheme/webDarkTheme + react v9 组件样式）的逐项核对；② 与 BootstrapBlazor 组件库（razor.scss / razor.js / bundle）的兼容性评估。审查按 11 个范围并行进行，所有结论均带证据（文件行号或官方源 URL）。

配套文档：`docs/fluentui-parity-review.md`（第一~三阶段：组件匹配度、颜色 token 对齐、弹层适配）。

---

## 一、总体结论

| 层 | 评价 |
|---|---|
| 浅色语义令牌（60+ 项） | **全部与 webLightTheme 逐一吻合**，无数值错误 |
| 深色令牌块 | 表面/文字/阴影/遮罩/status 色与 webDarkTheme 吻合；**品牌交互色档位系统性偏低一档**（见问题 M1） |
| 尺寸/间距层（新增） | 主体取值准确（btn/input/select/modal/card/alert 与官方源逐项吻合），但**落地方式打穿了组件库的变量机制**，存在 3 处高severity回归 |
| 组件兼容（JS） | **安全**：全部量高/定位走运行时动态测量（getOuterHeight/Popper/offsetWidth），无硬编码行高假设，新尺寸层无 JS 连锁风险 |
| 组件兼容（CSS 特异性） | "同特异性+后加载"策略大面积成立，但 **约 10 处 bundle 更高特异性规则会赢**（见 B 类清单） |
| 选择器有效性 | 主题 183 个类名选择器仅 1 个（`.validation-message`）在 bundle 无对应（框架级兜底，需确认意图） |
| 工程契约 | min 与源同步、var() 引用零落空、14 项契约测试全绿 |

**兼容程度评估**：主题与组件库总体兼容良好，亮色模式可用性高；主要缺口集中在 **暗色模式**（outline 按钮族缺失、品牌色档位、SweetAlert 白字白底）和 **新尺寸层与 BB 变量机制的冲突**（form-control padding、pagination、checkbox 尺寸变体）。

---

## 二、高severity问题（建议优先修复）

### H1. `.form-control` 的 32px 目标未达成（实际 35px）
bundle 内置 `.form-control{min-height:calc(1.5em + .75rem + 2×border)}`，14px 字号下 = 35px > 主题 padding 算出的 32px → 文本输入实际 35/28/42 而非目标的 32/24/40；而 `.form-select`（无 min-height）是 32px → 同一表单内 32/35 混排错位。
**修法**：主题 `.form-control/.form-select/.input-group-text` 规则补 `min-height: 32px`（sm 24 / lg 40）。

### H2. 直写 `.form-control` padding 打穿 DateTimePicker 图标预留区
bundle 走变量链 `.form-control{padding:var(--bb-form-control-padding)}`，`.datetime-picker` 将其重指为 `6px 33px 6px 12px`（右侧 33px 留给绝对定位的日历/clear 图标）。主题 `padding: 5px 12px`（同特异性后加载胜）→ 变量链整体失效，文字压图标。
**修法**：主题改走变量层 `.form-control{--bb-form-control-padding: 5px 12px}`，并在 `.datetime-picker` 上重指 `--bb-dt-picker-input-padding: 5px 33px 5px 12px`（含 has-icon 变体）。

### H3. Checkbox/Radio 尺寸变体与布局变体被压平
主题 `.form-check:not(.form-switch) .form-check-input`（0,3,0 后加载）赢过 bundle 的 `form-check-sm/md/lg/xl/xxl` 尺寸变体、`form-check-reverse`、`is-label`（均为 0,3,0）→ Size 参数全部失效塌缩到 16px；`.table-excel` 单元格内 checkbox 被组件 padding 规则打掉一半后，主题 `margin-left:-32px` 仍生效 → 勾选框被拉出裁剪。
**修法**：主题选择器追加 `:not(.form-check-sm):not(.form-check-md):not(.form-check-lg):not(.form-check-xl):not(.form-check-xxl):not(.form-check-reverse)`，并对 `.is-label`、`.table-excel .table-cell .form-check` 单独豁免。

另注：主题 `padding-left: 32px` 按"官方间距 16px"计算，但官方 label 在 after 位的 padding 实为 `spacingHorizontalXS(4px)`（indicator margin 8px + label 4px = **12px** 间距 → padding-left 应为 28px）；且 BB 另有 `.form-check-input + .form-check-label{margin-inline-start:.5rem}` 叠加 → 当前实际勾到文字间距约 24px，官方 12px，改动前 BB 约 8px。官方 checkbox 行高（点击目标）为 32px，主题 `min-height:20px` 是紧凑取舍，应注释声明。

### H4. SweetAlert 暗色"白字白底"
主题把 `.swal2-title`/`.swal2-content` 改为 `var(--bs-body-color)`（暗色 #fff），但 bundle 弹窗底色写死 `#fff` → 暗色下标题/正文不可见；`.swal2-footer` #adadad 白底上仅 2.3:1。原生 BB 配色暗色虽丑但可读，是主题引入的回归。
**修法**：补 `.swal2-popup{background-color:var(--bs-body-bg)}`（两主题通用）。

### H5. 分页：padding 变量落空 + 当前页残留 #0d6efd
BB 用 `.pagination .page-link{padding:var(--bb-pagination-link-padding)}`（0,2,0）接管 padding，主题的 `--bs-pagination-padding-y/x` 无消费者（实际 30px 非 32px）；`--bs-pagination-active-bg/-border-color` 未重指，独立分页当前页仍 Bootstrap 蓝（注释"current page keeps the themed brand fill"不成立；表格内分页不受影响）。
**修法**：`.pagination` 改设 `--bb-pagination-link-padding: 5px 12px`（注意镜像 375/456px 两个 media query），补 `--bs-pagination-active-bg/border-color: var(--bs-primary)`。

### H6. 暗色 `.btn-outline-*` 全族缺失
`fluent.css:557-651` 仅浅色规则。暗色下：outline-dark 文字/边框 #242424 在 #292929 底上约 1.03:1 **基本隐形**；outline-secondary 文字 2.35:1、hover 底 #f5f5f5 白闪；outline-primary/success/danger 文字 2.4–2.7:1 不达 AA。
**修法**：暗色块补 outline 族（文字/边框用主题已定义的 `*-text-emphasis` tint 色；outline-dark → grey84 #d6d6d6）。

---

## 三、中severity问题

### M1. 暗色品牌交互色档位系统性偏低（跨组件不统一）
官方 dark：`colorBrandBackground=brand70 #115ea3`、`Hover=brand80`、`colorCompoundBrandBackground=brand100 #479ef5`。主题暗色块不重指 `--bs-primary` → `.btn-primary` 暗色 rest/hover 与官方正好颠倒；checkbox checked / radio 圆点 / slider 拇指与进度 / calendar-today / tree-view active / picker hover 暗色仍 brand80（暗底约 2.7:1，不达 AA）；而 switch 暗色已正确用 brand100/110/90 → 同页不同组件品牌色不一致。
**修法**：暗色块统一补一组 brand100 系覆盖（照抄 switch 的既有模式），`.btn-primary` 暗色 rest 改 #115ea3。

### M2. `--bb-height: 35px` 生态未随 32px 控件同步
消费方：`.multi-select .dropdown-toggle{min-height:35px}`、`.form-control.is-display`、`.checkbox-list`、`.datetime-picker-bar{line-height:35px}`、`.multi-select-ph{line-height:35px}`、`.input-group>.form-range` → 这些比主题化输入框高 3px，同行表单错位。
**修法**：`:root` 重指 `--bb-height: 32px; --bb-multi-select-min-height: 32px`（连带检查 skeleton、btn-toggle 等使用方）。

### M3. Select 族触发器 34px
bundle `.select .form-select{padding:6px .75rem}`（0,2,0）胜主题（0,1,0）→ Select/SelectTree/SelectTable/SelectObject/Cascader 触发器 34px，与 32px 输入框并排错位。
**修法**：主题补 `.select{--bb-select-padding: 5px 12px}`（append 图标 30px 区无冲突，已核实）。

### M4. Modal header/footer padding 是死变量 + `.form-footer` 负边距错位
BB `Modal.razor.scss` 硬编码 `.modal-header/.modal-footer{padding:.5rem 1rem}` → 主题的 `--bs-modal-header-padding`/`--bs-modal-footer-gap` 无人消费，标题 16px vs 正文 24px 左缘错位；`.modal-body .form-footer` 负边距按 16px 写死（Table.razor.scss:930）→ 24px padding 下底部分隔线不再贯通。
**修法**：主题直接写 `.modal-header{padding:24px 24px 8px}` / `.modal-footer{padding:16px 24px 24px}` + `.modal-body .form-footer{margin:16px -24px -24px;padding:8px 24px 0}`（注意排除 `.modal-dialog-scrollable` 的 table 变体）。

### M5. hover 规则缺排除链，殃及 no-border/校验态/Display
主题 `.form-control:hover:not(:disabled):not([readonly]):not(:focus)` 未镜像 bundle 的 `:not(.is-valid):not(.is-invalid):not(.no-border):not(.is-display)` → CheckboxList/RadioList 无边框容器 hover 冒灰边；校验态输入 hover 丢绿/红边；Display 只读 hover 出边框。
**修法**：hover 选择器补齐同一排除链。

### M6. `--bs-secondary`→表面色 映射的 3 处隐形
语义映射本身成立，但组件库有 3 处直接拿它当前景/填充：ClockPicker 表盘刻度（白底 #ebebeb 隐形）、Circle `Color.Secondary` 进度环、Checkbox `form-check-secondary` 勾选态（白勾 on #ebebeb ≈1.2:1）。裸用 `.text-secondary`/`.link-secondary`/`.text-bg-secondary` 工具类同样失效（Badge 已被组件级规则救回）。
**修法**：3 条针对性覆盖（刻度 → stroke1 #d1d1d1；circle → #616161；checkbox secondary 勾选 → grey46 档填充）。

### M7. Nav/.nav-pills 未覆盖（#0d6efd 残留）
bundle `.nav-pills{--bs-nav-pills-link-active-bg:#0d6efd}` 硬编码旧蓝；fluent.css 全文无 `.nav` 规则。
**修法**：至少补 `.nav-pills{--bs-nav-pills-link-active-bg:var(--bs-primary)}`。

### M8. 暗色 dropdown 背景被 bundle 反杀
bundle `[data-bs-theme=dark] .dropdown-menu{--bs-dropdown-bg:#343a40}`（0,2,0）胜主题 `.dropdown-menu{--bs-dropdown-bg:var(--bs-body-bg)}`（0,1,0），主题暗色块未重设 → 暗色 TopMenu 下拉底色 Bootstrap 旧灰。
**修法**：主题暗色块补 `--bs-dropdown-bg: var(--bs-body-bg)`。

### M9. Select 弹层 4px 偏移是死代码（组件库拼写 bug）
bundle 消费规则 `.cascade .dropdown-menu,.selec .dropdown-menu{...}` 中 `.selec` 拼错（源 `Select.razor.scss:26`）→ `.select` 永不命中；Select 实际偏移由 C# `Offset="[0,10]"` 决定。**主题无法绕过，需修组件库**：改拼写 + 默认 Offset 改 `[0,4]`。

### M10. 暗色表头三变量被 bundle dark 块击穿
bundle `[data-bs-theme=dark] .table{--bb-table-header-hover-bg:#343a40;--bb-table-header-icon-hover-bg:#6c757d;--bb-table-header-icon-hover-color:#495057}` 定义在 `.table` 元素上，优先于主题在 `.table-container` 上的继承式定义。
**修法**：主题补 `[data-bs-theme='dark'] .table{...}` 覆写。

### M11. MultiSelect popover 弹层误套单选对勾规则
popover 模式 MultiSelect 的 tip 类只有 `select popover-region`（无 `multi-select`）→ 主题 `.select:not(.multi-select) ... .dropdown-item.active::before` 命中多选项 → 多余 ✓ 与 28px 缩进。
**修法**：主题规则追加 `:not(:has(.multi-select-item))`，或库侧给 MultiSelect 的 CustomClassString 加 "multi-select"。

### M12. transition 规则未包 reduced-motion
bundle 有 `prefers-reduced-motion` 关 transition 的媒体查询，主题在无保护下重加 transition 且后加载胜出 → 减弱动效用户仍看到 0.2s 过渡（官方 Fluent 组件全部内置 0.01ms 降级）。
**修法**：主题的 transition 规则外包 `@media (prefers-reduced-motion: no-preference)`。

### M13. Modal 圆角注释标错 token（值 12px vs 官方 Dialog 8px）
`--bs-border-radius-xxl: 12px` 实为 `borderRadius2XLarge`，注释写 borderRadiusXLarge（=8px）；官方 DialogSurface 用 8px。
**修法**：`--bs-modal-border-radius` 改 `var(--bs-border-radius-xl)`（8px）并修正注释；或保留 12px 改注释为 2XLarge。

### M14. Captcha 状态滑块对比度退化
状态底改深红/深绿后，滑块图标色仍 body-color（浅主题近黑）→ 对比度约 1.6:1（原浅底色无此问题）。
**修法**：状态态 `.captcha-bar{color:#fff}`。

---

## 四、低severity问题（打磨项，按主题归并）

**暗色 token 漂移/注释错标**（值差一档灰阶）：
- 暗色 `colorNeutralStroke2` 应为 grey32 `#525252`，主题多处用 grey30 `#4d4d4d`（dropdown divider :795、select search 边 :800、step 线 :876、segmented pressed :1881）。
- 暗色 `colorNeutralBackground1Selected` 应为 grey22 `#383838`，主题用 grey24 `#3d3d3d`（dropdown active :797、tree active :1400、table-drag :1357）→ 暗色选中与 hover 不可区分。
- Avatar 浅色底应为 Background6 `#e6e6e6`（主题 #ebebeb 是 Background5，注释错标）。
- Slider disabled 轨道官方为 `colorNeutralBackgroundDisabled`（#f0f0f0/#141414），主题用 #e0e0e0/#4d4d4d 且注释错标 strokeDisabled。
- `.swal2-footer` 暗色注释 "Stroke2 (grey26)" 双错（#424242=grey26=StrokeDisabled）。
- calendar 暗色 toolbar focus 应用 Pressed grey12 `#1f1f1f`（主题用 Hover 值）。

**暗色遗漏**：
- `.layout` 暗色块漏 `--bb-layout-menu-user-banner-background`（bundle 残留 #2c3034 legacy 色）。
- `.dropdown-logout` 暗色边框仍 brand80（建议 `var(--bs-border-color)`）。
- `.menu.is-bottom .nav` 分割线用 `--bs-gray-400`（暗色下亮灰 #d1d1d1；灰阶未做暗色翻转）。
- `.bb-previewer .bb-viewer-mask` 无暗色 .5 变体；Step 暗色 is-done 态 #107c10 过暗（建议 #54b054）。
- `.btn-close` focus 环仍 Bootstrap 旧蓝（`--bs-btn-close-focus-shadow` 未重指）。

**官方值/形态偏差**（多数为 v8 风格或设计取舍，需定位决策的见第五节）：
- 弹层边框官方为 `colorTransparentStroke`（靠 shadow16 分层），主题用可见 stroke1（注释表述需修正）。
- Popover body padding 官方 medium 16px（主题 12/10）；Toast 竖向 padding 官方 12px（主题 8）；Drawer body 官方水平 24px（BB 16，负边距耦合不建议改）。
- Tabs：官方 36px 高 + 3px 选中指示条（主题/BB 为 40px + 2px）。
- 菜单项官方 padding 全 6px + borderRadiusMedium（主题 6px/12px、直角）；`--bs-dropdown-min-width` 官方 138/300（主题 160 无上限）。
- Select 触发器图标色官方与 chevron 一致用 Foreground3 `#616161`（主题 DateTimePicker 用 Foreground4 #707070）；chevron 注释应为 colorNeutralStrokeAccessible（值巧合相同）。
- 单选 Listbox 对勾官方继承前景（非品牌色）、选中不换底（主题灰底+品牌对勾双重指示）。
- 日历日期格官方 hover 为浅品牌底 `#ebf3fc`、选中为浅底+描边（主题 hover 只改文字色、选中实色品牌底——实色底是官方"今天"的形态）。
- MessageBar(Alert) 官方有可见描边 stroke1/StatusBorder1（主题 `--bs-alert-border:0`；保留 Bootstrap 默认边框即零成本对齐，info 描边需补中性色）。
- Toast header 分隔线未主题化；Toast 表面仍半透明（rgba .85，官方不透明）；Progress 轨道内阴影与 `--bs-progress-bar-bg:#0d6efd` 未处理。
- 校验 invalid 色板：官方表单用 paletteRed（#d13438/#bc2f32），主题用 status cranberry（#c50f1f/#b10e1c）——主题级统一取舍，可保留。
- checkbox/radio disabled 仍 opacity 0.5（官方 disabled tokens，无淡出）；缺 pressed 态；indeterminate 官方为描边+内方块（主题填充式，可接受）。
- `.modal-title` 官方为 subtitle1 16px/600（主题只设字重，仍 20px）；`.swal2-title` 注释 fontSizeBase500 应为 subtitle1。
- `.btn-link` 继承 600 字重（官方 Link 为 Regular）；outline disabled 应透明底+strokeDisabled 描边（主题误填实底）；disabled 描边 token 应为 strokeDisabled 而非=底色。
- `.link-*` 工具类 hover 色是 bundle 硬编码旧色板 + !important（LinkButton 会闪回旧色）。
- `.btn-group-sm/lg > .btn`、`.input-group-sm/lg`、`.col-form-label-sm/lg`、`.pagination-sm/lg`、`.btn-xs` 未随尺寸层同步（BB 组件内部不产出，消费方可用）。
- `.form-control-sm/lg` 圆角官方全尺寸统一 4px（bundle sm 2px/lg 6px）。
- textarea 官方垂直 padding 6px、minHeight 52px（主题 5px 无 min-height）。
- Select 虚拟化 `RowHeight` 默认 33f vs 实际 32px（1px/行 漂移）；Table `RowHeight` 38f vs 实际 37px（主题后反而更准）。
- `.table-search` 33px、`.table-sm` 内编辑控件 30px、遗留 `.tree` 高亮条 29px 等组件内固定高度与新体系差 1-3px。
- `.validation-message` 在 bundle 无对应（仅兜 Blazor 框架 `<ValidationMessage>` 输出；BB 表单不输出该类）——需确认是有意兜底还是死规则。
- `--bs-popover-color` 是死定义（Bootstrap 5.3 无此变量）。

**覆盖缺口（官方有对应物、主题未处理）**：
- [中低] Split 分隔条箭头 hover 残留 `#0d6efd`（variables.scss:586）。
- [中低] ColorPicker 的 Pickr 弹层（IsSupportOpacity=true）完全未主题化（硬编码浅色、无暗色/阴影/圆角）。
- [中低] Cascader 项 hover 无填充（消费 `--bb-menu-item-hover-*`，但该变量只在 `.menu` 上定义 → cascade 上落空）。
- [低] Skeleton 暗色未对齐官方 stencil token（官方暗色 #575757 明显更亮）。
- [低] Toggle（bootstrap-toggle）off 态 Element 灰边框；ListView 卡片阴影 Element 风格；IconDialog 演示色。
- [低] Badge/Avatar/Divider/Carousel/Spinner/Tag/Empty——评估后**不建议覆盖**（自洽或改动收益过低），理由见各分项。

**工程/契约建议**：
- FluentThemeTest 扩展：① 尺寸层存在性断言（`--bs-btn-padding-y:5px`、`min-height:32px`、`padding-left`、`--bs-modal-padding:24px`）；② `--bb-disabled-bg/--bb-border-focus-color/--bb-border-hover-color/--bb-shadow/--bb-hover-shadow` 的暗色对等断言（这些 bb 变量 bundle 无兜底，漏配暗色无回退）；③ ComponentSections 增 `.pagination`/`.form-check`/`.form-range`。
- min 同步校验升级：由"变量集合相等"升级为"对源执行相同压缩变换后与 min 全文相等"（防非变量规则改了忘压缩）。
- RTL：主题物理属性（padding-left/margin-left/background-position:right）与逻辑属性混用，RTL 下 checkbox 缩进、select 图标位置会错（bundle 自身也混用，属一致性问题）。

---

## 五、需要维护者决策的定位项（非缺陷，方向选择）

1. **Tooltip 形态**：官方 v9 默认浅色面+箭头+shadow8+12px/240px；主题当前是 v8 风格深色无箭头。改回官方默认，还是按 inverted 变体对齐（#333/暗 #3d3d3d+恢复箭头），或保留现状修注释？
2. **Menu/Tabs 品牌填充交互**：主题 menu hover=品牌填充+白字、tabs 激活文字=品牌色，是 BB 既有 `#409eff` 交互的延续；官方 MenuItem/Nav/TabList 全部为中性 hover + 中性选中文字（仅图标用品牌色）。保留还是改中性？
3. **Switch 关态**：主题是 v8 填充轨道（#e0e0e0）；官方 v9 为透明底 + strokeAccessible(#616161) 描边/滑块。改否？
4. **焦点环实现**：主题用外侧双色 box-shadow 环；官方 v9 为贴边 1px border 变色 + inset 环（primary 另有内侧 2px onBrand 环）。当前方案在白页上外白环不可见；另 bundle 在 `.btn.active:focus-visible` 等高特异性规则里把焦点环盖掉了（ToggleButton 激活态/展开态焦点环消失），若保留现方案至少需补同特异性选择器。
5. **`.btn-secondary` 角色**：主题 secondary=灰面实心（Background5），官方 secondary appearance=白底+stroke1 描边（更接近主题的 outline-secondary）。是否交换/对齐？
6. **checkbox 行高/点击目标**：官方 32px 行高（indicator 带 8px 垂直 margin）；主题紧凑 20px。若贴官方会显著增大表单行距——密度取舍。
7. **表格行高**：当前 37px 介于官方 small 34 / medium 44 之间，不对齐任何一档（对齐 medium 需 11.5px 分数 padding）。维持现状。
8. **Modal 宽度**：官方 DialogSurface maxWidth 600px（Bootstrap 500px，主题未动）。改否（影响 SweetAlert 与既有表单弹窗布局回归）。

---

## 六、修复优先级建议

- **P0（功能破损/不可见）**：H1 min-height、H2 DateTimePicker padding 走变量层、H3 checkbox 变体排除、H4 swal 暗色底、H5 pagination 变量+active 色、H6 暗色 outline 族。
- **P1（成体系对齐）**：M1 暗色 brand100 系、M2 --bb-height、M3 select padding、M4 modal header/footer、M5 hover 排除链、M6 secondary 三处、M7 nav-pills、M8 暗色 dropdown bg、M12 reduced-motion。
- **P2（组件库侧修复，主题无法绕过）**：M9 Select.razor.scss `.selec` 错拼（+默认 Offset [0,4]）、M11 MultiSelect CustomClassString 补 multi-select 类、bundle `.form-control:focus` 硬编码 `rgba(13,110,253,.25)`（no-border/is-display 焦点环无人接管）。
- **P3（打磨）**：第四节低severity清单与第五节定位项按决策批量处理。

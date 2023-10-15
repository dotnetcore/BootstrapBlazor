<h1 align="center">Bootstrap Blazor 组件库</h1>

<div align="center">
<h2>一套基于 Bootstrap 和 Blazor 的企业级组件库</h2>


[![Nuget](https://img.shields.io/nuget/v/BootstrapBlazor.svg?color=red&logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Nuget](https://img.shields.io/nuget/dt/BootstrapBlazor.svg?logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Commit Date](https://img.shields.io/github/last-commit/ArgoZhang/BootstrapBlazor/main.svg?logo=github&logoColor=green&label=commit)](https://github.com/ArgoZhang/BootstrapBlazor)

</div>

---

### 发布周期

- 主版本号：与 Microsoft .NET 主版本号一致
- 次版本号：每个月增加 1
- 修订版本号：每周一发布正式版

--- 

## Release 2023-10-16 V7.11.1

### Bugs
* fix(MenuLink): 修复 `MenuLink` 地址为 `/` 时始终高亮问题 by @ray-gilbert in https://github.com/dotnetcore/BootstrapBlazor/pull/2204
* fix(CountUp): 修复不显示数字 `0` 问题 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2236
* fix(Upload): 修复重新上传后无法预览问题 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2240
* bug(Table): 修复固定表头模式下最后一列无法拖动调整宽度问题 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2266

### Features
* feat(QueryPageOptions): 增加 `IsVirtualScroll` 参数用于判断是否为虚拟滚动请求数据 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2211
* feat(Responsive): 增加 `ExtraExtraSmall` 阈值 `<375px` by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2215
* feat(Bootstrap): 更新 `bootstrap` 到 `v5.3.2` by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2217
* feat(DockView): 增加 `Reset` 实例方法用于重置布局 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2220
* feat(Dock): 增加 `GetLayoutConfig` 实例方法用于服务器端获得当前布局 `json` 配置 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2223
* feat(ColorPicker): 更新样式适配 `InputGroup` by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2226
* feat(Marquee): 新增 `Marquee` 滚动条幅组件 by @azlis in https://github.com/dotnetcore/BootstrapBlazor/pull/2225
* feat(Layout): 增加 `AllowDragTab` 参数用于控制多标签是否允许拖动功能 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2230
* feat(MaterialDesign): 增加 `TableAdvancedSort` 图标 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2238
* feat(BootstrapInputGroupLabel): 增加 `ShowRequiredMark` 参数用于控制是否显示必填标记符号 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2242
* feat(Carousel): 增加 `PlayMode` 参数用于控制走马灯播放模式 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2252
* feat(Chart): 增加 X 轴 Y 轴刻度等颜色设置 by @azlis in https://github.com/dotnetcore/BootstrapBlazor/pull/2248
* feat(Tooltip): 更新样式适配 `InputGroup` by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2254
* feat(Popover): 更新样式适配 `InputGroup` by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2256
* feat(Switch): 更新样式适配 `InputGroup` by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2259
* feat(Select): 增加 `GroupItemTemplate` 参数用于自定义分组项 `UI` by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2264
* feat(Stack): 新增布局组件 `Stack` 对应 `css` `flex` 布局 by @Vision-Zhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2250
* feat(InputNumber): 增加全局配置用户设置全站 `<input type="number" />` 步长 by @ArgoZhang in https://github.com/dotnetcore/BootstrapBlazor/pull/2268

## New Contributors
* @ray-gilbert made their first contribution in https://github.com/dotnetcore/BootstrapBlazor/pull/2204

**Full Changelog**: https://github.com/dotnetcore/BootstrapBlazor/compare/v7.11.0...v7.11.1
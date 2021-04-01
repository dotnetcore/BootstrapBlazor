<h1 align="center">Bootstrap Blazor 组件库</h1>

<div align="center">
<h2>一套基于 Bootstrap 和 Blazor 的企业级组件库</h2>


[![Nuget](https://img.shields.io/nuget/v/BootstrapBlazor.svg?color=red&logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Nuget](https://img.shields.io/nuget/dt/BootstrapBlazor.svg?logo=nuget&logoColor=green)](https://www.nuget.org/packages/BootstrapBlazor/)
[![Commit Date](https://img.shields.io/github/last-commit/ArgoZhang/BootstrapBlazor/master.svg?logo=github&logoColor=green&label=commit)](https://github.com/ArgoZhang/BootstrapBlazor)

</div>

---

### 发布周期

- 主版本号：与 Microsoft .NET 主版本号一致
- 次版本号：与 Microsoft .NET 次版本号一致
- 修订版本号：每周四发布正式版（翻车紧急修复会跳版本号）日常每天发布新功能测试版本或者修复 BUG 测试版本 beta-##

---

### 5.0.21

`2021-03-18`
 
* !1158 feat(#I3BULP): Table 组件增加数据服参数未设置时使用全局注入数据服务
* !1157 feat(#I3BUL0): MultiSelect 组件支持双向绑定数组类型
* !1155 feat(#I3BNXX): ValidateForm 组件增加 ValidateAllProperties 参数是否开启检查所有字段默认 false 仅检查表单中的绑定字段
* !1154 feat(#I3BN0L): ValidateForm 组件增加 SetError 方法可主动设置绑定字段提示信息
* !1153 feat(#I3BKXC): 优化 Upload 组件 ButtonUpload 模式上传失败后鼠标悬停图标
* !1152 fix(#I3ALSP): 修复 Upload 组件 ShowProgress 与 IsMultiple 参数共同使用时出错问题
* !1151 fix(#I3BGMV): 修复 Table 组件固定列后选中行样式被遮挡问题
* !1147 fix(#I3AB7H): Upload 组件上传中增加 Spin 动画
* !1145 fix(#I3BEBQ): 修复 Cascader 组件选中项样式
* !1144 fix(#I3BBNS): 修复 Table 组件加载树形数据后与 ClickToSelect 参数冲突问题
* !1143 perf(#I3BBMK): 优化 SweetAlert 组件内部弹窗性能
* !1142 feat(#I3BADG): Dialog 组件支持无限弹窗
* !1140 fix(#I3B9GU): 修复 Cascader 组件选择后显示文字不更新问题
* !1139 feat(#I3B8RN): 演示站点增加切换主题功能
* !1138 feat(#I3B8FX): Table 组件增加 ShowErrorToast 参数用于控制是否显示操作提示弹窗
* !1137 feat(#I3B7UV): 新增 Cascader 组件
* !1136 fix(#I3B502): 修复 Table 组件 ClickToSelect 属性与扩展操作按钮冲突问题
* !1134 fix(#I3B50S): 修复 Table 组件树形数据点击节点无刷新问题
* !1131 docs(#I3B1VU): Table 组件更新分页示例
* !1130 feat(#I3B1VL): EditorForm 组件自动布局支持 Textarea 组件类型
* !1128 fix(#I3AZOI): 修复 Menu 组件顶栏超过 5 个子菜单时样式不正确问题
* !1127 perf(#I3AYCH): 优化 Dialog 组件性能阻止点击关闭等按钮导致子组件二次渲染问题
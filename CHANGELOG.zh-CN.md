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

### 5.0.22

`2021-03-24`

#### 增加功能

- !1193 feat(#I3CQYI): Table 组件编辑按钮增加回调委托 [#I3CQYI](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1193)
- !1192 feat(#I3CQY7): 增加 Row 组件用于栅格系统布局 [#I3CQY7](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1192)
- !1191 feat(#I3CQXQ): 自动生成标签 AutoGenerateColumn 增加 ComponentType 属性用于自定义呈现组件 [#I3CQXQ](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1191)
- !1188 feat(#I3CQY7): Switch 组件适配 Row 组件支持 row form-row form-inline 三种模式 [#I3CQY7](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1188)
- !1187 feat(#I3CPJ8): Select 组件适配 Row 组件支持 row form-row form-inline 三种模式 [#I3CPJ8](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1187)
- !1172 feat(#I3CFJS): Table 组件树形数据支持 CRUD 并且保持编辑前展开收缩状态 [#I3CFJS](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1172)
- !1173 feat(#I3CBQT): 增加演示网站 wasm 模式对浏览器是否兼容性提示功能 [#I3CBQT](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1173)
- !1171 feat(#I3CBI6): DataAnnotation 支持复杂类型的验证 [#I3CBI6](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1171)
- !1167 feat(#I3C6GH): Table 组件展开树形数据是增加 Spin 动画效果 [#I3C6GH](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1167)

#### 问题修复

- !1189 fix(#I3CPLZ): 更新 EFCore 数据注入服务获取记录总数逻辑 [#I3CPLZ](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1189)
- !1185 fix(#I3CFAS): 修复 EFCore 数据注入服务排序导致递归循环引用问题 [#I3CFAS](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1185)
- !1183 fix(#I3CF4K): 更新 EFCore 数据注入服务使服务生命周期内部与参数一致默认 Scope [#I3CF4K](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1183)
- !1178 fix(#I3CCK1): 修复全局配置 SwalDelay 参数在 Swal 组件中未生效问题 [#I3CCK1](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1178)
- !1177 fix(#I3CCIQ): 修复全局配置 MessageDelay 参数在 Message 组件中未生效问题 [#I3CCIQ](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1177)
- !1175 fix(#I3CBSW): 修复 Timer 计时器组件不兼容 wasm 模式问题 [#I3CBSW](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1175)
- !1168 fix(#I3C8PH): 修复 Tab 组件首次加载时活动标签页蓝色火柴棍特效不能正常呈现问题 [#I3C8PH](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1168)
- !1163 fix(#I3C2W4): 修复内置数据服务导致高级搜索默认高亮状态问题 [#I3C2W4](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1163)
- !1161 fix(#I3C205): 更新 Switch 组件点击事件触发组件 [#I3C205](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1161)

#### 示例更新

- !1190 docs(#I3CQXN): 更新 Table 组件明细行示例代码
- !1182 docs(#I3CF12): 更新 Table 组件更新固定列示例代码
- !1169 docs(#I3C8Q3): 更新 dialog 示例代码
- !1166 docs(#I3C4HV): 更新 Table 组件固定表头示例代码
- !1165 docs(#I3C4F0): 更新 Table 组件多表头示例代码

#### 性能优化

- !1186 perf(#I3CPID): 优化 ValidateForm 组件提高内部性能 [#I3CPID](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1186)
- !1170 refactor(#I3CA0I): DynamicComponent 改名为 BootstrapDynamicComponent [#I3CA0I](https://gitee.com/LongbowEnterprise/BootstrapBlazor/pulls/1170)


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
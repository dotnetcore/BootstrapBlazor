# BootstrapBlazor 组件库

## 技术栈
- .NET 6 / 7 / 8 / 9 / 10 / 11 多目标框架
- 主库：Razor Class Library
- 演示站：BootstrapBlazor.Server
- 测试：bUnit

## 组件开发规范
- 参数用 [Parameter]，XML 注释必写中英文都要
- WASM 下 JS 互操作不能同步调用
- 本地化用 IStringLocalizer，不硬编码中文文案
- 异步全链路 async/await，禁止 .Result / .Wait()

## 验证命令
- 构建：dotnet build src/BootstrapBlazor/BootstrapBlazor.csproj
- 测试：dotnet test test/UnitTest/UnitTest.csproj

## 红线
- 不破坏现有公共 API（新增可，改签名需标注 Obsolete）
- 不自动 merge 到 main
- 不动 Version.props / Directory.Build.props
- 不提交密钥
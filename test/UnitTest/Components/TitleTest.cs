// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Author: Argo Zhang (argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TitleTest : BootstrapBlazorTestBase
{
    [Fact]
    public void ChildContent_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<MockTitleTest>();
        });
        cut.InvokeAsync(async () =>
        {
            var titleService = cut.FindComponent<MockTitleTest>().Instance.TitleService;
            await titleService.SetTitle("test");
        });

        cut.InvokeAsync(() =>
        {
            var title = cut.FindComponent<Title>();
            title.SetParametersAndRender();

            // 模拟 Module 为空
            var moduleProperty = title.Instance.GetType().GetProperty("Module", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            moduleProperty?.SetValue(title.Instance, null);

            var methodInfo = title.Instance.GetType().GetMethod("SetTitle", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            methodInfo?.Invoke(title.Instance, new object[] { new TitleOption() { Title = "test" } });
        });
    }

    [Fact]
    public void Text_Ok()
    {
        var cut = Context.RenderComponent<Title>(pb =>
        {
            pb.Add(a => a.Text, "Text");
        });
        var text = cut.Instance.Text;
        Assert.Equal("Text", text);
    }

    private class MockTitleTest : ComponentBase
    {
        [Inject]
        [NotNull]
        public TitleService? TitleService { get; set; }
    }
}

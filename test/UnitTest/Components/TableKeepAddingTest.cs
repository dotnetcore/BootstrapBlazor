// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace UnitTest.Components;

/// <summary>
/// <para lang="zh">Table 组件「保存并新增」功能单元测试</para>
/// <para lang="en">Unit test for the Table "Save and Add" feature</para>
/// </summary>
public class TableKeepAddingTest : TableDialogTestBase
{
    /// <summary>
    /// <para lang="zh">弹窗模式测试</para>
    /// <para lang="en">Test for the popup mode</para>
    /// </summary>
    [Fact]
    public async Task Popup_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        items.ForEach(i => i.Count = 0);

        var lastModel = default(Foo);
        var savedModel = default(Foo);
        var closedResult = default(bool?);
        var addCount = 0;

        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.IsMultipleSelect, true);
                pb.Add(a => a.ShowKeepAddingButton, true);
                pb.Add(a => a.KeepAddingButtonText, "test-keep-adding");
                pb.Add(a => a.KeepAddingButtonIcon, "test-keep-adding-icon");
                pb.Add(a => a.KeepAddingButtonColor, Color.Danger);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnAddAsync, () =>
                {
                    addCount++;
                    var foo = Foo.Generate(localizer);
                    foo.Name = $"add-{addCount}";
                    foo.Count = 0;
                    return Task.FromResult(foo);
                });
                pb.Add(a => a.OnSaveAsync, (Foo foo, ItemChangedType itemType) =>
                {
                    savedModel = foo;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnKeepAddingAsync, new Func<Foo, Task<Foo>>(last =>
                {
                    lastModel = last;
                    var foo = Foo.Generate(localizer);
                    foo.Name = "next-name";
                    foo.Count = last.Count + 10;
                    return Task.FromResult(foo);
                }));
                pb.Add(a => a.EditDialogCloseAsync, (model, saved) =>
                {
                    closedResult = saved;
                    return Task.CompletedTask;
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        // 新建弹窗中的「保存并新增」按钮参数已透传
        var editDialog = cut.FindComponent<EditDialog<Foo>>();
        Assert.True(editDialog.Instance.ShowKeepAddingButton);
        Assert.Equal("test-keep-adding", editDialog.Instance.KeepAddingButtonText);
        Assert.Equal("test-keep-adding-icon", editDialog.Instance.KeepAddingButtonIcon);
        Assert.Equal(Color.Danger, editDialog.Instance.KeepAddingButtonColor);

        var editDialogLocalizer = Context.Services.GetRequiredService<IStringLocalizer<EditDialog<Foo>>>();
        var saveAndCloseText = editDialogLocalizer["SaveAndCloseButtonText"].Value;

        // 「保存并新增」按钮已渲染 主保存按钮文本为「保存并关闭」
        var keepAddingButton = cut.Find(".bb-editor-footer .btn-danger");
        Assert.Contains("test-keep-adding", keepAddingButton.TextContent);
        Assert.Contains("test-keep-adding-icon", keepAddingButton.InnerHtml);
        Assert.Contains(saveAndCloseText, cut.Find(".bb-editor-footer .btn-primary").TextContent);

        // 点击「保存并新增」按钮
        var hideCount = Context.JSInterop.Invocations.Count(i => i.Arguments.Any(a => a?.ToString() == "hide"));
        await cut.InvokeAsync(() => keepAddingButton.Click());
        cut.WaitForAssertion(() => Assert.Equal(3, table.Instance.Rows.Count));

        // 当前数据已保存 且保存的数据即回调参数
        Assert.Same(savedModel, lastModel);

        // 弹窗未关闭
        Assert.Equal(hideCount, Context.JSInterop.Invocations.Count(i => i.Arguments.Any(a => a?.ToString() == "hide")));

        // 表单已重新绑定到下一条数据模型
        cut.WaitForAssertion(() => Assert.Equal("next-name", cut.FindComponent<EditorForm<Foo>>().Instance.Model!.Name));
        Assert.Equal(10, cut.FindComponent<EditorForm<Foo>>().Instance.Model!.Count);

        // 下一条数据尚未保存 关闭弹窗时 saved 为 false
        var modal = cut.FindComponent<Modal>();
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Equal(false, closedResult);

        // 再次打开新建弹窗 点击主保存按钮提交 数据保存并关闭弹窗
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        hideCount = Context.JSInterop.Invocations.Count(i => i.Arguments.Any(a => a?.ToString() == "hide"));
        await cut.InvokeAsync(() => cut.Find(".bb-editor-footer .btn-primary").Click());
        cut.WaitForAssertion(() => Assert.Equal(4, table.Instance.Rows.Count));
        cut.WaitForAssertion(() => Assert.Equal(hideCount + 1, Context.JSInterop.Invocations.Count(i => i.Arguments.Any(a => a?.ToString() == "hide"))));

        // 数据已保存 关闭弹窗时 saved 为 true
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());
        Assert.Equal(true, closedResult);

        // 编辑弹窗不显示「保存并新增」按钮
        var checkbox = cut.FindComponents<Checkbox<Foo>>()[1];
        await cut.InvokeAsync(checkbox.Instance.OnToggleClick);
        await cut.InvokeAsync(() => table.Instance.EditAsync());
        Assert.False(cut.FindComponent<EditDialog<Foo>>().Instance.ShowKeepAddingButton);
        Assert.Empty(cut.FindAll(".bb-editor-footer .btn-danger"));
        await cut.InvokeAsync(() => modal.Instance.CloseCallback());

        // 未设置 OnKeepAddingAsync 时回落使用 OnAddAsync 创建下一条数据
        table.Render(pb => pb.Add(a => a.OnKeepAddingAsync, (Func<Foo, Task<Foo>>?)null));
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        Assert.Equal(3, addCount);

        await cut.InvokeAsync(() => cut.Find(".bb-editor-footer .btn-danger").Click());
        cut.WaitForAssertion(() => Assert.Equal(5, table.Instance.Rows.Count));
        Assert.Equal(4, addCount);
        cut.WaitForAssertion(() => Assert.Equal("add-4", cut.FindComponent<EditorForm<Foo>>().Instance.Model!.Name));
    }

    /// <summary>
    /// <para lang="zh">抽屉模式测试</para>
    /// <para lang="en">Test for the drawer mode</para>
    /// </summary>
    [Fact]
    public async Task Drawer_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);
        items.ForEach(i => i.Count = 0);

        var lastModel = default(Foo);
        var savedModel = default(Foo);
        var closedResult = default(bool?);

        var cut = Context.Render<BootstrapBlazorRoot>(pb =>
        {
            pb.AddChildContent<Table<Foo>>(pb =>
            {
                pb.Add(a => a.RenderMode, TableRenderMode.Table);
                pb.Add(a => a.EditMode, EditMode.Drawer);
                pb.Add(a => a.Items, items);
                pb.Add(a => a.ShowToolbar, true);
                pb.Add(a => a.ShowKeepAddingButton, true);
                pb.Add(a => a.TableColumns, foo => builder =>
                {
                    builder.OpenComponent<TableColumn<Foo, string>>(0);
                    builder.AddAttribute(1, "Field", "Name");
                    builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                    builder.CloseComponent();
                });
                pb.Add(a => a.OnAddAsync, () =>
                {
                    var foo = Foo.Generate(localizer);
                    foo.Name = "add-name";
                    foo.Count = 0;
                    return Task.FromResult(foo);
                });
                pb.Add(a => a.OnSaveAsync, (Foo foo, ItemChangedType itemType) =>
                {
                    savedModel = foo;
                    return Task.FromResult(true);
                });
                pb.Add(a => a.OnKeepAddingAsync, new Func<Foo, Task<Foo>>(last =>
                {
                    lastModel = last;
                    var foo = Foo.Generate(localizer);
                    foo.Name = "next-name";
                    foo.Count = last.Count + 10;
                    return Task.FromResult(foo);
                }));
                pb.Add(a => a.EditDialogCloseAsync, (model, saved) =>
                {
                    closedResult = saved;
                    return Task.CompletedTask;
                });
            });
        });

        var table = cut.FindComponent<Table<Foo>>();
        await cut.InvokeAsync(() => table.Instance.AddAsync());

        // 抽屉已打开 且渲染出「保存并新增」按钮
        Assert.True(cut.FindComponent<Drawer>().Instance.IsOpen);
        Assert.Contains("保存并新增", cut.Find(".bb-editor-footer .btn-info").TextContent);

        // 点击「保存并新增」按钮
        await cut.InvokeAsync(() => cut.Find(".bb-editor-footer .btn-info").Click());
        cut.WaitForAssertion(() => Assert.Equal(3, table.Instance.Rows.Count));

        // 抽屉保持打开状态
        Assert.True(cut.FindComponent<Drawer>().Instance.IsOpen);

        // 当前数据已保存 且保存的数据即回调参数
        Assert.Same(savedModel, lastModel);

        // 表单已重新绑定到下一条数据模型
        cut.WaitForAssertion(() => Assert.Equal("next-name", cut.FindComponent<EditorForm<Foo>>().Instance.Model!.Name));
        Assert.Equal(10, cut.FindComponent<EditorForm<Foo>>().Instance.Model!.Count);

        // 点击关闭按钮关闭抽屉 下一条数据尚未保存 saved 为 false
        await cut.InvokeAsync(() => cut.Find(".bb-editor-footer .btn-secondary").Click());
        cut.WaitForAssertion(() => Assert.Empty(cut.FindComponents<Drawer>()));
        Assert.Equal(false, closedResult);

        // 再次打开抽屉 点击主保存按钮提交 抽屉关闭
        await cut.InvokeAsync(() => table.Instance.AddAsync());
        Assert.True(cut.FindComponent<Drawer>().Instance.IsOpen);

        await cut.InvokeAsync(() => cut.Find(".bb-editor-footer .btn-primary").Click());
        cut.WaitForAssertion(() => Assert.Equal(4, table.Instance.Rows.Count));
        cut.WaitForAssertion(() => Assert.Empty(cut.FindComponents<Drawer>()));
    }

    /// <summary>
    /// <para lang="zh">EditDialog 组件单元测试</para>
    /// <para lang="en">Unit test for the EditDialog component</para>
    /// </summary>
    [Fact]
    public async Task EditDialog_Ok()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<EditDialog<Foo>>>();
        var model = new Foo();
        var nextModel = new Foo() { Name = "second-name" };

        var closeCount = 0;
        var saveCount = 0;
        var lastModel = default(Foo);
        var saveResult = true;

        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.AddCascadingValue(new Func<Task>(() =>
            {
                closeCount++;
                return Task.CompletedTask;
            }));
            pb.Add(a => a.Model, model);
            pb.Add(a => a.ShowKeepAddingButton, true);
            pb.Add(a => a.BodyTemplate, new RenderFragment<Foo>(m => builder => builder.AddContent(0, $"MODEL:{m.Name}")));
            pb.Add(a => a.OnSaveAsync, context =>
            {
                saveCount++;
                return Task.FromResult(saveResult);
            });
            pb.Add(a => a.OnKeepAddingAsync, new Func<Foo, Task<Foo>>(last =>
            {
                lastModel = last;
                return Task.FromResult(nextModel);
            }));
        });

        // 默认文本与默认颜色
        cut.Contains(localizer["KeepAddingButtonText"].Value);
        cut.Contains(localizer["SaveAndCloseButtonText"].Value);
        cut.Contains("btn-info");
        cut.Contains("MODEL:");

        // 点击「保存并新增」按钮 保存成功 表单绑定到下一条模型 且不关闭
        await cut.InvokeAsync(() => cut.Find("button.btn-info").Click());
        cut.WaitForAssertion(() => Assert.Equal(1, saveCount));

        Assert.Same(model, lastModel);
        Assert.Equal(0, closeCount);
        cut.WaitForAssertion(() => cut.Contains("MODEL:second-name"));

        // 点击主保存按钮 保存成功并关闭
        await cut.InvokeAsync(() => cut.Find("button.btn-primary").Click());
        cut.WaitForAssertion(() => Assert.Equal(2, saveCount));
        cut.WaitForAssertion(() => Assert.Equal(1, closeCount));

        // 保存失败时保持打开状态 且不创建下一条数据模型
        saveResult = false;
        await cut.InvokeAsync(() => cut.Find("button.btn-primary").Click());
        cut.WaitForAssertion(() => Assert.Equal(3, saveCount));

        Assert.Equal(1, closeCount);
        cut.Contains("MODEL:second-name");

        // 点击「保存并新增」按钮后随即点击主保存按钮 本次提交按「保存并关闭」处理
        saveResult = true;
        var buttons = cut.FindComponents<Button>();
        var keepAddingButton = buttons.First(i => i.Instance.Text == localizer["KeepAddingButtonText"].Value);
        var saveButton = buttons.First(i => i.Instance.Text == localizer["SaveAndCloseButtonText"].Value);
        await cut.InvokeAsync(() => keepAddingButton.Instance.OnClick.InvokeAsync());
        await cut.InvokeAsync(() => saveButton.Instance.OnClick.InvokeAsync());
        await cut.InvokeAsync(() => cut.Find("form").Submit());
        cut.WaitForAssertion(() => Assert.Equal(4, saveCount));

        Assert.Equal(2, closeCount);

        // 未设置 OnKeepAddingAsync 时点击「保存并新增」按钮等同主保存按钮
        cut.Render(pb => pb.Add(a => a.OnKeepAddingAsync, (Func<Foo, Task<Foo>>?)null));
        await cut.InvokeAsync(() => cut.Find("button.btn-info").Click());
        cut.WaitForAssertion(() => Assert.Equal(5, saveCount));

        Assert.Equal(3, closeCount);

        // Tracking 模式不显示「保存并新增」按钮
        cut.Render(pb => pb.Add(a => a.IsTracking, true));
        Assert.Empty(cut.FindAll("button.btn-info"));
        cut.Contains(localizer["SaveButtonText"].Value);
    }

    /// <summary>
    /// <para lang="zh">未设置 OnKeepAddingAsync 时回退使用 OnAddAsync 创建下一条数据 回调返回空对象时抛出异常</para>
    /// <para lang="en">Falls back to the OnAddAsync callback to create the next item when OnKeepAddingAsync is not set. An exception is thrown when a null model is returned</para>
    /// </summary>
    [Fact]
    public async Task OnAddAsync_ReturnNull_Error()
    {
        var localizer = Context.Services.GetRequiredService<IStringLocalizer<Foo>>();
        var items = Foo.GenerateFoo(localizer, 2);

        var cut = Context.Render<Table<Foo>>(pb =>
        {
            pb.Add(a => a.RenderMode, TableRenderMode.Table);
            pb.Add(a => a.Items, items);
            pb.Add(a => a.ShowKeepAddingButton, true);
            pb.Add(a => a.TableColumns, foo => builder =>
            {
                builder.OpenComponent<TableColumn<Foo, string>>(0);
                builder.AddAttribute(1, "Field", "Name");
                builder.AddAttribute(2, "FieldExpression", Utility.GenerateValueExpression(foo, "Name", typeof(string)));
                builder.CloseComponent();
            });
            pb.Add(a => a.OnAddAsync, () => Task.FromResult<Foo>(null!));
        });

        var table = cut.Instance;

        // 触发连续新增时创建下一条数据 反射调用私有方法以便直接断言异常
        var methodInfo = typeof(Table<Foo>).GetMethod("CreateNextEditModelAsync", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
        Assert.NotNull(methodInfo);
        var task = (Task<Foo>)methodInfo.Invoke(table, [items[0]])!;

        // OnAddAsync 返回空对象时抛出异常
        var ex = await Assert.ThrowsAsync<InvalidOperationException>(() => task);
        Assert.Contains(nameof(Table<Foo>.OnAddAsync), ex.Message);

        // 空对象已赋值到 EditModel
        Assert.Null(table.EditModel);
    }
}

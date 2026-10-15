// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.AspNetCore.Components.Forms;
using System.Reflection;

namespace UnitTest.Components;

public class EditDialogTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Items_Ok()
    {
        var foo = new Foo();
        var cut = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, foo);
        });

        cut.Contains("bb-editor");
    }

    /// <summary>
    /// <para lang="zh">OnValidSubmitAsync 方法提交守卫单元测试 未设置 OnSaveAsync 提交中重入 组件已释放三种场景</para>
    /// <para lang="en">Unit test for the submit guard of the OnValidSubmitAsync method, covering the three scenarios: OnSaveAsync not set, reentrant submit and component disposed</para>
    /// </summary>
    [Fact]
    public async Task OnValidSubmitAsync_Guard_Ok()
    {
        var methodInfo = typeof(EditDialog<Foo>).GetMethod("OnValidSubmitAsync", BindingFlags.Instance | BindingFlags.NonPublic);
        var disposedField = typeof(EditDialog<Foo>).GetField("_isDisposed", BindingFlags.Instance | BindingFlags.NonPublic);
        Assert.NotNull(methodInfo);
        Assert.NotNull(disposedField);

        // 未设置 OnSaveAsync 时提交直接返回 不保存也不关闭
        var model1 = new Foo();
        var closeCount = 0;
        var cut1 = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.AddCascadingValue(new Func<Task>(() =>
            {
                closeCount++;
                return Task.CompletedTask;
            }));
            pb.Add(a => a.Model, model1);
        });
        await (Task)methodInfo.Invoke(cut1.Instance, [new EditContext(model1)])!;
        Assert.Equal(0, closeCount);

        // 上一次提交未完成时再次提交 直接返回不重复保存
        var model2 = new Foo();
        var source = new TaskCompletionSource();
        var saveCount = 0;
        var cut2 = Context.Render<EditDialog<Foo>>(pb =>
        {
            pb.Add(a => a.Model, model2);
            pb.Add(a => a.OnSaveAsync, async context =>
            {
                saveCount++;
                await source.Task;
                return true;
            });
        });

        var saving = (Task)methodInfo.Invoke(cut2.Instance, [new EditContext(model2)])!;
        Assert.Equal(1, saveCount);
        Assert.False(saving.IsCompleted);

        await (Task)methodInfo.Invoke(cut2.Instance, [new EditContext(model2)])!;
        Assert.Equal(1, saveCount);

        source.SetResult();
        await saving;
        Assert.Equal(1, saveCount);

        // 组件已释放时提交直接返回
        var model3 = new Foo();
        var cut3 = Context.Render<EditDialog<Foo>>(pb => pb.Add(a => a.Model, model3));
        var dialog = cut3.Instance;
        await ((IAsyncDisposable)dialog).DisposeAsync();
        Assert.True((bool)disposedField.GetValue(dialog)!);

        await (Task)methodInfo.Invoke(dialog, [new EditContext(model3)])!;
    }
}

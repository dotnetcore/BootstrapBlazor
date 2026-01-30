// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// ContextMenu 组件示例
/// </summary>
public partial class ContextMenus
{
    private List<TreeViewItem<TreeFoo>> TreeItems { get; set; } = TreeFoo.GetTreeItems();

    private ConsoleLogger _callbackLogger = default!;

    private ConsoleLogger _disabledLogger = default!;

    private static Task OnCopy(ContextMenuItem item, object value)
    {
        return Task.CompletedTask;
    }

    private static Task OnPaste(ContextMenuItem item, object value)
    {
        return Task.CompletedTask;
    }

    [NotNull]
    private List<Foo>? Items { get; set; }

    [NotNull]
    private Foo? Foo { get; set; }

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        Foo = Foo.Generate(FooLocalizer);
        Items = Foo.GenerateFoo(FooLocalizer);
    }

    TreeFoo? SelectModel = default;
    private Task OnBeforeShowCallback(object? item)
    {
        if (item is TreeFoo foo)
        {
            _callbackLogger.Log($"{foo.Text} trigger");
            SelectModel = foo;
        }
        return Task.CompletedTask;
    }


    Task OnCopySub(ContextMenuItem item, object value)
    {
        return Task.CompletedTask;
    }
    private bool OnDisabledCallback(ContextMenuItem item, object? context)
    {
        var ret = false;
        if (context is Foo foo)
        {
            ret = foo.Id == 1;
            _disabledLogger.Log($"{foo.Name} trigger {item.Text} Disabled: {ret}");
        }
        return ret;
    }
}

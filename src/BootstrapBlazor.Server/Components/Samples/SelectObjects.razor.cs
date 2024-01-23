﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
///
/// </summary>
public partial class SelectObjects
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [NotNull]
    private IEnumerable<ListViews.Product>? Products { get; set; }

    [NotNull]
    private IEnumerable<Foo>? Items { get; set; }

    [NotNull]
    private SelectObject? SelectObject { get; set; }

    protected override void OnInitialized()
    {
        base.OnInitialized();
        Products = Enumerable.Range(1, 8).Select(i => new ListViews.Product()
        {
            ImageUrl = $"./images/Pic{i}.jpg",
            Description = $"Pic{i}.jpg",
            Category = $"Group{(i % 4) + 1}"
        });

        Items = Foo.GenerateFoo(LocalizerFoo, 10);
    }

    private Task OnListViewItemClick(ListViews.Product arg, Action<string>? action)
    {

        //SelectObject.SetSelectValue(arg.Description);
        action?.Invoke(arg.Description);
        // 这里直接调用SelectObject的Close方法来关闭弹窗，具体的关闭时机由用户自己确定
        SelectObject.Close();


        return Task.CompletedTask;
    }
}

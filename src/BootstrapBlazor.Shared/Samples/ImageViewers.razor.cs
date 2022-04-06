// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Common;

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Images 示例类
/// </summary>
public partial class ImageViewers
{
    private List<string> PreviewList { get; } = new();

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PreviewList.AddRange(new string[]
        {
            "_content/BootstrapBlazor.Shared/images/ImageList1.jpeg",
            "_content/BootstrapBlazor.Shared/images/ImageList2.jpeg"
        });
    }

    private static IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = nameof(ImageViewer.Url),
            Description = "图片 Url",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.Alt),
            Description = "原生 alt 属性",
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.ShowPlaceHolder),
            Description = "是否显示占位符 适用于大图片加载",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.HandleError),
            Description = "加载失败时是否显示错误占位符",
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.PlaceHolderTemplate),
            Description = "占位模板 未设置 Url 或者正在加载大图时生效",
            Type = "RenderFragment",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.ErrorTemplate),
            Description = "错误模板 图片路径错误时生效",
            Type = "RenderFragment",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.FitMode),
            Description = "原生 object-fit 属性",
            Type = "ObjectFitMode",
            ValueList = "fill|contain|cover|none|scale-down",
            DefaultValue = "fill"
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.ZIndex),
            Description = "原生 z-index 属性",
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2050"
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.PreviewList),
            Description = "预览大图链接集合",
            Type = "List<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.OnLoadAsync),
            Description = "图片加载成功时回调方法",
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new AttributeItem() {
            Name = nameof(ImageViewer.OnErrorAsync),
            Description = "图片加载失败时回调方法",
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    };
}

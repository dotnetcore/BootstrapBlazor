// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// Images 示例类
/// </summary>
public partial class ImageViewers
{
    private List<string> PreviewList { get; } = [];

    [NotNull]
    private ImagePreviewer? ImagePreviewer { get; set; }

    private Task ShowImagePreviewer() => ImagePreviewer.Show();

    /// <summary>
    /// OnInitialized
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        PreviewList.AddRange(
        [
            "./images/ImageList1.jpeg",
            "./images/ImageList2.jpeg"
        ]);
    }

    private AttributeItem[] GetAttributes() =>
    [
        new() {
            Name = nameof(ImageViewer.Url),
            Description = Localizer["ImageViewersAttrUrl"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ImageViewer.Alt),
            Description = Localizer["ImageViewersAttrAlt"],
            Type = "string",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ImageViewer.ShowPlaceHolder),
            Description = Localizer["ImageViewersAttrShowPlaceHolder"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = nameof(ImageViewer.HandleError),
            Description = Localizer["ImageViewersAttrHandleError"],
            Type = "bool",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = nameof(ImageViewer.PlaceHolderTemplate),
            Description = Localizer["ImageViewersAttrPlaceHolderTemplate"],
            Type = "RenderFragment",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = nameof(ImageViewer.ErrorTemplate),
            Description = Localizer["ImageViewersAttrErrorTemplate"],
            Type = "RenderFragment",
            ValueList = "true|false",
            DefaultValue = "false"
        },
        new() {
            Name = nameof(ImageViewer.FitMode),
            Description = Localizer["ImageViewersAttrFitMode"],
            Type = "ObjectFitMode",
            ValueList = "fill|contain|cover|none|scale-down",
            DefaultValue = "fill"
        },
        new() {
            Name = nameof(ImageViewer.ZIndex),
            Description = Localizer["ImageViewersAttrZIndex"],
            Type = "int",
            ValueList = " — ",
            DefaultValue = "2050"
        },
        new() {
            Name = nameof(ImageViewer.PreviewList),
            Description = Localizer["ImageViewersAttrPreviewList"],
            Type = "List<string>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ImageViewer.IsIntersectionObserver),
            Description = Localizer["ImageViewersAttrIsIntersectionObserver"],
            Type = "bool",
            ValueList = "true/false",
            DefaultValue = "false"
        },
        new() {
            Name = nameof(ImageViewer.OnLoadAsync),
            Description = Localizer["ImageViewersAttrOnLoadAsync"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        },
        new() {
            Name = nameof(ImageViewer.OnErrorAsync),
            Description = Localizer["ImageViewersAttrOnErrorAsync"],
            Type = "Func<string, Task>",
            ValueList = " — ",
            DefaultValue = " — "
        }
    ];
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Shared.Samples;

/// <summary>
/// Rows
/// </summary>
public sealed partial class Rows
{
    private RowFoo Model { get; } = new()
    {
        Name = "张三",
        Count = 23,
        Address = "测试地址",
        DateTime = new DateTime(1997, 12, 05),
        Educations = new List<EnumEducation> { EnumEducation.Primary, EnumEducation.Middle }
    };

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbys { get; set; }

    private RowFoo RowFormModel { get; } = new()
    {
        Name = "张三",
        Count = 23,
        Address = "测试地址",
        DateTime = new DateTime(1997, 12, 05),
        Educations = new List<EnumEducation> { EnumEducation.Primary, EnumEducation.Middle }
    };

    private RowFoo NestedModel { get; } = new()
    {
        Name = "张三",
        Count = 23,
        Address = "测试地址",
        DateTime = new DateTime(1997, 12, 05),
        Educations = new List<EnumEducation> { EnumEducation.Primary, EnumEducation.Middle }
    };

    private RowFoo SpanModel { get; } = new()
    {
        Name = "张三",
        Count = 23,
        Address = "测试地址",
        DateTime = new DateTime(1997, 12, 05),
        Educations = new List<EnumEducation> { EnumEducation.Primary, EnumEducation.Middle }
    };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Hobbys = Foo.GenerateHobbies(LocalizerFoo);
    }

    private class RowFoo : Foo
    {
        [Required(ErrorMessage = "请选择学历")]
        [Display(Name = "学历")]
        [AutoGenerateColumn(Order = 60)]
        public List<EnumEducation>? Educations { get; set; }
    }

    private IEnumerable<AttributeItem> GetAttributes() => new AttributeItem[]
    {
        new AttributeItem() {
            Name = "ItemsPerRow",
            Description = Localizer["RowsItemsPerRow"],
            Type = "enum",
            ValueList = " One,Two,Three,Four,Six,Twelve ",
            DefaultValue = " One "
        },
        new AttributeItem() {
            Name = "RowType",
            Description = Localizer["RowsRowType"],
            Type = "enum?",
            ValueList = "Normal, Inline",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "ColSpan",
            Description = Localizer["RowsColSpan"],
            Type = "int?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new AttributeItem() {
            Name = "MaxCount",
            Description = Localizer["RowsMaxCount"],
            Type = "int?",
            ValueList = "-",
            DefaultValue = "null"
        }
    };
}

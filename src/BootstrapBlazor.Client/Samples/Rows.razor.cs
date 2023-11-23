﻿// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Client.Samples;

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
    private IEnumerable<SelectedItem>? Hobbies1 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbies2 { get; set; }

    [NotNull]
    private IEnumerable<SelectedItem>? Hobbies3 { get; set; }

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

        Hobbies1 = Foo.GenerateHobbies(LocalizerFoo);
        Hobbies2 = Foo.GenerateHobbies(LocalizerFoo);
        Hobbies3 = Foo.GenerateHobbies(LocalizerFoo);
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
        new()
        {
            Name = "ItemsPerRow",
            Description = Localizer["RowsItemsPerRow"],
            Type = "enum",
            ValueList = " One,Two,Three,Four,Six,Twelve ",
            DefaultValue = " One "
        },
        new()
        {
            Name = "RowType",
            Description = Localizer["RowsRowType"],
            Type = "enum?",
            ValueList = "Normal, Inline",
            DefaultValue = "null"
        },
        new()
        {
            Name = "ColSpan",
            Description = Localizer["RowsColSpan"],
            Type = "int?",
            ValueList = "-",
            DefaultValue = "null"
        },
        new()
        {
            Name = "MaxCount",
            Description = Localizer["RowsMaxCount"],
            Type = "int?",
            ValueList = "-",
            DefaultValue = "null"
        }
    };
}

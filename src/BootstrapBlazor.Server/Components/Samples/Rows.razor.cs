// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

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
        Educations = [EnumEducation.Primary, EnumEducation.Middle]
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
        Educations = [EnumEducation.Primary, EnumEducation.Middle]
    };

    private RowFoo NestedModel { get; } = new()
    {
        Name = "张三",
        Count = 23,
        Address = "测试地址",
        DateTime = new DateTime(1997, 12, 05),
        Educations = [EnumEducation.Primary, EnumEducation.Middle]
    };

    private RowFoo SpanModel { get; } = new()
    {
        Name = "张三",
        Count = 23,
        Address = "测试地址",
        DateTime = new DateTime(1997, 12, 05),
        Educations = [EnumEducation.Primary, EnumEducation.Middle]
    };

    /// <summary>
    /// OnInitialized 方法
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Hobbies1 = Foo.GenerateHobbies(FooLocalizer);
        Hobbies2 = Foo.GenerateHobbies(FooLocalizer);
        Hobbies3 = Foo.GenerateHobbies(FooLocalizer);
    }

    private class RowFoo : Foo
    {
        [Required(ErrorMessage = "请选择学历")]
        [Display(Name = "学历")]
        [AutoGenerateColumn(Order = 60)]
        public List<EnumEducation>? Educations { get; set; }
    }
}

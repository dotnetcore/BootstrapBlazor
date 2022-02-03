// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.ComponentModel.DataAnnotations;

namespace BootstrapBlazor.Shared;

/// <summary>
///
/// </summary>
public class Foo
{
    // 列头信息支持 Display DisplayName 两种标签

    /// <summary>
    ///
    /// </summary>
    [Key]
    [Display(Name = "主键")]
    [AutoGenerateColumn(Ignore = true)]
    public int Id { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Required(ErrorMessage = "{0}不能为空")]
    [AutoGenerateColumn(Order = 10, Filterable = true, Searchable = true)]
    [Display(Name = "姓名")]
    public string? Name { get; set; }

    /// <summary>
    ///
    /// </summary>
    [AutoGenerateColumn(Order = 1, FormatString = "yyyy-MM-dd", Width = 180)]
    [Display(Name = "日期")]
    public DateTime? DateTime { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "地址")]
    [Required(ErrorMessage = "{0}不能为空")]
    [AutoGenerateColumn(Order = 20, Filterable = true, Searchable = true)]
    public string? Address { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "数量")]
    [Required]
    [AutoGenerateColumn(Order = 40, Sortable = true)]
    public int Count { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "是/否")]
    [AutoGenerateColumn(Order = 50)]
    public bool Complete { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Required(ErrorMessage = "请选择学历")]
    [Display(Name = "学历")]
    [AutoGenerateColumn(Order = 60)]
    public EnumEducation? Education { get; set; }

    /// <summary>
    ///
    /// </summary>
    [Required(ErrorMessage = "请选择一种{0}")]
    [Display(Name = "爱好")]
    [AutoGenerateColumn(Order = 70, Editable = false)]
    public IEnumerable<string> Hobby { get; set; } = new List<string>();

    #region Static methods
    private static readonly Random random = new();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="localizer"></param>
    /// <returns></returns>
    public static Foo Generate(IStringLocalizer<Foo> localizer) => new()
    {
        Id = 1,
        Name = localizer["Foo.Name", "1000"],
        DateTime = System.DateTime.Now,
        Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
        Count = random.Next(1, 100),
        Complete = random.Next(1, 100) > 50,
        Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
    };

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static List<Foo> GenerateFoo(IStringLocalizer<Foo> localizer, int count = 80) => Enumerable.Range(1, count).Select(i => new Foo()
    {
        Id = i,
        Name = localizer["Foo.Name", $"{i:d4}"],
        DateTime = System.DateTime.Now.AddDays(i - 1),
        Address = localizer["Foo.Address", $"{random.Next(1000, 2000)}"],
        Count = random.Next(1, 100),
        Complete = random.Next(1, 100) > 50,
        Education = random.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middel
    }).ToList();

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static IEnumerable<SelectedItem> GenerateHobbys(IStringLocalizer<Foo> localizer) => localizer["Hobbys"].Value.Split(",").Select(i => new SelectedItem(i, i)).ToList();


    /// <summary>
    /// 通过 Id 获取头像链接
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetAvatarUrl(int id) => $"_content/BootstrapBlazor.Shared/images/avatars/150-{Math.Max(1, id % 25)}.jpg";

    /// <summary>
    /// 通过 Count 获得颜色
    /// </summary>
    /// <param name="count"></param>
    /// <returns></returns>
    public static Color GetProgressColor(int count) => count switch
    {
        >= 0 and < 10 => Color.Secondary,
        >= 10 and < 20 => Color.Danger,
        >= 20 and < 40 => Color.Warning,
        >= 40 and < 50 => Color.Info,
        >= 50 and < 70 => Color.Primary,
        _ => Color.Success
    };

    /// <summary>
    /// 通过 Id 获取 Title
    /// </summary>
    /// <returns></returns>
    private static string GetTitle() => random.Next(1, 80) switch
    {
        >= 1 and < 10 => "Clerk",
        >= 10 and < 50 => "Engineer",
        >= 50 and < 60 => "Manager",
        >= 60 and < 70 => "Chief",
        _ => "General Manager"
    };

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public static string GetTitle(int id) => Cache.GetOrAdd(id, key => GetTitle());

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public static Func<IEnumerable<Foo>, string, SortOrder, IEnumerable<Foo>> GetNameSortFunc() => Utility.GetSortFunc<Foo>();

    private static ConcurrentDictionary<int, string> Cache { get; } = new();
    #endregion
}

/// <summary>
///
/// </summary>
public enum EnumEducation
{
    /// <summary>
    ///
    /// </summary>
    [Display(Name = "小学")]
    Primary,

    /// <summary>
    ///
    /// </summary>
    [Display(Name = "中学")]
    Middel
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Localization;

namespace BootstrapBlazor.Shared.Services;

/// <summary>
/// 演示网站示例数据注入服务实现类
/// </summary>
internal class TableDemoDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
{
    [NotNull]
    private List<TModel>? Items { get; set; }

    private IStringLocalizer<TModel> Localizer { get; set; }

    public TableDemoDataService(IStringLocalizer<TModel> localizer)
    {
        Localizer = localizer;
    }

    /// <summary>
    /// 查询操作方法
    /// </summary>
    /// <param name="options"></param>
    /// <returns></returns>
    public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions options)
    {
        // 此处代码实战中不可用，仅仅为演示而写
        if (Items == null || Items.Count == 0)
        {
            Items = Foo.GenerateFoo((IStringLocalizer<Foo>)Localizer).Cast<TModel>().ToList();
        }

        var total = Items.Count;

        return Task.FromResult(new QueryData<TModel>()
        {
            Items = Items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList(),
            TotalCount = total
        });
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="model"></param>
    /// <param name="changedType"></param>
    /// <returns></returns>
    public override Task<bool> SaveAsync(TModel model, ItemChangedType changedType)
    {
        var ret = false;
        if (model is Foo foo)
        {
            if (changedType == ItemChangedType.Add)
            {
                var id = Items.Count + 1;
                while (Items.FirstOrDefault(item => (item as Foo)!.Id == id) != null)
                {
                    id++;
                }
                var item = new Foo()
                {
                    Id = id,
                    Name = foo.Name,
                    Address = foo.Address,
                    Complete = foo.Complete,
                    Count = foo.Count,
                    DateTime = foo.DateTime,
                    Education = foo.Education,
                    Hobby = foo.Hobby
                } as TModel;
                Items.Add(item!);
            }
            else
            {
                var item = Items.First(item =>
                {
                    var f = item as Foo;
                    return f?.Id == foo.Id;
                });
                var f = item as Foo;
                f!.Name = foo.Name;
                f!.Address = foo.Address;
                f!.Complete = foo.Complete;
                f!.Count = foo.Count;
                f!.DateTime = foo.DateTime;
                f!.Education = foo.Education;
                f!.Hobby = foo.Hobby;
            }
            ret = true;
        }
        return Task.FromResult(ret);
    }

    public override Task<bool> DeleteAsync(IEnumerable<TModel> models)
    {
        foreach (var model in models)
        {
            Items?.Remove(model);
        }

        return base.DeleteAsync(models);
    }
}

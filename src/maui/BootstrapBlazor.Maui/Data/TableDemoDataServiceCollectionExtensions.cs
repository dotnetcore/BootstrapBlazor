// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;

namespace BootstrapBlazor.Maui.Data;

/// <summary>
/// BootstrapBlazor 服务扩展类
/// </summary>
public static class TableDemoDataServiceCollectionExtensions
{
    /// <summary>
    /// 增加 PetaPoco 数据库操作服务
    /// </summary>
    /// <param name="services"></param>
    /// <returns></returns>
    public static IServiceCollection AddTableDemoDataService(this IServiceCollection services)
    {
        services.AddScoped(typeof(IDataService<>), typeof(TableDemoDataService<>));
        return services;
    }
}

/// <summary>
/// 演示网站示例数据注入服务实现类
/// </summary>
internal class TableDemoDataService<TModel> : DataServiceBase<TModel> where TModel : class
{
    private static readonly ConcurrentDictionary<Type, Func<IEnumerable<TModel>, string, SortOrder, IEnumerable<TModel>>> SortLambdaCache = new();

    [NotNull]
    private List<TModel>? Items { get; set; }

    private IStringLocalizer<Foo> Localizer { get; set; }

    public TableDemoDataService(IStringLocalizer<Foo> localizer)
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
        // 此处代码实战中不可用，仅仅为演示而写防止数据全部被删除
        if (Items == null || Items.Count == 0)
        {
            Items = Foo.GenerateFoo(Localizer).Cast<TModel>().ToList();
        }

        var items = Items.AsEnumerable();
        var isSearched = false;
        // 处理高级查询
        if (options.SearchModel is Foo model)
        {
            if (!string.IsNullOrEmpty(model.Name))
            {
                items = items.Cast<Foo>().Where(item => item.Name?.Contains(model.Name, StringComparison.OrdinalIgnoreCase) ?? false).Cast<TModel>();
            }

            if (!string.IsNullOrEmpty(model.Address))
            {
                items = items.Cast<Foo>().Where(item => item.Address?.Contains(model.Address, StringComparison.OrdinalIgnoreCase) ?? false).Cast<TModel>();
            }

            isSearched = !string.IsNullOrEmpty(model.Name) || !string.IsNullOrEmpty(model.Address);
        }

        if (options.Searches.Any())
        {
            // 针对 SearchText 进行模糊查询
            items = items.Where(options.Searches.GetFilterFunc<TModel>(FilterLogic.Or));
        }

        // 过滤
        var isFiltered = false;
        if (options.Filters.Any())
        {
            items = items.Where(options.Filters.GetFilterFunc<TModel>());
            isFiltered = true;
        }

        // 排序
        var isSorted = false;
        if (!string.IsNullOrEmpty(options.SortName))
        {
            // 外部未进行排序，内部自动进行排序处理
            var invoker = SortLambdaCache.GetOrAdd(typeof(Foo), key => LambdaExtensions.GetSortLambda<TModel>().Compile());
            items = invoker(items, options.SortName, options.SortOrder);
            isSorted = true;
        }

        var total = items.Count();

        return Task.FromResult(new QueryData<TModel>()
        {
            Items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList(),
            TotalCount = total,
            IsFiltered = isFiltered,
            IsSorted = isSorted,
            IsSearch = isSearched
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
                var f = Items.OfType<Foo>().FirstOrDefault(i => i.Id == foo.Id);
                if (f != null)
                {
                    f.Name = foo.Name;
                    f.Address = foo.Address;
                    f.Complete = foo.Complete;
                    f.Count = foo.Count;
                    f.DateTime = foo.DateTime;
                    f.Education = foo.Education;
                    f.Hobby = foo.Hobby;
                }
            }
            ret = true;
        }
        return Task.FromResult(ret);
    }

    public override Task<bool> DeleteAsync(IEnumerable<TModel> models)
    {
        foreach (var model in models)
        {
            Items.Remove(model);
        }

        return base.DeleteAsync(models);
    }
}

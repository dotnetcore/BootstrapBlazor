// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// DockView 基类
/// </summary>
public abstract class BaseDockView : ComponentBase
{
    [Inject]
    [NotNull]
    private ICacheManager? CacheManager { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? LocalizerFoo { get; set; }

    [Inject]
    [NotNull]
    private MockDataTableDynamicService? DataTableDynamicService { get; set; }

    /// <summary>
    /// 获得/设置 数据集合
    /// </summary>
    [NotNull]
    protected IEnumerable<Foo>? Items { get; set; }

    /// <summary>
    /// 获得/设置 带层次结构的数据集合
    /// </summary>
    [NotNull]
    protected List<TreeFoo>? TreeItems { get; set; }

    /// <summary>
    /// 获得/设置 <see cref="DataTableDynamicContext"/> 实例
    /// </summary>
    protected DataTableDynamicContext? DataTableDynamicContext { get; set; }

    /// <summary>
    /// 获得 <see cref="DynamicObjectContext"/> 实例方法
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    protected DynamicObjectContext GetDetailDataTableDynamicContext(DynamicObject context) => DataTableDynamicService.CreateDetailContext(context);

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        Items = Foo.GenerateFoo(LocalizerFoo, 50);

        // 模拟数据从数据库中获得
        TreeItems = TreeFoo.GenerateFoos(LocalizerFoo, 50);

        // 插入 Id 为 1 的子项
        TreeItems.AddRange(TreeFoo.GenerateFoos(LocalizerFoo, 2, 1, 100));

        // 插入 Id 为 101 的子项
        TreeItems.AddRange(TreeFoo.GenerateFoos(LocalizerFoo, 3, 101, 1010));

        DataTableDynamicContext = DataTableDynamicService.CreateContext();
    }

    /// <summary>
    /// 增加方法
    /// </summary>
    /// <returns></returns>
    protected Task<Foo> OnAddAsync() => Task.FromResult(new Foo() { Id = GenerateId(), DateTime = DateTime.Now, Address = $"Custom address  {DateTime.Now.Second}" });

    private int GenerateId()
    {
        var id = Items.Count();
        while (Items.Any(i => i.Id == id))
        {
            id++;
        }
        return id;
    }

    /// <summary>
    /// 转换方法
    /// </summary>
    /// <param name="items"></param>
    /// <returns></returns>
    protected static Task<IEnumerable<TableTreeNode<TreeFoo>>> TreeNodeConverter(IEnumerable<TreeFoo> items)
    {
        // 构造树状数据结构
        var ret = BuildTreeNodes(items.ToList(), 0);
        return Task.FromResult(ret.AsEnumerable());
    }

    private static List<TableTreeNode<TreeFoo>> BuildTreeNodes(List<TreeFoo> items, int parentId)
    {
        var ret = new List<TableTreeNode<TreeFoo>>();
        ret.AddRange(items.Where(i => i.ParentId == parentId).Select((foo, index) => new TableTreeNode<TreeFoo>(foo)
        {
            // 此处为示例，假设偶行数据都有子数据
            HasChildren = index % 2 == 0,
            // 如果子项集合有值 则默认展开此节点
            IsExpand = items.Any(i => i.ParentId == foo.Id),
            // 获得子项集合
            Items = BuildTreeNodes(items.Where(i => i.ParentId == foo.Id).ToList(), foo.Id)
        }));
        return ret;
    }

    /// <summary>
    /// 展开方法
    /// </summary>
    /// <param name="foo"></param>
    /// <returns></returns>
    protected Task<IEnumerable<TableTreeNode<TreeFoo>>> OnTreeExpand(TreeFoo foo) => CacheManager.GetOrCreateAsync($"{foo.Id}", async entry =>
    {
        // 模拟从数据库中查询
        await Task.Delay(1000);
        entry.SlidingExpiration = TimeSpan.FromMinutes(10);
        return TreeFoo.GenerateFoos(LocalizerFoo, 2, foo.Id, foo.Id * 100).Select(i => new TableTreeNode<TreeFoo>(i));
    });

    /// <summary>
    /// Tree 示例数据类
    /// </summary>
    public class TreeFoo : Foo
    {
        /// <summary>
        /// 获得/设置 父级节点 Id
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// GenerateFoos
        /// </summary>
        /// <returns></returns>
        public static List<TreeFoo> GenerateFoos(IStringLocalizer<Foo> localizer, int count = 80, int parentId = 0, int id = 0) => [.. Enumerable.Range(1, count).Select(i => new TreeFoo()
        {
            Id = id + i,
            ParentId = parentId,
            Name = localizer["Foo.Name", $"{id + i:d4}"],
            DateTime = System.DateTime.Now.AddDays(i - 1),
            Address = localizer["Foo.Address", $"{Random.Shared.Next(1000, 2000)}"],
            Count = Random.Shared.Next(1, 100),
            Complete = Random.Shared.Next(1, 100) > 50,
            Education = Random.Shared.Next(1, 100) > 50 ? EnumEducation.Primary : EnumEducation.Middle
        })];
    }
}

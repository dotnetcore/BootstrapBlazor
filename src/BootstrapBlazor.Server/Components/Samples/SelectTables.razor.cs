// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples;

/// <summary>
/// 可选择表格组件示例
/// </summary>
public partial class SelectTables
{
    [Inject]
    [NotNull]
    private IStringLocalizer<Foo>? FooLocalizer { get; set; }

    [Inject]
    [NotNull]
    private IStringLocalizer<SelectTables>? Localizer { get; set; }

    private readonly int[] PageItemsSource = [10, 20, 40];

    private Foo? _foo;

    private Foo? _colorFoo;

    private Foo? _templateFoo;

    private Foo? _disabledFoo;

    private Foo? _sortableFoo;

    private Foo? _searchFoo;

    private readonly SelectTableMode Model = new();

    private List<Foo> _selectedFoos = [];

    private static string? GetTextCallback(Foo foo) => foo.Name;

    private List<Foo> _items = default!;

    private IEnumerable<Foo> _filterItems = default!;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    protected override void OnInitialized()
    {
        base.OnInitialized();

        _items = Foo.GenerateFoo(FooLocalizer);
        _filterItems = Foo.GenerateFoo(FooLocalizer);
    }

    private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
    {
        // 此处代码拷贝后需要自行更改根据 options 中的条件从数据库中获取数据集合
        return Task.FromResult(new QueryData<Foo>()
        {
            Items = _items
        });
    }

    private Task<QueryData<Foo>> OnFilterQueryAsync(QueryPageOptions options)
    {
        // 此处代码拷贝后需要自行更改根据 options 中的条件从数据库中获取数据集合
        var items = _filterItems.Where(options.ToFilterFunc<Foo>());

        if (!string.IsNullOrEmpty(options.SortName))
        {
            items = items.Sort(options.SortName, options.SortOrder);
        }

        var count = items.Count();
        if (options.IsPage)
        {
            items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems);
        }

        return Task.FromResult(new QueryData<Foo>()
        {
            Items = items.ToList(),
            TotalCount = count,
            IsAdvanceSearch = true,
            IsFiltered = true,
            IsSearch = true,
            IsSorted = true
        });
    }

    class SelectTableMode
    {
        [Required]
        public Foo? Foo { get; set; }
    }
}

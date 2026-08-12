using BootstrapBlazor.Components;
using BootstrapBlazorTest.Server.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazorTest.Server.Components.Pages
{
    /// <summary>
    /// 表格编辑示例
    /// </summary>
    public partial class TableDemo : ComponentBase
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        private readonly ConcurrentDictionary<Foo, IEnumerable<SelectedItem>> _cache = new();

        private IEnumerable<SelectedItem> GetHobbies(Foo item) => _cache.GetOrAdd(item, f => Foo.GenerateHobbies(Localizer));

        private static IEnumerable<int> PageItemsSource => new int[] { 20, 40 };

        [NotNull]
        private List<Foo>? Items { get; set; }

        /// <summary>
        /// 查询操作方法
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
        {
            // 此处代码实战中不可用，仅仅为演示而写防止数据全部被删除
            if (Items == null || Items.Count == 0)
            {
                Items = Foo.GenerateFoo(Localizer);
            }

            var items = Items.Where(options.ToFilterFunc<Foo>());
            // 排序
            var isSorted = false;
            if (!string.IsNullOrEmpty(options.SortName))
            {
                items = items.Sort(options.SortName, options.SortOrder);
                isSorted = true;
            }

            var total = items.Count();
            return Task.FromResult(new QueryData<Foo>()
            {
                Items = items.Skip((options.PageIndex - 1) * options.PageItems).Take(options.PageItems).ToList(),
                TotalCount = total,
                IsFiltered = true,
                IsSorted = isSorted,
                IsSearch = true
            });
        }

        private Task<bool> OnSaveAsync(Foo foo, ItemChangedType changedType)
        {
            var ret = false;
            if (changedType == ItemChangedType.Add)
            {
                var id = Items.Count + 1;
                while (Items.Find(item => item.Id == id) != null)
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
                };
                Items.Add(item);
            }
            else
            {
                var f = Items.Find(i => i.Id == foo.Id);
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
            return Task.FromResult(ret);
        }

        private Task<bool> OnDeleteAsync(IEnumerable<Foo> foos)
        {
            foreach (var foo in foos)
            {
                Items.Remove(foo);
            }

            return Task.FromResult(true);
        }
    }
}

using BootstrapBlazor.Components;
using BootstrapBlazorTest.Server.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Diagnostics.CodeAnalysis;

namespace BootstrapBlazorTest.Server.Components.Pages
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Users
    {
        [Inject]
        [NotNull]
        private IStringLocalizer<Foo>? Localizer { get; set; }

        /// <summary>
        /// 获得/设置 分页配置数据源
        /// </summary>
        private static IEnumerable<int> PageItemsSource => new int[] { 10, 20, 40 };

        private static string GetAvatarUrl(int id) => $"images/avatars/150-{id}.jpg";

        private static Color GetProgressColor(int count) => count switch
        {
            >= 0 and < 10 => Color.Secondary,
            >= 10 and < 20 => Color.Danger,
            >= 20 and < 40 => Color.Warning,
            >= 40 and < 50 => Color.Info,
            >= 50 and < 70 => Color.Primary,
            _ => Color.Success
        };

        [NotNull]
        private List<Foo>? Items { get; set; }

        private Task<QueryData<Foo>> OnQueryAsync(QueryPageOptions options)
        {
            // 此处代码实战中不可用，仅仅为演示而写防止数据全部被删除
            if (Items == null || Items.Count == 0)
            {
                Items = Foo.GenerateFoo(Localizer, 23);
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
    }
}

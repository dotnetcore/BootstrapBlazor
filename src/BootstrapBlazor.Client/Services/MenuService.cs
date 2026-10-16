using System.Collections.Concurrent;
using System.Globalization;

namespace BootstrapBlazor.Client.Services;

internal sealed class MenuService(IStringLocalizer<BootstrapBlazor.Server.Components.Layout.NavMenu> localizer)
{
    private ConcurrentDictionary<string, List<MenuItem>> MenuCache { get; } = new();

    public List<MenuItem> GetMenus() => MenuCache.GetOrAdd(CultureInfo.CurrentCulture.Name, _ => localizer.GenerateMenus());
}

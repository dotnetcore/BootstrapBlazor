using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazorTest.Server.Components.Layout
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class MainLayout
    {
        private bool UseTabSet { get; set; } = true;

        private string Theme { get; set; } = "";

        private bool IsOpen { get; set; }

        private bool IsFixedHeader { get; set; } = true;

        private bool IsFixedTabHeader { get; set; } = true;

        private bool IsFixedFooter { get; set; } = true;

        private bool IsFullSide { get; set; } = true;

        private bool ShowFooter { get; set; } = true;

        private bool ShowTabInHeader { get; set; } = true;

        private List<MenuItem>? Menus { get; set; }

        /// <summary>
        /// OnInitialized 方法
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            Menus = GetIconSideMenuItems();
        }

        private static List<MenuItem> GetIconSideMenuItems()
        {
            var menus = new List<MenuItem>
            {
                new() { Text = "返回组件库", Icon = "fa-solid fa-fw fa-home", Url = "https://www.blazor.zone/components" },
                new() { Text = "Index", Icon = "fa-solid fa-fw fa-flag", Url = "/" , Match = NavLinkMatch.All},
                new() { Text = "Counter", Icon = "fa-solid fa-fw fa-check-square", Url = "/counter" },
                new() { Text = "Weather", Icon = "fa-solid fa-fw fa-database", Url = "/weather" },
                new() { Text = "Table", Icon = "fa-solid fa-fw fa-table", Url = "/table" },
                new() { Text = "花名册", Icon = "fa-solid fa-fw fa-users", Url = "/users" }
            };

            return menus;
        }

        private Task OnSideChanged(bool v)
        {
            IsFullSide = v;
            StateHasChanged();
            return Task.CompletedTask;
        }
    }
}

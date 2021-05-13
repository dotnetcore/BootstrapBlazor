// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Shared
{
    /// <summary>
    ///
    /// </summary>
    public sealed partial class NavMenu
    {
        private bool collapseNavMenu = true;

        [Inject]
        [NotNull]
        private IStringLocalizer<App>? AppLocalizer { get; set; }

        [Inject]
        [NotNull]
        private IStringLocalizer<NavMenu>? Localizer { get; set; }

        private string? NavMenuCssClass => CssBuilder.Default("sidebar-content")
            .AddClass("collapse", collapseNavMenu)
            .Build();

        private List<MenuItem> Menus { get; set; } = new List<MenuItem>(100);

        /// <summary>
        ///
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            InitMenus();
        }

        private async Task OnClickMenu(MenuItem item)
        {
            if (!item.Items.Any())
            {
                ToggleNavMenu();
                StateHasChanged();
            }

            if (!string.IsNullOrEmpty(item.Text))
            {
                await TitleService.SetWebSiteTitle($"{item.Text} - {AppLocalizer["Title"]}");
            }
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void InitMenus()
        {
            // 快速入门
            var item = new DemoMenuItem()
            {
                Text = Localizer["GetStarted"],
                Icon = "fa fa-fw fa-fa"
            };
            AddQuickStar(item);

            item = new DemoMenuItem()
            {
                Text = Localizer["LayoutComponents"],
                Icon = "fa fa-fw fa-desktop"
            };
            AddLayout(item);

            item = new DemoMenuItem()
            {
                Text = Localizer["NavigationComponents"],
                Icon = "fa fa-fw fa-bars"
            };
            AddNavigation(item);

            item = new DemoMenuItem()
            {
                Text = Localizer["FormsComponents"],
                Icon = "fa fa-fw fa-cubes"
            };
            AddForm(item);

            item = new DemoMenuItem()
            {
                Text = Localizer["DataComponents"],
                Icon = "fa fa-fw fa-database"
            };
            AddData(item);

            item = new DemoMenuItem()
            {
                Text = Localizer["NotificationComponents"],
                Icon = "fa fa-fw fa-comments"
            };
            AddNotice(item);

            item = new DemoMenuItem()
            {
                Text = Localizer["Components"],
                Icon = "fa fa-fw fa-fa",
                Url = "components"
            };
            AddSummary(item);
        }

        private void AddQuickStar(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Introduction"],
                Url = "introduction"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Install"],
                Url = "install"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["ProjectTemplate"],
                Url = "template"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Globalization"],
                Url = "globalization"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Localization"],
                Url = "localization"
            });
            item.AddItem(new DemoMenuItem()
            {
                IsNew = true,
                Text = Localizer["Labels"],
                Url = "labels"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["ServerBlazor"],
                Url = "install-server"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["ClientBlazor"],
                Url = "install-wasm"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["LayoutPage"],
                Url = "layout-page"
            });

            item.IsCollapsed = false;
            Menus.Add(item);
        }

        private void AddForm(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["AutoComplete"],
                Url = "autocompletes"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Button"],
                Url = "buttons"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Cascader"],
                Url = "cascaders"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Checkbox"],
                Url = "checkboxs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["CheckboxList"],
                Url = "checkboxlists"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["ColorPicker"],
                Url = "colorpickers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["DateTimePicker"],
                Url = "datetimepickers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["DateTimeRange"],
                Url = "datetimeranges"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Editor"],
                Url = "editors"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["EditorForm"],
                Url = "editorforms"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Input"],
                Url = "inputs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["InputNumber"],
                Url = "inputnumbers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Markdown"],
                Url = "markdowns"
            });
            item.AddItem(new DemoMenuItem()
            {
                IsUpdate = true,
                Text = Localizer["MultiSelect"],
                Url = "multiselects"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Radio"],
                Url = "radios"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Rate"],
                Url = "rates"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Select"],
                Url = "selects"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Slider"],
                Url = "sliders"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Switch"],
                Url = "switchs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Textarea"],
                Url = "textareas"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Toggle"],
                Url = "toggles"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Transfer"],
                Url = "transfers"
            });
            item.AddItem(new DemoMenuItem()
            {
                IsUpdate = true,
                Text = Localizer["Upload"],
                Url = "uploads"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["ValidateForm"],
                Url = "validateforms"
            });
            AddBadge(item);
        }

        private void AddData(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Avatar"],
                Url = "avatars"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Badge"],
                Url = "badges"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["BarcodeReader"],
                Url = "barcodereaders"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Card"],
                Url = "cards"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Calendar"],
                Url = "calendars"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Camera"],
                Url = "Cameras"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Captcha"],
                Url = "captchas"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Carousel"],
                Url = "carousels"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Chart"],
                Url = "charts"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Circle"],
                Url = "circles"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Collapse"],
                Url = "collapses"
            });
            item.AddItem(new DemoMenuItem()
            {
                IsNew = true,
                Text = Localizer["Display"],
                Url = "displays"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["DropdownWidget"],
                Url = "dropdownwidgets"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["GroupBox"],
                Url = "groupboxs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["HandwrittenPage"],
                Url = "handwrittenPage"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["ListView"],
                Url = "listviews"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Popover"],
                Url = "popovers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["QRCode"],
                Url = "qrcodes"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Search"],
                Url = "searchs"
            });
            AddTableItem(item);
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Tag"],
                Url = "tags"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Timeline"],
                Url = "timelines"
            });
            item.AddItem(new DemoMenuItem()
            {
                IsNew = true,
                Text = Localizer["Title"],
                Url = "titles"
            });
            item.AddItem(new DemoMenuItem()
            {
                IsNew = true,
                Text = Localizer["Download"],
                Url = "downloads"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Tooltip"],
                Url = "tooltips"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Tree"],
                Url = "trees"
            });

            AddBadge(item);
        }

        private void AddTableItem(DemoMenuItem item)
        {
            var it = new DemoMenuItem()
            {
                Text = Localizer["Table"]
            };

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableBase"],
                Url = "tables"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableColumn"],
                Url = "tables/column"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableRow"],
                Url = "tables/row"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableDetail"],
                Url = "tables/detail"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableSearch"],
                Url = "tables/search"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableFilter"],
                Url = "tables/filter"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableFixHeader"],
                Url = "tables/header"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableHeaderGroup"],
                Url = "tables/multi-header"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableFixColumn"],
                Url = "tables/fix-column"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TablePage"],
                Url = "tables/pages"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableToolbar"],
                Url = "tables/toolbar"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableEdit"],
                Url = "tables/edit"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableExport"],
                Url = "tables/export"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableSelection"],
                Url = "tables/selection"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableAutoRefresh"],
                Url = "tables/autorefresh"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableFooter"],
                Url = "tables/footer"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableDialog"],
                Url = "tables/dialog"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableWrap"],
                Url = "tables/wrap"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = Localizer["TableTree"],
                Url = "tables/tree"
            });

            it.AddItem(new DemoMenuItem()
            {
                IsNew = true,
                Text = Localizer["TableLaoding"],
                Url = "tables/loading"
            });

            item.AddItem(it);

            AddBadge(it, false);
        }

        private void AddNotice(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Alert"],
                Url = "alerts"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Console"],
                Url = "consoles"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Dialog"],
                Url = "dialogs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Drawer"],
                Url = "drawers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["EditDialog"],
                Url = "editdialogs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Light"],
                Url = "lights"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Message"],
                Url = "messages"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Modal"],
                Url = "modals"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Popconfirm"],
                Url = "popconfirms"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Progress"],
                Url = "progresss"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["SearchDialog"],
                Url = "searchdialogs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Spinner"],
                Url = "spinners"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["SweetAlert"],
                Url = "swals"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Timer"],
                Url = "timers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Toast"],
                Url = "toasts"
            });
            AddBadge(item);
        }

        private void AddNavigation(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Anchor"],
                Url = "anchors"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Breadcrumb"],
                Url = "breadcrumbs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Dropdown"],
                Url = "dropdowns"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["GoTop"],
                Url = "gotops"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Menu"],
                Url = "menus"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Nav"],
                Url = "navs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Pagination"],
                Url = "paginations"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Steps"],
                Url = "stepss"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Tab"],
                Url = "tabs"
            });

            AddBadge(item);
        }

        private void AddLayout(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Divider"],
                Url = "dividers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Layout"],
                Url = "layouts"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Footer"],
                Url = "footers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Row"],
                Url = "rows"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Scroll"],
                Url = "scrolls"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Skeleton"],
                Url = "skeletons"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = Localizer["Split"],
                Url = "splits"
            });

            AddBadge(item);
        }

        private void AddSummary(DemoMenuItem item)
        {
            // 计算组件总数
            var count = 0;
            count = Menus.Aggregate(count, (c, item) => { c += item.Items.Count(); return c; }, c => c - Menus[0].Items.Count());
            AddBadge(item, false, count);
            Menus.Insert(1, item);
        }

        private void AddBadge(DemoMenuItem item, bool append = true, int? count = null)
        {
            item.Component = CreateBadge(count ?? item.Items.Count(), item.IsNew, item.IsUpdate);
            if (append)
            {
                Menus.Add(item);
            }
        }

        private static BootstrapDynamicComponent CreateBadge(int count, bool isNew = false, bool isUpdate = false) => BootstrapDynamicComponent.CreateComponent<State>(new KeyValuePair<string, object?>[]
        {
            new(nameof(State.Count), count),
            new(nameof(State.IsNew), isNew),
            new(nameof(State.IsUpdate), isUpdate)
        });

        private class DemoMenuItem : MenuItem
        {
            public bool IsNew { get; set; }

            public bool IsUpdate { get; set; }

            /// <summary>
            ///
            /// </summary>
            /// <param name="item"></param>
            public override void AddItem(MenuItem item)
            {
                base.AddItem(item);

                var menu = (DemoMenuItem)item;
                if (menu.Parent != null)
                {
                    var pMenu = ((DemoMenuItem)menu.Parent);
                    if (menu.IsNew)
                    {
                        pMenu.IsNew = true;
                    }

                    if (menu.IsUpdate)
                    {
                        pMenu.IsUpdate = true;
                    }
                }

                item.Component = CreateBadge(0, menu.IsNew, menu.IsUpdate);
            }
        }
    }
}

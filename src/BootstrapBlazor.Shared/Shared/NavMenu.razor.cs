using BootstrapBlazor.Components;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
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

        private string? NavMenuCssClass => CssBuilder.Default("sidebar-content")
            .AddClass("collapse", collapseNavMenu)
            .Build();

        private readonly List<MenuItem> Items = new List<MenuItem>(100);

        private IEnumerable<MenuItem> Menus => Items;

        private string ActiveUrl { get; set; } = "";
        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

            ActiveUrl = Navigator.ToBaseRelativePath(Navigator.Uri);
            InitMenus();
        }

        private Task OnClickMenu(MenuItem item)
        {
            if (!item.Items.Any())
            {
                ToggleNavMenu();
                StateHasChanged();
            }
            return Task.CompletedTask;
        }

        private void ToggleNavMenu()
        {
            collapseNavMenu = !collapseNavMenu;
        }

        private void InitMenus()
        {
            // 快速入门
            var item = new MenuItem()
            {
                Text = "快速上手",
                Icon = "fa fa-fw fa-fa"
            };
            AddQuickStar(item);

            item = new MenuItem()
            {
                Text = "布局组件",
                Icon = "fa fa-fw fa-desktop"
            };
            AddLayout(item);

            item = new MenuItem()
            {
                Text = "导航组件",
                Icon = "fa fa-fw fa-bars"
            };
            AddNavigation(item);

            item = new MenuItem()
            {
                Text = "表单组件",
                Icon = "fa fa-fw fa-cubes"
            };
            AddForm(item);

            item = new MenuItem()
            {
                Text = "数据组件",
                Icon = "fa fa-fw fa-database"
            };
            AddData(item);

            item = new MenuItem()
            {
                Text = "消息组件",
                Icon = "fa fa-fw fa-comments"
            };
            AddNotice(item);

            item = new MenuItem()
            {
                Text = "网友投稿",
                Icon = "fa fa-fw fa-comments"
            };
            AddSuperWomen(item);

            item = new MenuItem()
            {
                Text = "组件总览",
                Icon = "fa fa-fw fa-fa",
                Url = "components"
            };
            AddSummary(item);
        }

        private void AddQuickStar(MenuItem item)
        {
            item.AddItem(new MenuItem()
            {
                Text = "简介",
                Url = "introduction"
            });
            item.AddItem(new MenuItem()
            {
                Text = "类库安装",
                Url = "install"
            });
            item.AddItem(new MenuItem()
            {
                Text = "服务器端模式 Server",
                Url = "install-server"
            });
            item.AddItem(new MenuItem()
            {
                Text = "客户端模式 wasm",
                Url = "install-wasm"
            });

            item.IsCollapsed = false;
            Items.Add(item);
        }

        private void AddForm(MenuItem item)
        {
            item.AddItem(new MenuItem()
            {
                Text = "自动完成 AutoComplete",
                Url = "autocompletes"
            });
            item.AddItem(new MenuItem()
            {
                Text = "按钮 Button",
                Url = "buttons"
            });
            item.AddItem(new MenuItem()
            {
                Text = "多选框 Checkbox",
                Url = "checkboxs"
            });
            item.AddItem(new MenuItem()
            {
                Text = "输入框 Input",
                Url = "inputs"
            });
            item.AddItem(new MenuItem()
            {
                Text = "数值框 InputNumber",
                Url = "inputnumbers"
            });
            item.AddItem(new MenuItem()
            {
                Text = "富文本框 Editor",
                Url = "editors"
            });
            item.AddItem(new MenuItem()
            {
                Text = "富文本框 Markdown",
                Url = "markdowns"
            });
            item.AddItem(new MenuItem()
            {
                Text = "单选框 Radio",
                Url = "radios"
            });
            item.AddItem(new MenuItem()
            {
                Text = "选择器 Select",
                Url = "selects"
            });
            item.AddItem(new MenuItem()
            {
                Text = "时间框 DateTimePicker",
                Url = "datetimepickers"
            });
            item.AddItem(new MenuItem()
            {
                Text = "评分 Rate",
                Url = "rates"
            });
            item.AddItem(new MenuItem()
            {
                Text = "滑块 Slider",
                Url = "sliders"
            });
            item.AddItem(new MenuItem()
            {
                Text = "开关 Switch",
                Url = "switchs"
            });
            item.AddItem(new MenuItem()
            {
                Text = "开关 Toggle",
                Url = "toggles"
            });
            item.AddItem(new MenuItem()
            {
                Text = "穿梭框 Transfer",
                Url = "transfers"
            });
            item.AddItem(new MenuItem()
            {
                Text = "上传组件 Upload",
                Url = "uploads"
            });

            AddBadge(item);
        }

        private void AddData(MenuItem item)
        {
            item.AddItem(new MenuItem()
            {
                Text = "头像框 Avatar",
                Url = "avatars"
            });
            item.AddItem(new MenuItem()
            {
                Text = "徽章 Badge",
                Url = "badges"
            });
            item.AddItem(new MenuItem()
            {
                Text = "卡片 Card",
                Url = "cards"
            });
            item.AddItem(new MenuItem()
            {
                Text = "日历框 Calendar",
                Url = "calendars"
            });
            item.AddItem(new MenuItem()
            {
                Text = "验证码 Captcha",
                Url = "captchas"
            });
            item.AddItem(new MenuItem()
            {
                Text = "走马灯 Carousel",
                Url = "carousels"
            });
            item.AddItem(new MenuItem()
            {
                Text = "图表 Chart",
                Url = "charts"
            });
            item.AddItem(new MenuItem()
            {
                Text = "进度环 Circle",
                Url = "circles"
            });
            item.AddItem(new MenuItem()
            {
                Text = "折叠 Collapse",
                Url = "collapses"
            });
            item.AddItem(new MenuItem()
            {
                Text = "列表组件 ListView",
                Url = "listviews"
            });
            item.AddItem(new MenuItem()
            {
                Text = "弹出框 Popover",
                Url = "popovers"
            });
            item.AddItem(new MenuItem()
            {
                Text = "搜索框 Search",
                Url = "searchs"
            });
            AddTableItem(item);
            item.AddItem(new MenuItem()
            {
                Text = "标签 Tag",
                Url = "tags"
            });
            item.AddItem(new MenuItem()
            {
                Text = "时间线 Timeline",
                Url = "timelines"
            });
            item.AddItem(new MenuItem()
            {
                Text = "工具条 Tooltip",
                Url = "tooltips"
            });
            item.AddItem(new MenuItem()
            {
                Text = "树形控件 Tree",
                Url = "trees"
            });

            AddBadge(item);
        }

        private void AddTableItem(MenuItem item)
        {
            var it = new MenuItem()
            {
                Text = "表格 Table"
            };

            it.AddItem(new MenuItem()
            {
                Text = "基本功能",
                Url = "tables"
            });

            it.AddItem(new MenuItem()
            {
                Text = "列设置",
                Url = "tables/column"
            });

            it.AddItem(new MenuItem()
            {
                Text = "行设置",
                Url = "tables/row"
            });

            it.AddItem(new MenuItem()
            {
                Text = "搜索功能",
                Url = "tables/search"
            });

            it.AddItem(new MenuItem()
            {
                Text = "过滤功能",
                Url = "tables/filter"
            });

            it.AddItem(new MenuItem()
            {
                Text = "固定表头",
                Url = "tables/header"
            });

            it.AddItem(new MenuItem()
            {
                Text = "分页功能",
                Url = "tables/pages"
            });

            it.AddItem(new MenuItem()
            {
                Text = "工具栏",
                Url = "tables/toolbar"
            });

            it.AddItem(new MenuItem()
            {
                Text = "表单维护",
                Url = "tables/edit"
            });

            item.AddItem(it);

            AddBadge(it, false);
        }

        private void AddNotice(MenuItem item)
        {
            item.AddItem(new MenuItem()
            {
                Text = "警告框 Alert",
                Url = "alerts"
            });
            item.AddItem(new MenuItem()
            {
                Text = "控制台 Console",
                Url = "consoles"
            });
            item.AddItem(new MenuItem()
            {
                Text = "对话框 Dialog",
                Url = "dialogs"
            });
            item.AddItem(new MenuItem()
            {
                Text = "抽屉 Drawer",
                Url = "drawers"
            });
            item.AddItem(new MenuItem()
            {
                Text = "消息框 Message",
                Url = "messages"
            });
            item.AddItem(new MenuItem()
            {
                Text = "模态框 Modal",
                Url = "modals"
            });
            item.AddItem(new MenuItem()
            {
                Text = "指示灯 Light",
                Url = "lights"
            });
            item.AddItem(new MenuItem()
            {
                Text = "确认框 Popconfirm",
                Url = "popconfirms"
            });
            item.AddItem(new MenuItem()
            {
                Text = "进度条 Progress",
                Url = "progresss"
            });
            item.AddItem(new MenuItem()
            {
                Text = "旋转图标 Spinner",
                Url = "spinners"
            });
            item.AddItem(new MenuItem()
            {
                Text = "轻量弹窗 Toast",
                Url = "toasts"
            });

            AddBadge(item);
        }

        private void AddSuperWomen(MenuItem item)
        {
            item.AddItem(new MenuItem()
            {
                Text = "条码扫描 BarcodeReader",
                Url = "barcodereaders"
            });
            AddBadge(item);
        }

        private void AddNavigation(MenuItem item)
        {
            item.AddItem(new MenuItem()
            {
                Text = "菜单 Menu",
                Url = "menus"
            });
            item.AddItem(new MenuItem()
            {
                Text = "导航栏 Nav",
                Url = "navs"
            });
            item.AddItem(new MenuItem()
            {
                Text = "下拉菜单 Dropdown",
                Url = "dropdowns"
            });
            item.AddItem(new MenuItem()
            {
                Text = "跳转组件 GoTop",
                Url = "gotops"
            });
            item.AddItem(new MenuItem()
            {
                Text = "分页 Pagination",
                Url = "paginations"
            });
            item.AddItem(new MenuItem()
            {
                Text = "步骤条 Steps",
                Url = "stepss"
            });
            item.AddItem(new MenuItem()
            {
                Text = "标签页 Tab",
                Url = "tabs"
            });

            AddBadge(item);
        }

        private void AddLayout(MenuItem item)
        {
            item.AddItem(new MenuItem()
            {
                Text = "分隔线 Divider",
                Url = "dividers"
            });
            item.AddItem(new MenuItem()
            {
                Text = "布局组件 Layout",
                Url = "layouts"
            });
            item.AddItem(new MenuItem()
            {
                Text = "页脚组件 Footer",
                Url = "footers"
            });
            item.AddItem(new MenuItem()
            {
                Text = "滚动条 Scroll",
                Url = "scrolls"
            });
            item.AddItem(new MenuItem()
            {
                Text = "分割面板 Split",
                Url = "splits"
            });

            AddBadge(item);
        }

        private void AddSummary(MenuItem item)
        {
            // 计算组件总数
            var count = 0;
            count = Items.Aggregate(count, (c, item) => { c += item.Items.Count(); return c; }, c => c - Items[0].Items.Count());
            AddBadge(item, false, count);
            Items.Insert(1, item);
        }

        private void AddBadge(MenuItem item, bool append = true, int? count = null)
        {
            item.Component = CreateBadge(count ?? item.Items.Count());

            var im = item.Items.FirstOrDefault(i => !string.IsNullOrEmpty(i.Url) && i.Url.Equals(ActiveUrl, System.StringComparison.OrdinalIgnoreCase));
            if (im != null)
            {
                im.IsActive = true;
                im.IsCollapsed = false;

                MenuItem.CascadingSetActive(im);
            }
            if (append) Items.Add(item);
        }

        private DynamicComponent CreateBadge(int count) => DynamicComponent.CreateComponent<Badge>(new KeyValuePair<string, object>[]
        {
            new KeyValuePair<string, object>(nameof(Badge.Color), Color.Info),
            new KeyValuePair<string, object>(nameof(Badge.IsPill), true),
            new KeyValuePair<string, object>(nameof(Badge.ChildContent), new RenderFragment(builder => {
                builder.AddContent(0, count);
            }))
        });
    }
}

// **********************************
// 框架名称：BootstrapBlazor 
// 框架作者：Argo Zhang
// 开源地址：
// Gitee : https://gitee.com/LongbowEnterprise/BootstrapBlazor
// GitHub: https://github.com/ArgoZhang/BootstrapBlazor 
// 开源协议：LGPL-3.0 (https://gitee.com/LongbowEnterprise/BootstrapBlazor/blob/dev/LICENSE)
// **********************************

using BootstrapBlazor.Components;
using BootstrapBlazor.Shared.Pages.Components;
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

        private List<MenuItem> Menus { get; set; } = new List<MenuItem>(100);

        /// <summary>
        /// 
        /// </summary>
        protected override void OnInitialized()
        {
            base.OnInitialized();

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
            var item = new DemoMenuItem()
            {
                Text = "快速上手",
                Icon = "fa fa-fw fa-fa"
            };
            AddQuickStar(item);

            item = new DemoMenuItem()
            {
                Text = "布局组件",
                Icon = "fa fa-fw fa-desktop"
            };
            AddLayout(item);

            item = new DemoMenuItem()
            {
                Text = "导航组件",
                Icon = "fa fa-fw fa-bars"
            };
            AddNavigation(item);

            item = new DemoMenuItem()
            {
                Text = "表单组件",
                Icon = "fa fa-fw fa-cubes"
            };
            AddForm(item);

            item = new DemoMenuItem()
            {
                Text = "数据组件",
                Icon = "fa fa-fw fa-database"
            };
            AddData(item);

            item = new DemoMenuItem()
            {
                Text = "消息组件",
                Icon = "fa fa-fw fa-comments"
            };
            AddNotice(item);

            item = new DemoMenuItem()
            {
                Text = "组件总览",
                Icon = "fa fa-fw fa-fa",
                Url = "components"
            };
            AddSummary(item);
        }

        private void AddQuickStar(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = "简介",
                Url = "introduction"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "类库安装",
                Url = "install"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "项目模板",
                Url = "template"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "全球化",
                Url = "globalization"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "本地化",
                Url = "localization"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "服务器端模式 Server",
                Url = "install-server"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "客户端模式 wasm",
                Url = "install-wasm"
            });

            item.IsCollapsed = false;
            Menus.Add(item);
        }

        private void AddForm(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = "表单组件 EditorForm",
                Url = "editorforms"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "表单组件 ValidateForm",
                Url = "forms"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "自动完成 AutoComplete",
                Url = "autocompletes"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "按钮 Button",
                Url = "buttons"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "多选框 Checkbox",
                Url = "checkboxs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "多选框组 CheckboxList",
                Url = "checkboxlists"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "时间框 DateTimePicker",
                Url = "datetimepickers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "时间范围框 DateTimeRange",
                Url = "datetimeranges",
                IsUpdate = true
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "下拉框 DropdownList",
                Url = "dropdownlists"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "富文本框 Editor",
                Url = "editors"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "输入框 Input",
                Url = "inputs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "数值框 InputNumber",
                Url = "inputnumbers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "富文本框 Markdown",
                Url = "markdowns"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "单选框 Radio",
                Url = "radios"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "评分 Rate",
                Url = "rates"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "选择器 Select",
                Url = "selects"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "多项选择器 MultiSelect",
                Url = "multi-selects"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "滑块 Slider",
                Url = "sliders"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "开关 Switch",
                Url = "switchs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "多行文本框 Textarea",
                Url = "textareas"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "开关 Toggle",
                Url = "toggles"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "穿梭框 Transfer",
                Url = "transfers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "上传组件 Upload",
                Url = "uploads"
            });

            AddBadge(item);
        }

        private void AddData(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = "头像框 Avatar",
                Url = "avatars"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "徽章 Badge",
                Url = "badges"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "卡片 Card",
                Url = "cards"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "日历框 Calendar",
                Url = "calendars"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "验证码 Captcha",
                Url = "captchas"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "走马灯 Carousel",
                Url = "carousels"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "图表 Chart",
                Url = "charts"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "进度环 Circle",
                Url = "circles"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "折叠 Collapse",
                Url = "collapses"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "列表组件 ListView",
                Url = "listviews"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "弹出框 Popover",
                Url = "popovers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "二维码 QRCode",
                Url = "qrcodes"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "搜索框 Search",
                Url = "searchs"
            });
            AddTableItem(item);
            item.AddItem(new DemoMenuItem()
            {
                Text = "标签 Tag",
                Url = "tags"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "时间线 Timeline",
                Url = "timelines"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "工具条 Tooltip",
                Url = "tooltips"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "树形控件 Tree",
                Url = "trees"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "条码扫描 BarcodeReader",
                Url = "barcodereaders"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "摄像头组件 Camera",
                Url = "Cameras"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "手写签名 HandwrittenPage",
                Url = "handwrittenPage"
            });

            AddBadge(item);
        }

        private void AddTableItem(DemoMenuItem item)
        {
            var it = new DemoMenuItem()
            {
                Text = "表格 Table"
            };

            it.AddItem(new DemoMenuItem()
            {
                Text = "基本功能",
                Url = "tables"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "列设置",
                Url = "tables/column",
                IsUpdate = true
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "行设置",
                Url = "tables/row"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "明细行",
                Url = "tables/detail"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "搜索功能",
                Url = "tables/search"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "筛选和排序",
                Url = "tables/filter"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "固定表头",
                Url = "tables/header"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "表头分组",
                Url = "tables/multi-header"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "固定列",
                Url = "tables/fix-column",
                IsUpdate = true
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "分页功能",
                Url = "tables/pages"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "工具栏",
                Url = "tables/toolbar"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "表单维护",
                Url = "tables/edit",
                IsUpdate = true
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "导出功能",
                Url = "tables/export"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "行选中",
                Url = "tables/selection"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "自动刷新",
                Url = "tables/autorefresh"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "统计合并",
                Url = "tables/footer"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "弹窗联动",
                Url = "tables/dialog"
            });

            it.AddItem(new DemoMenuItem()
            {
                Text = "折行演示",
                Url = "tables/wrap"
            });

            item.AddItem(it);

            AddBadge(it, false);
        }

        private void AddNotice(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = "警告框 Alert",
                Url = "alerts"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "控制台 Console",
                Url = "consoles"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "对话框 Dialog",
                Url = "dialogs",
                IsUpdate = true
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "抽屉 Drawer",
                Url = "drawers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "消息框 Message",
                Url = "messages"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "模态框 Modal",
                Url = "modals"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "指示灯 Light",
                Url = "lights"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "确认框 Popconfirm",
                Url = "popconfirms"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "进度条 Progress",
                Url = "progresss"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "旋转图标 Spinner",
                Url = "spinners"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "模态弹窗 SweetAlert",
                Url = "swals"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "轻量弹窗 Toast",
                Url = "toasts"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "计时器 Timer",
                Url = "timers"
            });
            AddBadge(item);
        }

        private void AddNavigation(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = "锚点 Anchor",
                Url = "anchors",
                IsNew = true
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "面包屑 Breadcrumb",
                Url = "breadcrumbs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "菜单 Menu",
                Url = "menus"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "导航栏 Nav",
                Url = "navs"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "下拉菜单 Dropdown",
                Url = "dropdowns"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "跳转组件 GoTop",
                Url = "gotops"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "分页 Pagination",
                Url = "paginations"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "步骤条 Steps",
                Url = "stepss"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "标签页 Tab",
                Url = "tabs"
            });

            AddBadge(item);
        }

        private void AddLayout(DemoMenuItem item)
        {
            item.AddItem(new DemoMenuItem()
            {
                Text = "分隔线 Divider",
                Url = "dividers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "布局组件 Layout",
                Url = "layouts"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "页脚组件 Footer",
                Url = "footers"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "滚动条 Scroll",
                Url = "scrolls"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "骨架屏 Skeleton",
                Url = "skeletons"
            });
            item.AddItem(new DemoMenuItem()
            {
                Text = "分割面板 Split",
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
            if (append) Menus.Add(item);
        }

        private static DynamicComponent CreateBadge(int count, bool isNew = false, bool isUpdate = false) => DynamicComponent.CreateComponent<State>(new KeyValuePair<string, object>[]
        {
            new KeyValuePair<string, object>(nameof(State.Count), count),
            new KeyValuePair<string, object>(nameof(State.IsNew), isNew),
            new KeyValuePair<string, object>(nameof(State.IsUpdate), isUpdate)
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
                    if (menu.IsNew) pMenu.IsNew = true;
                    if (menu.IsUpdate) pMenu.IsUpdate = true;
                }

                item.Component = CreateBadge(0, menu.IsNew, menu.IsUpdate);
            }
        }
    }
}

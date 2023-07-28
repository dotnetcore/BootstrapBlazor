// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.Shared;

using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Shared.Extensions;

internal static class MenusLocalizerExtensions
{
    public static List<MenuItem> GenerateMenus(this IStringLocalizer<NavMenu> Localizer)
    {
        var Menus = new List<MenuItem>();

        // 快速入门
        var item = new DemoMenuItem()
        {
            Text = Localizer["GetStarted"],
            Icon = "fa-solid fa-fw fa-font-awesome"
        };
        AddQuickStar(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["LayoutComponents"],
            Icon = "fa-fw fa-solid fa-desktop"
        };
        AddLayout(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["NavigationComponents"],
            Icon = "fa-fw fa-solid fa-bars"
        };
        AddNavigation(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["FormsComponents"],
            Icon = "fa-fw fa-solid fa-cubes"
        };
        AddForm(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["TableComponents"],
            Icon = "fa-fw fa-solid fa-table"
        };
        AddTable(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["DataComponents"],
            Icon = "fa-fw fa-solid fa-database"
        };
        AddData(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Charts"],
            Icon = "fa-fw fa-solid fa-chart-line"
        };
        AddChart(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["NotificationComponents"],
            Icon = "fa-fw fa-solid fa-comments"
        };
        AddNotice(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["SpeechComponents"],
            Icon = "fa-fw fa-solid fa-microphone"
        };
        AddSpeech(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["OtherComponents"],
            Icon = "fa-fw fa-solid fas fa-share-nodes"
        };
        AddOtherComponent(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Components"],
            Icon = "text-info fa-solid fa-fw fa-heart fa-beat",
            Url = "components"
        };
        AddSummary(item);

        return Menus;

        void AddOtherComponent(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["MouseFollowerIntro"],
                    Url = "mouse-follower"
                },
                new()
                {
                    Text = Localizer["Live2DDisplayIntro"],
                    Url = "live2d-display"
                },
            };

            AddBadge(item, count: 2);
        }

        void AddSpeech(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["SpeechIntro"],
                    Url = "speech"
                },
                new()
                {
                    Text = Localizer["Recognizer"],
                    Url = "recognizer"
                },
                new()
                {
                    Text = Localizer["Synthesizer"],
                    Url = "synthesizer"
                },
                new()
                {
                    Text = Localizer["SpeechWave"],
                    Url = "speech-wave"
                }
            };
            AddBadge(item, count: 3);
        }

        void AddQuickStar(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["Introduction"],
                    Url = "introduction"
                },
                new()
                {
                    Text = Localizer["Install"],
                    Url = "install",
                    Match = NavLinkMatch.All
                },
                new()
                {
                    Text = Localizer["ProjectTemplate"],
                    Url = "template"
                },
                new()
                {
                    Text = Localizer["Globalization"],
                    Url = "globalization"
                },
                new()
                {
                    Text = Localizer["Localization"],
                    Url = "localization"
                },
                new()
                {
                    Text = Localizer["Labels"],
                    Url = "labels"
                },
                new()
                {
                    Text = Localizer["GlobalException"],
                    Url = "global-exception"
                },
                new()
                {
                    Text = Localizer["ServerBlazor"],
                    Url = "install-server",
                },
                new()
                {
                    Text = Localizer["ClientBlazor"],
                    Url = "install-wasm",
                },
                new()
                {
                    Text = Localizer["MauiBlazor"],
                    Url = "install-maui",
                    Match = NavLinkMatch.All
                },
                new()
                {
                    Text = Localizer["Breakpoints"],
                    Url = "breakpoints"
                },
                new()
                {
                    Text = Localizer["ZIndex"],
                    Url = "layout"
                },
                new()
                {
                    Text = Localizer["Theme"],
                    Url = "themes"
                },
                new()
                {
                    Text = Localizer["FAIcon"],
                    Url = "fa-icon"
                },
                new()
                {
                    Text = Localizer["LayoutPage"],
                    Url = "layout-page"
                }
            };
            AddBadge(item, count: 0);
        }

        void AddForm(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["AutoComplete"],
                    Url = "auto-complete"
                },
                new()
                {
                    Text = Localizer["AutoFill"],
                    Url = "auto-fill"
                },
                new()
                {
                    Text = Localizer["Button"],
                    Url = "button"
                },
                new()
                {
                    Text = Localizer["Cascader"],
                    Url = "cascader"
                },
                new()
                {
                    Text = Localizer["Checkbox"],
                    Url = "checkbox"
                },
                new()
                {
                    Text = Localizer["CheckboxList"],
                    Url = "checkbox-list"
                },
                new()
                {
                    Text = Localizer["ColorPicker"],
                    Url = "color-picker"
                },
                new()
                {
                    Text = Localizer["DateTimePicker"],
                    Url = "datetime-picker"
                },
                new()
                {
                    Text = Localizer["DateTimeRange"],
                    Url = "datetime-range"
                },
                new()
                {
                    Text = Localizer["Editor"],
                    Url = "editor"
                },
                new()
                {
                    Text = Localizer["EditorForm"],
                    Url = "editor-form"
                },
                new()
                {
                    Text = Localizer["FloatingLabel"],
                    Url = "floating-label"
                },
                new()
                {
                    Text = Localizer["Input"],
                    Url = "inputs"
                },
                new()
                {
                    Text = Localizer["InputNumber"],
                    Url = "input-number"
                },
                new()
                {
                    Text = Localizer["InputGroup"],
                    Url = "input-group"
                },
                new()
                {
                    Text = Localizer["Ip"],
                    Url = "ips"
                },
                new()
                {
                    Text = Localizer["Markdown"],
                    Url = "markdowns"
                },
                new()
                {
                    Text = Localizer["CherryMarkdown"],
                    Url = "cherry-markdowns"
                },
                new()
                {
                    Text = Localizer["MultiSelect"],
                    Url = "multi-select"
                },
                new()
                {
                    Text = Localizer["OnScreenKeyboard"],
                    Url = "onscreen-keyboard"
                },
                new()
                {
                    Text = Localizer["PulseButton"],
                    Url = "pulse-button"
                },
                new()
                {
                    Text = Localizer["Radio"],
                    Url = "radio"
                },
                new()
                {
                    Text = Localizer["Rate"],
                    Url = "rate"
                },
                new()
                {
                    Text = Localizer["Select"],
                    Url = "select"
                },
                new()
                {
                    Text = Localizer["SelectTree"],
                    Url = "select-tree"
                },
                new()
                {
                    Text = Localizer["Slider"],
                    Url = "sliders"
                },
                new()
                {
                    Text = Localizer["Switch"],
                    Url = "switches"
                },
                new()
                {
                    Text = Localizer["Textarea"],
                    Url = "textarea"
                },
                new()
                {
                    Text = Localizer["Toggle"],
                    Url = "toggles"
                },
                new()
                {
                    Text = Localizer["Transfer"],
                    Url = "transfers"
                },
                new()
                {
                    Text = Localizer["Upload"],
                    Url = "uploads"
                },
                new()
                {
                    Text = Localizer["ValidateForm"],
                    Url = "validate-form"
                }
            };
            AddBadge(item);
        }

        void AddData(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["Ajax"],
                    Url = "ajax"
                },
                new()
                {
                    Text = Localizer["Avatar"],
                    Url = "avatars"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["AzureOpenAI"],
                    Url = "ai-chat"
                },
                new()
                {
                    Text = Localizer["Badge"],
                    Url = "badges"
                },
                new()
                {
                    Text = Localizer["BarcodeReader"],
                    Url = "barcode-reader"
                },
                new()
                {
                    Text = Localizer["Block"],
                    Url = "blocks"
                },
                new()
                {
                    Text = Localizer["Bluetooth"],
                    Url = "blue-tooth"
                },
                new()
                {
                    Text = Localizer["Card"],
                    Url = "cards"
                },
                new()
                {
                    Text = Localizer["Calendar"],
                    Url = "calendars"
                },
                new()
                {
                    Text = Localizer["Camera"],
                    Url = "Cameras"
                },
                new()
                {
                    Text = Localizer["Captcha"],
                    Url = "captchas"
                },
                new()
                {
                    Text = Localizer["Carousel"],
                    Url = "carousels"
                },
                new()
                {
                    Text = Localizer["Client"],
                    Url = "client"
                },
                new()
                {
                    Text = Localizer["Circle"],
                    Url = "circles"
                },
                new()
                {
                    Text = Localizer["Collapse"],
                    Url = "collapses"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["CountUp"],
                    Url = "count-ups"
                },
                new()
                {
                    Text = Localizer["Display"],
                    Url = "display"
                },
                new()
                {
                    Text = Localizer["Download"],
                    Url = "downloads"
                },
                new()
                {
                    Text = Localizer["DropdownWidget"],
                    Url = "dropdown-widget"
                },
                new ()
                {
                    Text=Localizer["Empty"],
                    Url = "empty"
                },
                new ()
                {
                    IsNew = true,
                    Text=Localizer["EyeDropper"],
                    Url = "eye-dropper"
                },
                new()
                {
                    Text = Localizer["FileIcon"],
                    Url = "file-icon"
                },
                new()
                {
                    Text = Localizer["FileViewer"],
                    Url = "FileViewers"
                },
                new()
                {
                    Text = Localizer["Geolocation"],
                    Url = "geolocations"
                },
                new()
                {
                    Text = Localizer["GroupBox"],
                    Url = "group-box"
                },
                new()
                {
                    Text = Localizer["Handwritten"],
                    Url = "handwritten"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["HtmlRenderer"],
                    Url = "html-renderer"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["Html2Pdf"],
                    Url = "html2pdf"
                },
                new()
                {
                    Text = Localizer["LinkButton"],
                    Url = "link-button"
                },
                new()
                {
                    Text = Localizer["ListView"],
                    Url = "list-view"
                },
                new()
                {
                    Text = Localizer["Locator"],
                    Url = "locators"
                },
                new()
                {
                    Text = Localizer["ImageViewer"],
                    Url = "image-viewer"
                },
                new()
                {
                    Text = Localizer["PdfReader"],
                    Url = "PdfReader"
                },
                new()
                {
                    Text = Localizer["Print"],
                    Url = "print"
                },
                new()
                {
                    Text = Localizer["QRCode"],
                    Url = "qr-code"
                },
                new()
                {
                    Text = Localizer["Repeater"],
                    Url = "repeater"
                },
                new()
                {
                    Text = Localizer["Search"],
                    Url = "search"
                },
                new()
                {
                    Text = Localizer["SignaturePad"],
                    Url = "signature-pad",
                },
                new()
                {
                    Text = Localizer["SpeechWave"],
                    Url = "speech-wave"
                },
                new()
                {
                    Text = Localizer["SwitchButton"],
                    Url = "switch-button"
                },
                new()
                {
                    Text = Localizer["Tag"],
                    Url = "tag"
                },
                new()
                {
                    Text = Localizer["Timeline"],
                    Url = "timelines"
                },
                new()
                {
                    Text = Localizer["Title"],
                    Url = "titles"
                },
                new()
                {
                    Text = Localizer["Topology"],
                    Url = "Topologies"
                },
                new()
                {
                    Text = Localizer["Tree"],
                    Url = "tree"
                },
                new()
                {
                    Text = Localizer["TreeView"],
                    Url = "tree-view"
                },
                new()
                {
                    Text = Localizer["Transition"],
                    Url = "transitions"
                },
                new()
                {
                    Text = Localizer["BaiduOcr"],
                    Url = "ocr"
                },
                new()
                {
                    Text = Localizer["VideoPlayer"],
                    Url = "videoPlayers"
                }
            };
            AddBadge(item);
        }

        void AddTable(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["TableBase"],
                    Url = "tables",
                    Match = NavLinkMatch.All
                },
                new()
                {
                    Text = Localizer["TableColumn"],
                    Url = "tables/column"
                },
                new()
                {
                    Text = Localizer["TableColumnDrag"],
                    Url = "tables/column/drag"
                },
                new()
                {
                    Text = Localizer["TableColumnResizing"],
                    Url = "tables/column/resizing"
                },
                new()
                {
                    Text = Localizer["TableColumnList"],
                    Url = "tables/column/list"
                },
                new()
                {
                    Text = Localizer["TableColumnTemplate"],
                    Url = "tables/column/template"
                },
                new()
                {
                    Text = Localizer["TableFixColumn"],
                    Url = "tables/fix-column"
                },
                new()
                {
                    Text = Localizer["TableRow"],
                    Url = "tables/row"
                },
                new()
                {
                    Text = Localizer["TableDetail"],
                    Url = "tables/detail"
                },
                new()
                {
                    Text = Localizer["TableSelection"],
                    Url = "tables/selection"
                },
                new()
                {
                    Text = Localizer["TableWrap"],
                    Url = "tables/wrap"
                },
                new()
                {
                    Text = Localizer["TableCell"],
                    Url = "tables/cell"
                },
                new()
                {
                    Text = Localizer["TableLookup"],
                    Url = "tables/lookup"
                },
                new()
                {
                    Text = Localizer["TableDynamic"],
                    Url = "tables/dynamic"
                },
                new()
                {
                    Text = Localizer["TableDynamicObject"],
                    Url = "tables/dynamicobject"
                },
                new()
                {
                    Text = Localizer["TableSearch"],
                    Url = "tables/search"
                },
                new()
                {
                    Text = Localizer["TableFilter"],
                    Url = "tables/filter"
                },
                new()
                {
                    Text = Localizer["TableFixHeader"],
                    Url = "tables/header"
                },
                new()
                {
                    Text = Localizer["TableHeaderGroup"],
                    Url = "tables/multi-header"
                },
                new()
                {
                    Text = Localizer["TablePage"],
                    Url = "tables/pages"
                },
                new()
                {
                    Text = Localizer["TableToolbar"],
                    Url = "tables/toolbar"
                },
                new()
                {
                    Text = Localizer["TableEdit"],
                    Url = "tables/edit"
                },
                new()
                {
                    Text = Localizer["TableTracking"],
                    Url = "tables/tracking"
                },
                new()
                {
                    Text = Localizer["TableExcel"],
                    Url = "tables/excel"
                },
                new()
                {
                    Text = Localizer["TableDynamicExcel"],
                    Url = "tables/dynamicexcel"
                },
                new()
                {
                    Text = Localizer["TableExport"],
                    Url = "tables/export"
                },
                new()
                {
                    Text = Localizer["TableAutoRefresh"],
                    Url = "tables/autorefresh"
                },
                new()
                {
                    Text = Localizer["TableFooter"],
                    Url = "tables/footer"
                },
                new()
                {
                    Text = Localizer["TableDialog"],
                    Url = "tables/dialog"
                },
                new()
                {
                    Text = Localizer["TableTree"],
                    Url = "tables/tree"
                },
                new()
                {
                    Text = Localizer["TableLaoding"],
                    Url = "tables/loading"
                },
                new()
                {
                    Text = Localizer["TableVirtualization"],
                    Url = "tables/virtualization"
                }
            };
            item.CssClass = "nav-table";
            AddBadge(item, count: 1);
        }

        void AddChart(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["ChartSummary"],
                    Url = "charts/index"
                },
                new()
                {
                    Text = Localizer["ChartLine"],
                    Url = "charts/line"
                },
                new()
                {
                    Text = Localizer["ChartBar"],
                    Url = "charts/bar"
                },
                new()
                {
                    Text = Localizer["ChartPie"],
                    Url = "charts/pie"
                },
                new()
                {
                    Text = Localizer["ChartDoughnut"],
                    Url = "charts/doughnut"
                },
                new()
                {
                    Text = Localizer["ChartBubble"],
                    Url = "charts/bubble"
                }
            };
            AddBadge(item, count: 5);
        }

        void AddNotice(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["Alert"],
                    Url = "alerts"
                },
                new()
                {
                    Text = Localizer["Console"],
                    Url = "consoles"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["ContextMenu"],
                    Url = "contextmenus"
                },
                new()
                {
                    Text = Localizer["Dialog"],
                    Url = "dialogs"
                },
                new()
                {
                    Text = Localizer["Dispatch"],
                    Url = "dispatches"
                },
                new()
                {
                    Text = Localizer["Drawer"],
                    Url = "drawers"
                },
                new()
                {
                    Text = Localizer["EditDialog"],
                    Url = "edit-dialog"
                },
                new()
                {
                    Text = Localizer["Light"],
                    Url = "lights"
                },
                new()
                {
                    Text = Localizer["Message"],
                    Url = "messages"
                },
                new()
                {
                    Text = Localizer["Modal"],
                    Url = "modals"
                },
                new()
                {
                    Text = Localizer["Notification"],
                    Url = "notification"
                },
                new()
                {
                    Text = Localizer["Popconfirm"],
                    Url = "pop-confirm"
                },
                new()
                {
                    Text = Localizer["Popover"],
                    Url = "popovers"
                },
                new()
                {
                    Text = Localizer["Progress"],
                    Url = "progress"
                },
                new()
                {
                    Text = Localizer["Reconnector"],
                    Url = "reconnectors"
                },
                new()
                {
                    Text = Localizer["Responsive"],
                    Url = "responsive"
                },
                new()
                {
                    Text = Localizer["SearchDialog"],
                    Url = "search-dialog"
                },
                new()
                {
                    Text = Localizer["Spinner"],
                    Url = "spinner"
                },
                new()
                {
                    Text = Localizer["SweetAlert"],
                    Url = "sweet-alert"
                },
                new()
                {
                    Text = Localizer["Timer"],
                    Url = "timers"
                },
                new()
                {
                    Text = Localizer["Toast"],
                    Url = "toasts"
                },
                new()
                {
                    Text = Localizer["Tooltip"],
                    Url = "tooltips"
                }
            };
            AddBadge(item);
        }

        void AddNavigation(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["Anchor"],
                    Url = "anchors"
                },
                new()
                {
                    Text = Localizer["AnchorLink"],
                    Url = "anchorlinks"
                },
                new()
                {
                    Text = Localizer["AutoRedirect"],
                    Url = "autoredirects"
                },
                new()
                {
                    Text = Localizer["Breadcrumb"],
                    Url = "breadcrumbs"
                },
                new()
                {
                    Text = Localizer["Dropdown"],
                    Url = "dropdowns"
                },
                new()
                {
                    Text = Localizer["FullScreen"],
                    Url = "fullscreen"
                },
                new()
                {
                    Text = Localizer["GoTop"],
                    Url = "go-top"
                },
                new()
                {
                    Text = Localizer["Logout"],
                    Url = "logouts"
                },
                new()
                {
                    Text = Localizer["Menu"],
                    Url = "menus"
                },
                new()
                {
                    Text = Localizer["Navigation"],
                    Url = "navigation"
                },
                new()
                {
                    Text = Localizer["Pagination"],
                    Url = "pagination"
                },
                new()
                {
                    Text = Localizer["RibbonTab"],
                    Url = "ribbon-tabs"
                },
                new()
                {
                    Text = Localizer["Steps"],
                    Url = "steps"
                },
                new()
                {
                    Text = Localizer["Tab"],
                    Url = "tabs"
                }
            };
            AddBadge(item);
        }

        void AddLayout(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["Divider"],
                    Url = "divider"
                },
                new()
                {
                    Text = Localizer["Dragdrop"],
                    Url = "drag-drop"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["DockView"],
                    Url = "dockviews"
                },
                new()
                {
                    Text = Localizer["Layout"],
                    Url = "layouts"
                },
                new()
                {
                    Text = Localizer["Footer"],
                    Url = "footers"
                },
                new()
                {
                    Text = Localizer["Row"],
                    Url = "row"
                },
                new()
                {
                    Text = Localizer["Scroll"],
                    Url = "scrolls"
                },
                new()
                {
                    Text = Localizer["Skeleton"],
                    Url = "skeleton"
                },
                new()
                {
                    Text = Localizer["Split"],
                    Url = "splits"
                }
            };
            AddBadge(item);
        }

        void AddSummary(DemoMenuItem item)
        {
            // 计算组件总数
            var count = 0;
            count = Menus.OfType<DemoMenuItem>().Sum(i => i.Count);
            AddBadge(item, false, count);
            Menus.Insert(1, item);
        }

        void AddBadge(DemoMenuItem item, bool append = true, int? count = null)
        {
            item.Count = count ?? item.Items.Count();
            item.Template = CreateBadge(count ?? item.Items.Count(),
               isNew: item.Items.OfType<DemoMenuItem>().Any(i => i.IsNew),
               isUpdate: item.Items.OfType<DemoMenuItem>().Any(i => i.IsUpdate));
            foreach (var menu in item.GetAllSubItems().OfType<DemoMenuItem>().Where(i => ShouldBadge(i)))
            {
                menu.Template = CreateBadge(0, menu.IsNew, menu.IsUpdate);
            }
            if (append)
            {
                Menus.Add(item);
            }

            static bool ShouldBadge(DemoMenuItem? item) => item != null && (item.IsNew || item.IsUpdate);

            static RenderFragment CreateBadge(int count, bool isNew = false, bool isUpdate = false) => BootstrapDynamicComponent.CreateComponent<State>(new Dictionary<string, object?>
            {
                [nameof(State.Count)] = count,
                [nameof(State.IsNew)] = isNew,
                [nameof(State.IsUpdate)] = isUpdate
            }).Render();
        }
    }

    private class DemoMenuItem : MenuItem
    {
        public bool IsNew { get; set; }

        public bool IsUpdate { get; set; }

        public int Count { get; set; }
    }
}

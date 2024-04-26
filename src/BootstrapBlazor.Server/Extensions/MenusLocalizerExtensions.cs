// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Server.Extensions;

internal static class MenusLocalizerExtensions
{
    public static List<MenuItem> GenerateMenus(this IStringLocalizer<NavMenu> Localizer)
    {
        var menus = new List<MenuItem>();

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
            Text = Localizer["DockViewComponents"],
            Icon = "fa-fw fa-solid fa-table-cells-large"
        };
        AddDockView(item);

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
            Text = Localizer["Services"],
            Icon = "fa-fw fa-solid fa-screwdriver-wrench",
        };
        AddServices(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["OtherComponents"],
            Icon = "fa-fw fa-solid fas fa-share-nodes"
        };
        AddOtherComponent(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Utility"],
            Icon = "fa-fw fa-solid fa-code"
        };

        AddBootstrapBlazorUtility(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Components"],
            Icon = "fa-solid fa-fw fa-heart fa-beat icon-summary",
            Url = "components"
        };
        AddSummary(item);

        return menus;

        void AddBootstrapBlazorUtility(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["JSExtension"],
                    Url = "js-extensions"
                },
                new()
                {
                    Text = Localizer["OnlineText"],
                    Url = "online"
                }
            };
            AddBadge(item);
        }

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
                new()
                {
                    IsNew = true,
                    Text = Localizer["Splitting"],
                    Url = "splitting"
                },
            };

            AddBadge(item, count: 3);
        }

        void AddSpeech(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["SpeechIntro"],
                    Url = "speech/index"
                },
                new()
                {
                    Text = Localizer["Recognizer"],
                    Url = "speech/recognizer"
                },
                new()
                {
                    Text = Localizer["Synthesizer"],
                    Url = "speech/synthesizer"
                },
                new()
                {
                    Text = Localizer["SpeechWave"],
                    Url = "speech/wave"
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
                    Url = "label"
                },
                new()
                {
                    Text = Localizer["GlobalException"],
                    Url = "global-exception"
                },
                new()
                {
                    Text = Localizer["WebAppBlazor"],
                    Url = "install-webapp",
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
                    Url = "breakpoint"
                },
                new()
                {
                    Text = Localizer["ZIndex"],
                    Url = "z-index"
                },
                new()
                {
                    Text = Localizer["Theme"],
                    Url = "theme"
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
                    Match = NavLinkMatch.All,
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
                    IsNew = true,
                    Text = Localizer["ClockPicker"],
                    Url = "clock-picker"
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
                    Match = NavLinkMatch.All,
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
                    IsNew = true,
                    Text = Localizer["ListGroup"],
                    Url = "list-group"
                },
                new()
                {
                    Match = NavLinkMatch.All,
                    Text = Localizer["Input"],
                    Url = "input"
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
                    Url = "ip"
                },
                new()
                {
                    Text = Localizer["Markdown"],
                    Url = "markdown"
                },
                new()
                {
                    Text = Localizer["CherryMarkdown"],
                    Url = "cherry-markdown"
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
                    Match = NavLinkMatch.All,
                    Text = Localizer["Select"],
                    Url = "select"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["SelectObject"],
                    Url = "select-object"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["SelectTable"],
                    Url = "select-table"
                },
                new()
                {
                    Text = Localizer["SelectTree"],
                    Url = "select-tree"
                },
                new()
                {
                    Text = Localizer["Slider"],
                    Url = "slider"
                },
                new()
                {
                    Text = Localizer["Switch"],
                    Url = "switch"
                },
                new()
                {
                    Text = Localizer["Textarea"],
                    Url = "textarea"
                },
                new()
                {
                    Text = Localizer["TimePicker"],
                    Url = "time-picker"
                },
                new()
                {
                    Text = Localizer["Toggle"],
                    Url = "toggle"
                },
                new()
                {
                    Text = Localizer["Transfer"],
                    Url = "transfer"
                },
                new()
                {
                    Text = Localizer["Upload"],
                    Url = "upload"
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
                    Url = "avatar"
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
                    Url = "badge"
                },
                new()
                {
                    Text = Localizer["BarcodeReader"],
                    Url = "barcode-reader"
                },
                new()
                {
                    Text = Localizer["BarcodeGenerator"],
                    Url = "barcode-generator"
                },
                new()
                {
                    Text = Localizer["Block"],
                    Url = "block"
                },
                new()
                {
                    Text = Localizer["Bluetooth"],
                    Url = "blue-tooth"
                },
                new()
                {
                    Text = Localizer["Card"],
                    Url = "card"
                },
                new()
                {
                    Text = Localizer["Calendar"],
                    Url = "calendar"
                },
                new()
                {
                    Text = Localizer["Camera"],
                    Url = "camera"
                },
                new()
                {
                    Text = Localizer["Captcha"],
                    Url = "captcha"
                },
                new()
                {
                    Text = Localizer["Carousel"],
                    Url = "carousel"
                },
                new()
                {
                    Text = Localizer["Circle"],
                    Url = "circle"
                },
                new()
                {
                    Text = Localizer["Collapse"],
                    Url = "collapse"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["CountUp"],
                    Url = "count-up"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["CodeEditor"],
                    Url = "code-editors"
                },
                new()
                {
                    Text = Localizer["Display"],
                    Url = "display"
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
                    Url = "file-viewer"
                },
                new()
                {
                    Text = Localizer["GroupBox"],
                    Url = "group-box"
                },
                new()
                {
                    Text = Localizer["Gantt"],
                    Url = "gantt",
                    IsNew = true
                },
                new()
                {
                    Text = Localizer["Handwritten"],
                    Url = "handwritten"
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
                    Text = Localizer["ImageViewer"],
                    Url = "image-viewer"
                },
                new()
                {
                    Text = Localizer["ImageCropper"],
                    Url = "image-cropper"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["MindMap"],
                    Url = "mind-map"
                },
                new()
                {
                    Text = Localizer["PdfReader"],
                    Url = "pdf-reader"
                },
                new()
                {
                    Text = Localizer["Print"],
                    Url = "print"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["QueryBuilder"],
                    Url = "query-builder"
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
                    IsNew = true,
                    Text = Localizer["Segmented"],
                    Url = "segmented"
                },
                new()
                {
                    Text = Localizer["SignaturePad"],
                    Url = "signature-pad",
                },
                new()
                {
                    Text = Localizer["SwitchButton"],
                    Url = "switch-button"
                },
                new()
                {
                    Text = Localizer["SvgEditor"],
                    Url = "svg-editors",
                    IsNew = true,
                },
                new()
                {
                    Text = Localizer["Tag"],
                    Url = "tag"
                },
                new()
                {
                    Text = Localizer["Timeline"],
                    Url = "timeline"
                },
                new()
                {
                    Text = Localizer["Topology"],
                    Url = "topology"
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
                    Url = "transition"
                },
                new()
                {
                    Text = Localizer["VideoPlayer"],
                    Url = "video-player"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["Waterfall"],
                    Url = "tutorials/waterfall"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["WebSerial"],
                    Url = "web-serial"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["WebSpeech"],
                    Url = "web-speech"
                }
            };
            AddBadge(item);
        }

        void AddDockView(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["DockViewIndex"],
                    Url = "dock-view/index"
                },
                new()
                {
                    Text = Localizer["DockViewColumn"],
                    Url = "dock-view/col"
                },
                new()
                {
                    Text = Localizer["DockViewRow"],
                    Url = "dock-view/row"
                },
                new()
                {
                    Text = Localizer["DockViewStack"],
                    Url = "dock-view/stack"
                },
                new()
                {
                    Text = Localizer["DockViewComplex"],
                    Url = "dock-view/complex"
                },
                new()
                {
                    Text = Localizer["DockViewNest"],
                    Url = "dock-view/nest"
                },
                new()
                {
                    Text = Localizer["DockViewVisible"],
                    Url = "dock-view/visible"
                },
                new()
                {
                    Text = Localizer["DockViewLock"],
                    Url = "dock-view/lock"
                },
                new()
                {
                    Text = Localizer["DockViewLayout"],
                    Url = "dock-view/layout"
                }
            };
            AddBadge(item, count: 1);
        }

        void AddTable(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Match = NavLinkMatch.All,
                    Text = Localizer["TableBase"],
                    Url = "table"
                },
                new()
                {
                    Match = NavLinkMatch.All,
                    Text = Localizer["TableColumn"],
                    Url = "table/column"
                },
                new()
                {
                    Text = Localizer["TableColumnDrag"],
                    Url = "table/column/drag"
                },
                new()
                {
                    Text = Localizer["TableColumnResizing"],
                    Url = "table/column/resizing"
                },
                new()
                {
                    Text = Localizer["TableColumnList"],
                    Url = "table/column/list"
                },
                new()
                {
                    Text = Localizer["TableColumnTemplate"],
                    Url = "table/column/template"
                },
                new()
                {
                    Text = Localizer["TableFixColumn"],
                    Url = "table/fix-column"
                },
                new()
                {
                    Text = Localizer["TableRow"],
                    Url = "table/row"
                },
                new()
                {
                    Text = Localizer["TableDetail"],
                    Url = "table/detail"
                },
                new()
                {
                    Text = Localizer["TableSelection"],
                    Url = "table/selection"
                },
                new()
                {
                    Text = Localizer["TableWrap"],
                    Url = "table/wrap"
                },
                new()
                {
                    Text = Localizer["TableCell"],
                    Url = "table/cell"
                },
                new()
                {
                    Text = Localizer["TableLookup"],
                    Url = "table/lookup"
                },
                new()
                {
                    Match = NavLinkMatch.All,
                    Text = Localizer["TableDynamic"],
                    Url = "table/dynamic"
                },
                new()
                {
                    Text = Localizer["TableDynamicObject"],
                    Url = "table/dynamic-object"
                },
                new()
                {
                    Text = Localizer["TableSearch"],
                    Url = "table/search"
                },
                new()
                {
                    Text = Localizer["TableFilter"],
                    Url = "table/filter"
                },
                new()
                {
                    Text = Localizer["TableFixHeader"],
                    Url = "table/header"
                },
                new()
                {
                    Text = Localizer["TableHeaderGroup"],
                    Url = "table/multi-header"
                },
                new()
                {
                    Text = Localizer["TablePage"],
                    Url = "table/page"
                },
                new()
                {
                    Text = Localizer["TableToolbar"],
                    Url = "table/toolbar"
                },
                new()
                {
                    Text = Localizer["TableEdit"],
                    Url = "table/edit"
                },
                new()
                {
                    Text = Localizer["TableTracking"],
                    Url = "table/tracking"
                },
                new()
                {
                    Text = Localizer["TableExcel"],
                    Url = "table/excel"
                },
                new()
                {
                    Text = Localizer["TableDynamicExcel"],
                    Url = "table/dynamic-excel"
                },
                new()
                {
                    Text = Localizer["TableExport"],
                    Url = "table/export"
                },
                new()
                {
                    Text = Localizer["TableAutoRefresh"],
                    Url = "table/auto-refresh"
                },
                new()
                {
                    Text = Localizer["TableFooter"],
                    Url = "table/footer"
                },
                new()
                {
                    Text = Localizer["TableDialog"],
                    Url = "table/dialog"
                },
                new()
                {
                    Text = Localizer["TableTree"],
                    Url = "table/tree"
                },
                new()
                {
                    Text = Localizer["TableLoading"],
                    Url = "table/loading"
                },
                new()
                {
                    Text = Localizer["TableVirtualization"],
                    Url = "table/virtualization"
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
                    Url = "chart/index"
                },
                new()
                {
                    Text = Localizer["ChartLine"],
                    Url = "chart/line"
                },
                new()
                {
                    Text = Localizer["ChartBar"],
                    Url = "chart/bar"
                },
                new()
                {
                    Text = Localizer["ChartPie"],
                    Url = "chart/pie"
                },
                new()
                {
                    Text = Localizer["ChartDoughnut"],
                    Url = "chart/doughnut"
                },
                new()
                {
                    Text = Localizer["ChartBubble"],
                    Url = "chart/bubble"
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
                    Url = "alert"
                },
                new()
                {
                    Text = Localizer["Console"],
                    Url = "console"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["ContextMenu"],
                    Url = "context-menu"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["CountButton"],
                    Url = "count-button"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["DialButton"],
                    Url = "dial-button"
                },
                new()
                {
                    Text = Localizer["Dialog"],
                    Url = "dialog"
                },
                new()
                {
                    Text = Localizer["Drawer"],
                    Url = "drawer"
                },
                new()
                {
                    Text = Localizer["EditDialog"],
                    Url = "edit-dialog"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["FlipClock"],
                    Url = "flip-clock"
                },
                new()
                {
                    Text = Localizer["Light"],
                    Url = "light"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["Marquee"],
                    Url = "marquee"
                },
                new()
                {
                    Text = Localizer["Message"],
                    Url = "message"
                },
                new()
                {
                    Text = Localizer["Modal"],
                    Url = "modal"
                },
                new()
                {
                    Text = Localizer["Notification"],
                    Url = "notification"
                },
                new()
                {
                    Text = Localizer["PopConfirm"],
                    Url = "pop-confirm"
                },
                new()
                {
                    Text = Localizer["Popover"],
                    Url = "popover"
                },
                new()
                {
                    Text = Localizer["Progress"],
                    Url = "progress"
                },
                new()
                {
                    Text = Localizer["Reconnector"],
                    Url = "reconnector"
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
                    IsNew = true,
                    Text = Localizer["SlideButton"],
                    Url = "slide-button"
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
                    Url = "timer"
                },
                new()
                {
                    Text = Localizer["Toast"],
                    Url = "toast"
                },
                new()
                {
                    Text = Localizer["Tooltip"],
                    Url = "tooltip"
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
                    Match = NavLinkMatch.All,
                    Text = Localizer["Anchor"],
                    Url = "anchor"
                },
                new()
                {
                    Text = Localizer["AnchorLink"],
                    Url = "anchor-link"
                },
                new()
                {
                    Text = Localizer["AutoRedirect"],
                    Url = "auto-redirect"
                },
                new()
                {
                    Text = Localizer["Breadcrumb"],
                    Url = "breadcrumb"
                },
                new()
                {
                    Text = Localizer["Dropdown"],
                    Url = "dropdown"
                },
                new()
                {
                    Text = Localizer["GoTop"],
                    Url = "go-top"
                },
                new()
                {
                    Text = Localizer["Logout"],
                    Url = "logout"
                },
                new()
                {
                    Text = Localizer["Menu"],
                    Url = "menu"
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
                    Url = "ribbon-tab"
                },
                new()
                {
                    Text = Localizer["Steps"],
                    Url = "step"
                },
                new()
                {
                    Text = Localizer["Tab"],
                    Url = "tab"
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
                    Text = Localizer["DragDrop"],
                    Url = "drag-drop"
                },
                new()
                {
                    Text = Localizer["Layout"],
                    Url = "layout"
                },
                new()
                {
                    Text = Localizer["Footer"],
                    Url = "footer"
                },
                new()
                {
                    Text = Localizer["Row"],
                    Url = "row"
                },
                new()
                {
                    Text = Localizer["Scroll"],
                    Url = "scroll"
                },
                new()
                {
                    Text = Localizer["Skeleton"],
                    Url = "skeleton"
                },
                new()
                {
                    Text = Localizer["Split"],
                    Url = "split"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["Stack"],
                    Url = "stack"
                }
            };
            AddBadge(item);
        }

        void AddServices(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["AzureTranslator"],
                    Url = "translator"
                },
                new()
                {
                    Text = Localizer["BaiduOcr"],
                    Url = "ocr"
                },
                new()
                {
                    Text = Localizer["BrowserFinger"],
                    Url = "browser-finger"
                },
                new()
                {
                    Text = Localizer["Clipboard"],
                    Url = "clipboard-service"
                },
                new()
                {
                    Text = Localizer["Client"],
                    Url = "client"
                },
                new()
                {
                    Text = Localizer["ConnectionService"],
                    Url = "connection-service"
                },
                new()
                {
                    Text = Localizer["Dispatch"],
                    Url = "dispatch"
                },
                new()
                {
                    Text = Localizer["Download"],
                    Url = "download"
                },
                new()
                {
                    Text = Localizer["DialogService"],
                    Url = "dialog-service"
                },
                new()
                {
                    Text = Localizer["FullScreen"],
                    Url = "fullscreen"
                },
                new()
                {
                    Text = Localizer["Festival"],
                    Url = "festival"
                },
                new()
                {
                    Text = Localizer["Holiday"],
                    Url = "holiday"
                },
                new()
                {
                    Text = Localizer["Geolocation"],
                    Url = "geolocation"
                },
                new()
                {
                    Text = Localizer["Html2Pdf"],
                    Url = "html2pdf"
                },
                new()
                {
                    Text = Localizer["HtmlRenderer"],
                    Url = "html-renderer"
                },
                new()
                {
                    Text = Localizer["Locator"],
                    Url = "locator"
                },
                new()
                {
                    Text = Localizer["Lookup"],
                    Url = "lookup"
                },
                new()
                {
                    Text = Localizer["PrintService"],
                    Url = "print-service"
                },
                new()
                {
                    Text = Localizer["Title"],
                    Url = "title"
                },
                new()
                {
                    Text = Localizer["ZipArchive"],
                    Url = "zip-archive"
                }
            };
            AddBadge(item);
        }

        void AddSummary(DemoMenuItem item)
        {
            // 计算组件总数
            var count = 0;
            count = menus.OfType<DemoMenuItem>().Sum(i => i.Count);
            AddBadge(item, false, count);
            menus.Insert(1, item);
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
                menus.Add(item);
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

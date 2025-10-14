// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Longbow.SerialPorts;
using Microsoft.AspNetCore.Components.Routing;

namespace BootstrapBlazor.Server.Extensions;

internal static class MenusLocalizerExtensions
{
    public static List<MenuItem> GenerateMenus(this IStringLocalizer<NavMenu> Localizer)
    {
        var menus = new List<MenuItem>();

        var item = new DemoMenuItem()
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
            Text = Localizer["DockViewComponents2"],
            Icon = "fa-fw fa-solid fa-table-cells-large"
        };
        AddDockView2(item);

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
            Text = Localizer["SocketComponents"],
            Icon = "fa-fw fa-solid fa-satellite-dish text-danger"
        };
        AddSocket(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["ModbusComponents"],
            Icon = "fa-fw fa-solid fa-satellite-dish text-danger"
        };
        AddModbus(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["SerialPortComponents"],
            Icon = "fa-fw fa-solid fa-satellite-dish text-danger"
        };
        AddSerialPort(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Services"],
            Icon = "fa-fw fa-solid fa-screwdriver-wrench",
        };
        AddServices(item);

        item = new DemoMenuItem()
        {
            Text = Localizer["Icons"],
            Icon = "fa-fw fa-solid fa-icons",
        };
        AddIcons(item);

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

        // 快速入门
        item = new DemoMenuItem()
        {
            Text = Localizer["GetStarted"],
            Icon = "fa-solid fa-fw fa-font-awesome"
        };
        AddQuickStar(item);

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
                },
                new()
                {
                    Text = Localizer["WebSpeech"],
                    Url = "speech/web-speech"
                }
            };
            AddBadge(item, count: 5);
        }

        void AddSocket(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    IsNew = true,
                    Text = Localizer["TcpSocketFactory"],
                    Url = "socket-factory"
                },
                new()
                {
                    Text = Localizer["SocketManualReceive"],
                    Url = "socket/manual-receive"
                },
                new()
                {
                    Text = Localizer["SocketAutoReceive"],
                    Url = "socket/auto-receive"
                },
                new()
                {
                    Text = Localizer["DataPackageAdapter"],
                    Url = "socket/adapter"
                },
                new()
                {
                    Text = Localizer["SocketAutoConnect"],
                    Url = "socket/auto-connect"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["SocketDataEntity"],
                    Url = "socket/data-entity"
                }
            };

            AddBadge(item, count: 2);
        }

        void AddModbus(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    IsNew = true,
                    Text = Localizer["ModbusFactory"],
                    Url = "modbus-factory"
                }
            };

            AddBadge(item, count: 2);
        }

        void AddSerialPort(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    IsNew = true,
                    Text = Localizer["SerialPortFactory"],
                    Url = "serial-port-factory"
                }
            };

            AddBadge(item, count: 2);
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
                    Text = Localizer["GlobalOption"],
                    Url = "global-option"
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
                    Text = Localizer["LayoutPage"],
                    Url = "layout-page"
                }
            };
            AddSummary(item);
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
                    Text = Localizer["SelectRegion"],
                    Url = "select-region"
                },
                new()
                {
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
                    Text = Localizer["OtpInput"],
                    Url = "otp-input"
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
                    Text = Localizer["SelectObject"],
                    Url = "select-object"
                },
                new()
                {
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
                    Text = Localizer["InputUpload"],
                    Url = "upload-input"
                },
                new()
                {
                    Text = Localizer["ButtonUpload"],
                    Url = "upload-button"
                },
                new()
                {
                    Text = Localizer["AvatarUpload"],
                    Url = "upload-avatar"
                },
                new()
                {
                    Text = Localizer["CardUpload"],
                    Url = "upload-card"
                },
                new()
                {
                    Text = Localizer["DropUpload"],
                    Url = "upload-drop"
                },
                new()
                {
                    Text = Localizer["ValidateForm"],
                    Url = "validate-form"
                },
                new()
                {
                    Text = Localizer["Vditor"],
                    Url = "vditor"
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
                    Text = Localizer["Affix"],
                    Url = "affix"
                },
                new()
                {
                    Text = Localizer["Avatar"],
                    Url = "avatar"
                },
                new()
                {
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
                    Text = Localizer["ShieldBadge"],
                    Url = "shield-badge"
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
                    Text = Localizer["CountUp"],
                    Url = "count-up"
                },
                new()
                {
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
                    Text=Localizer["ExportPdfButton"],
                    Url = "export-pdf-button"
                },
                new ()
                {
                    Text=Localizer["Empty"],
                    Url = "empty"
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
                    Url = "gantt"
                },
                new()
                {
                    Text = Localizer["Handwritten"],
                    Url = "handwritten"
                },
                new()
                {
                    Text = Localizer["Icon"],
                    Url = "icon"
                },
                new()
                {
                    Text = Localizer["IFrame"],
                    Url = "iframe"
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
                    Text = Localizer["MindMap"],
                    Url = "mind-map"
                },
                new()
                {
                    Text = Localizer["Mermaid"],
                    Url = "mermaid"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["OfficeViewer"],
                    Url = "office-viewer"
                },
                new()
                {
                    Text = Localizer["PdfReader"],
                    Url = "pdf-reader"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["PdfViewer"],
                    Url = "pdf-viewer"
                },
                new()
                {
                    Text = Localizer["Player"],
                    Url = "player"
                },
                new()
                {
                    Text = Localizer["Print"],
                    Url = "print"
                },
                new()
                {
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
                    Text = Localizer["RDKit"],
                    Url = "rdkit"
                },
                new()
                {
                    Text = Localizer["Search"],
                    Url = "search"
                },
                new()
                {
                    Text = Localizer["Segmented"],
                    Url = "segmented"
                },
                new()
                {
                    Text = Localizer["SmilesDrawer"],
                    Url = "smiles-drawer"
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
                    Url = "svg-editors"
                },
                new()
                {
                    Text = Localizer["Tag"],
                    Url = "tag"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["TaskDashBoard"],
                    Url = "task-board"
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
                    Text = Localizer["Typed"],
                    Url = "typed"
                },
                new()
                {
                    Text = Localizer["UniverSheet"],
                    Url = "univer-sheet"
                },
                new()
                {
                    Text = Localizer["VideoPlayer"],
                    Url = "video-player"
                },
                new()
                {
                    Text = Localizer["Waterfall"],
                    Url = "tutorials/waterfall"
                },
                new()
                {
                    Text = Localizer["Watermark"],
                    Url = "watermark"
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

        void AddDockView2(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["DockViewV2Index"],
                    Url = "dock-view2/index"
                },
                new()
                {
                    Text = Localizer["DockViewV2Column"],
                    Url = "dock-view2/col"
                },
                new()
                {
                    Text = Localizer["DockViewV2Row"],
                    Url = "dock-view2/row"
                },
                new()
                {
                    Text = Localizer["DockViewV2Group"],
                    Url = "dock-view2/group"
                },
                new()
                {
                    Text = Localizer["DockViewV2Complex"],
                    Url = "dock-view2/complex"
                },
                new()
                {
                    Text = Localizer["DockViewV2Nest"],
                    Url = "dock-view2/nest"
                },
                new()
                {
                    Text = Localizer["DockViewV2Visible"],
                    Url = "dock-view2/visible"
                },
                new()
                {
                    Text = Localizer["DockViewV2Lock"],
                    Url = "dock-view2/lock"
                },
                new()
                {
                    Text = Localizer["DockViewV2Title"],
                    Url = "dock-view2/title"
                },
                new()
                {
                    Text = Localizer["DockViewV2Layout"],
                    Url = "dock-view2/layout"
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
                },
                new()
                {
                    Text = Localizer["TableAttribute"],
                    Url = "table/attribute"
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
                    Text = Localizer["ContextMenu"],
                    Url = "context-menu"
                },
                new()
                {
                    Text = Localizer["CountButton"],
                    Url = "count-button"
                },
                new()
                {
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
                    Text = Localizer["DriverJs"],
                    Url = "driver-js"
                },
                new()
                {
                    Text = Localizer["EditDialog"],
                    Url = "edit-dialog"
                },
                new()
                {
                    Text = Localizer["FlipClock"],
                    Url = "flip-clock"
                },
                new()
                {
                    Text = Localizer["FullScreenButton"],
                    Url = "fullscreen-button"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["NetworkMonitor"],
                    Url = "network-monitor"
                },
                new()
                {
                    Text = Localizer["Light"],
                    Url = "light"
                },
                new()
                {
                    Text = Localizer["IntersectionObserver"],
                    Url = "intersection-observer"
                },
                new()
                {
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
                    Text = Localizer["Meet"],
                    Url = "meet"
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
                },
                new()
                {
                    Text = Localizer["WinBox"],
                    Url = "win-box"
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
                    IsNew = true,
                    Text = Localizer["Navbar"],
                    Url = "navbar"
                },
                new()
                {
                    Text = Localizer["Row"],
                    Url = "row"
                },
                new()
                {
                    Text = Localizer["SortableList"],
                    Url = "sortable-list"
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
                    Text = Localizer["Stack"],
                    Url = "stack"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["Toolbar"],
                    Url = "toolbar"
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
                    Text = Localizer["Bluetooth"],
                    Url = "blue-tooth"
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
                    Text = Localizer["DialogService"],
                    Url = "dialog-service"
                },
                new()
                {
                    Text = Localizer["Dispatch"],
                    Url = "dispatch"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["Dom2ImageService"],
                    Url = "dom2image"
                },
                new()
                {
                    Text = Localizer["Download"],
                    Url = "download"
                },
                new()
                {
                    Text = Localizer["DrawerService"],
                    Url = "drawer-service"
                },
                new()
                {
                    Text = Localizer["FullScreen"],
                    Url = "fullscreen"
                },
                new ()
                {
                    Text=Localizer["EyeDropper"],
                    Url = "eye-dropper"
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
                    Text = Localizer["Html2Image"],
                    Url = "html2image"
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
                    Text = Localizer["Mask"],
                    Url = "mask"
                },
                new()
                {
                    IsNew = true,
                    Text = Localizer["OpcDaService"],
                    Url = "opc-da"
                },
                new()
                {
                    Text = Localizer["PrintService"],
                    Url = "print-service"
                },
                new()
                {
                    Text = Localizer["ThemeProvider"],
                    Url = "theme-provider"
                },
                new()
                {
                    Text = Localizer["TotpService"],
                    Url = "otp-service"
                },
                new()
                {
                    Text = Localizer["Title"],
                    Url = "title"
                },
                new()
                {
                    Text = Localizer["AudioDevice"],
                    Url = "audio-device"
                },
                new()
                {
                    Text = Localizer["VideoDevice"],
                    Url = "video-device"
                },
                new()
                {
                    Text = Localizer["WebSerial"],
                    Url = "web-serial"
                },
                new()
                {
                    Text = Localizer["ZipArchive"],
                    Url = "zip-archive"
                }
            };
            AddBadge(item);
        }

        void AddIcons(DemoMenuItem item)
        {
            item.Items = new List<DemoMenuItem>
            {
                new()
                {
                    Text = Localizer["FAIcon"],
                    Url = "fa-icon"
                },
                new()
                {
                    Text = Localizer["BootstrapIcon"],
                    Url = "bs-icon"
                },
                new()
                {
                    Text = Localizer["MaterialIcon"],
                    Url = "md-icon"
                },
                new()
                {
                    Text = Localizer["FluentSystemIcon"],
                    Url = "fluent-icon"
                },
                new()
                {
                    Text = Localizer["OctIcon"],
                    Url = "oct-icon"
                },
                new()
                {
                    Text = Localizer["UniverIcon"],
                    Url = "univer-icon"
                },
                new()
                {
                    Text = Localizer["IconPark"],
                    Url = "icon-park"
                },
                new()
                {
                    Text = Localizer["ElementIcon"],
                    Url = "element-icon"
                },
                new()
                {
                    Text = Localizer["AntDesignIcon"],
                    Url = "ant-design-icon"
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
            menus.Insert(0, item);
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

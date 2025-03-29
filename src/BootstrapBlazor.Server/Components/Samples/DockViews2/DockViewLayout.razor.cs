// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Server.Components.Samples.DockViews2;

/// <summary>
/// DockViewLayout 组件
/// </summary>
public partial class DockViewLayout
{
    [Inject]
    [NotNull]
    private IStringLocalizer<DockViewLayout>? Localizer { get; set; }

    [NotNull]
    private DockViewV2? _dockView = null;

    private Task Reset() => _dockView.Reset(_layoutConfig);

    private void OnToggleLayout1()
    {
        _layoutConfig = LayoutConfig1;
    }

    private void OnToggleLayout2()
    {
        _layoutConfig = LayoutConfig2;
    }

    private void OnToggleLayout3()
    {
        _layoutConfig = LayoutConfig3;
    }

    private string _layoutConfig = LayoutConfig1;

    const string LayoutConfig1 = """{"grid":{"root":{"type":"branch","data":[{"type":"branch","data":[{"type":"leaf","data":{"views":["bb_5893789"],"activeView":"bb_5893789","id":"0"},"size":364},{"type":"leaf","data":{"views":["bb_11251481"],"activeView":"bb_11251481","id":"1"},"size":364},{"type":"leaf","data":{"views":["bb_39754773"],"activeView":"bb_39754773","id":"2"},"size":364}],"size":601}],"size":1092},"width":1092,"height":601,"orientation":"VERTICAL"},"panels":{"bb_5893789":{"id":"bb_5893789","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签一","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_5893789","additionalAttributes":null},"title":"标签一"},"bb_11251481":{"id":"bb_11251481","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签二","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_11251481","additionalAttributes":null},"title":"标签二"},"bb_39754773":{"id":"bb_39754773","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签三","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_39754773","additionalAttributes":null},"title":"标签三"}},"activeGroup":"1"}""";

    const string LayoutConfig2 = """{"grid":{"root":{"type":"branch","data":[{"type":"leaf","data":{"views":["bb_50123566"],"activeView":"bb_50123566","id":"2"},"size":200},{"type":"leaf","data":{"views":["bb_34309365"],"activeView":"bb_34309365","id":"1"},"size":200},{"type":"leaf","data":{"views":["bb_24575431"],"activeView":"bb_24575431","id":"0"},"size":201}],"size":1092},"width":1092,"height":601,"orientation":"VERTICAL"},"panels":{"bb_24575431":{"id":"bb_24575431","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签三","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_24575431","additionalAttributes":null},"title":"标签三"},"bb_34309365":{"id":"bb_34309365","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签二","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_34309365","additionalAttributes":null},"title":"标签二"},"bb_50123566":{"id":"bb_50123566","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签一","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_50123566","additionalAttributes":null},"title":"标签一"}},"activeGroup":"2"}""";

    const string LayoutConfig3 = """{"grid":{"root":{"type":"branch","data":[{"type":"leaf","data":{"views":["bb_50123566"],"activeView":"bb_50123566","id":"2"},"size":300},{"type":"leaf","data":{"views":["bb_34309365","bb_24575431"],"activeView":"bb_24575431","id":"1"},"size":301}],"size":1092},"width":1092,"height":601,"orientation":"VERTICAL"},"panels":{"bb_34309365":{"id":"bb_34309365","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签二","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_34309365","additionalAttributes":null},"title":"标签二"},"bb_24575431":{"id":"bb_24575431","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签三","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_24575431","additionalAttributes":null},"title":"标签三"},"bb_50123566":{"id":"bb_50123566","contentComponent":"component","tabComponent":"component","params":{"componentName":"component","title":"标签一","titleWidth":null,"titleClass":null,"class":null,"visible":true,"showClose":true,"width":null,"height":null,"key":null,"isLock":false,"type":"component","id":"bb_50123566","additionalAttributes":null},"title":"标签一"}},"activeGroup":"1"}""";
}

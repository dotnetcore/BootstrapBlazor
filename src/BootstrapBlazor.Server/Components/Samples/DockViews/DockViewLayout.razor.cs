// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Server.Components.Samples.DockViews;

/// <summary>
/// DockViewLayout 组件
/// </summary>
public partial class DockViewLayout
{
    [NotNull]
    private DockView? DockView { get; set; }

    private Task Reset() => DockView.Reset();

    private void OnToggleLayout1()
    {
        LayoutConfig = LayoutConfig1;
    }

    private void OnToggleLayout2()
    {
        LayoutConfig = LayoutConfig2;
    }

    private void OnToggleLayout3()
    {
        LayoutConfig = LayoutConfig3;
    }

    string? LayoutConfig;

    const string LayoutConfig1 = """"
{"root":{"type":"row","content":[{"type":"column","content":[{"type":"stack","content":[{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_45517422","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签三","componentType":"component","componentState":{"id":"bb_45517422","showClose":true,"class":null,"key":"标签三","lock":false}}],"width":50,"minWidth":0,"height":33.460076045627375,"minHeight":0,"id":"","isClosable":true,"maximised":false,"activeItemIndex":0},{"type":"stack","content":[{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_42425232","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签二","componentType":"component","componentState":{"id":"bb_42425232","showClose":true,"class":null,"key":"标签二","lock":false}}],"width":50,"minWidth":0,"height":66.53992395437263,"minHeight":0,"id":"","isClosable":true,"maximised":false,"activeItemIndex":0}],"width":50,"minWidth":50,"height":50,"minHeight":50,"id":"","isClosable":true},{"type":"stack","content":[{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_21184535","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签一","componentType":"component","componentState":{"id":"bb_21184535","showClose":true,"class":null,"key":"标签一","lock":false}}],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_54183781","isClosable":true,"maximised":false,"activeItemIndex":0}],"width":50,"minWidth":50,"height":50,"minHeight":50,"id":"","isClosable":true},"openPopouts":[],"settings":{"constrainDragToContainer":true,"reorderEnabled":true,"popoutWholeStack":false,"blockedPopoutsThrowError":true,"closePopoutsOnUnload":true,"responsiveMode":"none","tabOverlapAllowance":0,"reorderOnTabMenuClick":true,"tabControlOffset":10,"popInOnClose":false},"dimensions":{"borderWidth":5,"borderGrabWidth":5,"minItemHeight":10,"minItemWidth":10,"headerHeight":25,"dragProxyWidth":300,"dragProxyHeight":200},"header":{"show":"top","popout":"lock/unlock","dock":"dock","close":"close","maximise":"maximise","minimise":"minimise","tabDropdown":"additional tabs"},"resolved":true}
"""";

    const string LayoutConfig2 = """"
{"root":{"type":"row","content":[{"type":"stack","content":[{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_24636646","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签三","componentType":"component","componentState":{"id":"bb_24636646","showClose":true,"class":null,"key":"标签三","lock":false}}],"width":33.333333333333336,"minWidth":0,"height":50,"minHeight":0,"id":"","isClosable":true,"maximised":false,"activeItemIndex":0},{"type":"stack","content":[{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_60600063","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签一","componentType":"component","componentState":{"id":"bb_60600063","showClose":true,"class":null,"key":"标签一","lock":false}}],"width":33.333333333333336,"minWidth":0,"height":50,"minHeight":0,"id":"bb_54183781","isClosable":true,"maximised":false,"activeItemIndex":0},{"type":"stack","content":[{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_6744750","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签二","componentType":"component","componentState":{"id":"bb_6744750","showClose":true,"class":null,"key":"标签二","lock":false}}],"width":33.33333333333333,"minWidth":0,"height":50,"minHeight":0,"id":"bb_6744750","isClosable":true,"maximised":false,"activeItemIndex":0}],"width":50,"minWidth":50,"height":50,"minHeight":50,"id":"","isClosable":true},"openPopouts":[],"settings":{"constrainDragToContainer":true,"reorderEnabled":true,"popoutWholeStack":false,"blockedPopoutsThrowError":true,"closePopoutsOnUnload":true,"responsiveMode":"none","tabOverlapAllowance":0,"reorderOnTabMenuClick":true,"tabControlOffset":10,"popInOnClose":false},"dimensions":{"borderWidth":5,"borderGrabWidth":5,"minItemHeight":10,"minItemWidth":10,"headerHeight":25,"dragProxyWidth":300,"dragProxyHeight":200},"header":{"show":"top","popout":"lock/unlock","dock":"dock","close":"close","maximise":"maximise","minimise":"minimise","tabDropdown":"additional tabs"},"resolved":true}
"""";

    const string LayoutConfig3 = """"
{"root":{"type":"stack","content":[{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_24636646","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签三","componentType":"component","componentState":{"id":"bb_24636646","showClose":true,"class":null,"key":"标签三","lock":false}},{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_60600063","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签一","componentType":"component","componentState":{"id":"bb_60600063","showClose":true,"class":null,"key":"标签一","lock":false}},{"type":"component","content":[],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_6744750","maximised":false,"isClosable":true,"reorderEnabled":true,"title":"标签二","componentType":"component","componentState":{"id":"bb_6744750","showClose":true,"class":null,"key":"标签二","lock":false}}],"width":50,"minWidth":0,"height":50,"minHeight":0,"id":"bb_60600063","isClosable":true,"maximised":false,"activeItemIndex":1},"openPopouts":[],"settings":{"constrainDragToContainer":true,"reorderEnabled":true,"popoutWholeStack":false,"blockedPopoutsThrowError":true,"closePopoutsOnUnload":true,"responsiveMode":"none","tabOverlapAllowance":0,"reorderOnTabMenuClick":true,"tabControlOffset":10,"popInOnClose":false},"dimensions":{"borderWidth":5,"borderGrabWidth":5,"minItemHeight":10,"minItemWidth":10,"headerHeight":25,"dragProxyWidth":300,"dragProxyHeight":200},"header":{"show":"top","popout":"lock/unlock","dock":"dock","close":"close","maximise":"maximise","minimise":"minimise","tabDropdown":"additional tabs"},"resolved":true}
"""";
}

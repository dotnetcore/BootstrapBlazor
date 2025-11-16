// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class FilterLogicItemTest : BootstrapBlazorTestBase
{
    [Fact]
    public async Task FilterLogicItem_Ok()
    {
        var logic = FilterLogic.And;
        var cut = Context.Render<FilterLogicItem>(pb =>
        {
            pb.Add(a => a.Logic, FilterLogic.And);
            pb.Add(a => a.LogicChanged, EventCallback.Factory.Create<FilterLogic>(this, v =>
            {
                logic = v;
            }));
        });

        var select = cut.FindComponent<Select<FilterLogic>>();
        await cut.InvokeAsync(() => select.Instance.SetValue(FilterLogic.Or));
        Assert.Equal(FilterLogic.Or, logic);
    }
}

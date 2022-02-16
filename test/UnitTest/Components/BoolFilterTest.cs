// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace UnitTest.Components;

public class BoolFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Reset_Ok()
    {
        var cut = Context.RenderComponent<BoolFilter>();

        var filter = cut.Instance;
        cut.InvokeAsync(() => filter.Reset());
    }
    [Fact]
    public void GetFilterConditions_Ok()
    {
        var cut = Context.RenderComponent<BoolFilter>();

        var filter = cut.Instance;
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.Empty(condtions);

        // Set Value
        var items = cut.FindAll(".dropdown-item");
        cut.InvokeAsync(() => items[1].Click());
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.Single(condtions);
    }

    [Fact]
    public void IsHeaderRow_OnSelectedItemChanged()
    {
        var cut = Context.RenderComponent<MockBoolFilter>();
        var filter = cut.Instance;
        var items = cut.FindAll(".dropdown-item");
        IEnumerable<FilterKeyValueAction>? condtions = null;
        cut.InvokeAsync(() => items[1].Click());
        cut.InvokeAsync(() => condtions = filter.GetFilterConditions());
        Assert.Single(condtions);
    }

    public class MockBoolFilter : BoolFilter
    {
        protected override void OnInitialized()
        {
            base.OnInitialized();

            if (TableFilter == null)
            {
                TableFilter = new TableFilter();
                TableFilter.SetParametersAsync(ParameterView.FromDictionary(new Dictionary<string, object?>
                {
                    [nameof(TableFilter.IsHeaderRow)] = true
                }));
            }
        }
    }
}

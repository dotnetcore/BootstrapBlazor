// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class TableEnumFilterTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Type_Ok()
    {
        Assert.ThrowsAny<InvalidOperationException>(() => Context.RenderComponent<EnumFilter>());

        var cut = Context.RenderComponent<TableColumnFilter>(pb =>
        {
            pb.Add(a => a.Column, new MockColumn());
        });
        var filter = cut.FindComponent<EnumFilter>();
        Assert.Equal(typeof(EnumEducation), filter.Instance.Type);
    }

    [Fact]
    public async Task FilterAction_Ok()
    {
        var cut = Context.RenderComponent<EnumFilter>(pb =>
        {
            pb.Add(a => a.Type, typeof(EnumEducation));
        });
        var filter = cut.Instance;

        var newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = EnumEducation.Primary },
                new FilterKeyValueAction() { FieldValue = EnumEducation.Middle }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        var conditions = filter.GetFilterConditions();
        Assert.Equal(2, conditions.Filters.Count);

        await cut.InvokeAsync(() => filter.Reset());
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        // Improve test coverage
        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = null },
                new FilterKeyValueAction() { FieldValue = null }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction()
        {
            Filters =
            [
                new FilterKeyValueAction() { FieldValue = true },
                new FilterKeyValueAction() { FieldValue = false }
            ]
        };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);

        newConditions = new FilterKeyValueAction() { FieldValue = "1" };
        await cut.InvokeAsync(() => filter.SetFilterConditionsAsync(newConditions));
        conditions = filter.GetFilterConditions();
        Assert.Empty(conditions.Filters);
    }

    class MockColumn : TableColumn<Foo, EnumEducation>
    {
        public MockColumn()
        {
            PropertyType = typeof(EnumEducation);
            FieldName = "Education";
        }
    }
}

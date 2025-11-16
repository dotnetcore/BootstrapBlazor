// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Components;

public class SearchFilterActionTest
{
    [Fact]
    public async Task SearchFilterAction_Ok()
    {
        var filter = new SearchFilterAction("Name", "Argo");
        Assert.Equal("Argo", filter.Value);

        filter.Reset();
        Assert.Null(filter.Value);

        await filter.SetFilterConditionsAsync(new FilterKeyValueAction
        {
            FieldKey = "Name",
            FieldValue = "Argo Zhang"
        });
        Assert.Equal("Argo Zhang", filter.Value);

        await filter.SetFilterConditionsAsync(new FilterKeyValueAction
        {
            Filters =
            [
                new FilterKeyValueAction
                {
                    FieldKey = "Name",
                    FieldValue = "Argo"
                }
            ]
        });
        Assert.Equal("Argo", filter.Value);
    }
}

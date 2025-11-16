// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Services;

public class DataServiceTest
{
    [Fact]
    public async Task DeleteAsync_Ok()
    {
        var sc = new ServiceCollection();
        sc.AddSingleton(typeof(IDataService<>), typeof(MockDataService<>));

        var provider = sc.BuildServiceProvider();
        var service = provider.GetRequiredService<IDataService<Foo>>();
        var ret = await service.DeleteAsync(new Foo[] { });
        Assert.True(ret);
    }

    [Fact]
    public async Task SaveAsync_Ok()
    {
        var sc = new ServiceCollection();
        sc.AddSingleton(typeof(IDataService<>), typeof(MockDataService<>));

        var provider = sc.BuildServiceProvider();
        var service = provider.GetRequiredService<IDataService<Foo>>();

        var ret = await service.SaveAsync(new Foo(), ItemChangedType.Add);
        Assert.True(ret);
    }

    private class MockDataService<TModel> : DataServiceBase<TModel> where TModel : class
    {
        public override Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            throw new NotImplementedException();
        }

        public override Task<bool> DeleteAsync(IEnumerable<TModel> models)
        {
            return base.DeleteAsync(models);
        }

        public override Task<bool> SaveAsync(TModel model, ItemChangedType changedType)
        {
            return base.SaveAsync(model, changedType);
        }
    }
}

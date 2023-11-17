// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.DependencyInjection;

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

    private class MockDataService<TModel> : DataServiceBase<TModel> where TModel : class, new()
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

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Core;

[Collection("TableDrawerTestContext")]
public class TableDrawerTestBase
{
    protected TestContext Context { get; }

    public TableDrawerTestBase()
    {
        Context = TableDrawerTestHost.Instance;
    }
}

[CollectionDefinition("TableDrawerTestContext")]
public class TableDrawerTestCollection : ICollectionFixture<TableDrawerTestHost>
{

}

public class TableDrawerTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public TableDrawerTestHost()
    {
        Instance = new TestContext();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        ConfigureServices(Instance.Services);

        ConfigureConfiguration(Instance.Services);

        // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
        Instance.Services.GetRequiredService<ICacheManager>();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.AddSingleton(typeof(IDataService<>), typeof(MockEFCoreDataService<>));
    }

    protected virtual void ConfigureConfiguration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }

    private class MockNullDataService<TModel>(IStringLocalizer<TModel> localizer) : IDataService<TModel> where TModel : class, new()
    {
        IStringLocalizer<TModel> Localizer { get; set; } = localizer;

        public Task<bool> AddAsync(TModel model) => Task.FromResult(true);

        public Task<bool> DeleteAsync(IEnumerable<TModel> models) => Task.FromResult(true);

        public Task<QueryData<TModel>> QueryAsync(QueryPageOptions option)
        {
            QueryData<TModel>? ret = default;
            if (Localizer is IStringLocalizer<Foo> l)
            {
                var foos = Foo.GenerateFoo(l, 2);
                ret = new QueryData<Foo>()
                {
                    Items = foos,
                    TotalCount = 2,
                    IsAdvanceSearch = true,
                    IsFiltered = true,
                    IsSearch = true,
                    IsSorted = true
                } as QueryData<TModel>;
            }
            return Task.FromResult(ret!);
        }

        public Task<bool> SaveAsync(TModel model, ItemChangedType changedType) => Task.FromResult(true);
    }

    private class MockEFCoreDataService<TModel>(IStringLocalizer<TModel> localizer) : MockNullDataService<TModel>(localizer), IEntityFrameworkCoreDataService where TModel : class, new()
    {
        public Task CancelAsync() => Task.CompletedTask;

        public Task EditAsync(object model) => Task.CompletedTask;
    }
}

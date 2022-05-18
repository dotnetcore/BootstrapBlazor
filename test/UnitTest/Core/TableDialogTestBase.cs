// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Localization;

namespace UnitTest.Core;

[Collection("TableDialogTestContext")]
public class TableDialogTestBase
{
    protected TestContext Context { get; }

    public TableDialogTestBase()
    {
        Context = TableDialogTestHost.Instance;
    }
}

[CollectionDefinition("TableDialogTestContext")]
public class TableDialogTestCollection : ICollectionFixture<TableDialogTestHost>
{

}

public class TableDialogTestHost : IDisposable
{
    [NotNull]
    internal static TestContext? Instance { get; private set; }

    public TableDialogTestHost()
    {
        Instance = new TestContext();

        // Mock 脚本
        Instance.JSInterop.Mode = JSRuntimeMode.Loose;

        ConfigureServices(Instance.Services);

        ConfigureConfigration(Instance.Services);

        // 渲染 BootstrapBlazorRoot 组件 激活 ICacheManager 接口
        Instance.Services.GetRequiredService<ICacheManager>();
    }

    protected virtual void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.AddSingleton(typeof(IDataService<>), typeof(MockEFCoreDataService<>));
        services.ConfigureJsonLocalizationOptions(op => op.AdditionalJsonAssemblies = new[] { typeof(Alert).Assembly });
    }

    protected virtual void ConfigureConfigration(IServiceCollection services)
    {
        // 增加单元测试 appsettings.json 配置文件
        services.AddConfiguration();
    }

    public void Dispose()
    {
        Instance.Dispose();
        GC.SuppressFinalize(this);
    }

    private class MockNullDataService<TModel> : IDataService<TModel> where TModel : class, new()
    {
        IStringLocalizer<TModel> Localizer { get; set; }

        public MockNullDataService(IStringLocalizer<TModel> localizer) => Localizer = localizer;

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

    private class MockEFCoreDataService<TModel> : MockNullDataService<TModel>, IEntityFrameworkCoreDataService where TModel : class, new ()
    {
        public MockEFCoreDataService(IStringLocalizer<TModel> localizer) : base(localizer)
        {

        }

        public Task CancelAsync() => Task.CompletedTask;

        public Task EditAsync(object model) => Task.CompletedTask;
    }
}

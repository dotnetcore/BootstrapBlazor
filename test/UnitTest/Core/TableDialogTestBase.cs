// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Microsoft.Extensions.Localization;

namespace UnitTest.Core;

public class TableDialogTestBase : BootstrapBlazorTestBase
{
    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddBootstrapBlazor(op => op.ToastDelay = 2000);
        services.AddSingleton(typeof(IDataService<>), typeof(MockEFCoreDataService<>));
    }

    private class MockNullDataService<TModel>(IStringLocalizer<TModel> localizer) : IDataService<TModel> where TModel : class
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

    private class MockEFCoreDataService<TModel>(IStringLocalizer<TModel> localizer) : MockNullDataService<TModel>(localizer), IEntityFrameworkCoreDataService where TModel : class
    {
        public Task CancelAsync() => Task.CompletedTask;

        public Task EditAsync(object model) => Task.CompletedTask;
    }

    protected class MockEFCoreDataService(IStringLocalizer<Foo> localizer) : IDataService<Foo>, IEntityFrameworkCoreDataService
    {
        IStringLocalizer<Foo> Localizer { get; set; } = localizer;

        public Task<bool> AddAsync(Foo model) => Task.FromResult(true);

        public Task<bool> DeleteAsync(IEnumerable<Foo> models) => Task.FromResult(true);

        public Task<QueryData<Foo>> QueryAsync(QueryPageOptions option)
        {
            var foos = Foo.GenerateFoo(Localizer, 2);
            var ret = new QueryData<Foo>()
            {
                Items = foos,
                TotalCount = 2,
                IsAdvanceSearch = true,
                IsFiltered = true,
                IsSearch = true,
                IsSorted = true
            };
            return Task.FromResult(ret);
        }

        public Task<bool> SaveAsync(Foo model, ItemChangedType changedType) => Task.FromResult(true);

        public Task CancelAsync() => Task.CompletedTask;

        public Task EditAsync(object model) => Task.CompletedTask;
    }
}

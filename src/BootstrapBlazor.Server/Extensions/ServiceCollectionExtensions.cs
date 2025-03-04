// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

using Longbow.Tasks.Services;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Options;
using System.Text.Encodings.Web;
using System.Text.Unicode;

namespace Microsoft.Extensions.DependencyInjection;

static class ServiceCollectionExtensions
{
    public static IServiceCollection AddBootstrapBlazorServerService(this IServiceCollection services)
    {
        // 增加中文编码支持网页源码显示汉字
        services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

        // 增加错误日志
        services.AddLogging(logging => logging.AddFileLogger());

        // 增加多语言支持配置信息
        services.AddRequestLocalization<IOptionsMonitor<BootstrapBlazorOptions>>((localizerOption, blazorOption) =>
        {
            blazorOption.OnChange(Invoke);
            Invoke(blazorOption.CurrentValue);
            return;

            void Invoke(BootstrapBlazorOptions option)
            {
                var supportedCultures = option.GetSupportedCultures();
                localizerOption.SupportedCultures = supportedCultures;
                localizerOption.SupportedUICultures = supportedCultures;
            }
        });

        services.AddControllers();

        // 增加 SignalR 服务数据传输大小限制配置
        services.Configure<HubOptions>(option => option.MaximumReceiveMessageSize = null);

        // 增加后台任务服务
        services.AddTaskServices();
        services.AddHostedService<ClearTempFilesService>();
        services.AddHostedService<MockOnlineContributor>();

        // 增加通用服务
        services.AddBootstrapBlazorServices();

        return services;
    }
}

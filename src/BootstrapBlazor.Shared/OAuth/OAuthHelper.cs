// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using BootstrapBlazor.Shared.OAuth;
using Longbow.GiteeAuth;
using Longbow.GitHubAuth;
using Longbow.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Linq;
using System.Collections.Concurrent;
using System.Net.Http.Headers;
using System.Security.Policy;

namespace Bootstrap.Shared.OAuth;

/// <summary>
/// Gitee 授权帮助类
/// </summary>
public static class OAuthHelper
{
    private static ConcurrentDictionary<string, AzureOpenAIUser> Users { get; } = new();

    /// <summary>
    /// 设置 GiteeOptions.Events.OnCreatingTicket 方法
    /// </summary>
    /// <param name="option"></param>
    public static void Configure<TOptions>(TOptions option) where TOptions : LgbOAuthOptions
    {
        option.Events.OnCreatingTicket = async context =>
        {
            // call webhook
            var config = context.HttpContext.RequestServices.GetRequiredService<IConfiguration>();
            var webhookUrls = config.GetRequiredSection(typeof(TOptions).Name)
                .GetValue("StarredUrl", "")!
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (var webhookUrl in webhookUrls)
            {
                if (option is GitHubOptions)
                {
                    var requestMessage = new HttpRequestMessage(HttpMethod.Put, webhookUrl);
                    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/vnd.github+json"));
                    requestMessage.Headers.Add("Authorization", $"Bearer {context.AccessToken}");
                    await context.Backchannel.SendAsync(requestMessage, context.HttpContext.RequestAborted);
                }
                else
                {
                    var parameters = new Dictionary<string, string?>()
                    {
                        { "access_token", context.AccessToken }
                    };
                    var url = QueryHelpers.AddQueryString(webhookUrl, parameters);
                    var requestMessage = new HttpRequestMessage(HttpMethod.Put, url);
                    requestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    await context.Backchannel.SendAsync(requestMessage, context.HttpContext.RequestAborted);
                }
            }

            // 生成用户
            var user = context.User.ToAuthUser();
            Users.GetOrAdd(user.Login, new AzureOpenAIUser()
            {
                Id = user.Id,
                Login = user.Login,
                Name = user.Name,
                Avatar_Url = user.Avatar_Url
            });
        };
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginName"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public static bool TryGet(string loginName, [NotNullWhen(true)] out AzureOpenAIUser? user) => Users.TryGetValue(loginName, out user);

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginName"></param>
    /// <param name="user"></param>
    public static bool TryUpdate(string loginName, [NotNullWhen(true)] out AzureOpenAIUser? user)
    {
        var ret = false;
        if (TryGet(loginName, out user))
        {
            user.Count++;
            ret = true;
        }
        return ret;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="loginName"></param>
    /// <returns></returns>
    public static bool Validate(string loginName)
    {
        var ret = false;
        if (TryGet(loginName, out var user))
        {
            ret = user.Valid();
        }
        return ret;
    }
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Longbow.GiteeAuth;
using Longbow.GitHubAuth;
using Longbow.OAuth;
using Microsoft.AspNetCore.Authentication.OAuth;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace BootstrapBlazor.Shared.OAuth;

/// <summary>
/// 
/// </summary>
public class OAuthService
{
    [NotNull]
    private GiteeOptions? GiteeOptions { get; }

    [NotNull]
    private GitHubOptions? GitHubOptions { get; }

    private HttpClient Client { get; }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="configuration"></param>
    /// <param name="httpClientFactory"></param>
    public OAuthService(IConfiguration configuration, IHttpClientFactory httpClientFactory)
    {
        GiteeOptions = configuration.GetSection(nameof(GiteeOptions)).Get<GiteeOptions>();
        GitHubOptions = configuration.GetSection(nameof(GitHubOptions)).Get<GitHubOptions>();

        Client = httpClientFactory.CreateClient();
    }

    /// <summary>
    /// 
    /// </summary>
    public Task<bool> Authenticate()
    {
        return Task.FromResult(false);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="authenticationScheme"></param>
    /// <returns></returns>
    public string BuildChallengeUrl(string authenticationScheme) => authenticationScheme switch
    {
        "Gitee" => BuildGiteeChallengeUrl(),
        _ => BuildGitHubChallengeUrl()
    };

    private string BuildGiteeChallengeUrl()
    {
        var parameters = new Dictionary<string, string?>
        {
            { "client_id", GiteeOptions.ClientId },
            { "scope", FormatScope(GiteeOptions.Scope) },
            { "response_type", "code" },
            { "redirect_uri", "http://localhost:5053" + GiteeOptions.CallbackPath },
        };

        return QueryHelpers.AddQueryString(GiteeOptions.AuthorizationEndpoint, parameters);
    }

    private string BuildGitHubChallengeUrl()
    {
        var parameters = new Dictionary<string, string?>
        {
            { "client_id", GitHubOptions.ClientId },
            { "scope", FormatScope(GitHubOptions.Scope) },
            { "response_type", "code" },
            { "redirect_uri", GitHubOptions.CallbackPath },
        };

        return QueryHelpers.AddQueryString(GitHubOptions.AuthorizationEndpoint, parameters);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task ExchangeCodeAsync(string code)
    {
        // ExchangeCodeAsync
        // https://gitee.com/oauth/token?grant_type=authorization_code&code={code}&client_id={client_id}&redirect_uri={redirect_uri}&client_secret={client_secret}
        var parameters = new Dictionary<string, string?>
        {
            { "grant_type", "authorization_code" },
            { "code", code },
            { "client_id", GiteeOptions.ClientId },
            { "redirect_uri", "http://localhost:5053" + GiteeOptions.CallbackPath },
            { "client_secret", GiteeOptions.ClientSecret },
        };

        var url = QueryHelpers.AddQueryString(GiteeOptions.TokenEndpoint, parameters);
        var resp = await Client.PostAsync(url, null);
        var content = await resp.Content.ReadAsStringAsync();
        var payload = JsonDocument.Parse(content);
        var tokens = OAuthTokenResponse.Success(payload);

        // Get User Info
        var tokenRequestParameters = new Dictionary<string, string?>()
        {
            { "access_token", tokens.AccessToken }
        };
        var userUrl = QueryHelpers.AddQueryString(GiteeOptions.UserInformationEndpoint, tokenRequestParameters);
        content = await Client.GetStringAsync(userUrl);
        var element = JsonDocument.Parse(content).RootElement;

        var target = element.EnumerateObject();
        var id = target.TryGetValue("id");
        var name = target.TryGetValue("name");
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="scopes"></param>
    /// <returns></returns>
    protected virtual string FormatScope(IEnumerable<string> scopes)
                => string.Join(" ", scopes); // OAuth2 3.3 space separated
}

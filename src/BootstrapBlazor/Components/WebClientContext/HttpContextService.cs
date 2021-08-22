// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// 
    /// </summary>
    public class HttpContextService
    {
        /// <summary>
        /// 获得/设置 操作日志主键ID
        /// </summary>
        public string? Id { get; private set; }

        /// <summary>
        /// 获得/设置 用户名称
        /// </summary>
        public string? UserName { get; private set; }

        /// <summary>
        /// 获得/设置 客户端IP
        /// </summary>
        public string? Ip { get; private set; }

        /// <summary>
        /// 获得/设置 客户端地点
        /// </summary>
        public string? City { get; private set; }

        /// <summary>
        /// 获得/设置 客户端浏览器
        /// </summary>
        public string? Browser { get; private set; }

        /// <summary>
        /// 获得/设置 客户端操作系统
        /// </summary>
        public string? OS { get; private set; }

        /// <summary>
        /// 获取/设置 请求网址
        /// </summary>
        public string? RequestUrl { get; private set; }

        /// <summary>
        /// 获得/设置 客户端 UserAgent
        /// </summary>
        public string? UserAgent { get; private set; }

        /// <summary>
        /// 获得/设置 客户端 Referer
        /// </summary>
        public string? Referer { get; private set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="navigation"></param>
        public HttpContextService(IHttpContextAccessor httpContextAccessor, NavigationManager navigation)
        {
            var context = httpContextAccessor.HttpContext;
            var headers = context.Request.Headers;

            // UserAgent
            var agent = headers["User-Agent"];
            UserAgent = agent;

            // OS/Browser
            if (!string.IsNullOrEmpty(UserAgent))
            {
                var at = new UserAgent(UserAgent);
                OS = $"{at.OS.Name} {at.OS.Version}";
                Browser = $"{at.Browser.Name} {at.Browser.Version}";
            }

            // Referer
            Referer = headers["Referer"];

            // RequestUrl
            RequestUrl = navigation.Uri;
        }

        /// <summary>
        /// 获得/设置 模态弹窗返回值任务实例
        /// </summary>
        internal TaskCompletionSource<bool> ReturnTask { get; } = new();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task RetrieveRemoteIp()
        {
            var callback = Cache.FirstOrDefault().Callback;
            if (callback != null)
            {
                await callback.Invoke("/ip.axd");
                await ReturnTask.Task;
            }
        }

        internal void SetIp(string ip)
        {
            Ip = ip;
            ReturnTask.SetResult(true);
        }

        /// <summary>
        /// 获得 回调委托缓存集合
        /// </summary>
        private List<(IComponent Key, Func<string, ValueTask> Callback)> Cache { get; set; } = new();

        /// <summary>
        /// 注册服务
        /// </summary>
        /// <param name="key"></param>
        /// <param name="callback"></param>
        internal void Register(IComponent key, Func<string, ValueTask> callback) => Cache.Add((key, callback));

        /// <summary>
        /// 注销事件
        /// </summary>
        internal void UnRegister(IComponent key)
        {
            var item = Cache.FirstOrDefault(i => i.Key == key);
            if (item.Key != null) Cache.Remove(item);
        }
    }
}

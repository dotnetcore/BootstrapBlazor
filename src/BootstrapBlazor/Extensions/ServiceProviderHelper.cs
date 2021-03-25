// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace BootstrapBlazor.Components
{
    /// <summary>
    /// ServiceProviderHelper 注入服务扩展类
    /// </summary>
    public static class ServiceProviderHelper
    {
        private static Lazy<ServiceProvider>? _serviceProviderCreator;
        private static IServiceProvider? _provider;

        internal static void RegisterService(IServiceCollection services)
        {
            _serviceProviderCreator = new Lazy<ServiceProvider>(() => services.BuildServiceProvider());
        }

        internal static void RegisterProvider(IServiceProvider services)
        {
            _provider = services;
        }

        /// <summary>
        /// 获取系统 IServiceProvider 接口
        /// </summary>
        public static IServiceProvider? ServiceProvider
        {
            get
            {
                return OperatingSystem.IsBrowser()
                    ? (_provider ?? _serviceProviderCreator?.Value)
                    : HttpContextLocal.Current()?.RequestServices;
            }
        }

        /// <summary>
        /// 获取本地 HttpContext 上下文
        /// </summary>
        private static class HttpContextLocal
        {
            private static Func<object>? _asyncLocalAccessor;
            private static Func<object, object>? _holderAccessor;
            private static Func<object, HttpContext>? _httpContextAccessor;

            private static Func<object> CreateAsyncLocalAccessor()
            {
                var fieldInfo = typeof(HttpContextAccessor).GetField("_httpContextCurrent", BindingFlags.Static | BindingFlags.NonPublic);
                var field = Expression.Field(null, fieldInfo!);
                return Expression.Lambda<Func<object>>(field).Compile();
            }

            private static Func<object, object> CreateHolderAccessor(object asyncLocal)
            {
                var holderType = asyncLocal.GetType().GetGenericArguments()[0];
                var pi = typeof(AsyncLocal<>).MakeGenericType(holderType).GetProperty("Value");
                var para_exp = Expression.Parameter(typeof(object));
                var convert = Expression.Convert(para_exp, asyncLocal.GetType());
                var body = Expression.Property(convert, pi!);
                return Expression.Lambda<Func<object, object>>(body, para_exp).Compile();
            }

            private static Func<object, HttpContext> CreateHttpContextAccessor(object holder)
            {
                var para_exp = Expression.Parameter(typeof(object));
                var convert = Expression.Convert(para_exp, holder.GetType());
                var field = Expression.Field(convert, "Context");
                var convertAsResult = Expression.Convert(field, typeof(HttpContext));
                return Expression.Lambda<Func<object, HttpContext>>(convertAsResult, para_exp).Compile();
            }

            /// <summary>
            /// 获取当前 HttpContext 对象
            /// </summary>
            /// <returns></returns>
            public static HttpContext? Current()
            {
                var asyncLocal = (_asyncLocalAccessor ??= CreateAsyncLocalAccessor())();
                if (asyncLocal == null)
                {
                    return null;
                }

                var holder = (_holderAccessor ??= CreateHolderAccessor(asyncLocal))(asyncLocal);
                if (holder == null)
                {
                    return null;
                }

                return (_httpContextAccessor ??= CreateHttpContextAccessor(holder))(holder);
            }
        }
    }
}

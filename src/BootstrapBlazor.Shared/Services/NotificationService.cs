// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BootstrapBlazor.Shared.Services
{
    /// <summary>
    /// 
    /// </summary>
    public class NotificationService
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="payload"></param>
        /// <returns></returns>
        public void Dispatch(GiteePostBody payload)
        {
            lock (locker)
            {
                Cache.ForEach(cb =>
                {
                    cb(payload);
                });
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal void Subscribe(Func<GiteePostBody, Task> callback)
        {
            lock (locker)
            {
                Cache.Add(callback);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="callback"></param>
        /// <exception cref="NotImplementedException"></exception>
        internal void UnSubscribe(Func<GiteePostBody, Task> callback)
        {
            lock (locker)
            {
                Cache.Remove(callback);
            }
        }

        private List<Func<GiteePostBody, Task>> Cache { get; } = new(50);

        private object locker = new();
    }
}

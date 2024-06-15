// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapBlazor.Components;

/// <summary>
/// 
/// </summary>
public class MaskService : IMaskService
{

    private Dictionary<ComponentBase, (Func<MaskOption?, Task> Show, Func<Task> Close)> Cache = new Dictionary<ComponentBase, (Func<MaskOption?, Task> Show, Func<Task> Close)>();

    /// <summary>
    /// 
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="show"></param>
    /// <param name="close"></param>
    internal void Register(ComponentBase obj, Func<MaskOption?, Task> show, Func<Task> close)
    {
        Cache.Add(obj, (show, close));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task Close()
    {
        var callback = Cache.FirstOrDefault();
        if (callback.Value.Close == null)
        {
#if NET8_0_OR_GREATER
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-webapp step 7 for BootstrapBlazorRoot");
#else
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-server step 7 for BootstrapBlazorRoot");
#endif
        }

        await callback.Value.Close();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns></returns>
    public async Task Show(MaskOption option)
    {
        var callback = Cache.FirstOrDefault();
        if (callback.Value.Show == null)
        {
#if NET8_0_OR_GREATER
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-webapp step 7 for BootstrapBlazorRoot");
#else
            throw new InvalidOperationException($"{GetType().Name} not registered. refer doc https://www.blazor.zone/install-server step 7 for BootstrapBlazorRoot");
#endif
        }

        await callback.Value.Show(option);
    }
}

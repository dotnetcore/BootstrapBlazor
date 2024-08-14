// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

namespace BootstrapBlazor.Components;

/// <summary>
/// BootstrapInput 组件
/// </summary>
public partial class BootstrapInput<TValue>
{
    /// <summary>
    /// 获得/设置 是否为只读 默认 false
    /// </summary>
    [Parameter]
    public bool Readonly { get; set; }

    /// <summary>
    /// 获得/设置 用户删除后是否自动更改为默认值 0 默认 false
    /// </summary>
    [Parameter]
    public bool AutoSetDefaultWhenNull { get; set; }

    private string? ReadonlyString => Readonly ? "true" : null;

    /// <summary>
    /// <inheritdoc/>
    /// </summary>
    /// <param name="value"></param>
    /// <param name="result"></param>
    /// <param name="validationErrorMessage"></param>
    /// <returns></returns>
    protected override bool TryParseValueFromString(string value, [MaybeNullWhen(false)] out TValue result, out string? validationErrorMessage)
    {
        bool ret;
        var v = IsTrim ? value.Trim() : value;
        if (AutoSetDefaultWhenNull && string.IsNullOrEmpty(v))
        {
            result = default!;
            validationErrorMessage = null;
            ret = true;
        }
        else
        {
            ret = base.TryParseValueFromString(v, out result, out validationErrorMessage);
        }
        return ret;
    }
}

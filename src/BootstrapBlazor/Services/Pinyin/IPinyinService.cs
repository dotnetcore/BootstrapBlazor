// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// 拼音服务接口
/// </summary>
public interface IPinyinService
{
    /// <summary>
    /// 获得首字母拼音方法
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    HashSet<string> GetFirstLetters(string text);

    /// <summary>
    /// 获得完整拼音方法
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    string GetPinyin(string text);

    /// <summary>
    /// 判断是否为中文字符
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    bool IsChinese(char c);

    /// <summary>
    /// 判断字符串是否包含中文字符
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    bool ContainsChinese(string text);
}
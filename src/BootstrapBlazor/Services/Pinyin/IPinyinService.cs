// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
/// <para lang="zh">拼音服务接口</para>
/// <para lang="en">拼音服务接口</para>
/// </summary>
public interface IPinyinService
{
    /// <summary>
    /// <para lang="zh">获得首字母拼音方法</para>
    /// <para lang="en">Gets首字母拼音方法</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="caseCategory"></param>
    /// <returns></returns>
    HashSet<string> GetFirstLetters(string text, PinyinLetterCaseCategory caseCategory = PinyinLetterCaseCategory.UppercaseLetter);

    /// <summary>
    /// <para lang="zh">获得完整拼音方法</para>
    /// <para lang="en">Gets完整拼音方法</para>
    /// </summary>
    /// <param name="text"></param>
    /// <param name="caseCategory"></param>
    /// <returns></returns>
    HashSet<string> GetPinyin(string text, PinyinLetterCaseCategory caseCategory = PinyinLetterCaseCategory.UppercaseLetter);

    /// <summary>
    /// <para lang="zh">判断是否为中文字符</para>
    /// <para lang="en">判断whether为中文字符</para>
    /// </summary>
    /// <param name="c"></param>
    /// <returns></returns>
    bool IsChinese(char c);

    /// <summary>
    /// <para lang="zh">判断字符串是否包含中文字符</para>
    /// <para lang="en">判断字符串whether包含中文字符</para>
    /// </summary>
    /// <param name="text"></param>
    /// <returns></returns>
    bool ContainsChinese(string text);
}

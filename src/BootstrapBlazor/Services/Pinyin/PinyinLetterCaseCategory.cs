// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace BootstrapBlazor.Components;

/// <summary>
///  <para lang="zh">Pinyin 大小写枚举</para>
///  <para lang="en">Pinyin 大小写enum</para>
/// </summary>
public enum PinyinLetterCaseCategory
{
    /// <summary>
    ///  <para lang="zh">Indicates that the character is an uppercase letter.</para>
    ///  <para lang="en">Indicates that the character is an uppercase letter.</para>
    /// </summary>
    UppercaseLetter = 0,

    /// <summary>
    ///  <para lang="zh">Represents a Unicode character that is classified as a lowercase letter.</para>
    ///  <para lang="en">Represents a Unicode character that is classified as a lowercase letter.</para>
    /// </summary>
    LowercaseLetter = 1,
}

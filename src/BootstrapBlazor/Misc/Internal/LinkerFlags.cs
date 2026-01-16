// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

#if NET6_0_OR_GREATER
namespace Microsoft.AspNetCore.Internal;

[ExcludeFromCodeCoverage]
internal static class LinkerFlags
{
    /// <summary>
    /// <para lang="zh">Flags for a member that is JSON (de)serialized.</para>
    /// <para lang="en">Flags for a member that is JSON (de)serialized.</para>
    /// </summary>
    public const DynamicallyAccessedMemberTypes JsonSerialized = DynamicallyAccessedMemberTypes.PublicConstructors | DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.PublicProperties;

    /// <summary>
    /// <para lang="zh">Flags for a component</para>
    /// <para lang="en">Flags for a component</para>
    /// </summary>
    public const DynamicallyAccessedMemberTypes Component = DynamicallyAccessedMemberTypes.All;
}
#endif

﻿// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the Apache 2.0 License
// See the LICENSE file in the project root for more information.
// Maintainer: Argo Zhang(argo@live.ca) Website: https://www.blazor.zone

namespace UnitTest.Core;

public class TestBase
{
    protected TestContext Context { get; }

    public TestBase()
    {
        Context = new TestContext();
        Context.JSInterop.Mode = JSRuntimeMode.Loose;
    }
}

// Copyright (c) Argo Zhang (argo@163.com). All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.
// Website: https://www.blazor.zone or https://argozhang.github.io/

using HarmonyLib;

namespace UnitTest.Components;

public class BootstrapBlazorRootTest : BootstrapBlazorTestBase
{
    [Fact]
    public void Render_Ok()
    {
        var cut = Context.RenderComponent<BootstrapBlazorRoot>();
        cut.Contains("<app>");

        // 更改 OperatingSystem.IsBrowser 返回值
        var harmony = new Harmony("bb");
        var original = typeof(OperatingSystem).GetMethod(nameof(OperatingSystem.IsBrowser));
        var prefix = typeof(HookOperatingSystem).GetMethod(nameof(HookOperatingSystem.Prefix));
        harmony.Patch(original, new HarmonyMethod(prefix));

        cut.SetParametersAndRender();
        cut.DoesNotContain("<app>");

        harmony.Unpatch(original, prefix);
    }

    private class HookOperatingSystem
    {
        public static bool Prefix(ref bool __result)
        {
            // 更改结果为 true
            __result = true;

            // 不再调用原生方法
            return false;
        }
    }
}

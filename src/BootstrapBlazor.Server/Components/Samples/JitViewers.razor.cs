using Microsoft.AspNetCore.Components;

namespace BootstrapBlazor.Server.Components.Samples;

public partial class JitViewers : ComponentBase
{
    [Inject, NotNull]
    private IStringLocalizer<JitViewers>? Localizer { get; set; }
}


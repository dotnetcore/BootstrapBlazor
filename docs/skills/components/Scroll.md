---
component: Scroll
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Scroll Skill

## Component Purpose

Scroll Component

Primary source directory: `src/BootstrapBlazor/Components/Scroll`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Scroll/Scroll.razor`
- `src/BootstrapBlazor/Components/Scroll/Scroll.razor.cs`
- `src/BootstrapBlazor/Components/Scroll/Scroll.razor.js`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `Height` | `string?` | Gets or sets Height |
| `ScrollHoverWidth` | `int?` | Gets or sets Scroll Hover Width. Default null. Use in config class if not set |
| `ScrollWidth` | `int?` | Gets or sets Scroll Width. Default null. Use in config class if not set |
| `Width` | `string?` | Gets or sets Width |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Scrolls.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Scrolls.razor.cs`

Sample analysis:

- Direct `<Scroll>` tag usages detected: 1
- Observed attributes in official Sample: `@ref`, `Height`
Sample-derived snippet:

```razor
<Scroll @ref="_scroll" Height="200px">
            <div class="m-1">@Localizer["ScrollNormalDescription"]</div>
            <div class="bg-primary" style="height: 100px;"></div>
            <div class="bg-secondary" style="height: 100px;"></div>
            <div class="bg-warning" style="height: 100px;"></div>
            <div class="bg-success" style="height: 100px;"></div>
            <div class="bg-info" style="height: 100px;"></div>
            <div class="m-1">@Localizer["ScrollNormalBottom"]</div>
        </Scroll>
```

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Use the official Sample-derived snippet above as the starting point.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Scroll` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
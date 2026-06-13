---
component: Block
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Block Skill

## Component Purpose

Conditional Output Component

Primary source directory: `src/BootstrapBlazor/Components/Block`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Block/Block.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnQueryCondition` | `Func<string?, Task<bool>>?` | Callback/event parameter; Gets or sets whether to show this Block. Default is true |
| `Authorized` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the authorized content |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child content |
| `NotAuthorized` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the not authorized content |
| `Condition` | `bool?` | Gets or sets whether to show this Block. Default is null (not participating in judgment). Show if set to true |
| `Name` | `string?` | Gets or sets the Block name. This name is passed to the user via the first parameter of |
| `Roles` | `IEnumerable<string>?` | Gets or sets the allowed roles for the Block |
| `Users` | `IEnumerable<string>?` | Gets or sets the allowed users for the Block |

## Events And Callbacks

`OnQueryCondition: Func<string?, Task<bool>>?`

## Templates And Child Content

`Authorized: RenderFragment?`, `ChildContent: RenderFragment?`, `NotAuthorized: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSetAsync`
- JS interop or module dependency detected: `False`
- Razor component file detected: `False`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Blocks.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Blocks.razor.cs`

Sample analysis:

- Direct `<Block>` tag usages detected: 5
- Observed attributes in official Sample: `Name`, `OnQueryCondition`, `Roles`, `Users`
Sample-derived snippet:

```razor
<Block OnQueryCondition="OnQueryCondition" Name="Normal">
            <div>@Localizer["Content"]</div>
        </Block>
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

1. Read `src/BootstrapBlazor/Components/Block` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
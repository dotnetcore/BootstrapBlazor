---
component: Logout
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Logout Skill

## Component Purpose

ListView component

Primary source directory: `src/BootstrapBlazor/Components/Logout`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Logout/Logout.razor`
- `src/BootstrapBlazor/Components/Logout/Logout.razor.cs`
- `src/BootstrapBlazor/Components/Logout/LogoutLink.razor`
- `src/BootstrapBlazor/Components/Logout/LogoutLink.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets omponent |
| `HeaderTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets component HeaderTemplate |
| `LinkTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets component LinkTemplate |
| `AvatarRadius` | `string?` | Gets or sets the avatar border radius. Default is null |
| `DisplayName` | `string?` | Gets or sets componentdisplay |
| `Icon` | `string?` | Gets or sets icon |
| `ImageUrl` | `string?` | Gets or sets component |
| `PrefixDisplayNameText` | `string?` | Gets or sets componentdisplay Default is  |
| `PrefixUserNameText` | `string?` | Gets or sets component Default is  |
| `ShowUserName` | `bool` | Default: `true`; Gets or sets whetherdisplay?Default is true display |
| `Text` | `string?` | Gets or sets button |
| `Url` | `string?` | Gets or sets button |
| `UserName` | `string?` | Gets or sets component |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`, `HeaderTemplate: RenderFragment?`, `LinkTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Logouts.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Logouts.razor.cs`

Sample analysis:

- Direct `<Logout>` tag usages detected: 5
- Observed attributes in official Sample: `class`, `DisplayName`, `ImageUrl`, `ShowUserName`, `UserName`
Sample-derived snippet:

```razor
<Logout ImageUrl="@WebsiteOption.Value.GetAssetUrl("images/Argo.png")" DisplayName="Administrators" UserName="Admin" />
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

1. Read `src/BootstrapBlazor/Components/Logout` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
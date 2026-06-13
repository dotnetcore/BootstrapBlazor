---
component: Dropdown
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Dropdown Skill

## Component Purpose

Dropdown Component

Primary source directory: `src/BootstrapBlazor/Components/Dropdown`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Dropdown/Dropdown.razor`
- `src/BootstrapBlazor/Components/Dropdown/Dropdown.razor.cs`
- `src/BootstrapBlazor/Components/Dropdown/Dropdown.razor.js`
- `src/BootstrapBlazor/Components/Dropdown/DropdownItem.razor`
- `src/BootstrapBlazor/Components/Dropdown/DropdownItem.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClick` | `EventCallback<MouseEventArgs>` | Callback/event parameter; Gets or sets OnClick Event |
| `OnClickWithoutRender` | `Func<Task>?` | Callback/event parameter; Gets or sets OnClick Event without render parent |
| `OnDisabledCallback` | `Func<bool>?` | Callback/event parameter; Gets or sets Disabled Callback. Default is null. Priority higher than |
| `OnSelectedItemChanged` | `Func<SelectedItem, Task>?` | Callback/event parameter; SelectedItemChanged Callback |
| `ButtonTemplate` | `RenderFragment<SelectedItem?>?` | Template parameter; verify context type; Gets or sets Button Content Template |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `ItemsTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Items Template. Default is null |
| `ItemTemplate` | `RenderFragment<SelectedItem>?` | Template parameter; verify context type; Gets or sets Item Template |
| `Color` | `Color` | Default: `Color.Primary`; Gets or sets Color. Default is Color.Primary |
| `Direction` | `Direction` | Gets or sets Dropdown Direction. Default is Dropdown (down) |
| `Disabled` | `bool` | Gets or sets Disabled. Default is false. Priority lower than |
| `FixedButtonText` | `string?` | Gets or sets Fixed Button Text. Default is null |
| `Icon` | `string?` | Gets or sets Icon |
| `IsAsync` | `bool` | Gets or sets Is Async Button. Default is false. If true, button is disabled and shows loading animation on click |
| `IsFixedButtonText` | `bool` | Gets or sets Whether Fixed Button Text. Default is false |
| `IsKeepDisabled` | `bool` | Gets or sets Keep Disabled after async completion. Default is false |
| `IsOutline` | `bool` | Gets or sets the Outline style. Default is false |
| `Items` | `IEnumerable<SelectedItem>?` | Gets or sets Data Items |
| `LoadingIcon` | `string?` | Gets or sets Loading Icon. Default is fa-solid fa-spin fa-spinner |
| `MenuAlignment` | `Alignment` | Gets or sets Menu Alignment. Default is none |
| `ShowFixedButtonTextInDropdown` | `bool` | Gets or sets Whether Show Fixed Button Text in Dropdown. Default is false |
| `ShowSplit` | `bool` | Gets or sets Is Split Button. Default is false |
| `Size` | `Size` | Gets or sets Size. Default is none |
| `Text` | `string?` | Gets or sets Text |

## Events And Callbacks

`OnClick: EventCallback<MouseEventArgs>`, `OnClickWithoutRender: Func<Task>?`, `OnDisabledCallback: Func<bool>?`, `OnSelectedItemChanged: Func<SelectedItem, Task>?`

## Templates And Child Content

`ButtonTemplate: RenderFragment<SelectedItem?>?`, `ChildContent: RenderFragment?`, `ItemsTemplate: RenderFragment?`, `ItemTemplate: RenderFragment<SelectedItem>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Dropdowns.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Dropdowns.razor.cs`

Sample analysis:

- Direct `<Dropdown>` tag usages detected: 38
- Observed attributes in official Sample: `Color`, `Direction`, `DisplayText`, `FixedButtonText`, `Icon`, `IsAsync`, `IsFixedButtonText`, `IsOutline`, `Items`, `MenuAlignment`, `OnClickWithoutRender`, `OnSelectedItemChanged`, `ShowLabel`, `ShowSplit`, `Size`, `TValue`
Sample-derived snippet:

```razor
<Dropdown TValue="string" Items="Items" OnSelectedItemChanged="@ShowMessage" Color="Color.Secondary"></Dropdown>
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

1. Read `src/BootstrapBlazor/Components/Dropdown` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
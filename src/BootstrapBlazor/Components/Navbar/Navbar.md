---
component: Navbar
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Navbar Skill

## Component Purpose

Navbar component

Primary source directory: `src/BootstrapBlazor/Components/Navbar`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Navbar/Navbar.razor`
- `src/BootstrapBlazor/Components/Navbar/Navbar.razor.cs`
- `src/BootstrapBlazor/Components/Navbar/NavbarBrand.razor`
- `src/BootstrapBlazor/Components/Navbar/NavbarBrand.razor.cs`
- `src/BootstrapBlazor/Components/Navbar/NavbarCollapse.razor`
- `src/BootstrapBlazor/Components/Navbar/NavbarCollapse.razor.cs`
- `src/BootstrapBlazor/Components/Navbar/NavbarDropdown.razor`
- `src/BootstrapBlazor/Components/Navbar/NavbarDropdown.razor.cs`
- `src/BootstrapBlazor/Components/Navbar/NavbarDropdownDivider.razor`
- `src/BootstrapBlazor/Components/Navbar/NavbarDropdownItem.razor`
- `src/BootstrapBlazor/Components/Navbar/NavbarDropdownItem.razor.cs`
- `src/BootstrapBlazor/Components/Navbar/NavbarGroup.razor`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the child component template |
| `BackgroundColorCssClass` | `string?` | Gets or sets the background color CSS class name. Default is null |
| `Direction` | `Direction` | Gets or sets the dropdown direction. Default is Dropdown |
| `Height` | `string?` | Gets or sets the height value. Default is 200px |
| `ImageCss` | `string?` | Gets or sets the CSS class of img element. Default is null |
| `ImageUrl` | `string?` | Gets or sets the display image URL. Default is null |
| `IsScrolling` | `bool` | Gets or sets whether to enable scrolling |
| `MenuAlignment` | `Alignment` | Gets or sets the menu alignment. Default is none |
| `Size` | `Size` | Default: `Size.Medium`; Gets or sets the component size. Default is |
| `Target` | `string?` | Gets or sets the A tag target parameter. Default is null |
| `Text` | `string?` | Gets or sets the dropdown menu title text |
| `Url` | `string?` | Gets or sets the menu item text |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Navbars.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Navbars.razor.cs`

Sample analysis:

- Direct `<Navbar>` tag usages detected: 1
- No direct `<Navbar>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Source-validated skeleton:

```razor
<Navbar>
</Navbar>
```

This skeleton is not a substitute for source validation. Add only parameters that exist in the current source.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Navbar` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
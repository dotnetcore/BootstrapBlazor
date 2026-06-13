---
component: Input
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Input Skill

## Component Purpose

BootstrapInput Component

Primary source directory: `src/BootstrapBlazor/Components/Input`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Input/BootstrapInput.razor`
- `src/BootstrapBlazor/Components/Input/BootstrapInput.razor.cs`
- `src/BootstrapBlazor/Components/Input/BootstrapInput.razor.js`
- `src/BootstrapBlazor/Components/Input/BootstrapInputBase.cs`
- `src/BootstrapBlazor/Components/Input/BootstrapInputEventBase.cs`
- `src/BootstrapBlazor/Components/Input/BootstrapInputGroup.razor`
- `src/BootstrapBlazor/Components/Input/BootstrapInputGroup.razor.cs`
- `src/BootstrapBlazor/Components/Input/BootstrapInputGroupIcon.razor`
- `src/BootstrapBlazor/Components/Input/BootstrapInputGroupIcon.razor.cs`
- `src/BootstrapBlazor/Components/Input/BootstrapInputGroupLabel.cs`
- `src/BootstrapBlazor/Components/Input/BootstrapPassword.cs`
- `src/BootstrapBlazor/Components/Input/FloatingLabel.razor`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Formatter` | `Func<TValue?, string>?` | Callback/event parameter; Gets or sets the formatter function |
| `OnBlurAsync` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the callback method for blur event, default is null |
| `OnClear` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the callback method when clearing text box. Default is null |
| `OnEnterAsync` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the callback method for Enter key press, default is null |
| `OnEscAsync` | `Func<TValue, Task>?` | Callback/event parameter; Gets or sets the callback method for Esc key press, default is null |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Child Content |
| `Alignment` | `Alignment` | Gets or sets Label Alignment. Default null (Start Alignment) |
| `AutoSetDefaultWhenNull` | `bool` | Gets or sets whether to automatically set default value when user deletes. Default is false |
| `ClearIcon` | `string?` | Gets or sets the clear button icon. Default is null |
| `Color` | `Color` | Default: `Color.None`; Gets or sets the button color |
| `Digits` | `int` | Default: `6`; Gets or sets the length of the OTP input. Default is 6 |
| `FormatString` | `string?` | Gets or sets the format string, e.g., "yyyy-MM-dd" for date types |
| `IsAutoFocus` | `bool` | Gets or sets whether to automatically focus, default is false |
| `IsClearable` | `bool` | Gets or sets whether to show clear button. Default is false |
| `IsGroupBox` | `bool` | Gets or sets Whether it is GroupBox style. Default false |
| `IsGroupLabel` | `bool?` | Gets or sets Whether it is a label inside InputGroup or TableToolbar. Default null (Not set) |
| `IsReadonly` | `bool` | Gets or sets whether the OTP input is readonly. Default is false |
| `IsSelectAllTextOnEnter` | `bool` | Gets or sets whether to automatically select all text on Enter key press, default is false |
| `IsSelectAllTextOnFocus` | `bool` | Gets or sets whether to automatically select all text on focus, default is false |
| `IsTrim` | `bool` | Gets or sets whether to automatically trim whitespace, default is false |
| `PlaceHolder` | `string?` | Gets or sets the placeholder attribute value |
| `Readonly` | `bool` | Gets or sets whether readonly. Default is false |
| `ShowRequiredMark` | `bool` | Gets or sets Whether to show required mark. Default false |
| `Type` | `OtpInputType` | Gets or sets the value type of the OTP input. Default is |
| `UseInputEvent` | `bool` | Gets or sets Whether to trigger bind-value:event="oninput" when entering value in text box. Default false |
| `Width` | `int?` | Gets or sets Label Width. Default null (Auto Fit) |

## Events And Callbacks

`Formatter: Func<TValue?, string>?`, `OnBlurAsync: Func<TValue, Task>?`, `OnClear: Func<TValue, Task>?`, `OnEnterAsync: Func<TValue, Task>?`, `OnEscAsync: Func<TValue, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `OnAfterRenderAsync`, `Dispose`, `DisposeAsync`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Inputs.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Inputs.razor.cs`

Sample analysis:

- Direct `<Input>` tag usages detected: 0
- No direct `<Input>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Input.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Input` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
---
component: Stack
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Stack Skill

## Component Purpose

Stack Component

Primary source directory: `src/BootstrapBlazor/Components/Stack`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Stack/Stack.razor`
- `src/BootstrapBlazor/Components/Stack/Stack.razor.cs`
- `src/BootstrapBlazor/Components/Stack/StackItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Content |
| `AlignItems` | `StackAlignItems` | Gets or sets Align Items. Default StackAlignItems.Stretch |
| `AlignSelf` | `StackAlignItems` | Gets or sets Align Self. Default StackAlignItems.Stretch |
| `IsFill` | `bool` | Gets or sets Is Fill. Default false |
| `IsReverse` | `bool` | Gets or sets Is Reverse Layout. Default false |
| `IsRow` | `bool` | Gets or sets Is Row Layout. Default false |
| `IsWrap` | `bool` | Gets or sets Is Wrap. Default false |
| `Justify` | `StackJustifyContent` | Gets or sets Justify Content. Default StackJustifyContent.Start |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

`ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `Dispose`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Stacks.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Stacks.razor.cs`

Sample analysis:

- Direct `<Stack>` tag usages detected: 1
- Observed attributes in official Sample: `AlignItems`, `IsReverse`, `IsRow`, `IsWrap`, `Justify`
Sample-derived snippet:

```razor
<Stack IsRow="@IsRow" Justify="@Justify" AlignItems="@AlignItems" IsWrap="@IsWrap" IsReverse="@IsReverse">
            <StackItem>
                <div class="stack-item-demo">Item 1</div>
            </StackItem>
            <StackItem AlignSelf="@AlignSelf">
                <div class="stack-item-demo">Item 2</div>
            </StackItem>
            <StackItem>
                <div class="stack-item-demo">Item 3</div>
            </StackItem>
        </Stack>
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

1. Read `src/BootstrapBlazor/Components/Stack` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
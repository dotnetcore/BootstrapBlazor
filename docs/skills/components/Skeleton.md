---
component: Skeleton
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Skeleton Skill

## Component Purpose

SkeletonAvatar Component

Primary source directory: `src/BootstrapBlazor/Components/Skeleton`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Skeleton/SkeletonAvatar.razor`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonAvatar.razor.cs`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonBase.cs`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonEditor.razor`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonParagraph.razor`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonTable.razor`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonTable.razor.cs`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonTree.razor`
- `src/BootstrapBlazor/Components/Skeleton/SkeletonTree.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `Active` | `bool` | Default: `true`; Gets or sets Whether active. Default true |
| `Circle` | `bool` | Gets or sets Whether circle. Default false |
| `Columns` | `int` | Default: `3`; Gets or sets Columns. Default 3 |
| `Icon` | `string?` | Gets or sets Loading Icon |
| `Round` | `bool` | Default: `true`; Gets or sets Whether round. Default true |
| `Rows` | `int` | Default: `7`; Gets or sets Rows. Default 7 |
| `ShowToolbar` | `bool` | Default: `true`; Gets or sets Whether to show toolbar |

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Skeletons.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Skeletons.razor.cs`

Sample analysis:

- Direct `<Skeleton>` tag usages detected: 0
- No direct `<Skeleton>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Skeleton.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Skeleton` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
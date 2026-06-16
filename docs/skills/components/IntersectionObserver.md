---
component: IntersectionObserver
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# IntersectionObserver Skill

## Component Purpose

omponent

Primary source directory: `src/BootstrapBlazor/Components/IntersectionObserver`.

Source files reviewed:

- `src/BootstrapBlazor/Components/IntersectionObserver/IntersectionObserver.razor`
- `src/BootstrapBlazor/Components/IntersectionObserver/IntersectionObserver.razor.cs`
- `src/BootstrapBlazor/Components/IntersectionObserver/IntersectionObserver.razor.js`
- `src/BootstrapBlazor/Components/IntersectionObserver/IntersectionObserverEntry.cs`
- `src/BootstrapBlazor/Components/IntersectionObserver/IntersectionObserverItem.razor`
- `src/BootstrapBlazor/Components/IntersectionObserver/IntersectionObserverItem.razor.cs`
- `src/BootstrapBlazor/Components/IntersectionObserver/LoadMore.razor`
- `src/BootstrapBlazor/Components/IntersectionObserver/LoadMore.razor.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnIntersecting` | `Func<IntersectionObserverEntry, Task>?` | Callback/event parameter; Gets or sets callback method |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets omponent |
| `LoadingTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets template Default is null |
| `NoMoreTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets dataisplayemplate Default is?null |
| `AutoUnobserveWhenIntersection` | `bool` | Default: `true`; Gets or sets hether Default is true  |
| `AutoUnobserveWhenNotIntersection` | `bool` | Gets or sets whether Default is false  |
| `CanLoading` | `bool` | Default: `true`; Gets or sets whetherdata Default is?true |
| `NoMoreText` | `string?` | /  ?null ?/para> Gets or sets data Default is?null ?/para> |
| `RootMargin` | `string?` | Margin around the root. Can have values similar to the CSS margin property, e.g. "10px 20px 30px 40px" (top, right, b... |
| `Threshold` | `string` | Default: `"1"`; Gets or sets  ?Default is?1 |
| `UseElementViewport` | `bool` | Default: `true`; Gets or sets whether?Default is?true ?The element that is used as the viewport for checking visibility of the target.... |

## Events And Callbacks

`OnIntersecting: Func<IntersectionObserverEntry, Task>?`

## Templates And Child Content

`ChildContent: RenderFragment?`, `LoadingTemplate: RenderFragment?`, `NoMoreTemplate: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/IntersectionObservers.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/IntersectionObservers.razor.cs`

Sample analysis:

- Direct `<IntersectionObserver>` tag usages detected: 4
- Observed attributes in official Sample: `AutoUnobserveWhenIntersection`, `OnIntersecting`, `Threshold`
Sample-derived snippet:

```razor
<IntersectionObserver OnIntersecting="OnIntersectingAsync">
        <div class="bb-list-main scroll">
            @foreach (var image in _images)
            {
                <IntersectionObserverItem>
                    <div class="bb-list-item">
                        <img src="@image" />
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

1. Read `src/BootstrapBlazor/Components/IntersectionObserver` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
---
component: Carousel
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Carousel Skill

## Component Purpose

Carousel component

Primary source directory: `src/BootstrapBlazor/Components/Carousel`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Carousel/Carousel.razor`
- `src/BootstrapBlazor/Components/Carousel/Carousel.razor.cs`
- `src/BootstrapBlazor/Components/Carousel/Carousel.razor.js`
- `src/BootstrapBlazor/Components/Carousel/CarouselImage.cs`
- `src/BootstrapBlazor/Components/Carousel/CarouselItem.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnClick` | `Func<string, Task>?` | Callback/event parameter; Gets or sets the click callback delegate |
| `OnSlideChanged` | `Func<int, Task>?` | Callback/event parameter; Gets or sets the callback method after slide switch |
| `CaptionTemplate` | `RenderFragment?` | Template parameter; verify context type; Gets or sets the Caption template. Default is null |
| `ChildContent` | `RenderFragment?` | Template parameter; verify context type; Gets or sets child component. Requires |
| `Caption` | `string?` | Gets or sets the Caption text. Default is null. Can be customized by setting |
| `CaptionClass` | `string?` | Gets or sets the Caption style. Default is null |
| `DisableTouchSwiping` | `bool` | Gets or sets whether to disable mobile touch swiping. Default is false |
| `HoverPause` | `bool` | Default: `true`; Gets or sets whether to pause on hover. Default is true |
| `Images` | `IEnumerable<string>` | Default: `[]`; Get Images collection |
| `ImageUrl` | `string?` | Gets or sets the image URL |
| `Interval` | `int` | Default: `5000`; Gets or sets the Slider interval. Default is 5000 |
| `IsFade` | `bool` | Gets or sets whether to use fade effect. Default is false |
| `NextIcon` | `string?` | Gets or sets the next icon |
| `PlayMode` | `CarouselPlayMode` | Gets or sets the auto play mode. Default is |
| `PreviousIcon` | `string?` | Gets or sets the previous icon |
| `ShowControls` | `bool` | Default: `true`; Gets or sets whether to show control buttons. Default is true |
| `ShowIndicators` | `bool` | Default: `true`; Gets or sets whether to show indicators. Default is true |
| `Width` | `string?` | Gets or sets the width of internal images |

## Events And Callbacks

`OnClick: Func<string, Task>?`, `OnSlideChanged: Func<int, Task>?`

## Templates And Child Content

`CaptionTemplate: RenderFragment?`, `ChildContent: RenderFragment?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Carousels.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Carousels.razor.cs`

Sample analysis:

- Direct `<Carousel>` tag usages detected: 11
- Observed attributes in official Sample: `DisableTouchSwiping`, `Images`, `IsFade`, `OnClick`, `ShowControls`, `ShowIndicators`, `Width`
Sample-derived snippet:

```razor
<Carousel Images="@_images" Width="280" ShowControls="false" ShowIndicators="false" />
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

1. Read `src/BootstrapBlazor/Components/Carousel` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
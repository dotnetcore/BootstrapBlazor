---
component: Affix
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Affix Skill

## Component Purpose

`Affix` renders sticky child content with CSS `position: sticky`. It is for keeping a compact block visible inside a scrolling area.

Primary source directory: `src/BootstrapBlazor/Components/Affix`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Affix/Affix.razor`
- `src/BootstrapBlazor/Components/Affix/Affix.razor.cs`

## Usage Scenarios

Use `Affix` for sticky headers, action rows, anchors, or status controls inside scrollable content.

Do not use it as a modal, drawer, overlay, or JS-driven fixed-position container.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ChildContent` | `RenderFragment?` | Required child content. Always provide content. |
| `Offset` | `float` | Pixel offset. Pass a number, not a CSS string such as `16px`. |
| `Position` | `AffixPosition` | Sticky edge. Default source behavior is `AffixPosition.Top`. |
| `ZIndex` | `int?` | Optional z-index. Use only when layering above nearby content is required. |

## Events And Callbacks

No callback/event parameters are declared in the current source.

## Templates And Child Content

`ChildContent` is required and is rendered directly inside the sticky container.

## Cascading Parameters

No `CascadingParameter` properties are declared in the current source.

## Implementation Notes

- The component inherits `BootstrapComponentBase`.
- CSS is built from `AdditionalAttributes`; caller-supplied `class` and `style` are merged.
- The rendered element uses `position: sticky`.
- No JS interop dependency is visible in the current implementation.

## Sample Mapping

Official Sample:

- `src/BootstrapBlazor.Server/Components/Samples/Affixs.razor`

Sample usage shows a default top sticky row and a bottom sticky row with `Position="AffixPosition.Bottom"` and `Offset="10"`.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

Minimal:

```razor
<Affix>
    <div style="line-height: 50px;">Fixed row Affix</div>
</Affix>
```

Bottom sticky action:

```razor
<Affix Position="AffixPosition.Bottom" Offset="10" ZIndex="100">
    <Button Text="Back to top" Color="Color.Secondary" />
</Affix>
```

## Common Mistakes

Wrong:

```razor
<Affix Offset="16px" />
```

Correct:

```razor
<Affix Offset="16">
    <Button Text="Save" Color="Color.Primary" />
</Affix>
```

Do not attach events such as `OnChange`; the current component does not expose event parameters.

## Obsolete Members

No obsolete members were detected in the current source.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Affix` before generating code.
2. Read `src/BootstrapBlazor.Server/Components/Samples/Affixs.razor` when sample behavior matters.
3. Prefer source over Sample, and Sample over this Skill when conflicts exist.
4. Generate only APIs verified in the current repository.

---
component: Searches
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Searches Skill

## Component Purpose

CheckboxList search meta data class

Primary source directory: `src/BootstrapBlazor/Components/Searches`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Searches/CheckboxListSearchMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/DateTimeRangeSearchMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/DateTimeSearchMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/ISearchFormItemMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/MultipleSelectSearchMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/MultipleStringSearchMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/NumberSearchMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/SearchFormItemMetadataBase.cs`
- `src/BootstrapBlazor/Components/Searches/SelectSearchMetadata.cs`
- `src/BootstrapBlazor/Components/Searches/StringSearchMetadata.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

No [Parameter] properties were detected in the current source.

## Events And Callbacks

No callback/event parameters were detected in the current source scan.

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: None detected in current source.
- JS interop or module dependency detected: `False`
- Razor component file detected: `False`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Searches.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Searches.razor.cs`

Sample analysis:

- Direct `<Searches>` tag usages detected: 1
- No direct `<Searches>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.
Sample-derived snippet:

```razor
<Searches> Localizer
@inject IStringLocalizer<Foo> FooLocalizer
@inject IOptions<WebsiteOptions> WebsiteOption

<h3>@Localizer["SearchesTitle"]</h3>

<h4>@Localizer["SearchesSubTitle"]</h4>

<DemoBlock Title="@Localizer["SearchesNormalTitle"]"
           Introduction="@Localizer["SearchesNormalIntro"]"
           Name="Normal">
    <section ignore>@((MarkupString)Localizer["SearchesNormalDescription"].Value)</section>
    <Search IsAutoFocus="true"
            PlaceHolder="@Localizer["SearchesPlaceHolder"]"
            OnSearch="@OnSearch"
            IsSelectAllTextOnFocus="true"></Search>
    <ConsoleLogger @ref="Logger"></ConsoleLogger>
</DemoBlock>

<DemoBlock Title="@Localizer["SearchesDisplayButtonTitle"]"
           Introduction="@Localizer["SearchesDisplayButtonIntro"]"
           Name="DisplayButton">
    <Search PlaceHolder="@Localizer["SearchesPlaceHolder"]"
            ShowClearButton="true"
            OnSearch="@OnDisplaySearch"></Search>
    <ConsoleLogger @ref="DisplayLogger"></ConsoleLogger>
...
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

1. Read `src/BootstrapBlazor/Components/Searches` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
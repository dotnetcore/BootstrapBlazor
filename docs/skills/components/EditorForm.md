---
component: EditorForm
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# EditorForm Skill

## Component Purpose

Editor Form Base Component

Primary source directory: `src/BootstrapBlazor/Components/EditorForm`.

Source files reviewed:

- `src/BootstrapBlazor/Components/EditorForm/EditorForm.razor`
- `src/BootstrapBlazor/Components/EditorForm/EditorForm.razor.cs`
- `src/BootstrapBlazor/Components/EditorForm/EditorItem.cs`
- `src/BootstrapBlazor/Components/EditorForm/IEditorItem.cs`
- `src/BootstrapBlazor/Components/EditorForm/IShowLabel.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `ColumnOrderCallback` | `Func<IEnumerable<ITableColumn>, IEnumerable<ITableColumn>>?` | Callback/event parameter; Gets or sets Custom Column Sort Rule. Default is null, use internal sort mechanism |
| `FieldChanged` | `EventCallback<TValue>` | Callback/event parameter; Gets or sets the bound field value changed callback |
| `FieldExpression` | `Expression<Func<TValue>>?` | Callback/event parameter; Gets or sets the value expression |
| `Buttons` | `RenderFragment?` | Template parameter; verify context type; Gets or sets Buttons Template |
| `EditTemplate` | `RenderFragment<TModel>?` | Template parameter; verify context type; Gets or sets the edit template |
| `FieldItems` | `RenderFragment<TModel>?` | Template parameter; verify context type; Gets or sets the field items collection template. |
| `AutoGenerateAllItem` | `bool` | Default: `true`; Gets or sets Whether to Auto Generate All Items. Default is true |
| `Cols` | `int` | Verify current source before use |
| `ComponentParameters` | `IEnumerable<KeyValuePair<string, object>>?` | Verify current source before use |
| `ComponentType` | `Type?` | Verify current source before use |
| `Editable` | `bool` | Obsolete; do not use; Default: `true` |
| `Field` | `TValue?` | Gets or sets the bound field value |
| `GroupName` | `string?` | Verify current source before use |
| `GroupOrder` | `int` | Verify current source before use |
| `GroupType` | `EditorFormGroupType` | Gets or sets group type. Default is |
| `Ignore` | `bool?` | Verify current source before use |
| `IgnoreItems` | `List<string>?` | Gets or sets the ignore items collection. Default is null |
| `IsDisplay` | `bool` | Gets or sets Whether to Show as Display Component. Default is false |
| `IsFixedSearchWhenSelect` | `bool` | Obsolete; do not use |
| `IsPopover` | `bool` | Verify current source before use |
| `IsRenderWhenValueChanged` | `bool` | Gets or sets Whether to Re-render Component when Value Changed. Default is false |
| `IsShowDisplayTooltip` | `bool` | Gets or sets Whether to Show Display Component Tooltip. Default is false |
| `ItemChangedType` | `ItemChangedType` | Gets or sets Item Changed Type. Add or Update |
| `Items` | `IEnumerable<SelectedItem>?` | Verify current source before use |
| `ItemsPerRow` | `int?` | Gets or sets Items Per Row. Default is null |
| `LabelAlign` | `Alignment` | Gets or sets Label Alignment in Inline mode. Default is None, equivalent to Left |
| `LabelWidth` | `int?` | Gets or sets Label Width. Default is null, use global setting --bb-row-label-width if not set |
| `Lookup` | `IEnumerable<SelectedItem>?` | Verify current source before use |
| `LookupService` | `ILookupService?` | Verify current source before use |
| `LookupServiceData` | `object?` | Verify current source before use |
| `LookupServiceKey` | `string?` | Verify current source before use |
| `LookupStringComparison` | `StringComparison` | Default: `StringComparison.OrdinalIgnoreCase` |
| `Model` | `TModel?` | Gets or sets Model |
| `Order` | `int` | Verify current source before use |
| `PlaceHolder` | `string?` | Verify current source before use |
| `PlaceHolderText` | `string?` | Gets or sets Default Placeholder Text. Default is null |
| `Readonly` | `bool?` | Verify current source before use |
| `Required` | `bool?` | Verify current source before use |
| `RequiredErrorMessage` | `string?` | Verify current source before use |
| `Rows` | `int` | Verify current source before use |

Only the first 40 important parameters are listed. Inspect the current source for the remaining 9 parameters before generating code.

## Events And Callbacks

`ColumnOrderCallback: Func<IEnumerable<ITableColumn>, IEnumerable<ITableColumn>>?`, `FieldChanged: EventCallback<TValue>`, `FieldExpression: Expression<Func<TValue>>?`

## Templates And Child Content

`Buttons: RenderFragment?`, `EditTemplate: RenderFragment<TModel>?`, `FieldItems: RenderFragment<TModel>?`

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnInitialized`, `OnParametersSet`, `Dispose`
- JS interop or module dependency detected: `False`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/EditorForms.razor`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/EditorForms.razor.cs`

Sample analysis:

- Direct `<EditorForm>` tag usages detected: 9
- Observed attributes in official Sample: `AutoGenerateAllItem`, `GroupType`, `IgnoreItems`, `IsDisplay`, `ItemsPerRow`, `LabelAlign`, `LabelWidth`, `Model`, `RowType`, `TModel`
Sample-derived snippet:

```razor
<EditorForm Model="@Model">
            <FieldItems>
                <EditorItem @bind-Field="@context.Education" Readonly="true"></EditorItem>
                <EditorItem @bind-Field="@context.Complete" Readonly="true"></EditorItem>
                <EditorItem @bind-Field="@context.Hobby" Items="@Hobbies" Readonly="true"></EditorItem>
            </FieldItems>
            <Buttons>
                <Button Icon="fa-solid fa-floppy-disk" Text="@Localizer["SubButtonText"]"></Button>
            </Buttons>
        </EditorForm>
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

1. Read `src/BootstrapBlazor/Components/EditorForm` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
---
component: Speech
namespace: BootstrapBlazor.Components
skillVersion: 1
lastUpdated: 2026-06-13
---

# Speech Skill

## Component Purpose

IRecognizerProvider Interface Definition

Primary source directory: `src/BootstrapBlazor/Components/Speech`.

Source files reviewed:

- `src/BootstrapBlazor/Components/Speech/IRecognizerProvider.cs`
- `src/BootstrapBlazor/Components/Speech/ISynthesizerProvider.cs`
- `src/BootstrapBlazor/Components/Speech/RecognizerOption.cs`
- `src/BootstrapBlazor/Components/Speech/RecognizerService.cs`
- `src/BootstrapBlazor/Components/Speech/SpeechWave.razor`
- `src/BootstrapBlazor/Components/Speech/SpeechWave.razor.cs`
- `src/BootstrapBlazor/Components/Speech/SynthesizerOption.cs`
- `src/BootstrapBlazor/Components/Speech/SynthesizerService.cs`

## Usage Scenarios

Use this component or support type only when the current source and official Sample confirm it fits the requested UI behavior.

Do not use it for scenarios that require parameters, events, or services not present in the current source.

## Parameters

| Name | Type | Notes |
| --- | --- | --- |
| `OnTimeout` | `Func<Task>?` | Callback/event parameter; Gets or sets Callback delegate when countdown ends |
| `Show` | `bool` | Gets or sets Whether to show waveform. Default false |
| `ShowUsedTime` | `bool` | Default: `true`; Gets or sets Whether to show used time. Default true |
| `TotalTime` | `int` | Default: `60 * 1000`; Gets or sets Total Time. Default 60000 ms |

## Events And Callbacks

`OnTimeout: Func<Task>?`

## Templates And Child Content

No RenderFragment parameters were detected in the current source scan.

## Cascading Parameters

No CascadingParameter properties were detected in the current source scan.

## Implementation Notes

- Lifecycle methods detected: `OnParametersSetAsync`, `Dispose`
- JS interop or module dependency detected: `True`
- Razor component file detected: `True`
- Keep generated code aligned with the files listed above.

## Sample Mapping

Official Sample mapping:

- `src/BootstrapBlazor.Server/Components/Samples/Speeches`

Sample files reviewed:

- `src/BootstrapBlazor.Server/Components/Samples/Speeches/Index.razor`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/Index.razor.cs`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/Recognizers.razor`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/Recognizers.razor.cs`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/SpeechWaves.razor`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/SpeechWaves.razor.cs`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/Synthesizers.razor`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/Synthesizers.razor.cs`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/WebSpeeches.razor`
- `src/BootstrapBlazor.Server/Components/Samples/Speeches/WebSpeeches.razor.cs`

Sample analysis:

- Direct `<Speech>` tag usages detected: 0
- No direct `<Speech>` tag usage was detected in the matched Sample files; inspect the Sample manually before generating code.

When the Sample and this Skill differ, prefer the current source, then the official Sample, then this Skill.

## Examples

No direct Razor example is generated because this directory does not expose an authoritative `Speech.razor` usage pattern in the current repository.

## Common Mistakes

- Do not invent parameters, events, two-way bindings, or template context types.
- Do not copy examples from older BootstrapBlazor versions.
- Do not use obsolete members.
- If a Sample exists, analyze it before relying on Skill examples.
- If no Sample exists, validate every generated parameter against the current source.

## Obsolete Members

No obsolete members were detected in the current source scan.

## Agent Rules

1. Read `src/BootstrapBlazor/Components/Speech` before generating code.
2. Read the official Sample listed above when it exists.
3. Read this Skill after source and Sample analysis.
4. Prefer source over Sample, and Sample over this Skill when conflicts exist.
5. Generate only APIs verified in the current repository.
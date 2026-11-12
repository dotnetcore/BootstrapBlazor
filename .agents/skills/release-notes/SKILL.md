---
name: release-notes
description: Create, curate, or translate BootstrapBlazor release notes in English and Chinese from GitHub-generated notes or user-provided change data. Use for versioned release-note tasks in this repository.
---

# BootstrapBlazor Release Notes

Create matching English and Chinese release-note drafts for this repository. Do not publish a GitHub Release or create a tag.

## Gather release data

1. Identify the latest published stable release tag from the repository's releases/tags. Do not use prereleases as the baseline unless requested.
2. Get the target version from the user's request; if the request says "current version", check the project version. Use the requested release date; if none is given, use the current date.
3. Compare the target version with the latest stable version. If the target is not newer, stop and tell the user that no release notes are needed until a newer release tag is available; do not generate notes or create/update files. In particular, a project prerelease for a version that is already stable does not count as a newer release.
4. Use GitHub's generated release notes as the default source; the user does not need to provide a separate raw file. This step requires the `gh` CLI, an authenticated GitHub account (`gh auth status`), and network access; cloud or CI agents may not have them. For `dotnetcore/BootstrapBlazor`, generate notes without creating a release:

   ```sh
   gh api --method POST repos/dotnetcore/BootstrapBlazor/releases/generate-notes \
     -f tag_name=vX.Y.Z \
     -f previous_tag_name=vPREVIOUS \
     -f target_commitish=main \
     --jq .body
   ```

   Replace the example tag values with the confirmed target and previous tags. The endpoint returns generated text; it does not publish a release. The GitHub draft page at `https://github.com/dotnetcore/BootstrapBlazor/releases/new` can also generate the same source. If the `gh` CLI, authentication, or GitHub access is unavailable, explain the blocker and use raw change data only if the user provides it.
5. If the user supplies a source file or explicit change list, use that as the scope instead of adding unrelated PRs from elsewhere.

## Curate the changes

- Keep the note concise and user-facing. Organize `fix` entries under `Fixes` and `feat` entries under `Features`; the Chinese headings are `修复` and `新功能`.
- Exclude documentation-only and internal maintenance changes from these sections unless the user asks to include them.
- Do not silently drop a change because a later revert or follow-up appears. Inspect the related entries and ask the user when the final shipped behavior is unclear.
- When one component or dependency was upgraded several times during the release range, normally report the final version. Keep PR references that support the summarized change; follow the user's manually curated English version when it differs.
- Do not invent behavior or implementation details beyond the generated notes, linked PR information, or user-confirmed edits.

## Write the two files

- Save versioned notes as `ChangeLogs/{version}_en.md` and `ChangeLogs/{version}_zh.md` at the repository root. Create `ChangeLogs/` if it does not exist.
- Preserve existing files and manually edited content. In particular, do not overwrite an existing English note without explicit instruction.
- Draft English first. Translate the final, user-confirmed English note into Chinese; do not reconstruct the Chinese version independently from older raw data.
- Keep dates, versions, section and entry order, authors, PR links, and Full Changelog links aligned in both files.
- Translate only prose. Preserve component/scope names, commit prefixes, API and parameter identifiers, package names, version numbers, usernames, and URLs.
- Use this entry format:

  ```markdown
  * fix(Component): concise description by @author in https://github.com/dotnetcore/BootstrapBlazor/pull/123
  * feat(Component): concise description by @author in https://github.com/dotnetcore/BootstrapBlazor/pull/124
  ```

- Use this document structure:

  ```markdown
  ## Release YYYY-MM-DD VX.Y.Z

  ### Fixes
  ...

  ### Features
  ...

  **Full Changelog**: https://github.com/dotnetcore/BootstrapBlazor/compare/vPREVIOUS...vCURRENT
  ```

  In the Chinese file, translate the section headings and descriptions while retaining the same structure and identifiers.

## Final review

- Confirm the title date, version, previous tag, and Full Changelog comparison.
- Compare both files for matching PR IDs, authors, entry counts, and ordering.
- Ensure no unrequested PRs were added and no user edits were lost.
- Report the paths of both files and any unresolved inclusion or release-range decisions.

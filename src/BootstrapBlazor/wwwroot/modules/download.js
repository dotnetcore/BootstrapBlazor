export async function downloadFileFromStream(fileName, contentStreamReference) {
    const arrayBuffer = await contentStreamReference.arrayBuffer()
    const blob = new Blob([arrayBuffer])
    const url = URL.createObjectURL(blob)
    const anchorElement = document.createElement('a')
    anchorElement.href = url
    if (fileName == null) fileName = ""
    anchorElement.download = fileName
    anchorElement.click()
    anchorElement.remove()
    URL.revokeObjectURL(url)
}

export function downloadFileFromUrl(fileName, url) {
    const anchorElement = document.createElement('a')
    anchorElement.href = url
    if (fileName == null) fileName = ""
    anchorElement.download = fileName
    anchorElement.click()
    anchorElement.remove()
}

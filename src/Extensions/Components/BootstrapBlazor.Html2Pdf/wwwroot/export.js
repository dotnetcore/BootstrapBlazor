import './html2pdf.bundle.min.js'

export function exportPdf(html, fileName) {
    const element = document.createElement("div")
    element.innerHTML = html
    html2pdf().from(element).save(fileName)
    return true
}

export function exportPdfById(id, fileName) {
    let ret = false
    const element = document.getElementById(id)
    if (element) {
        html2pdf().from(element).save(fileName)
        ret = true
    }
    return ret
}

export function exportPdfByElement(el, fileName) {
    let ret = false
    if (el) {
        html2pdf().from(el).save(fileName)
        ret = true
    }
    return ret
}

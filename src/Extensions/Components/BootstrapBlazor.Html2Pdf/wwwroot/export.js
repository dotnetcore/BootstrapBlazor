import './html2pdf.bundle.min.js'

export function exportPdf(html, fileName) {
    const opt = getDefaultOption()

    const element = document.createElement("div")
    element.innerHTML = html
    html2pdf(element, opt)
    return true
}

export function exportPdfAsBase64(html) {
    const opt = getDefaultOption()

    const element = document.createElement("div")
    element.innerHTML = html

    return new Promise((resolve) => {
        html2pdf().set(opt).from(element).output('datauristring').then(pdfBase64 => {
            resolve(pdfBase64)
        })
    })
}

const getDefaultOption = () => {
    return {
        ...{
            image: { type: 'jpeg', quality: 0.95 },
            pagebreak: { mode: ['avoid-all', 'css', 'legacy'] },
            html2canvas: {
                scale: 2,
                useCORS: true,
            },
            jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' },
        }
    }
}

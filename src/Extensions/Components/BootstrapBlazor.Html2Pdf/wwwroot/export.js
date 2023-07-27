import './html2pdf.bundle.min.js'

export function exportPdf(html, fileName) {
    const opt = getDefaultOption()

    const element = document.createElement("div")
    element.innerHTML = html
    html2pdf(element, opt)
    return true
}

export async function exportPdfAsBase64(html) {
    const opt = getDefaultOption()

    const element = document.createElement("div")
    element.innerHTML = html

    const payload = await html2pdf().set(opt).from(element).outputPdf()
    return btoa(payload)
}

export async function exportPdfById(id) {
    let ret = null
    const opt = getDefaultOption()

    const element = document.getElementById(id)
    if (element) {
        const payload = await html2pdf().set(opt).from(element).outputPdf()
        ret = btoa(payload)
    }
    return ret
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

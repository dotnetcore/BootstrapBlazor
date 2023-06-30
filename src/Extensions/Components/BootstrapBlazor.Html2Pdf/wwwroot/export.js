import './html2pdf.bundle.min.js'

export function exportPdf(html, fileName) {
    const opt = {
        ...{
            image: { type: 'jpeg', quality: 0.95 },
            pagebreak: { mode: ['avoid-all', 'css', 'legacy'] },
            html2canvas: {
                scale: 2,
                useCORS: true,
            },
            jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' },
        },
        ...{ filename: fileName }
    };
    const element = document.createElement("div")
    element.innerHTML = html
    html2pdf(element, opt)
    return true
}

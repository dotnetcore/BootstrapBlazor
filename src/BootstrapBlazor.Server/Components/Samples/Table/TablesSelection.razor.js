export function scroll(id) {
    const element = document.getElementById(id);
    if (element) {
        const selectedRow = element.querySelector('tr.active');
        if (selectedRow) {
            selectedRow.scrollIntoView({ behavior: 'smooth', block: 'center' });
        }
    }
}

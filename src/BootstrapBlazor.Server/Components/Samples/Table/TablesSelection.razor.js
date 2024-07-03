export function scroll(id) {
    const element = document.getElementById(id);
    if (element) {
        const selectedRow = element.querySelector('.form-check.is-checked');
        if (selectedRow) {
            const row = selectedRow.closest('tr');
            if (row) {
                row.scrollIntoView({ behavior: 'smooth' });
            }
        }
    }
}

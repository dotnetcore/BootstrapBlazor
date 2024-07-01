import Data from "../../modules/data.js"

export function hide(id) {
    const el = document.getElementById(id);
    const key = el.parentElement.getAttribute('id');
    if (key) {
        const confirm = Data.get(key);
        confirm.hide();
    }
}

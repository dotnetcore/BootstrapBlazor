import { getAutoThemeValue, setTheme } from "./utility.js"

const getStoredTheme = () => localStorage.getItem('theme')
const setStoredTheme = theme => localStorage.setItem('theme', theme)

export function getPreferredTheme() {
    const storedTheme = getStoredTheme()
    if (storedTheme) {
        return storedTheme
    }

    return getAutoThemeValue();
}

export function saveTheme(theme) {
    setTheme(theme);
    setStoredTheme(theme);
}

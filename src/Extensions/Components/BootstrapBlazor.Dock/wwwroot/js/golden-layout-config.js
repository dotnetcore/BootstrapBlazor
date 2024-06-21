const saveConfig = (layout, option) => {
    option = {
        enableLocalStorage: false,
        ...option
    }
    if (option.enableLocalStorage) {
        removeConfig(option)
        localStorage.setItem(option.localStorageKey, JSON.stringify(layout.saveLayout()));
    }
}

const removeConfig = option => {
    for (let index = localStorage.length; index > 0; index--) {
        const k = localStorage.key(index - 1);
        if (indexOfKey(k, option)) {
            localStorage.removeItem(k);
        }
    }
}

const indexOfKey = (key, option) => {
    return key.indexOf(`${option.prefix}-`) > -1
}

export { saveConfig, removeConfig }

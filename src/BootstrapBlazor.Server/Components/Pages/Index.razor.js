import Data from '../../_content/BootstrapBlazor/modules/data.js'

export function init(id, text1, text2, text3) {
    const el = document.getElementById(id)
    const index = {
        element: el, text1, text2, text3,
        cursorElement: el.nextElementSibling,
        type: () => {
            const typeChar = (original, reverse) => {
                const plant = original.concat()
                return new Promise((resolver, reject) => {
                    index.cursorElement.classList.add('active')
                    index.eventHandler = setInterval(() => {
                        if (plant.length > 0) {
                            if (!reverse) {
                                index.element.textContent = index.element.textContent + plant.shift()
                            }
                            else {
                                plant.pop()
                                index.element.textContent = plant.join('')
                            }
                        }
                        else {
                            clearInterval(index.eventHandler)
                            index.eventHandler = false
                            index.cursorElement.classList.remove('active')

                            index.typeHandler = window.setTimeout(() => {
                                window.clearTimeout(index.typeHandler)
                                index.typeHandler = false
                                if (reverse) {
                                    return resolver()
                                } else {
                                    typeChar(original, true).then(() => {
                                        return resolver()
                                    })
                                }
                            }, 1000)
                        }
                    }, 200)
                })
            }

            const loop = () => {
                index.handler = setTimeout(() => {
                    clearTimeout(index.handler)
                    index.handler = false
                    typeChar(text1, false).then(() => {
                        typeChar(text2, false).then(() => {
                            typeChar(text3).then(() => {
                                loop()
                            })
                        })
                    })
                }, 200)
            }

            loop()
        }
    }

    index.type()
    Data.set(id, index)
}

export function dispose(id) {
    const index = Data.get(id)
    Data.remove(id)

    if (index.handler) {
        clearTimeout(index.handler)
    }
    if (index.eventHandler) {
        clearInterval(index.eventHandler)
    }
    if (index.typeHandler) {
        clearTimeout(index.typeHandler)
    }
}

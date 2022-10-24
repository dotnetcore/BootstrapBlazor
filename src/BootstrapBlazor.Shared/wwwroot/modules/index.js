import BlazorComponent from "../../../_content/BootstrapBlazor/modules/base/blazor-component.js";

export class Index extends BlazorComponent {
    _init() {
        this._cursorElement = this._element.nextElementSibling
        this._type(this._config.arguments[0], this._config.arguments[1], this._config.arguments[2])
    }

    _type(text1, text2, text3) {
        const typeChar = (original, reverse) => {
            const plant = original.concat()
            return new Promise((resolver, reject) => {
                this._cursorElement.classList.add('active')
                this._eventHandler = window.setInterval(() => {
                    if (plant.length > 0) {
                        if (!reverse) {
                            this._element.textContent = this._element.textContent + plant.shift()
                        } else {
                            plant.pop()
                            this._element.textContent = plant.join('')
                        }
                    } else {
                        window.clearInterval(this._eventHandler)
                        this._eventHandler = false
                        this._cursorElement.classList.remove('active')

                        this._typeHandler = window.setTimeout(() => {
                            window.clearTimeout(this._typeHandler);
                            this._typeHandler = false
                            if (reverse) {
                                return resolver();
                            } else {
                                typeChar(original, true).then(() => {
                                    return resolver();
                                });
                            }
                        }, 1000);
                    }
                }, 200);
            });
        };

        const loop = () => {
            this._handler = window.setTimeout(() => {
                window.clearTimeout(this._handler)
                this._handler = false
                typeChar(text1, false).then(() => {
                    typeChar(text2, false).then(() => {
                        typeChar(text3).then(() => {
                            loop()
                        });
                    });
                });
            }, 200)
        }

        loop()
    }

    _dispose() {
        if (this._handler) {
            window.clearTimeout(this._handler)
        }
        if (this._eventHandler) {
            window.clearInterval(this._eventHandler)
        }
        if (this._typeHandler) {
            window.clearTimeout(this._typeHandler)
        }
    }
}

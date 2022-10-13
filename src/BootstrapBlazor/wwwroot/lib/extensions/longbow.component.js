(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined'
        ? module.exports = factory()
        : typeof define === 'function' && define.amd
            ? define(factory)
            : (global = typeof globalThis !== 'undefined'
                ? globalThis
                : global || self, global.bb = factory());
})(this, function () {

    class Config {
        static get Default() {
            return {};
        }

        static get DefaultType() {
            return {};
        }

        static get NAME() {
            throw new Error('You have to implement the static method "NAME", for each component!');
        }

        dispose() {
            for (const propertyName of Object.getOwnPropertyNames(this)) {
                this[propertyName] = null;
            }
        }
    }

    const VERSION = '1.0.0';

    class BaseComponent extends Config {
        constructor(element, config = {}) {
            super();

            if (!element) {
                return;
            }

            this._element = element;
            this._config = this._getConfig(config);
            Data.set(this._element, this.constructor.DATA_KEY, this);
        }

        _getConfig(config) {
            config = this._mergeConfigObj(config, this._element);
            config = this._configAfterMerge(config);

            this._typeCheckConfig(config);

            return config;
        }

        _configAfterMerge(config) {
            return config;
        }

        _mergeConfigObj(config, element) {
            const jsonConfig = isElement$1(element) ? bootstrap.Manipulator.getDataAttribute(element, 'config') : {}; // try to parse
            const dataConfig = isElement$1(element) ? bootstrap.Manipulator.getDataAttributes(element) : {};
            config = typeof config === 'object' ? config : {}
            return {
                ...this.constructor.Default,
                ...(typeof jsonConfig === 'object' ? jsonConfig : {}),
                ...dataConfig,
                ...config
            };
        }

        _typeCheckConfig(config, configTypes = this.constructor.DefaultType) {
            for (const property of Object.keys(configTypes)) {
                const expectedTypes = configTypes[property];
                const value = config[property];
                const valueType = isElement$1(value) ? 'element' : toType(value);

                if (!new RegExp(expectedTypes).test(valueType)) {
                    throw new TypeError(`${this.constructor.NAME.toUpperCase()}: Option "${property}" provided type "${valueType}" but expected type "${expectedTypes}".`);
                }
            }
        }

        dispose() {
            Data.remove(this._element, this.constructor.DATA_KEY);

            this.constructor.EVENT_KEY.forEach(key => {
                bootstrap.EventHandler.off(this._element, key);
            });

            super.dispose();
        }

        static getInstance(element) {
            return Data.get(getElement(element), this.DATA_KEY);
        }

        static getOrCreateInstance(element, config = {}) {
            return this.getInstance(element) || new this(element, typeof config === 'object' ? config : null);
        }

        static get VERSION() {
            return VERSION;
        }

        static get EVENT_KEY() {
            return [];
        }

        static get DATA_KEY() {
            return `bb.${this.NAME}`;
        }
    }

    // bootstrap.EventHandler.on(document, 'click', function (e) {
    //     const el = e.target;
    //     if (el.closest(`.${Confirm.Default.class}.show`) === null && el.closest(`${Confirm.Default.confirm_container}`) === null) {
    //         const owner = Utility.getDescribedElement(el.closest('.dropdown-toggle'));
    //         const popoverSelector = `.${Confirm.Default.class}.show`;
    //         document.querySelectorAll(popoverSelector).forEach(function (ele) {
    //             if (ele !== owner) {
    //                 const element = Utility.getDescribedOwner(ele);
    //                 if (element) {
    //                     const p = bootstrap.Popover.getInstance(element);
    //                     if (p) {
    //                         p.hide();
    //                     }
    //                 }
    //             }
    //         });
    //     }
    // });
    //
    // bootstrap.EventHandler.on(document, 'click', '.anchor-link', function (e) {
    //     const hash = this.getAttribute('id');
    //     if (hash) {
    //         const title = this.getAttribute('data-bb-title');
    //         const href = window.location.origin + window.location.pathname + '#' + hash;
    //         Utility.copy(href);
    //         const tooltip = bootstrap.Tooltip.getOrCreateInstance(this, {
    //             title: title
    //         });
    //         tooltip.show();
    //         const handler = window.setTimeout(function () {
    //             window.clearTimeout(handler);
    //             tooltip.dispose();
    //         }, 1000);
    //     }
    // });
    //
    // bootstrap.EventHandler.on(document, 'click', '[data-toggle="anchor"]', function (e) {
    //     e.preventDefault();
    //     const target = Utility.getDescribedElement(this, 'data-bb-target');
    //     if (target) {
    //         const container = Utility.getDescribedElement(this, 'data-bb-container') || document.defaultView;
    //         const rect = target.getBoundingClientRect();
    //         let margin = rect.top;
    //         let marginTop = getComputedStyle(target).getPropertyValue('margin-top').replace('px', '');
    //         if (marginTop) {
    //             margin = margin - parseInt(marginTop);
    //         }
    //         let offset = this.getAttribute('data-bb-offset');
    //         if (offset) {
    //             margin = margin - parseInt(offset);
    //         }
    //         const winScroll = Utility.getWindowScroll(container);
    //         container.scrollTo(0, margin + winScroll.scrollTop);
    //     }
    //
    // });

    /* Carousel */
    const NAME$Carousel = "Carousel"

    class Carousel extends BaseComponent {
        constructor(element, config = {}) {
            super(element, config);

            this._carousel = bootstrap.Carousel.getOrCreateInstance(this._element, this._config);
            this._addEventListeners();
        }

        _addEventListeners() {
            bootstrap.EventHandler.on(this._element, 'mouseenter', () => {
                const bars = this._element.querySelectorAll('[data-bs-slide]');
                bars.forEach(slide => {
                    slide.classList.remove('d-none');
                });
                this._enterHandler = window.setTimeout(() => {
                    window.clearTimeout(this._enterHandler);
                    this._enterHandler = null;
                    this._element.classList.add('hover');
                }, 10);
            });
            bootstrap.EventHandler.on(this._element, 'mouseleave', () => {
                const bars = this._element.querySelectorAll('[data-bs-slide]');
                bars.forEach(slide => {
                    slide.classList.add('d-none');
                });
                this._leaveHandler = window.setTimeout(() => {
                    window.clearTimeout(this._leaveHandler);
                    this._leaveHandler = null;
                    this._element.classList.remove('hover');
                }, 10);
            });
        }

        dispose() {
            if (this._carousel !== null) {
                this._carousel.dispose();
            }

            if (this._enterHandler !== null) {
                window.clearTimeout(this._enterHandler);
            }
            if (this._leaveHandler !== null) {
                window.clearTimeout(this._leaveHandler);
            }

            super.dispose();
        }

        static init(element) {
            element = getElement(element);
            new Carousel(element);
        }

        static dispose(element) {
            element = getElement(element);
            if (element) {
                const p = this.getInstance(element);
                if (p) {
                    p.dispose();
                }
            }
        }

        static get NAME() {
            return NAME$Carousel;
        }
    }

    class AutoRedirect extends Config {
        constructor(config = {}) {
            super();

            this._config = config;
            this._mousePosition = {};
            this._count = 1;

            this._fnMouseHandler = e => {
                if (this._mousePosition.screenX !== e.screenX || this._mousePosition.screenY !== e.screenY) {
                    this._mousePosition.screenX = e.screenX;
                    this._mousePosition.screenY = e.screenY;
                    this._count = 1;
                }
            }

            this._fnKeyHandler = () => {
                this._count = 1;
            }

            bootstrap.EventHandler.on(document, 'mousemove', this._fnMouseHandler);
            bootstrap.EventHandler.on(document, 'keydown', this._fnKeyHandler)

            this._lockHandler = window.setInterval(() => {
                if (this._count++ > this._config.interval) {
                    window.clearInterval(this._lockHandler);
                    this._config.dotnetInvoker.invokeMethodAsync(this._config.method);

                    this.dispose();
                }
            }, 1000);
        }

        dispose() {
            bootstrap.EventHandler.off(document, 'mousemove', this._fnMouseHandler);
            bootstrap.EventHandler.off(document, 'keydown', this._fnKeyHandler)

            if (this._lockHandler) {
                window.clearInterval(this._lockHandler);
            }
            delete window.AutoRedirect;
            super.dispose();
        }

        static init(dotnetInvoker, interval, method) {
            if (typeof window.AutoRedirect === 'undefined') {
                window.AutoRedirect = new AutoRedirect({dotnetInvoker, interval: interval / 1000, method});
            }
        }

        static dispose() {
            if (window.AutoRedirect) {
                window.AutoRedirect.dispose();
            }
        }
    }

    class Utility {
        static vibrate() {
            if ('vibrate' in window.navigator) {
                window.navigator.vibrate([200, 100, 200]);
                const handler = window.setTimeout(function () {
                    window.clearTimeout(handler);
                    window.navigator.vibrate([]);
                }, 1000);
            }
        }

        static copy(text = '') {
            if (navigator.clipboard) {
                navigator.clipboard.writeText(text);
            } else {
                const input = document.createElement('input');
                input.setAttribute('type', 'text');
                input.setAttribute('value', text);
                input.setAttribute('hidden', 'true');
                document.body.appendChild(input);
                input.select();
                document.execCommand('copy');
                document.body.removeChild(input);
            }
        }

        static getWindowScroll(node) {
            const win = this.getWindow(node);
            const scrollLeft = win.pageXOffset;
            const scrollTop = win.pageYOffset;
            return {
                scrollLeft: scrollLeft,
                scrollTop: scrollTop
            };
        }

        static getWindow(node) {
            if (node == null) {
                return window;
            }

            if (node.toString() !== '[object Window]') {
                const ownerDocument = node.ownerDocument;
                return ownerDocument ? ownerDocument.defaultView || window : window;
            }

            return node;
        }

        static getDescribedElement(element, selector = 'aria-describedby') {
            if (isElement$1(element)) {
                const id = element.getAttribute(selector);
                if (id) {
                    return document.querySelector(`#${id}`);
                }
            }
            return null;
        }

        static getDescribedOwner(element, selector = 'aria-describedby') {
            if (isElement$1(element)) {
                const id = element.getAttribute('id');
                if (id) {
                    return document.querySelector(`[${selector}="${id}"]`);
                }
            }
            return null;
        }

        static getTargetElement(element, selector = 'data-bs-target') {
            if (isElement$1(element)) {
                const id = element.getAttribute(selector);
                if (id) {
                    return document.querySelector(id);
                }
            }
            return null;
        }
    }

    const MILLISECONDS_MULTIPLIER = 1000;

    const getTransitionDurationFromElement = (element, delay = 0) => {
        if (!element) {
            return 0;
        } // Get transition-duration of the element


        let {
            transitionDuration,
            transitionDelay
        } = window.getComputedStyle(element);
        const floatTransitionDuration = Number.parseFloat(transitionDuration);
        const floatTransitionDelay = Number.parseFloat(transitionDelay); // Return 0 if element or transition duration is not found

        if (!floatTransitionDuration && !floatTransitionDelay) {
            return 0;
        } // If multiple durations are defined, take the first


        transitionDuration = transitionDuration.split(',')[0];
        transitionDelay = transitionDelay.split(',')[0];
        return (Number.parseFloat(transitionDuration) + Number.parseFloat(transitionDelay)) * MILLISECONDS_MULTIPLIER + delay;
    };

    const isDisabled = element => {
        if (!element || element.nodeType !== Node.ELEMENT_NODE) {
            return true;
        }

        if (element.classList.contains('disabled')) {
            return true;
        }

        if (typeof element.disabled !== 'undefined') {
            return element.disabled;
        }

        return element.hasAttribute('disabled') && element.getAttribute('disabled') !== 'false';
    };

    const toType = object => {
        if (object === null || object === undefined) {
            return `${object}`;
        }

        return Object.prototype.toString.call(object).match(/\s([a-z]+)/i)[1].toLowerCase();
    };

    const isElement$1 = object => {
        if (!object || typeof object !== 'object') {
            return false;
        }

        if (typeof object.jquery !== 'undefined') {
            object = object[0];
        }

        return typeof object.nodeType !== 'undefined';
    };

    const getElement = object => {
        if (isElement$1(object)) {
            return object.jquery ? object[0] : object;
        }

        if (typeof object === 'string' && object.length > 0) {
            return document.querySelector(object);
        }
        return null;
    };

    const elementMap = new Map();
    const Data = {
        set(element, key, instance) {
            if (!elementMap.has(element)) {
                elementMap.set(element, new Map());
            }

            const instanceMap = elementMap.get(element); // make it clear we only want one instance per element
            // can be removed later when multiple key/instances are fine to be used

            if (!instanceMap.has(key) && instanceMap.size !== 0) {
                // eslint-disable-next-line no-console
                console.error(`Bootstrap doesn't allow more than one instance per element. Bound instance: ${Array.from(instanceMap.keys())[0]}.`);
                return;
            }

            instanceMap.set(key, instance);
        },

        get(element, key) {
            if (elementMap.has(element)) {
                return elementMap.get(element).get(key) || null;
            }

            return null;
        },

        remove(element, key) {
            if (!elementMap.has(element)) {
                return;
            }

            const instanceMap = elementMap.get(element);
            instanceMap.delete(key); // free up element references if there are no instances left for an element

            if (instanceMap.size === 0) {
                elementMap.delete(element);
            }
        }
    };

    return {
        AutoRedirect,
        Carousel,
        Utility
    };
});

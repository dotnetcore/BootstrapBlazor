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
    }

    const VERSION = '1.0.0';

    class BaseComponent extends Config {
        constructor(element, config) {
            super();

            if (!element) {
                return;
            }

            this._element = element;
            this._config = this._getConfig(config);
            Data.set(this._element, this.constructor.DATA_KEY, this);
        }

        dispose() {
            Data.remove(this._element, this.constructor.DATA_KEY);
            //EventHandler.off(this._element, this.constructor.EVENT_KEY);

            for (const propertyName of Object.getOwnPropertyNames(this)) {
                this[propertyName] = null;
            }
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

        static get DATA_KEY() {
            return `bb.${this.NAME}`;
        }
    }

    const NAME$Popover = 'Popover';

    class Popover extends BaseComponent {
        constructor(element, config) {
            super(element, config);

            this._popover = bootstrap.Popover.getOrCreateInstance(element, config);
            this._hackPopover();
            this._setListeners();
        }

        _hackPopover() {
            this._popover._isWithContent = () => true;
            let getTipElement = this._popover._getTipElement;
            let fn = tip => {
                if (this._config.css !== null) {
                    tip.classList.add(this._config.css);
                }
            }
            this._popover._getTipElement = function () {
                let tip = getTipElement.call(this);
                tip.classList.add('popover-dropdown');
                tip.classList.add('shadow');
                fn(tip);
                return tip;
            }
        }

        _setListeners() {
            let hasDisplayNone = false;

            this._show = () => {
                let disabled = this._config.showCallback.call(this._element);
                if (!disabled) {
                    this._element.setAttribute('aria-expanded', 'true');
                    this._element.classList.add('show');
                }
                if (disabled) {
                    event.preventDefault();
                }
            };

            this._inserted = () => {
                let pId = this._element.getAttribute('aria-describedby');
                if (pId) {
                    let pop = document.getElementById(pId);
                    let body = pop.querySelector('.popover-body');
                    if (!body) {
                        body = document.createElement('div');
                        body.classList.add('popover-body');
                        pop.append(body);
                    }
                    body.classList.add('show');
                    let content = this._config.bodyElement;
                    if (content.classList.contains("d-none")) {
                        hasDisplayNone = true;
                        content.classList.remove("d-none");
                    }
                    body.append(content);
                }
            };

            this._hide = () => {
                let pId = this._element.getAttribute('aria-describedby');
                if (pId) {
                    let content = this._config.bodyElement;
                    if (hasDisplayNone) {
                        content.classList.add("d-none");
                    }
                    this._element.append(content);
                }
                this._element.classList.remove('show');
            }

            bootstrap.EventHandler.on(this._element, 'show.bs.popover', this._show);
            bootstrap.EventHandler.on(this._element, 'inserted.bs.popover', this._inserted);
            bootstrap.EventHandler.on(this._element, 'hide.bs.popover', this._hide);

            if (this._config.dismiss != null) {
                bootstrap.EventHandler.on(document, 'click', this._config.dismiss, this._hide);
            }
        }

        _getConfig(config) {
            let fn = function () {
                let disabled = this.classList.contains('disabled');
                if (!disabled && this.parentNode != null) {
                    disabled = this.parentNode.classList.contains('disabled');
                }
                if (!disabled) {
                    let el = this.querySelector('.form-control');
                    if (el != null) {
                        disabled = el.classList.contains('disabled');
                        if (!disabled) {
                            disabled = el.getAttribute('disabled') === 'disabled';
                        }
                    }
                }
                return disabled;
            };
            config = {
                ...{
                    placement: this._element.getAttribute('bs-data-placement') || 'auto',
                    showCallback: fn
                },
                ...config
            };
            config = super._getConfig(config);
            if (!isElement$1(config.bodyElement)) {
                config.bodyElement = this._element.parentNode.querySelector(config.dropdown);
            }
            return config;
        }

        invoke(method) {
            let fn = this._popover[method];
            if (typeof fn === 'function') {
                fn.call(this._popover);
            }
            if (method === 'dispose') {
                this._popover = null;
                this.dispose();
            }
        }

        dispose() {
            if (this._popover !== null) {
                this._popover.dispose();
            }
            super.dispose();
        }

        static invoke(el, method) {
            el = getElement(el);
            if (!el.classList.contains('dropdown-toggle')) {
                el = el.querySelector('.dropdown-toggle');
            }
            let p = this.getInstance(el);
            if (p !== null) {
                p.invoke(method);
            }
        }

        static get Default() {
            return {
                ...{
                    dropdown: '.dropdown-menu'
                },
                ...super.Default
            };
        }

        static get NAME() {
            return NAME$Popover;
        }
    }

    bootstrap.EventHandler.on(document, 'click', '.dropdown-toggle', function (e) {
        let el = e.target;
        if (!el.classList.contains('dropdown-toggle')) {
            el = el.closest('.dropdown-toggle');
        }
        let isBootstrapDrop = el.getAttribute('data-bs-toggle') === 'dropdown';
        if (!isBootstrapDrop) {
            let p = bb.Popover.getInstance(el);
            if (p == null) {
                p = new bb.Popover(el);
                p.invoke('show');
                e.stopPropagation();
            }
        }
    });

    bootstrap.EventHandler.on(document, 'click', function (e) {
        let el = e.target;
        if (el.closest('.popover-dropdown.show') === null) {
            document.querySelectorAll('.popover-dropdown.show').forEach(function (ele) {
                let pId = ele.getAttribute('id');
                if (pId) {
                    let popover = document.querySelector('[aria-describedby="' + pId + '"]');
                    let p = bootstrap.Popover.getInstance(popover);
                    if (p !== null) {
                        p.hide();
                    }
                }
            });
        }
    });

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
        Popover
    };
});

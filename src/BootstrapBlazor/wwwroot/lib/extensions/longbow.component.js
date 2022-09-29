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

    /* Tooltip */
    const NAME$Tooltip = "Tooltip"

    class Tooltip extends BaseComponent {
        constructor(element, config) {
            super(element, config);

            this._getOrCreateInstance()
            this._setListeners();
        }

        _configAfterMerge(config) {
            let css = this._element.classList.contains(config.invalidClass)
                ? config.invalidClass
                : this._element.getAttribute('data-bs-customclass');
            config = {
                ...{
                    customClass: css || ''
                },
                ...super._configAfterMerge(config)
            };
            return config;
        }

        _getOrCreateInstance() {
            this._tooltip = bootstrap.Tooltip.getOrCreateInstance(this._element, this._config);
        }

        _setListeners() {
            this._inserted = () => {
                const tip = Utility.getDescribedElement(this._element);
                if (tip !== null) {
                    tip.classList.add(this._config.invalidClass);
                }
            };

            bootstrap.EventHandler.on(this._element, 'inserted.bs.tooltip', `.${this._config.invalidClass}`, this._inserted);
        }

        show() {
            if (!this._isShown()) {
                this._tooltip.show();
            }
        }

        hide() {
            if (this._isShown()) {
                this._tooltip.hide();
            }
        }

        dispose() {
            if (this._tooltip.tip !== null) {
                this._tooltip.dispose();
            }
            super.dispose();
        }

        _isShown() {
            return this._tooltip === null ? false : this._tooltip._isShown();
        }

        static _create(element, config) {
            new bb.Tooltip(element, config);
        }

        static init(element, title) {
            element = getElement(element);
            if (element) {
                const config = {
                    title: title
                }
                const p = this.getInstance(element);
                if (p !== null && p._isShown()) {
                    p.hide();

                    const duration = getTransitionDurationFromElement(element, 15);
                    const handler = window.setTimeout(() => {
                        window.clearTimeout(handler);
                        p.dispose();

                        this._create(element, config);
                    }, duration);
                } else {
                    this._create(element, config);
                }

                // check the elment is the first child of form
                const form = element.closest('form');
                if (form) {
                    const el = form.querySelector(`.${this.Default.invalidClass}`);
                    if (element === el) {
                        element.focus();
                    }
                }
            }
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

        static get Default() {
            return {
                invalidClass: 'is-invalid'
            }
        }

        static get NAME() {
            return NAME$Tooltip;
        }
    }

    class Popover extends Tooltip {
        _getOrCreateInstance() {
            this._tooltip = bootstrap.Popover.getOrCreateInstance(this._element, this._config);
        }

        _setListeners() {
            this._inserted = () => {
                const tip = Utility.getDescribedElement(this._element);
                if (tip !== null) {
                    tip.classList.add('is-invalid');
                }
            };

            bootstrap.EventHandler.on(this._element, 'inserted.bs.popover', '.is-invalid', this._inserted);
        }

        static _create(element, config) {
            new bb.Popover(element, config);
        }

        static init(element, title, content) {
            element = getElement(element);
            if (element) {
                const config = {
                    title: title,
                    content: content
                }
                const p = bb.Popover.getInstance(element);
                if (p) {
                    p.hide();

                    const duration = getTransitionDurationFromElement(element, 15);
                    const handler = window.setTimeout(() => {
                        window.clearTimeout(handler);
                        p.dispose();

                        Popover._create(element, config);
                    }, duration);
                } else {
                    Popover._create(element, config);
                }
            }
        }
    }

    class DropdownBase extends Tooltip {
        constructor(element, config) {
            super(element, config);

            this._hackPopover();
        }

        _getOrCreateInstance() {
            this._dropdown = bootstrap.Popover.getOrCreateInstance(this._element, this._config);
        }

        _isDisabled() {
            let disabled = isDisabled(this._element)
                || isDisabled(this._element.parentNode)
            if (!disabled) {
                const el = this._element.querySelector('.form-control');
                disabled = isElement$1(el) && isDisabled(el);
            }
            return disabled;
        }

        _hackPopover() {
            this._dropdown._isWithContent = () => true;

            let getTipElement = this._dropdown._getTipElement;
            let fn = tip => {
                tip.classList.add(this._config.class);
                tip.classList.add('shadow');
            }
            this._dropdown._getTipElement = () => {
                let tip = getTipElement.call(this._dropdown);
                fn(tip);
                return tip;
            }
        }

        _isShown() {
            return this._dropdown === null ? false : this._dropdown._isShown();
        }

        hide() {
            if (this._isShown()) {
                this._dropdown.hide();
            }
        }

        show() {
            if (!this._isShown()) {
                this._dropdown.show();
            }
        }

        invoke(method) {
            let fn = this._dropdown[method];
            if (typeof fn === 'function') {
                fn.call(this._dropdown);
            }
            if (method === 'dispose') {
                this._dropdown = null;
                this.dispose();
            }
        }

        dispose() {
            if (this._dropdown !== null) {
                this._dropdown.dispose();
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
    }

    /* Dropdown */
    const NAME$Dropdown = 'Dropdown';

    class Dropdown extends DropdownBase {
        _configAfterMerge(config) {
            config = {
                ...{
                    bodyElement: this._element.parentNode.querySelector(config.dropdown)
                },
                ...super._configAfterMerge(config)
            };
            return config;
        }

        _setListeners() {
            let hasDisplayNone = false;

            this._show = () => {
                const disabled = this._isDisabled();

                if (!disabled) {
                    this._element.setAttribute('aria-expanded', 'true');
                    this._element.classList.add('show');
                }
                if (disabled) {
                    event.preventDefault();
                }
            };

            this._inserted = () => {
                const dropdown = Utility.getDescribedElement(this._element);
                if (dropdown) {
                    let body = dropdown.querySelector('.popover-body');
                    if (!body) {
                        body = document.createElement('div');
                        body.classList.add('popover-body');
                        dropdown.append(body);
                    }
                    body.classList.add('show');
                    const content = this._config.bodyElement;
                    if (content.classList.contains("d-none")) {
                        hasDisplayNone = true;
                        content.classList.remove("d-none");
                    }
                    body.append(content);
                }
            };

            this._hide = () => {
                const content = this._config.bodyElement;
                if (hasDisplayNone) {
                    content.classList.add("d-none");
                }
                this._element.classList.remove('show');
                this._element.append(content);
            }

            bootstrap.EventHandler.on(this._element, 'show.bs.popover', this._show);
            bootstrap.EventHandler.on(this._element, 'inserted.bs.popover', this._inserted);
            bootstrap.EventHandler.on(this._element, 'hide.bs.popover', this._hide);

            if (this._config.dismiss != null) {
                bootstrap.EventHandler.on(document, 'click', this._config.dismiss, this._hide);
            }
        }

        static get Default() {
            return {
                class: 'popover-dropdown',
                dropdown: '.dropdown-menu'
            }
        }

        static get NAME() {
            return NAME$Dropdown;
        }
    }

    bootstrap.EventHandler.on(document, 'click', '.dropdown-toggle', function (e) {
        let el = e.target;
        if (!el.classList.contains('dropdown-toggle')) {
            el = el.closest('.dropdown-toggle');
        }
        const bb_toggle_type = el.getAttribute('data-bs-toggle');
        if (bb_toggle_type === 'bb.dropdown') {
            let p = bootstrap.Popover.getInstance(el);
            if (p == null) {
                p = new bb.Dropdown(el);
                p.show();
            }
        }
    });

    bootstrap.EventHandler.on(document, 'click', function (e) {
        const el = e.target;
        const owner = Utility.getDescribedElement(el.closest('.dropdown-toggle'));
        const selector = `.${Dropdown.Default.class}.show`;
        document.querySelectorAll(selector).forEach(function (ele) {
            if (ele !== owner) {
                const element = Utility.getDescribedOwner(ele);
                if (element) {
                    let p = bootstrap.Popover.getInstance(element);
                    if (p !== null) {
                        p.hide();
                    }
                }
            }
        });
    });

    /* Confirm */
    const NAME$Confirm = 'Confirm';

    class Confirm extends DropdownBase {
        constructor(element, config) {
            super(element, config);

            this._id = element.getAttribute('id');
        }

        _getPopoverBodyElement() {
            return document.querySelector(`[data-bs-target="${this._id}"]`);
        }

        _setListeners() {
            this._show = () => {
                const disabled = this._isDisabled();
                if (disabled) {
                    event.preventDefault();
                } else {
                    this._element.setAttribute('aria-expanded', 'true');
                    this._element.classList.add('show');
                }

            };

            this._inserted = () => {
                const body = this._getPopoverBodyElement();
                const dorpdown = Utility.getDescribedElement(this._element);
                dorpdown.append(body);
            };

            this._hide = () => {
                const body = this._getPopoverBodyElement();
                if (body) {
                    const container = document.querySelector(this._config.confirm_container);
                    container.append(body);
                }

                const dropdown = Utility.getDescribedElement(this._element);
                if (dropdown) {
                    dropdown.classList.remove('show');
                }

                this._element.classList.remove('show');
                this._element.setAttribute('aria-expanded', 'false');
            }

            bootstrap.EventHandler.on(this._element, 'show.bs.popover', this._show);
            bootstrap.EventHandler.on(this._element, 'inserted.bs.popover', this._inserted);
            bootstrap.EventHandler.on(this._element, 'hide.bs.popover', this._hide);

            if (this._config.dismiss != null) {
                bootstrap.EventHandler.one(document, 'click', this._config.dismiss, this._hide);
            }
        }

        dispose() {
            if (this._config.dismiss != null) {
                bootstrap.EventHandler.off(document, 'click', this._config.dismiss, this._hide);
            }
            super.dispose();
        }

        static _create(element) {
            let p = new bb.Confirm(element);
            p.show();
        }

        static init(element) {
            element = getElement(element);
            let p = bb.Confirm.getInstance(element);
            if (p !== null && p._isShown()) {
                p.hide();

                const duration = getTransitionDurationFromElement(element, 15);
                let handler = window.setTimeout(() => {
                    window.clearTimeout(handler);
                    p.dispose();

                    Confirm._create(element);
                }, duration);
            } else {
                Confirm._create(element);
            }
        }

        static submit(element) {
            element = getElement(element)
            const form = element.closest('form');
            if (form !== null) {
                const button = document.createElement('button');
                button.setAttribute('type', 'submit');
                button.setAttribute('hidden', 'true');
                form.append(button);
                button.click();
                button.remove();
            }
        }

        static get Default() {
            return {
                class: 'popover-confirm',
                dismiss: '[data-bb-dismiss]',
                confirm_container: '.popover-confirm-container'
            }
        }

        static get NAME() {
            return NAME$Confirm;
        }
    }

    bootstrap.EventHandler.on(document, 'click', function (e) {
        const el = e.target;
        if (el.closest(`.${Confirm.Default.class}.show`) === null && el.closest(`${Confirm.Default.confirm_container}`) === null) {
            const owner = Utility.getDescribedElement(el.closest('.dropdown-toggle'));
            const popoverSelector = `.${Confirm.Default.class}.show`;
            document.querySelectorAll(popoverSelector).forEach(function (ele) {
                if (ele !== owner) {
                    const element = Utility.getDescribedOwner(ele);
                    if (element) {
                        const p = bootstrap.Popover.getInstance(element);
                        if (p) {
                            p.hide();
                        }
                    }
                }
            });
        }
    });

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

        static getDescribedElement(element) {
            if (isElement$1(element)) {
                const id = element.getAttribute('aria-describedby');
                if (id) {
                    return document.querySelector(`#${id}`);
                }
            }
            return null;
        }

        static getDescribedOwner(element) {
            if (isElement$1(element)) {
                const id = element.getAttribute('id');
                if (id) {
                    return document.querySelector(`[aria-describedby="${id}"]`);
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
        Tooltip,
        Popover,
        Dropdown,
        Confirm,
        Utility
    };
});

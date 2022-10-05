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

    /* Tooltip */
    const NAME$Tooltip = "Tooltip"

    class Tooltip extends BaseComponent {
        constructor(element, config = {}) {
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
            if (this._tooltip !== null && this._tooltip.tip !== null) {
                this._tooltip.dispose();
            }
            super.dispose();
        }

        _isShown() {
            return this._tooltip === null ? false : this._tooltip._isShown();
        }

        static _create(element, config) {
            new Tooltip(element, config);
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

    /* Popover */
    const NAME$Popover = 'Popover';

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

        static get NAME() {
            return NAME$Popover;
        }
    }

    class DropdownBase extends Tooltip {
        constructor(element, config = {}) {
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
                bootstrap.EventHandler.on(document, 'click', this._config.dismiss, () => this.hide());
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
        const selector = `.${Dropdown.Default.class}.show`;
        const el = e.target;
        if (el.closest(selector)) {
            return;
        }
        const owner = Utility.getDescribedElement(el.closest('.dropdown-toggle'));
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
            let p = Confirm.getInstance(element);
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

    bootstrap.EventHandler.on(document, 'click', '.anchor-link', function (e) {
        const hash = this.getAttribute('id');
        if (hash) {
            const title = this.getAttribute('data-bb-title');
            const href = window.location.origin + window.location.pathname + '#' + hash;
            Utility.copy(href);
            const tooltip = bootstrap.Tooltip.getOrCreateInstance(this, {
                title: title
            });
            tooltip.show();
            const handler = window.setTimeout(function () {
                window.clearTimeout(handler);
                tooltip.dispose();
            }, 1000);
        }
    });

    bootstrap.EventHandler.on(document, 'click', '[data-toggle="anchor"]', function (e) {
        e.preventDefault();
        const target = Utility.getDescribedElement(this, 'data-bb-target');
        if (target) {
            const container = Utility.getDescribedElement(this, 'data-bb-container') || document.defaultView;
            const rect = target.getBoundingClientRect();
            let margin = rect.top;
            let marginTop = getComputedStyle(target).getPropertyValue('margin-top').replace('px', '');
            if (marginTop) {
                margin = margin - parseInt(marginTop);
            }
            let offset = this.getAttribute('data-bb-offset');
            if (offset) {
                margin = margin - parseInt(offset);
            }
            const winScroll = Utility.getWindowScroll(container);
            container.scrollTo(0, margin + winScroll.scrollTop);
        }

    });

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

    class Menu extends BaseComponent {
        static _dispose(collapse, element) {
            if (element._isShown()) {
                element.hide();
                const duration = getTransitionDurationFromElement(collapse, 15);
                let handler = window.setTimeout(() => {
                    window.clearTimeout(handler);
                    element.dispose();
                }, duration);
            } else {
                element.dispose();
            }
        }

        static dispose(element) {
            element = getElement(element);
            const collapses = element.querySelectorAll('[data-bs-toggle="collapse"]');
            collapses.forEach(element => {
                const collapse = Utility.getTargetElement(element);
                const c = bootstrap.Collapse.getInstance(collapse);
                if (c !== null) {
                    this._dispose(collapse, c);
                }
            });
        }

        static init(element) {
            element = getElement(element);
            if (element) {
                let activeLink = element.querySelector('.nav-link.active');
                if (activeLink === null) {
                    const url = window.location.pathname.substring(1);
                    activeLink = element.querySelector(`[href="${url}"]`);
                    if (activeLink != null) {
                        activeLink.classList.add('active');
                    }
                }
                while (activeLink !== null) {
                    const menu = activeLink.closest('.collapse.submenu');
                    if (menu !== null && !menu.classList.contains('show')) {
                        const id = menu.getAttribute('id');
                        const triggerElement = element.querySelector(`[data-bs-toggle="collapse"][data-bs-target="#${id}"]`);
                        if (triggerElement !== null) {
                            triggerElement.click();
                        }
                    }
                    activeLink = menu;
                }
            }
        }

        static reset(element) {
            element = getElement(element);
            const expandAll = element.getAttribute('data-bb-expand') === 'true';
            const collapses = element.querySelectorAll('[data-bs-toggle="collapse"]');
            collapses.forEach(element => {
                const collapse = Utility.getTargetElement(element);
                const c = bootstrap.Collapse.getInstance(collapse);
                if (c !== null) {
                    if (expandAll) {
                        if (!c._isShown()) {
                            c.show();
                        }
                    } else {
                        this._dispose(collapse, c);
                    }
                } else {
                    if (expandAll) {
                        new bootstrap.Collapse(collapse, {
                            toggle: true
                        });
                    }
                }
            });
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
        Confirm,
        Dropdown,
        Menu,
        Popover,
        Tooltip,
        Utility
    };
});

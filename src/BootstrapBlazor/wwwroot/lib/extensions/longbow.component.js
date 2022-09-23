(function (global, factory) {
    typeof exports === 'object' && typeof module !== 'undefined'
        ? module.exports = factory()
        : typeof define === 'function' && define.amd
            ? define(factory)
            : (global = typeof globalThis !== 'undefined'
                ? globalThis
                : global || self, global.bb = factory());
})(this, (function () {

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
            config = this._mergeConfigObj(config);
            config = this._configAfterMerge(config);

            return config;
        }

        _configAfterMerge(config) {
            return config;
        }

        _mergeConfigObj(config) {
            return {
                ...this.constructor.Default,
                ...(typeof config === 'object' ? config : {})
            };
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
            if (!this._popover.hacked) {
                this._popover.hacked = true;
                this._popover._isWithContent = () => true;
                var getTipElement = this._popover._getTipElement;
                var fn = tip => {
                    if (this._config.css !== null) {
                        tip.classList.add(this._config.css);
                    }
                }
                this._popover._getTipElement = function () {
                    var tip = getTipElement.call(this);
                    tip.classList.add('popover-dropdown');
                    tip.classList.add('shadow');
                    fn(tip);
                    return tip;
                }
            }
        }

        _setListeners() {
            var that = this;
            var hasDisplayNone = false;
            this._element.addEventListener('show.bs.popover', function () {
                var disabled = that._config.showCallback.call(that._element);
                if (!disabled) {
                    that._element.setAttribute('aria-expanded', 'true');
                    that._element.classList.add('show');
                }
                if (disabled) {
                    event.preventDefault();
                }
            });
            this._element.addEventListener('inserted.bs.popover', function () {
                var pId = that._element.getAttribute('aria-describedby');
                if (pId) {
                    var pop = document.getElementById(pId);
                    var body = pop.querySelector('.popover-body');
                    if (!body) {
                        body = document.createElement('div');
                        body.classList.add('popover-body');
                        pop.append(body);
                    }
                    body.classList.add('show');
                    var content = that._config.bodyElement;
                    if (content.classList.contains("d-none")) {
                        hasDisplayNone = true;
                        content.classList.remove("d-none");
                    }
                    body.append(content);
                }
            });
            this._element.addEventListener('hide.bs.popover', function () {
                var pId = that._element.getAttribute('aria-describedby');
                if (pId) {
                    var content = that._config.bodyElement;
                    if (hasDisplayNone) {
                        content.classList.add("d-none");
                    }
                    that._element.append(content);
                }
                that._element.classList.remove('show');
            });
        }

        _getConfig(config) {
            var fn = function () {
                var disabled = this.classList.contains('disabled');
                if (!disabled && this.parentNode != null) {
                    disabled = this.parentNode.classList.contains('disabled');
                }
                if (!disabled) {
                    var ctl = this.querySelector('.form-control');
                    if (ctl != null) {
                        disabled = ctl.classList.contains('disabled');
                        if (!disabled) {
                            disabled = ctl.getAttribute('disabled') === 'disabled';
                        }
                    }
                }
                return disabled;
            };
            config = {
                ...{
                    css: null,
                    placement: this._element.getAttribute('bs-data-placement') || 'auto',
                    bodyElement: this._element.parentNode.querySelector('.dropdown-menu'),
                    showCallback: fn
                },
                ...config
            };
            return super._getConfig(config);
        }

        invoke(method) {
            var fn = this._popover[method];
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

        static get NAME() {
            return NAME$Popover;
        }
    }

    document.addEventListener('click', function (e) {
        var el = e.target;
        if (el.closest('.popover-dropdown.show') === null) {
            document.querySelectorAll('.popover-dropdown.show').forEach(function (ele) {
                var pId = ele.getAttribute('id');
                if (pId) {
                    var popover = document.querySelector('[aria-describedby="' + pId + '"]');
                    var p = bootstrap.Popover.getInstance(popover);
                    if (p !== null) {
                        p.hide();
                    }
                }
            });
        }
    });

    const isElement = object => {
        if (!object || typeof object !== 'object') {
            return false;
        }

        if (typeof object.jquery !== 'undefined') {
            object = object[0];
        }

        return typeof object.nodeType !== 'undefined';
    };

    const getElement = object => {
        if (isElement(object)) {
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

    const index_umd = {
        Popover
    };

    return index_umd;
}));

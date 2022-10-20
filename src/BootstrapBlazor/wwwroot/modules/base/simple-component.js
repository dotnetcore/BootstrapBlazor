import Config from './config.js'
import Data from './data.js'

/**
 * Constants
 */

const VERSION = '1.0.0'

/**
 * Class definition
 */

export default class SimpleComponent extends Config {
    constructor(element, config = {}) {
        super()

        this._element = element
        this._config = this._getConfig(config)

        Data.set(this._element, this.constructor.DATA_KEY, this)
        this._init()
    }

    _init() {

    }

    _getConfig(config) {
        config = this._mergeConfigObj(config, this._element)
        config = this._configAfterMerge(config)
        this._typeCheckConfig(config)
        return config
    }

    _execute(args) {

    }

    _dispose() {
    }

    // Public
    dispose() {
        this._dispose()

        Data.remove(this._element, this.constructor.DATA_KEY)
        for (const propertyName of Object.getOwnPropertyNames(this)) {
            this[propertyName] = null
        }
    }

    // Static
    static init(element) {
        if(element) {
            new this(element, {arguments: [].slice.call(arguments, 1)})
        }
        else {
            console.log(`the ${element} not allow null`)
        }
    }

    static execute(element) {
        const instance = this.getOrCreateInstance(element)
        instance._execute([].slice.call(arguments, 1))
    }

    static dispose(element) {
        const component = this.getInstance(element)
        if (component) {
            component.dispose()
        }
    }

    static getInstance(element) {
        return Data.get(element, this.DATA_KEY)
    }

    static getOrCreateInstance(element, config = {}) {
        return this.getInstance(element) || new this(element, typeof config === 'object' ? config : null)
    }

    static get DATA_KEY() {
        return `bb.${this.NAME}`
    }

    static get NAME() {
        return this.name
    }

    static get VERSION() {
        return VERSION
    }
}

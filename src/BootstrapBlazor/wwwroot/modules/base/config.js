/**
 * --------------------------------------------------------------------------
 * Bootstrap (v5.2.2): util/config.js
 * Licensed under MIT (https://github.com/twbs/bootstrap/blob/main/LICENSE)
 * --------------------------------------------------------------------------
 */

import { isElement, toType } from './index.js'
import Manipulator from './manipulator.js'

/**
 * Constants
 */

const VERSION = '1.0.0'

/**
 * Class definition
 */

export default class Config {
    // Getters
    static get Default() {
        return {}
    }

    static get DefaultType() {
        return {}
    }

    static get NAME() {
        throw new Error('You have to implement the static method "NAME", for each component!')
    }

    static get VERSION() {
        return VERSION
    }

    _getConfig(config) {
        config = this._mergeConfigObj(config)
        config = this._configAfterMerge(config)
        this._typeCheckConfig(config)
        return config
    }

    _configAfterMerge(config) {
        return config
    }

    _mergeConfigObj(config, element) {
        const jsonConfig = isElement(element) ? Manipulator.getDataAttribute(element, 'config') : {}
        const dataConfig = isElement(element) ? Manipulator.getDataAttributes(element) : {}
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
            const expectedTypes = configTypes[property]
            const value = config[property]
            const valueType = isElement(value) ? 'element' : toType(value)

            if (!new RegExp(expectedTypes).test(valueType)) {
                throw new TypeError(
                    `${this.constructor.NAME.toUpperCase()}: Option "${property}" provided type "${valueType}" but expected type "${expectedTypes}".`
                )
            }
        }
    }
}

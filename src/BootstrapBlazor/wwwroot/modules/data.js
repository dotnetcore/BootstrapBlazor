if (typeof (Array.prototype.remove) !== 'function') {
    Array.prototype.remove = function(item) {
        const index = this.indexOf(item);
        if (index > -1) {
            this.splice(index, 1);
        }
        return this;
    }
}

const elementMap = new Map()

export default {
    set(element, instance) {
        if (!elementMap.has(element)) {
            elementMap.set(element, instance)
        }
    },

    get(element) {
        if (elementMap.has(element)) {
            return elementMap.get(element)
        }

        return null
    },

    remove(element) {
        if (!elementMap.has(element)) {
            return
        }

        elementMap.delete(element)
    }
}

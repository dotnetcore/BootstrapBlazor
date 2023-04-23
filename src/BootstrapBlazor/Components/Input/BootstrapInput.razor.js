import Data from '../../modules/data.js'
import EventHandler from '../../modules/event-handler.js'

export function init(el, invoke, enter, enterCallbackMethod, esc, escCallbackMethod) {
    EventHandler.on(el, 'keyup', e => {
        if (enter && e.key === 'Enter') {
            obj.invokeMethodAsync(enterCallbackMethod, $el.val());
        }
        else if (esc && e.key === 'Escape') {
            obj.invokeMethodAsync(escCallbackMethod);
        }
    });
}

export function selectAll(el) {
    el.select();
}

export function selectAllOnFocus() {
    var $el = $(el);
    $el.on('focus', function () {
        $(this).select();
    });

}

export function selectAllOnEnter() {
    var $el = $(el);
    $el.on('keyup', function (e) {
        if (e.key === 'Enter') {
            $(this).select();
        }
    });
}

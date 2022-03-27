(function ($) {
    $.extend({
        bb_copyText: function (text) {
            if (navigator.clipboard) {
                navigator.clipboard.writeText(text);
            }
            else {
                if (typeof ele !== "string") return false;
                var input = document.createElement('input');
                input.setAttribute('type', 'text');
                input.setAttribute('value', text);
                document.body.appendChild(input);
                input.select();
                document.execCommand('copy');
                document.body.removeChild(input);
            }
        }
    });
})(jQuery);

(function ($) {
    $.extend({
        bb_reconnect: function () {
            var reconnectHandler = window.setInterval(function () {
                var $com = $('#components-reconnect-modal');
                if ($com.length > 0) {
                    var cls = $com.attr("class");
                    if (cls === 'components-reconnect-show') {
                        window.clearInterval(reconnectHandler);

                        async function attemptReload() {
                            await fetch('');
                            location.reload();
                        }
                        attemptReload();
                        setInterval(attemptReload, 5000);
                    }
                }
            }, 2000);
        }
    });
})(jQuery);

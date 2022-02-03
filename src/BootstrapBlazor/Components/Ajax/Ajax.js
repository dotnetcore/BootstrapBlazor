(function ($) {
    $.extend({
        bb_ajax: function (url, method, data) {
            data = JSON.stringify(data);
            var res = null;
            $.ajax({
                url: url,
                data: data,
                method: method,
                contentType: 'application/json',
                dataType: 'json',
                async: false,
                success: function (result) {
                    res = result;
                },
                error: function (XMLHttpRequest, textStatus, errorThrown) {
                    return null;
                }
            });
            if (res == null) {
                return null;
            }
            return JSON.stringify(res);
        },
        bb_ajax_goto: function (url) {
            window.location.href = url;
        }
    });
})(jQuery);

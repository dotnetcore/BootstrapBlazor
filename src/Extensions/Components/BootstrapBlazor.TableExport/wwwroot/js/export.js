(function ($) {
    $.generatefile = function (fileName, bytesBase64, contenttype) {
        var link = document.createElement('a');
        link.download = fileName;
        link.href = 'data:' + contenttype + ';base64,' + bytesBase64;
        document.body.appendChild(link);
        link.click();
        document.body.removeChild(link);
    };
})(jQuery);

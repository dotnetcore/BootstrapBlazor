(function ($) {
    $.extend({
        bb_cascader_hide: function (el) {
            const dropdownEl = document.getElementById(el);
            const dropdown = new bootstrap.Dropdown(dropdownEl);
            dropdown.hide();
        }
    });
})(jQuery);

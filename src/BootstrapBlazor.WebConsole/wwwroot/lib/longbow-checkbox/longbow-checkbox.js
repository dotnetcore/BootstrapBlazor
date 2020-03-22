(function ($) {
    'use strict';

    var lgbCheckbox = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, lgbCheckbox.DEFAULTS, options);

        var that = this;
        this.$element.on('click', function (e) {
            e.preventDefault();
            if (that.$element.hasClass(that.options.disabledClass)) return;
            that.$element.toggleClass(that.options.checkedClass);

            // set checkbox-original val
            var checkbox = that.$element.find(':checkbox');
            var checked = checkbox.prop('checked');
            checkbox.prop('checked', !checked);
        });
    };

    lgbCheckbox.VERSION = '1.0';
    lgbCheckbox.Author = 'argo@163.com';
    lgbCheckbox.DataKey = "lgb.checkbox";
    lgbCheckbox.Template = '<label role="checkbox" aria-checked="false" class="form-checkbox">';
    lgbCheckbox.Template += '<span class="checkbox-input"><span class="checkbox-inner"></span><input type="checkbox" /></span><span class="checkbox-label"></span>';
    lgbCheckbox.Template += '</label>';
    lgbCheckbox.DEFAULTS = {
        borderClass: null,
        disabledClass: 'is-disabled',
        checkedClass: 'is-checked',
        textClass: {
            'border-primary': 'text-primary',
            'border-info': 'text-info',
            'border-success': 'text-success',
            'border-warning': 'text-warning',
            'border-danger': 'text-danger',
            'border-secondary': 'text-secondary'
        },
        attributes: ["data-valid", "data-required-msg", "class"]
    };
    lgbCheckbox.AllowMethods = /disabled|enable|val/;

    var _proto = lgbCheckbox.prototype;

    _proto.disabled = function () {
        this.$element.addClass(this.options.disabledClass);
    };

    _proto.enable = function () {
        this.$element.removeClass(this.options.disabledClass);
    };

    _proto.val = function (value, valid) {
        if (value !== undefined) {
            this.$element.find(':checkbox').val(value);

            // trigger changed.lgbCheckbox
            this.$element.trigger('changed.lgbCheckbox');

            // trigger lgbValidate
            if (valid && this.$input.attr('data-valid') === 'true') this.$input.trigger('input.lgb.validate');
        }
    };

    function Plugin(option) {
        var params = $.makeArray(arguments).slice(1);
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(lgbCheckbox.DataKey);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(lgbCheckbox.DataKey, data = new lgbCheckbox(this, options));
            if (!lgbCheckbox.AllowMethods.test(option)) return;
            if (typeof option === 'string') {
                data[option].apply(data, params);
            }
        });
    }

    $.fn.lgbCheckbox = Plugin;
    $.fn.lgbCheckbox.Constructor = lgbCheckbox;

    $(function () {
        $('.form-checkbox').lgbCheckbox();
    });
})(jQuery);

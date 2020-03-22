(function ($) {
    'use strict';

    var lgbSelect = function (element, options) {
        this.$element = $(element);
        this.options = $.extend({}, lgbSelect.DEFAULTS, options);
        this.initDom();
    };

    lgbSelect.VERSION = '1.0';
    lgbSelect.Author = 'argo@163.com';
    lgbSelect.DataKey = "lgb.select";
    lgbSelect.Template = '<div class="form-select dropdown" data-toggle="lgbSelect">';
    lgbSelect.Template += '<span class="form-select-append">';
    lgbSelect.Template += '    <i class="fa fa-angle-up"></i>';
    lgbSelect.Template += '</span>';
    lgbSelect.Template += '<div class="dropdown-menu-arrow"></div>';
    lgbSelect.Template += '<div class="dropdown-menu"></div>';
    lgbSelect.Template += '</div>';
    lgbSelect.DEFAULTS = {
        placeholder: "请选择 ...",
        borderClass: null,
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
    lgbSelect.AllowMethods = /disabled|enable|val|reset|get/;

    var _proto = lgbSelect.prototype;
    _proto.initDom = function () {
        var that = this;

        this.initBySelect();
        if (this.options.borderClass) {
            this.$input.addClass(this.options.borderClass);
            this.$menubar.addClass(this.options.textClass[this.options.borderClass]);
        }
        this.$input.attr('placeholder', this.options.placeholder);

        this.$element.data(lgbSelect.DataKey, this);

        this.$ctl.on('click', 'a.dropdown-item', function (e) {
            e.preventDefault();

            var $this = $(this);
            $this.parent().children().removeClass('active');
            that.val($this.attr('data-val'), true);
        });

        var getUID = function (prefix) {
            if (!prefix) prefix = 'lgb';
            do prefix += ~~(Math.random() * 1000000);
            while (document.getElementById(prefix));
            return prefix;
        };

        // init for
        var $for = this.$ctl.parent().find('[for="' + this.$element.attr('id') + '"]');
        if ($for.length > 0) {
            var id = getUID();
            this.$input.attr('id', id);
            $for.attr('for', id);
        }
    };

    _proto.initBySelect = function () {
        var $input = this.$element.prev();

        // 新控件 <div class="form-select">
        this.$ctl = $(lgbSelect.Template).insertBefore(this.$element);
        if ($input.attr('data-toggle') === 'lgbSelect') {
            this.$input = $input.addClass("form-select-input").attr("aria-haspopup", "true").attr("aria-expanded", "false").attr("data-toggle", "dropdown").attr("readonly", true);
        }
        else {
            this.$input = $('<input type="text" readonly="readonly" class="form-control form-select-input" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false"/>');
        }

        // 下拉组合框
        this.$menubar = this.$ctl.find('.form-select-append');
        this.$menus = this.$ctl.find('.dropdown-menu');
        this.$input.insertBefore(this.$menubar);
        var data = this.$element.find('option').map(function () {
            return { value: this.value, text: this.text, selected: this.selected }
        }).toArray();

        // create new element 
        // <input type="hidden" data-toggle="lgbSelect" />
        this.createElement();

        // init dropdown-menu
        this.reset(data);

        // init dropdown-menu data
        this.$input.dropdown();
    };

    _proto.createElement = function () {
        var that = this;

        // move attributes
        this.options.attributes.forEach(function (name, index) {
            var value = that.$element.attr(name);
            if (value !== undefined) {
                if (name === 'class') that.$input.addClass(value).removeClass('d-none');
                else that.$input.attr(name, that.$element.attr(name));
            }
        });

        // save attributes
        var attrs = [];
        ["id", "name", "class", "data-valid", "data-default-val"].forEach(function (v, index) {
            var value = that.$element.attr(v);
            if (value !== undefined) attrs.push({ name: v, value: value });
        });

        var disabled = this.$element.prop('disabled');
        // set disabled property
        if (disabled) {
            this.disabled();
        }

        // replace element select -> input hidden
        this.$element.remove();
        this.$element = $('<input type="hidden" data-toggle="lgbSelect" />').val(that.val()).insertBefore(this.$input);

        // restore attributes
        attrs.forEach(function (v) {
            that.$element.attr(v.name, v.value);
        });
    };

    _proto.disabled = function () {
        this.$ctl.addClass('is-disabled');
        this.$input.attr('disabled', 'disabled');
    };

    _proto.enable = function () {
        this.$ctl.removeClass('is-disabled');
        this.$input.removeAttr('disabled');
    };

    _proto.reset = function (value, init) {
        var that = this;

        // keep old value
        var oldValue = this.$input.attr('value');

        // default select value
        if (init == undefined) init = true;

        // warning: must use attr('value') method instead of val(). otherwise the others input html element will filled by first element value.
        // see https://gitee.com/LongbowEnterprise/longbow-select/issues/IZ3BR?from=project-issue
        this.$input.attr('value', '').removeClass('is-valid is-invalid');
        this.$menus.html('');
        var $activeItem = null;
        $.each(value, function (index) {
            var $item = $('<a class="dropdown-item" href="#" data-val="' + this.value + '">' + this.text + '</a>');
            that.$menus.append($item);
            if (init) {
                if (this.selected === true || this.value === oldValue || index === 0 || this.value === that.$element.attr('data-default-val')) {
                    that.$input.attr('value', this.text);
                    that.$element.val(this.value).attr('data-text', this.text);
                    $activeItem = $item;
                }
            }
        });
        if ($activeItem !== null) $activeItem.addClass('active');

        this.source = value;
    };

    _proto.get = function (callback) {
        if ($.isFunction(callback)) {
            callback.call(this.$element, this.source);
        }
    };

    _proto.val = function (value, valid) {
        if (value !== undefined) {
            var text = this.$menus.find('a.dropdown-item[data-val="' + value + '"]').text();
            this.$input.val(text);
            this.$element.val(value).attr('data-text', text);
            this.$menus.find('.dropdown-item').removeClass('active');
            this.$menus.find('.dropdown-item[data-val="' + value + '"]').addClass('active');

            // trigger changed.lgbselect
            this.$element.trigger('changed.lgbSelect');

            // trigger lgbValidate
            if (valid && this.$input.attr('data-valid') === 'true') this.$input.trigger('input.lgb.validate');
        }
        else {
            return this.$element.val();
        }
    };

    function Plugin(option) {
        var params = $.makeArray(arguments).slice(1);
        return this.each(function () {
            var $this = $(this);
            var data = $this.data(lgbSelect.DataKey);
            var options = typeof option === 'object' && option;

            if (!data) $this.data(lgbSelect.DataKey, data = new lgbSelect(this, options));
            if (!lgbSelect.AllowMethods.test(option)) return;
            if (typeof option === 'string') {
                data[option].apply(data, params);
            }
        });
    }

    $.fn.lgbSelect = Plugin;
    $.fn.lgbSelect.Constructor = lgbSelect;

    $(function () {
        $('select[data-toggle="lgbSelect"]').lgbSelect();
    });
})(jQuery);

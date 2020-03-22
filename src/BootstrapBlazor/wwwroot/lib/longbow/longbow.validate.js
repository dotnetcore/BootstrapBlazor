(function ($) {
    'use strict';

    var Validate = function (element, options) {
        var that = this;
        this.$element = $(element);
        this.options = $.extend({
            pendingRequest: 0,
            pending: {},
            successList: [],
            optional: function () { return false; },
            invalid: {},
            getLength: function (value, element) {
                switch (element.nodeName.toLowerCase()) {
                    case "select":
                        return $("option:selected", element).length;
                    case "input":
                        if (this.checkable(element)) {
                            return $(element).filter(":checked").length;
                        }
                }
                return value.length;
            },
            checkable: function (element) {
                return (/radio|checkbox/i).test(element.type);
            },
            depend: function (param, element) {
                return this.dependTypes[typeof param] ? this.dependTypes[typeof param](param, element) : true;
            },
            dependTypes: {
                "boolean": function (param) {
                    return param;
                },
                "string": function (param, element) {
                    return !!$(param, element.form).length;
                },
                "function": function (param, element) {
                    return param(element);
                }
            },
            previousValue: function (element, method) {
                method = typeof method === "string" && method || "remote";
                return $.data(element, "previousValue") || $.data(element, "previousValue", {
                    old: null,
                    valid: true,
                    message: "请修正本字段"
                });
            },
            startRequest: function (element) {
                if (!this.pending[element.name]) {
                    this.pendingRequest++;
                    $(element).addClass(this.settings.pendingClass);
                    this.pending[element.name] = true;
                }
            },
            stopRequest: function (element, valid) {
                this.pendingRequest--;

                // Sometimes synchronization fails, make sure pendingRequest is never < 0
                if (this.pendingRequest < 0) {
                    this.pendingRequest = 0;
                }
                delete this.pending[element.name];
                $(element).removeClass(this.settings.pendingClass);
            },
            showErrors: function (errors) {
                for (var name in errors) {
                    var element = document.getElementById(name);
                    var $element = $(element);
                    that.tooltip.call(that, element, false);
                    $element.attr('data-original-title', errors[name]).tooltip('show');
                }
            },
            defaultMessage: function (element, rule) {
                return that.defaultMessage(element, rule);
            },
            resetInternals: function () { },
            errorsFor: function (element) {
                that.tooltip.call(that, element, true);
            },
            settings: $.validator.defaults
        }, this.defaults(), options);

        // fix bug Edge
        this.$element.find('select' + this.options.childClass).on('input', function (e) {
            e.stopPropagation();
        }).on('change', function () {
            $(this).trigger('input.lgb.validate');
        });

        this.$element.on('input.lgb.validate', this.options.childClass, function () {
            if (!that.validElement(this)) $(this).tooltip('show');
        }).on('inserted.bs.tooltip', this.options.childClass, function () {
            $('#' + $(this).attr('aria-describedby')).addClass(that.options.errorClass);
        });

        if (!this.options.validButtons) return;
        this.$element.find(this.options.validButtons).on('click.lgb.validate', function (e) {
            var valid = that.valid();
            $(this).attr(Validate.DEFAULTS.validResult, valid);
            if (!valid) {
                e.preventDefault();
                e.stopImmediatePropagation();
            }
        });
        if (this.options.modal) {
            // 关闭 modal 时移除所有验证信息
            $(this.options.modal).on('show.bs.modal', function (e) {
                that.reset();
            });

            // bs bug 弹窗内控件值更改后再次点击关闭按钮是 hide.bs.modal 事件不被触发
            // 兼容 键盘事件 ESC
            var dismissTooltip = function (e) {
                // 移除残留 tooltip
                var $modal = $(that.options.modal)
                $modal.find('[aria-describedby]').each(function (index, ele) {
                    var tooltipId = $(ele).attr('aria-describedby');
                    var $tooltip = $('#' + tooltipId);
                    if ($tooltip.length === 1) {
                        $tooltip.tooltip('dispose');
                    }
                });
            };
            $(this.options.modal).on('click', '[data-dismiss="modal"]', dismissTooltip);
            $(this.options.modal).on('keydown', function (event) {
                // ESC
                if (event.which === 27) {
                    event.preventDefault();
                    dismissTooltip(event);
                }
            });
        }
    };

    Validate.VERSION = '2.0';

    Validate.DEFAULTS = {
        validClass: 'is-valid',
        errorClass: 'is-invalid',
        ignoreClass: '.ignore',
        childClass: '[data-valid="true"]',
        validResult: 'data-valid-result'
    };

    Validate.prototype.defaults = function () {
        return $.extend(Validate.DEFAULTS, {
            validButtons: this.$element.attr('data-valid-button'),
            modal: this.$element.attr('data-valid-modal')
        });
    };

    Validate.prototype.reset = function () {
        var css = this.options.validClass + ' ' + this.options.errorClass;
        this.$element.find(this.options.childClass).each(function () {
            var $this = $(this);
            $this.tooltip('dispose');
            $this.removeClass(css);
        });
    };

    Validate.prototype.valid = function () {
        var that = this;
        var op = this.options;
        var $firstElement = null;

        this.$element.find(op.childClass + ':visible').not(op.ignoreClass).each(function () {
            if (!that.validElement(this) && $firstElement === null) $firstElement = $(this);
        });
        if ($firstElement) $firstElement.tooltip('show');
        return $firstElement === null;
    };

    Validate.prototype.validElement = function (element) {
        var result = this.check(element);
        this.tooltip(element, result);
        return result;
    };

    Validate.prototype.tooltip = function (element, valid) {
        if (valid === "pending") return;

        var op = this.options;
        var $this = $(element);
        if (valid) $this.tooltip('dispose');
        else {
            if (!$this.hasClass(op.errorClass)) $this.tooltip();
        }
        if (!valid) {
            $this.removeClass(op.validClass).addClass(op.errorClass);
        }
        else {
            $this.removeClass(op.errorClass).addClass(op.validClass);
        }
    };

    Validate.prototype.check = function (element) {
        var result = true;
        var $this = $(element);
        if ($this.is(':hidden')) return result;
        var methods = this.rules(element);
        var proxy = function (rule) {
            if ($.isFunction($.validator.methods[rule])) {
                result = $.validator.methods[rule].call(this.options, $this.val(), element, methods[rule]);
                if (!result) {
                    $this.attr('data-original-title', this.defaultMessage(element, { method: rule, parameters: methods[rule] }));
                    return result;
                }

                // checkGroup rule
                if (rule === 'checkGroup') {
                    var $checkers = this.$element.find(this.options.childClass).filter(function () {
                        var $this = $(this);
                        return $this.hasClass(rule) || $this.attr(rule);
                    });
                    $checkers.removeClass(this.options.errorClass).removeClass(this.options.validClass);
                    if (result) $checkers.tooltip('dispose');
                    else $checkers.addClass(this.options.errorClass).tooltip();
                }
            }
            else {
                console.log('没有匹配的方法 ' + rule);
            }
            return true;
        }
        var remote = null;
        for (var rule in methods) {
            if (rule !== 'remote') {
                result = proxy.call(this, rule);
                if (!result) return false;
            }
            else remote = rule;
        }
        if (remote !== null) result = proxy.call(this, remote);
        return result;
    };

    Validate.prototype.defaultMessage = function (element, rule) {
        var message = $(element).attr('data-' + rule.method + '-msg') || rule.method === 'required' && $(element).attr('placeholder') || $.validator.messages[rule.method];
        var theregex = /\$?\{(\d+)\}/g;
        if (typeof message === "function") {
            message = message.call(this, rule.parameters, element);
        } else if (theregex.test(message)) {
            message = $.validator.format(message.replace(theregex, "{$1}"), rule.parameters);
        }
        return message;
    };

    Validate.prototype.attributeRules = function (element, rules) {
        var $element = $(element), value;

        $.each(["remote"], function () {
            var para = $element.attr(this);
            if (para) {
                if (element.name === "") element.name = element.id;
                rules[this] = $.formatUrl(para);
            }
        });

        $.each(["radioGroup", "checkGroup"], function () {
            if (rules[this]) {
                delete rules.required;
                return false;
            }
        });
        return rules;
    };

    Validate.prototype.rules = function (element) {
        var $this = $(element);
        var rules = $this.data('lgb.Validate.Rules');
        if (!rules) $this.data('lgb.Validate.Rules', rules = this.attributeRules(element, $.validator.normalizeRules($.extend(
            { required: true },
            $.validator.classRules(element),
            $.validator.attributeRules(element)
        ))));
        return rules;
    };

    function Plugin(option) {
        return this.each(function () {
            var $this = $(this);
            var data = $this.data('lgb.Validate');
            var options = typeof option === 'object' && option;

            if (!data && /valid|defaults/.test(option)) return;
            if (!data) $this.data('lgb.Validate', data = new Validate(this, options));
            if (typeof option === 'string') data[option]();
        });
    }

    $.fn.lgbValidate = Plugin;
    $.fn.lgbValidate.Constructor = Validate;
    $.fn.lgbValidator = function () {
        return this.data('lgb.Validate');
    };
    $.fn.lgbValid = function () {
        var $this = this;
        return $this.attr(Validate.DEFAULTS.validResult) === 'true';
    };

    $(function () {
        if ($.isFunction($.validator)) {
            $.validator.addMethod("equalTo", function (value, element, param) {
                var target = $(param);
                if (this.settings.onfocusout && target.not(".validate-equalTo-blur").length) {
                    target.addClass("validate-equalTo-blur").on("blur.validate-equalTo", function () {
                        var validator = $(element).parents('[data-toggle="LgbValidate"]').data('lgb.Validate');
                        validator.validElement(element);
                    });
                }
                return value === target.val();
            });

            $.validator.addMethod("ip", function (value, element) {
                return this.optional(element) || /^(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9])\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[1-9]|0)\.(25[0-5]|2[0-4][0-9]|[0-1]{1}[0-9]{2}|[1-9]{1}[0-9]{1}|[0-9])$/.test(value);
            }, "请填写正确的IP地址");

            $.validator.addMethod("radioGroup", function (value, element) {
                return $(element).find(':checked').length === 1;
            }, "请选择一个选项");

            $.validator.addMethod("checkGroup", function (value, element) {
                return $(element).parents('[data-toggle="LgbValidate"]').find(':checked').length >= 1;
            }, "请选择一个选项");

            $.validator.addMethod("userName", function (value, element) {
                return this.optional(element) || /^[a-zA-Z0-9_@.]*$/.test(value);
            }, "登录名称不可以包含非法字符");

            $.validator.addMethod("greaterThan", function (value, element, target) {
                return this.optional(element) || $(target).val() <= value;
            }, "");

            $.validator.addMethod("lessThan", function (value, element, target) {
                return this.optional(element) || $(target).val() >= value;
            }, "");
        }
        $('[data-toggle="LgbValidate"]').lgbValidate();
    });
})(jQuery);
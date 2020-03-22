(function ($) {
    var findIdField = function (tableName) {
        var idField = tableName.bootstrapTable("getOptions").idField;
        if (idField === undefined) idField = "Id";
        return idField;
    };

    var swalDeleteOptions = {
        title: "删除数据",
        html: '您确定要删除选中的所有数据吗',
        type: "warning",
        showCancelButton: true,
        confirmButtonColor: '#dc3545',
        cancelButtonColor: '#6c757d',
        confirmButtonText: "我要删除",
        cancelButtonText: "取消"
    };

    DataEntity = function (options) {
        this.options = options;
    };

    DataEntity.prototype = {
        load: function (value) {
            for (name in this.options) {
                var ctl = $(this.options[name]);
                if (ctl.attr('data-toggle') === "dropdown") {
                    ctl.val(value[name]).dropdown('val');
                }
                else if (ctl.attr('data-toggle') === 'toggle') {
                    ctl.bootstrapToggle(value[name] ? 'on' : 'off');
                }
                else if (ctl.attr('data-toggle') === 'lgbSelect') {
                    ctl.lgbSelect('val', value[name]);
                }
                else {
                    ctl.val(value[name]);
                }
            }
        },
        reset: function () {
            for (name in this.options) {
                var ctl = $(this.options[name]);
                var dv = ctl.attr("data-default-val");
                if (dv === undefined) dv = "";
                if (ctl.attr('data-toggle') === "dropdown") {
                    ctl.val(dv).dropdown('val');
                }
                else if (ctl.attr('data-toggle') === 'toggle') {
                    ctl.bootstrapToggle(dv === "true" ? 'on' : 'off');
                }
                else if (ctl.attr('data-toggle') === 'lgbSelect') {
                    ctl.lgbSelect('val', dv);
                }
                else {
                    ctl.val(dv);
                }
            }
        },
        get: function () {
            var target = {};
            for (name in this.options) {
                var ctl = $(this.options[name]);
                var dv = ctl.attr('data-default-val');
                if (ctl.attr('data-toggle') === 'toggle') {
                    target[name] = ctl.prop('checked');
                    continue;
                }
                else if (dv !== undefined && ctl.val() === "") target[name] = dv;
                else target[name] = ctl.val();

                // check boolean value
                if (ctl.attr('data-bool') === 'true') {
                    if (target[name] === "true" || target[name] === "True") target[name] = true;
                    if (target[name] === "false" || target[name] === "False") target[name] = false;
                }
            }
            return target;
        }
    };

    DataTable = function (options) {
        var that = this;
        this.options = $.extend(true, { delTitle: "删除数据", saveTitle: "保存数据" }, DataTable.settings, options);
        this.dataEntity = new DataEntity(options.map);

        // handler click event
        for (var name in this.options.click) {
            $(name).on('click', { handler: this.options.click[name] }, function (e) {
                e.preventDefault();
                e.data.handler.call(that, this);
            });
        }

        // handler extra click event
        for (var cId in this.options.events) {
            $(cId).on('click', { handler: this.options.events[cId] }, function (e) {
                var options = that.options;
                var row = {};
                if (options.bootstrapTable !== null) {
                    var arrselections = options.bootstrapTable.bootstrapTable('getSelections');
                    if (arrselections.length === 0) {
                        lgbSwal({ title: '请选择要编辑的数据', type: "warning" });
                        return;
                    }
                    else if (arrselections.length > 1) {
                        lgbSwal({ title: '请选择一个要编辑的数据', type: "warning" });
                        return;
                    }
                    else {
                        row = arrselections[0];
                    }
                }
                e.data.handler.call(this, row);
            });
        }
    };

    DataTable.settings = {
        url: undefined,
        bootstrapTable: null,
        treegridParentId: 'ParentId',
        modal: '#dialogNew',
        click: {
            '#btn_query': function (element) {
                if (this.options.bootstrapTable !== null) {
                    var options = this.options.bootstrapTable.bootstrapTable('getOptions');
                    if (options.advancedSearchModal) {
                        $(options.advancedSearchModal).modal('hide');
                    }
                    // fix bug: 翻页后再更改查询条件导致页码未更改数据为空
                    // 更改页码为 1 即可
                    // https://gitee.com/LongbowEnterprise/BootstrapAdmin/issues/I1A739
                    var options = this.options.bootstrapTable.data('bootstrap.table').options;
                    options.pageNumber = 1;
                    this.options.bootstrapTable.bootstrapTable('refresh');
                }
                handlerCallback.call(this, null, element, { oper: 'query' });
            },
            '#btn_reset': function () {
                if (this.options.bootstrapTable !== null) {
                    var options = this.options.bootstrapTable.bootstrapTable('getOptions');
                    if (options.advancedSearchModal) {
                        $(options.advancedSearchModal).find('[data-default-val]').each(function (index, element) {
                            var $ele = $(element);
                            var val = $ele.attr('data-default-val');
                            if ($ele.prop('nodeName') === 'INPUT') {
                                if ($ele.hasClass('form-select-input')) {
                                    $ele.prev().lgbSelect('val', val);
                                }
                                else {
                                    $ele.val(val);
                                }
                            }
                        });
                    }
                }
            },
            '#btn_add': function (element) {
                this.dataEntity.reset();
                if (this.options.modal.constructor === String) $(this.options.modal).modal("show");
                if (this.options.bootstrapTable !== null) this.options.bootstrapTable.bootstrapTable('uncheckAll');
                handlerCallback.call(this, null, element, { oper: 'create' });
            },
            '#btn_edit': function (element) {
                var options = this.options;
                var data = {};
                if (options.bootstrapTable !== null) {
                    var arrselections = options.bootstrapTable.bootstrapTable('getSelections');
                    if (arrselections.length === 0) {
                        lgbSwal({ title: '请选择要编辑的数据', type: "warning" });
                        return;
                    }
                    else if (arrselections.length > 1) {
                        lgbSwal({ title: '请选择一个要编辑的数据', type: "warning" });
                        return;
                    }
                    else {
                        data = arrselections[0];
                        this.dataEntity.load(data);
                        if (options.modal.constructor === String) $(options.modal).modal("show");
                    }
                }
                handlerCallback.call(this, null, element, { oper: 'edit', data: data });
            },
            '#btn_delete': function (element) {
                var that = this;
                var options = this.options;
                if (options.bootstrapTable !== null) {
                    var arrselections = options.bootstrapTable.bootstrapTable('getSelections');
                    if (arrselections.length === 0) {
                        lgbSwal({ title: '请选择要删除的数据', type: "warning" });
                        return;
                    }
                    else {
                        swal($.extend({}, swalDeleteOptions)).then(function (result) {
                            if (result.value) {
                                var idField = findIdField(options.bootstrapTable);
                                var iDs = arrselections.map(function (element, index) { return element[idField]; });
                                $.bc({
                                    url: options.url, data: iDs, method: 'delete', title: options.delTitle, logData: arrselections,
                                    callback: function (result) {
                                        if (result) options.bootstrapTable.bootstrapTable('refresh');
                                        handlerCallback.call(that, null, element, { oper: 'del', success: result, data: arrselections });
                                    }
                                });
                            }
                        });
                    }
                }
            },
            '#btnSubmit': function (element) {
                var that = this;
                var options = $.extend(true, {}, this.options, { data: this.dataEntity.get() });
                $.bc({
                    url: options.url, data: options.data, title: options.saveTitle, modal: options.modal, method: "post",
                    callback: function (result) {
                        if (result) {
                            options.bootstrapTable.bootstrapTable('refresh');
                            handlerCallback.call(that, null, element, { oper: 'save', success: result, data: [options.data] });
                        }
                    }
                });
            }
        }
    };

    DataTable.prototype = {
        constructor: DataTable,
        idEvents: function () {
            var op = {
                dataEntity: this.dataEntity,
                table: this.options.bootstrapTable,
                treegridParentId: this.options.treegridParentId,
                modal: this.options.modal,
                src: this,
                url: this.options.url
            };
            return {
                'click .edit': function (e, value, row, index) {
                    op.dataEntity.load(row);
                    op.table.bootstrapTable('uncheckAll');
                    op.table.bootstrapTable('check', index);
                    handlerCallback.call(op.src, null, e, { oper: 'edit', data: row });
                    $(op.modal).modal("show");
                },
                'click .del': function (e, value, row, index) {
                    var displayName = "本项目";
                    if (row.Name) displayName = " <span class='text-danger font-weight-bold'>" + row.Name + "</span> ";
                    var text = "您确定要删除" + displayName + "吗？";
                    var data = $.extend({}, row);
                    data = [data];

                    // 判断是否为父项菜单
                    var idField = findIdField(op.table);
                    var idValue = row[idField];

                    if (idValue != undefined) {
                        var nodes = op.table.bootstrapTable('getData').filter(function (row, index, data) {
                            return idValue == row[op.treegridParentId];
                        });
                        if ($.isArray(nodes) && nodes.length > 0) {
                            $.each(nodes, function (index, element) {
                                data.push($.extend({}, element));
                            });
                            text = "本删除项含有级联子项目</br>您确定要删除 <span class='text-danger font-weight-bold'>" + row.Name + "</span> 以及子项目吗？";
                        }
                    }
                    swal($.extend({}, swalDeleteOptions, { html: text })).then(function (result) {
                        if (result.value) {
                            var idField = findIdField(op.table);
                            var iDs = data.map(function (element, index) {
                                return element[idField];
                            });
                            $.bc({
                                url: op.url, data: iDs, method: 'delete', title: '删除数据', logData: data,
                                callback: function (result) {
                                    if (result) op.table.bootstrapTable('refresh');
                                    handlerCallback.call(op.src, null, e, { oper: 'del', success: result, data: data });
                                }
                            });
                        }
                    });
                }
            };
        }
    };

    function handlerCallback(callback, element, data) {
        if ($.isFunction(callback)) callback.call(e, data);
        if ($.isFunction(this.options.callback)) this.options.callback.call(element, data);
    }
}(jQuery));
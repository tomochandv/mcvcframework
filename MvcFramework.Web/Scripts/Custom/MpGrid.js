var Grid = {
    _id: '#',
    _url: '',
    _height: 350,
    _sortname: '',
    _multiselect: false,
    GridInit: function (id, parentDiv, modelData, sortname, url, height, caption, multiselect) {
        _id = '#' + id;
        _url = url;
        _height = height;
        autowidth: true,
        _sortname = sortname,
        _multiselect = multiselect,
        $(_id).jqGrid({
            url: url,
            datatype: "json",
            mtype: 'POST',
            width: '100%',
            height: _height,
            colModel: modelData,
            shrinkToFit: false,
            jsonReader: { repeatitems: false },
            caption: caption,
            rowNum: 50,
            rowList: [10, 20, 30, 50],
            sortname: sortname,
            multiselect: _multiselect,
            viewrecords: true,
            ajaxGridOptions: {
                type: 'post',
                async: false,
                error: function () { alert('grid error.'); }
            },
            pager: jQuery('#gridpager'),
            onPaging: function () {
                $(_id).jqGrid('setGridParam', { datatype: 'json' });
            },
            loadComplete: function (data) {
                if ($(_id).jqGrid('getGridParam', 'datatype') == 'json') {
                    $(_id).jqGrid('setGridParam', { datatype: 'json', data: data.rows, pageServer: data.page, recortsServer: data.records, lastpageServer: data.total });
                    this.refreshIndex();
                }
                else {
                    $(_id).jqGrid('setGridParam', {
                        page: $(_id).jqGrid('getGridParam', 'pageServer'),
                        records: $(_id).jqGrid('getGridParam', 'recordsServer'),
                        lastpage: $(_id).jqGrid('getGridParam', 'lastpageServer')
                    });
                    this.updatepager(false, true);
                }
            },
            onSelectRow: function (id) {
                jQuery(_id).editRow(id, true);
            }
        });
        $(".ui-jqgrid .ui-pg-input").css("height", "20px");

        $(window).on('resize', function () {
            var pid = "#" + parentDiv
            $(_id).setGridWidth($(pid).width(), false);
            $(_id).setGridHeight($(window).height() - _height, true);
        }).trigger('resize');
    },
    SearchGrid: function (jsonPostData) {
        $(_id).jqGrid('setGridParam', { datatype: 'json', page: jsonPostData.page });
        if (jsonPostData != null) {
            $(_id).jqGrid('setGridParam', { postData: jsonPostData });
            $(_id).trigger("reloadGrid");
        } else {
            alert("데이터가 존재하지 않습니다.");
            $(_id).trigger("reloadGrid");
        }
    },
    SearchGridId: function (id, jsonPostData) {
        $(_id).jqGrid('setGridParam', { datatype: 'json', page: jsonPostData.page });
        if (jsonPostData != null) {
            $(_id).jqGrid('setGridParam', { postData: jsonPostData });
            $(_id).trigger("reloadGrid");
        } else {
            alert("데이터가 존재하지 않습니다.");
            $(_id).trigger("reloadGrid");
        }
    }
}

var Formatter = {
    TextBox: function (cellvalue, options, rowObject) {
        var txt = '<input type="text" style="width:100%;" value="' + cellvalue + '" />';
        return txt;
    }
}
'use strict';

var queryConfiguration = {
    load: function () {
        var data1 = {
            title: "Query Configuration",
            uri: function () {
                return '/api/data/queryconfiguration';
            },
            identifier: "QueryConfiguration",
            type: "Add",
            fields: queryConfiguration.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAdd(data1);

        var data2 = {
            title: "Query Configuration",
            uri: function () {
                return '/api/data/queryconfiguration/' + queryConfiguration.state.editId;
            },
            identifier: "QueryConfiguration",
            type: "Edit",
            fields: queryConfiguration.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvEdit(data2);

        var data3 = {
            title: "Query Configuration",
            uri: function () {
                return '/api/data/queryconfiguration';
            }, //dtQueryConfiguration, mdlAddQueryConfiguration
            identifier: "QueryConfiguration",
            columns: ['id', 'queryName', 'queryDetails']
        };

        parameterCtrl.datatable.loadDataTable(data3);

        $('#dtQueryConfiguration tbody').on('click', '.edit-record', function () {
            var dataId = $(this).data('id');
            var $editForm = $('#frmEditQueryConfiguration');
            $.ajax({
                url: '/api/data/queryconfiguration/' + dataId,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    queryConfiguration.state.editId = response.id;
                    // Load data to form 
                    $editForm.find('input[name="QueryName"]').val(response.queryName);
                    $editForm.find('textarea[name="QueryDetails"]').val(response.queryDetails);
                }
            });
        });
    },
    fvFields: function () {
        return {
            fields: {
                "QueryName": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter query name'
                        }
                    }
                },
                "QueryDetails": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter query details'
                        }
                    }
                }
            },
            plugins: parameterCtrl.fv.plugins(),
            init: parameterCtrl.fv.init()
        }
    },
    state: {
        editId: null
    }
};

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        queryConfiguration.load();
    })();
});

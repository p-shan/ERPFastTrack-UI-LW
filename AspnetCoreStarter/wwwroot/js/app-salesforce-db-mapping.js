'use strict';

var salesforceDbMapping = {
    load: function () {
        var data1 = {
            title: "Salesforce DB Mapping",
            uri: function () {
                return '/api/data/salesforcedbmapping';
            },
            identifier: "SalesforceDBMapping",
            type: "Add",
            fields: salesforceDbMapping.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAdd(data1);

        var data2 = {
            title: "Salesforce DB Mapping",
            uri: function () {
                return '/api/data/salesforcedbmapping/' + salesforceDbMapping.state.editId;
            },
            identifier: "SalesforceDBMapping",
            type: "Edit",
            fields: salesforceDbMapping.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvEdit(data2);

        var data3 = {
            title: "Salesforce DB Mapping",
            uri: function () {
                return '/api/data/salesforcedbmapping';
            },
            identifier: "SalesforceDBMapping",
            columns: ['id', 'name', 'sfConnName', 'dbConnName']
        };
        parameterCtrl.datatable.loadDataTable(data3);

        $('#dtSalesforceDBMapping tbody').on('click', '.edit-record', function () {
            var dataId = $(this).data('id');
            var $editForm = $('#frmEditSalesforceDBMapping');
            $.ajax({
                url: '/api/data/salesforcedbmapping/' + dataId,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    debugger;
                    salesforceDbMapping.state.editId = response.id;
                    // Load data to form 
                    $editForm.find('input[name="Name"]').val(response.name);
                    $editForm.find('select[name="SfConnId"]').val(response.sfConnId).trigger('change');
                    $editForm.find('select[name="DbConnId"]').val(response.dbConnId).trigger('change');
                }
            });
        });

        var commonData4 = {
            title: function (data) {
                return data.url;
            },
            template: function (option) {
                var $option = $(
                    '<div><strong>' + option.text + '</strong></div><small class="text-sm">' + option.title + '</small>'
                );
                return $option;
            }
        };

        var data4 = {
            uri: '/api/data/salesforceconnection',
            title: "Salesforce Connection",
            fields: [
                {
                    selIdentifier: '#selAddSalesforceConnection',
                    resKey: 'id',
                    resValue: 'name',
                    parentIdentifier: '#mdlAddSalesforceDBMapping .modal-content',
                    title: commonData4.title,
                    template: commonData4.template
                },
                {
                    selIdentifier: '#selEditSalesforceConnection',
                    resKey: 'id',
                    resValue: 'name',
                    parentIdentifier: '#mdlEditSalesforceDBMapping .modal-content',
                    title: commonData4.title,
                    template: commonData4.template
                }
            ]
        };

        parameterCtrl.selectAjax(data4);

        var commonData5 = {
            title: function (data) {
                return data.connectionString;
            },
            template: function (option) {
                var $option = $(
                    '<div><strong>' + option.text + '</strong></div><small class="text-sm">' + option.title + '</small>'
                );
                return $option;
            }
        };

        var data5 = {
            uri: '/api/data/databaseconnection',
            title: "Database Connection",
            fields: [
                {
                    selIdentifier: '#selAddDatabaseConnection',
                    resKey: 'id',
                    resValue: 'name',
                    parentIdentifier: '#mdlAddSalesforceDBMapping .modal-content',
                    title: commonData5.title,
                    template: commonData5.template
                },
                {
                    selIdentifier: '#selEditDatabaseConnection',
                    resKey: 'id',
                    resValue: 'name',
                    parentIdentifier: '#mdlEditSalesforceDBMapping .modal-content',
                    title: commonData5.title,
                    template: commonData5.template
                }
            ]
        };

        parameterCtrl.selectAjax(data5);
    },
    fvFields: function () {
        return {
            fields: {
                "Name": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter connection name'
                        }
                    }
                },
                "SfConnId": {
                    validators: {
                        callback: {
                            message: 'Please choose Salesforce Connection',
                            callback: function (input) {
                                return input.value.length > 0;
                            },
                        },
                    },
                },
                "DbConnId": {
                    validators: {
                        callback: {
                            message: 'Please choose Database Connection',
                            callback: function (input) {
                                return input.value.length > 0;
                            },
                        },
                    },
                },
            },
            plugins: parameterCtrl.fv.plugins(),
            init: parameterCtrl.fv.init()
        }
    },
    state: {
        editId: null,
        getEditId: function () {
            return salesforceDbMapping.state.editId;
        }
    }
};

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        salesforceDbMapping.load();
    })();
});

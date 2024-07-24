'use strict';

var project = {
    load: function () {
        var data1 = {
            title: "Project",
            uri: function () {
                return '/api/operations/project';
            },
            identifier: "Project",
            type: "Add",
            fields: project.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAdd(data1);

        var data2 = {
            title: "Project",
            uri: function () {
                return '/api/operations/project/' + project.state.editId;
            },
            identifier: "Project",
            type: "Edit",
            fields: project.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvEdit(data2);

        var data3 = {
            title: "Project",
            uri: function () {
                return '/api/operations/project';
            },
            identifier: "Project",
            columns: ['id', 'name', 'sfDbMappingName']
        };
        parameterCtrl.datatable.loadDataTable(data3);

        $('#dtProject tbody').on('click', '.edit-record', function () {
            var dataId = $(this).data('id');
            var $editForm = $('#frmEditProject');
            $.ajax({
                url: '/api/operations/project/' + dataId,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    project.state.editId = response.id;
                    // Load data to form 
                    $editForm.find('input[name="Name"]').val(response.name);
                    $editForm.find('select[name="SfDbMappingId"]').val(response.sfDbMappingId).trigger('change');
                }
            });
        });

        var commonData4 = {
            title: function (data) {
                return data.sfConnUrl;
            },
            template: function (option) {
                var $option = $(
                    '<div><strong>' + option.text + '</strong></div><small class="text-sm"><div class="mb-0 alert alert-primary d-flex align-items-center" role="alert"><span class="alert-icon text-primary me-2 p-2"><i class="fa-solid fa-database fa-fw"></i></span>' + $(option.element).data('dbConnString') + '</div> <div class="mt-1 mb-0 alert alert-info d-flex align-items-center" role="alert"><span class="alert-icon text-info me-2 p-2"><i class="fa-brands fa-salesforce fa-fw"></i></span>' + $(option.element).data('sfConnUrl') + '</div></small>'
                );
                return $option;
            },
            attributes: [
                {
                    key: "dbConnString",
                    value: "dbConnString"
                },
                {
                    key: "sfConnUrl",
                    value: "sfConnUrl"
                }
            ]
        };

        var data4 = {
            uri: '/api/data/salesforcedbmapping',
            title: "Salesforce Database Mapping",
            fields: [
                {
                    selIdentifier: '#selAddSfDbMapping',
                    resKey: 'id',
                    resValue: 'name',
                    parentIdentifier: '#mdlAddProject .modal-content',
                    title: commonData4.title,
                    attributes: commonData4.attributes,
                    template: commonData4.template
                },
                {
                    selIdentifier: '#selEditSfDbMapping',
                    resKey: 'id',
                    resValue: 'name',
                    parentIdentifier: '#mdlEditProject .modal-content',
                    title: commonData4.title,
                    attributes: commonData4.attributes,
                    template: commonData4.template
                }
            ]
        };

        parameterCtrl.selectAjax(data4);
    },
    fvFields: function () {
        return {
            fields: {
                "Name": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter project name'
                        }
                    }
                },
                "SfDbMappingId": {
                    validators: {
                        callback: {
                            message: 'Please choose Salesforce Database Mapping',
                            callback: function (input) {
                                return input.value.length > 0;
                            },
                        },
                    },
                }
            },
            plugins: parameterCtrl.fv.plugins(),
            init: parameterCtrl.fv.init()
        }
    },
    state: {
        editId: null,
        getEditId: function () {
            return project.state.editId;
        }
    }
};

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        project.load();
    })();
});


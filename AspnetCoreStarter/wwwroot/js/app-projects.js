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
            fields: project.fvWizardFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvWizardAdd(data1);
        /*
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
        */
        var data3 = {
            title: "Project",
            uri: function () {
                return '/api/operations/project';
            },
            identifier: "Project",
            columns: ['id', 'name', 'sourceType', 'destinationType', 'sourceConnName', 'destConnName'],
            customColumnDefs: [
                {
                    // Active
                    targets: 3,
                    searchable: true,
                    title: 'Source Type',
                    orderable: true,
                    render: function (d, type, full, meta) {
                        var data = '';
                        switch (parseInt(full["projectTypeId"])) {
                            case 1:
                                data = '<span class="badge bg-label-primary">DATABASE</span>';
                                break;
                            case 2:
                                data = '<span class="badge bg-label-warning">FILE SOURCE</span>';
                                break;
                        }
                        return data;
                    }
                },
                {
                    // Active
                    targets: 4,
                    searchable: true,
                    title: 'Destination Type',
                    orderable: true,
                    render: function (d, type, full, meta) {
                        var data = '';
                        switch (parseInt(full["destinationTypeId"])) {
                            case 1:
                                data = '<span class="badge bg-label-info"> SALESFORCE</span>';
                                break;
                        }
                        return data;
                    }
                }
            ]
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
    fvWizardFields: function () {
        return [
            {
                generateFormData: true,
                postValidate: function (e, formData, currForm, nextForm) {

                    var srcValue = parseInt(formData.Source);
                    var destValue = parseInt(formData.Destination);
                    var srcTitle = "";
                    var destTitle = "";

                    switch (srcValue) {
                        case 1:
                            srcTitle = "Database Connection";
                            var commonData6 = {
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

                            var data6 = {
                                uri: '/api/data/databaseconnection',
                                title: "Database Connection",
                                fields: [
                                    {
                                        destroyAtStart: true,
                                        selIdentifier: '#selSourceDetails',
                                        resKey: 'id',
                                        resValue: 'name',
                                        name: "SrcDetails",
                                        form: nextForm,
                                        parentIdentifier: '#wzdAddProjectForm2',
                                        title: commonData6.title,
                                        template: commonData6.template
                                    }
                                ]
                            };

                            parameterCtrl.selectAjax(data6);

                            break;
                        case 2:
                            srcTitle = "File Source Connection";
                            var commonData6 = {
                                title: function (data) {
                                    return data.fileLocation;
                                },
                                template: function (option) {
                                    var $option = $(
                                        '<div><strong>' + option.text + '</strong></div><small class="text-sm">' + option.title + '</small>'
                                    );
                                    return $option;
                                }
                            };

                            var data6 = {
                                uri: '/api/data/filesourceconnection',
                                title: "File Source Connection",
                                fields: [
                                    {
                                        destroyAtStart: true,
                                        selIdentifier: '#selSourceDetails',
                                        resKey: 'id',
                                        resValue: 'name',
                                        name: "SrcDetails",
                                        form: nextForm,
                                        parentIdentifier: '#wzdAddProjectForm2',
                                        title: commonData6.title,
                                        template: commonData6.template
                                    }
                                ]
                            };

                            parameterCtrl.selectAjax(data6);
                            break;
                        default:
                            break;
                    }
                    switch (destValue) {
                        case 1:
                        default:
                            destTitle = "Salesforce Connection";
                            var commonData5 = {
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

                            var data5 = {
                                uri: '/api/data/salesforceconnection',
                                title: "Salesforce Connection",
                                fields: [
                                    {
                                        destroyAtStart: true,
                                        selIdentifier: '#selDestDetails',
                                        resKey: 'id',
                                        resValue: 'name',
                                        name: "DestDetails",
                                        form: nextForm,
                                        parentIdentifier: '#wzdAddProjectForm2',
                                        title: commonData5.title,
                                        template: commonData5.template
                                    }
                                ]
                            };
                            break;
                    }

                    parameterCtrl.selectAjax(data5);

                    $("#lblDestDetails").html(destTitle);
                    $("#lblSrcDetails").html(srcTitle);

                },
                fvFields: {
                    fields: {
                        Name: {
                            validators: {
                                notEmpty: {
                                    message: 'Please provide the project'
                                }
                            }
                        },
                        Source: {
                            validators: {
                                notEmpty: {
                                    message: 'Please select a source'
                                }
                            }
                        },
                        Destination: {
                            validators: {
                                notEmpty: {
                                    message: 'Please select a destination'
                                }
                            }
                        }
                    },
                    plugins: parameterCtrl.fvWizard.plugins()
                }
            }, {
                postValidate: function (e, formData, currForm, nextForm) {
                    console.log(formData);
                    return true;
                },
                generateFormData: true,
                fvFields: {
                    fields: {
                        "SrcDetails": {
                            validators: {
                                callback: {
                                    message: 'Please choose Source Connection',
                                    callback: function (input) {
                                        return input.value.length > 0;
                                    },
                                },
                            },
                        },
                        "DestDetails": {
                            validators: {
                                callback: {
                                    message: 'Please choose Destination Connection',
                                    callback: function (input) {
                                        return input.value.length > 0;
                                    },
                                },
                            },
                        }
                    },
                    plugins: parameterCtrl.fvWizard.plugins()
                }
            }]
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


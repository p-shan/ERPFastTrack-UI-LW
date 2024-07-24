'use strict';

$(document).ready(function () {
    loadProjects();
});

var jobs = {
    state: {},
    setMapping: function (index, type, setFlag, dbField, sfField, lookupId, lookUpName) {
        var existingVal = {
            "ExternalFlag": false,
            "LookupFlag": false,
            "Value": "",
            "LookupId": "",
            "LookupName": ""
        };
        var mappingIndex = -1;

        if (isAvail(jobs.state["mapping"])) {
            mappingIndex = jobs.state["mapping"].findIndex(m => m.Key === dbField);
            if (mappingIndex < 0) {
                jobs.state["mapping"].push({
                    Key: dbField, Value: existingVal
                });

                mappingIndex = jobs.state["mapping"].findIndex(m => m.Key === dbField);
            }
            else {
                existingVal = jobs.state["mapping"][mappingIndex].Value;
            }
        }
        else {
            jobs.state["mapping"] = [];
            jobs.state["mapping"].push({
                Key: dbField, Value: existingVal
            });

            mappingIndex = jobs.state["mapping"].findIndex(m => m.Key === dbField);
        }

        switch (type) {
            case "SFFIELD":
                if (setFlag) {
                    jobs.state["mapping"][mappingIndex].Value = {
                        "ExternalFlag": false,
                        "LookupFlag": false,
                        "Value": sfField,
                        "LookupId": "",
                        "LookupName": ""
                    };
                }
                else {
                    jobs.state["mapping"].splice(mappingIndex, 1);
                }
                break;
            case "EXTID":
                if (setFlag) {
                    jobs.state["mapping"][mappingIndex].Value.ExternalFlag = true;
                }
                else {
                    jobs.state["mapping"][mappingIndex].Value.ExternalFlag = false;
                }
                break;
            case "LOOKUP":
                if (setFlag) {
                    jobs.state["mapping"][mappingIndex].Value.LookupFlag = true;
                    jobs.state["mapping"][mappingIndex].Value.LookupId = lookupId;
                    jobs.state["mapping"][mappingIndex].Value.LookupName = lookUpName;
                }
                else {
                    jobs.state["mapping"][mappingIndex].Value.LookupFlag = false;
                    jobs.state["mapping"][mappingIndex].Value.LookupId = "";
                    jobs.state["mapping"][mappingIndex].Value.LookupName = "";
                } break;
            default:
        }
    },
    load: function () {
    },
    genSections: {
        projects: {
            title: "Projects",
            identifier: "Projects",
            postLoad: function (data) {

                var commonData4 = {
                    title: function (data) {
                        return data.name;
                    },
                    template: function (option) {
                        var destinationTypeId = $(option.element).data('destinationTypeId');
                        var projectTypeId = $(option.element).data('projectTypeId');
                        var projectType = "";
                        switch (projectTypeId) {
                            case 1:
                                projectType = "Database ➡️ Salesforce";
                                var $option = $(
                                    `<div>
                                      <h4 class="mb-1 d-flex align-items-center">                    
                                        <strong>${option.text}</strong> <small class="text-sm"> <span class="ms-2 badge bg-label-dark">${projectType}</span></small>
                                      </h4>
                                    </div>
                                    <small class="text-sm">
                                        <div class="mb-0 alert alert-primary d-flex align-items-center" role="alert">
                                        <span class="alert-icon text-primary me-2 p-2">
                                            <i class="fa-solid fa-database fa-fw"></i>
                                        </span>${$(option.element).data('sourceConnString')}
                                        </div>
                                        <div class="mt-1 mb-0 alert alert-info d-flex align-items-center" role="alert">
                                        <span class="alert-icon text-info me-2 p-2">
                                            <i class="fa-brands fa-salesforce fa-fw"></i>
                                        </span>${$(option.element).data('destConnUrl')}
                                        </div>
                                    </small>`
                                );
                                return $option;
                            case 2:
                                var destinationTypeId = $(option.element).data('destinationTypeId');
                                projectType = "File Source ➡️ Salesforce";
                                var $option = $(
                                    `<div>
                                      <h4 class="mb-1 d-flex align-items-center">                    
                                        <strong>${option.text}</strong> <small class="text-sm"> <span class="ms-2 badge bg-label-dark">${projectType}</span></small>
                                      </h4>
                                    </div>
                                    <small class="text-sm">
                                        <div class="mb-0 alert alert-warning d-flex align-items-center" role="alert">
                                        <span class="alert-icon text-warning me-2 p-2">
                                            <i class="fa-solid fa-file fa-fw"></i>
                                        </span>${$(option.element).data('sourceConnString')}
                                        </div>
                                        <div class="mt-1 mb-0 alert alert-info d-flex align-items-center" role="alert">
                                        <span class="alert-icon text-info me-2 p-2">
                                            <i class="fa-brands fa-salesforce fa-fw"></i>
                                        </span>${$(option.element).data('destConnUrl')}
                                        </div>
                                    </small>`
                                );
                                return $option;
                        }


                    },
                    attributes: [
                        {
                            key: "sourceConnString",
                            value: "sourceConnString"
                        },
                        {
                            key: "destConnUrl",
                            value: "destConnUrl"
                        },
                        {
                            key: "projectTypeId",
                            value: "projectTypeId"
                        },
                        {
                            key: "destinationTypeId",
                            value: "destinationTypeId"
                        }
                    ]
                };

                var data4 = {
                    uri: '/api/operations/project',
                    title: "Project",
                    fields: [
                        {
                            selIdentifier: '#selProjects',
                            resKey: 'id',
                            resValue: 'name',
                            parentIdentifier: '#hbsInnersectionProjects .card-body',
                            title: commonData4.title,
                            attributes: commonData4.attributes,
                            template: commonData4.template
                        }
                    ]
                };

                parameterCtrl.selectAjax(data4, function (resp) {
                    $("#btnClearJobs").click(function () {
                        parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.jobs);
                        parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.upsertJobs);
                        parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.mapping);
                        $("#selProjects").prop("disabled", false).trigger("change");
                        $("#selProjects").val("").trigger("change");
                        $("#btnLoadJobs").prop("disabled", false);
                    });

                    $("#btnLoadJobs").click(function () {
                        if ($("#selProjects").val().length > 0) {
                            $.ajax({
                                url: '/api/operations/project/' + $("#selProjects").val(),
                                type: 'GET',
                                contentType: 'application/json',
                                success: function (response) {
                                    jobs.state["selectedProject"] = response;

                                    parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.jobs);
                                    parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.upsertJobs);
                                    parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.mapping);

                                    loadJobs();

                                    $("#selProjects").prop("disabled", true).trigger("change");
                                    $("#btnLoadJobs").prop("disabled", true);
                                }
                            });
                        }
                        else {
                            Swal.fire({
                                title: 'Notification',
                                text: 'Please select the project to proceed.',
                                icon: 'info',
                                customClass: {
                                    confirmButton: 'btn btn-primary'
                                },
                                buttonsStyling: false
                            });
                        }
                    });
                });
            },
            postUnload: function (data) {

            }
        },
        jobs: {
            title: "Jobs",
            identifier: "Jobs",
            data: function () {
                var srcHeaderName = "";
                switch (jobs.state.selectedProject.projectTypeId) {
                    case 1: srcHeaderName = "Query Name";
                        break;
                    case 2: srcHeaderName = "File Detail Name";
                }
                return { srcHeaderName: srcHeaderName, destHeaderName: "Salesforce Object" };
            },
            postLoad: function (data) {
                var data3 = {
                    title: "Jobs",
                    uri: function () {
                        return '/api/operations/job/project/' + $("#selProjects").val();
                    },
                    identifier: "Jobs",
                    columns: ['id', 'name', 'type', 'srcSubDetailName', 'destSubDetailName'],
                    customActionColumnRender: function (d, type, full, meta) {
                        return (
                            '<span class="text-nowrap"><button class="btn btn-sm btn-icon me-2 edit-record" data-id="' + full['id'] + '"><i class="ti ti-edit"></i></button>' +
                            '<button class="btn btn-sm btn-icon delete-record" data-id="' + full['id'] + '"><i class="ti ti-trash"></i></button></span>'
                        );
                    },
                    customColumnDefs: [
                        {
                            // Active
                            targets: 3,
                            searchable: true,
                            title: 'Source Type',
                            orderable: true,
                            render: function (d, type, full, meta) {
                                var data = '';
                                switch (parseInt(full["source"])) {
                                    case 1:
                                        data = '<span class="badge bg-label-info"><i class="ti ti-database"></i> DATABASE</span>';
                                        break;
                                    case 2:
                                        data = '<span class="badge bg-label-warning"><i class="ti ti-file"></i> FILE SOURCE</span>';
                                        break;
                                }
                                return data;
                            }
                        }
                    ],
                    buttonFunc: function () {
                        return ([
                            {
                                text: 'Add Job to Project',
                                className: 'add-new btn btn-primary mb-3 mb-md-0',
                                init: function (api, node, config) {
                                    $(node).removeClass('btn-secondary');
                                },
                                action: function (e, dt, node, config) {
                                    jobs.state["loadedJob"] = null;
                                    jobs.state["mapping"] = null;
                                    parameterCtrl.loadSectionWithCallbacks(jobs.genSections.upsertJobs);

                                    $(".sticky-element").sticky({
                                        topSpacing: $('.layout-navbar').height() + 7,
                                        zIndex: 9
                                    });
                                }
                            }, {
                                text: 'Refresh',
                                className: 'add-new btn btn-warning mb-3 ms-1 mb-md-0',
                                action: function (e, dt, node, config) {
                                    dt.clear().draw();
                                    dt.ajax.reload();
                                }
                            }
                        ]);
                    }
                };
                parameterCtrl.datatable.loadDataTable(data3);

                $('#dtJobs tbody').on('click', '.edit-record', function () {
                    var dataId = $(this).data('id');

                    $.ajax({
                        url: '/api/operations/job/' + dataId,
                        type: 'GET',
                        contentType: 'application/json',
                        success: function (response) {
                            jobs.state["loadedJob"] = response;
                            jobs.state["mapping"] = JSON.parse(JSON.stringify(response.mapping));

                            parameterCtrl.loadSectionWithCallbacks(jobs.genSections.upsertJobs);

                            $(".sticky-element").sticky({
                                topSpacing: $('.layout-navbar').height() + 7,
                                zIndex: 9
                            });
                        }
                    });
                });

            },
            postUnload: function (data) {

            }
        },
        upsertJobs: {
            title: "UpsertJobs",
            identifier: "UpsertJobs",
            postLoad: function (data) {
                if (isAvail(jobs.state.loadedJob)) {
                    $("#txtJobName").val(jobs.state.loadedJob.name);
                }

                $("#btnResetMapping").attr('style', 'display: none !important;');
                $("#btnResetMapping").click(function () {
                    parameterCtrl.loadSectionWithCallbacks(jobs.genSections.mapping);
                });

                $("#btnDiscardJob").click(function () {
                    parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.upsertJobs);
                });

                $("#btnSaveJob").click(function () {

                    let errorMessages = "";

                    let hasExternalId = false;
                    if (isAvail(jobs.state.mapping)) {
                        jobs.state.mapping.forEach((item, index) => {
                            let val = item.Value;
                            if (val && val.ExternalFlag)
                                hasExternalId = true;
                        });
                    }

                    if ($("#txtJobName").val().trim().length <= 0) {
                        errorMessages += "Please provide Job Name.<br>";
                    }

                    if (!hasExternalId) {
                        errorMessages += "Please provide External Id.";
                    }

                    if (!errorMessages) {
                        Swal.fire({
                            text: 'Are you sure you would like to add this job?',
                            icon: 'warning',
                            showCancelButton: true,
                            confirmButtonText: 'Yes',
                            customClass: {
                                confirmButton: 'btn btn-primary me-2',
                                cancelButton: 'btn btn-label-secondary'
                            },
                            buttonsStyling: false
                        }).then(function (result) {
                            if (result.value) {
                                var jsonData = {
                                    name: $("#txtJobName").val(),
                                    pId: $("#selProjects").val(),
                                    queryId: $("#selSourceDetails").val(),
                                    SObjectName: $("#selSfObjects").val(),
                                    mapping: jobs.state.mapping
                                };

                                var method = 'POST';
                                var uri = '/api/operations/job';
                                var successMessage = 'created';
                                var errorMessage = 'adding';
                                if (isAvail(jobs.state["loadedJob"])) {
                                    method = 'PUT';
                                    successMessage = 'updated';
                                    errorMessage = 'updating';
                                    jsonData.id = jobs.state["loadedJob"].id;
                                    uri = '/api/operations/job/' + jsonData.id;
                                }

                                var jsonData = {
                                    name: $("#txtJobName").val(),
                                    pId: $("#selProjects").val(),
                                    srcSubDetailId: $("#selSourceDetails").val(),
                                    destSubDetailName: $("#selSfObjects").val(),
                                    mapping: jobs.state.mapping
                                };

                                $.ajax({
                                    url: uri,
                                    type: method,
                                    contentType: 'application/json',
                                    data: JSON.stringify(jsonData),
                                    success: function (response) {
                                        if (response.status) {
                                            Swal.fire({
                                                title: 'Success!',
                                                text: 'Job has been ' + successMessage + '!',
                                                icon: 'success',
                                                customClass: {
                                                    confirmButton: 'btn btn-primary'
                                                },
                                                buttonsStyling: false
                                            }).then(() => {
                                                parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.upsertJobs);
                                                parameterCtrl.loadSectionWithCallbacks(jobs.genSections.jobs);
                                            });
                                        }
                                        else {
                                            // Handle errors here
                                            Swal.fire({
                                                title: 'Error!',
                                                text: 'There was an error while ' + errorMessage + ' Job - ' + response.errorMessage,
                                                icon: 'error',
                                                customClass: {
                                                    confirmButton: 'btn btn-primary'
                                                },
                                                buttonsStyling: false
                                            });
                                        }
                                    },
                                    error: function (error) {
                                        // Handle errors here
                                        Swal.fire({
                                            title: 'Error!',
                                            text: 'There was an error while ' + errorMessage + ' Job',
                                            icon: 'error',
                                            customClass: {
                                                confirmButton: 'btn btn-primary'
                                            },
                                            buttonsStyling: false
                                        });
                                    }
                                });
                            }
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Notification',
                            html: errorMessages,
                            icon: 'info',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        });
                    }


                });

                $("#btnStartMapping").click(function () {
                    if ($("#selSourceDetails").val().length <= 0) {
                        Swal.fire({
                            title: 'Notification',
                            text: 'Please select the database table to proceed.',
                            icon: 'info',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        });
                    }
                    else if ($("#selSfObjects").val().length <= 0) {
                        Swal.fire({
                            title: 'Notification',
                            text: 'Please select the salesforce object to proceed.',
                            icon: 'info',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        });
                    }
                    else {
                        $("#btnStartMapping").attr('style', 'display: none !important;');
                        $("#btnResetMapping").removeAttr('style');
                        $("#selSourceDetails").prop("disabled", true).trigger("change");
                        $("#selSfObjects").prop("disabled", true).trigger("change");

                        parameterCtrl.loadSectionWithCallbacks(jobs.genSections.mapping);
                        if (isAvail(jobs.state.loadedJob)) {

                        }
                    }
                });

                switch (jobs.state.selectedProject.projectTypeId) {
                    case 1:
                        var data4 = {
                            uri: '/api/data/queryconfiguration/databaseconnection/' + jobs.state.selectedProject.sourceConnId,
                            title: "Database Table",
                            fields: [
                                {
                                    selIdentifier: '#selSourceDetails',
                                    resKey: 'id',
                                    resValue: 'queryName',
                                    parentIdentifier: '#hbsInnersectionUpsertJobs .card-sourceDetails',
                                    changeEvent: function () {
                                        $.ajax({
                                            url: '/api/operations/source/datadetails',
                                            type: 'POST',
                                            data: JSON.stringify({
                                                "Id": $("#selSourceDetails").val(),
                                                "SourceType": jobs.state.selectedProject.projectTypeId
                                            }),
                                            contentType: 'application/json',
                                            success: function (response) {
                                                if (response.error) {
                                                    var sourceName = "";
                                                    switch (jobs.state.selectedProject.projectTypeId) {
                                                        case 1: sourceName = "Database Connection";
                                                            break;

                                                        case 2: sourceName = "File Source Connection";
                                                            break;
                                                    }
                                                    Swal.fire({
                                                        title: sourceName + 'Error!',
                                                        text: 'There was an error while fetching data for source - ' + response.errorMessage,
                                                        icon: 'error',
                                                        customClass: {
                                                            confirmButton: 'btn btn-primary'
                                                        },
                                                        buttonsStyling: false
                                                    });
                                                }
                                                else
                                                    jobs.state["selectedSource"] = response;
                                            }
                                        });
                                    }
                                }
                            ]
                        };
                        parameterCtrl.selectAjax(data4, function (data) {
                            if (isAvail(jobs.state.loadedJob))
                                $("#selSourceDetails").val(jobs.state.loadedJob.srcSubDetailId).prop("disabled", true).trigger("change");
                        }, function (response) {
                            var errorMessage = "";
                            if (response.error) {
                                errorMessage = response.errorMessage;
                            }
                            else {
                                errorMessage = "Data not available for sources";
                            }

                            Swal.fire({
                                title: 'Error!',
                                text: 'There was an error while fetching data for sources - ' + errorMessage,
                                icon: 'error',
                                customClass: {
                                    confirmButton: 'btn btn-primary'
                                },
                                buttonsStyling: false
                            }).then(() => {
                                parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.upsertJobs);
                            });
                        });
                        break;
                    case 2:
                        var data4 = {
                            uri: '/api/data/filesourcedetail/filesourceconnection/' + jobs.state.selectedProject.sourceConnId,
                            title: "File Source",
                            fields: [
                                {
                                    selIdentifier: '#selSourceDetails',
                                    resKey: 'id',
                                    resValue: 'fileSourceDetailName',
                                    parentIdentifier: '#hbsInnersectionUpsertJobs .card-sourceDetails',
                                    changeEvent: function () {
                                        $.ajax({
                                            url: '/api/operations/source/datadetails',
                                            type: 'POST',
                                            data: JSON.stringify({
                                                "Id": $("#selSourceDetails").val(),
                                                "SourceType": jobs.state.selectedProject.projectTypeId
                                            }),
                                            contentType: 'application/json',
                                            success: function (response) {
                                                if (response.error) {
                                                    Swal.fire({
                                                        title: 'Error!',
                                                        text: 'There was an error while fetching data for selected source - ' + response.errorMessage,
                                                        icon: 'error',
                                                        customClass: {
                                                            confirmButton: 'btn btn-primary'
                                                        },
                                                        buttonsStyling: false
                                                    });
                                                }
                                                else
                                                    jobs.state["selectedSource"] = response;
                                            }
                                        });
                                    }
                                }
                            ]
                        };
                        parameterCtrl.selectAjax(data4, function (data) {
                            if (isAvail(jobs.state.loadedJob))
                                $("#selSourceDetails").val(jobs.state.loadedJob.srcSubDetailId).prop("disabled", true).trigger("change");
                        }, function (response) {
                            var errorMessage = "";
                            if (response.error) {
                                errorMessage = response.errorMessage;
                            }
                            else {
                                errorMessage = "Data not available for sources";
                            }

                            Swal.fire({
                                title: 'Error!',
                                text: 'There was an error while fetching data for sources - ' + errorMessage,
                                icon: 'error',
                                customClass: {
                                    confirmButton: 'btn btn-primary'
                                },
                                buttonsStyling: false
                            }).then(() => {
                                parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.upsertJobs);
                            });
                        });
                        break;
                }

                var data5 = {
                    uri: '/api/SalesforceOps/getsfobjects/' + jobs.state.selectedProject.destConnId,
                    title: "Salesforce Object",
                    fields: [
                        {
                            selIdentifier: '#selSfObjects',
                            resKey: 'name',
                            resValue: 'name',
                            parentIdentifier: '#hbsInnersectionUpsertJobs .card-sfobjects',
                            changeEvent: function () {
                                var jsonData = {
                                    salesforceConnectionId: jobs.state.selectedProject.destConnId,
                                    objectName: $("#selSfObjects").val(),
                                };
                                $.ajax({
                                    url: '/api/SalesforceOps/getsfobjectdetails',
                                    type: 'POST',
                                    contentType: 'application/json',
                                    data: JSON.stringify(jsonData),
                                    success: function (response) {
                                        if (response.error) {
                                            Swal.fire({
                                                title: 'Error!',
                                                text: 'There was an error while fetching data for destination - ' + response.errorMessage,
                                                icon: 'error',
                                                customClass: {
                                                    confirmButton: 'btn btn-primary'
                                                },
                                                buttonsStyling: false
                                            });
                                        }
                                        else
                                            jobs.state["selectedSfObject"] = response;
                                    }
                                });
                            }
                        }
                    ]
                };
                parameterCtrl.selectAjax(data5, function (data) {
                    if (isAvail(jobs.state.loadedJob))
                        $("#selSfObjects").val(jobs.state.loadedJob.destSubDetailName).prop("disabled", true).trigger("change");
                }, function (response) {
                    Swal.fire({
                        title: 'Token expired!',
                        text: response.errorMessage,
                        icon: 'error',
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false
                    }).then(() => {
                        parameterCtrl.unloadSectionWithCallbacks(jobs.genSections.upsertJobs);
                    });
                });
            },
            data: function () {
                return ({
                    projectTypeId: jobs.state.selectedProject.projectTypeId,
                    sfConnName: jobs.state.selectedProject.destConnName,
                    sourceConnName: jobs.state.selectedProject.sourceConnName
                });
            },
            postUnload: function (data) {

            }
        },
        mapping: {
            title: "Mapping",
            identifier: "Mapping",
            postLoad: function (data) {
                if (isAvail(jobs.state["loadedJob"])) {
                    for (var i = 0; i < jobs.state.selectedSource.details.fields.length; i++) {
                        var map = jobs.state.loadedJob.mapping.find(obj => obj.Key === jobs.state.selectedSource.details.fields[i].fieldId);
                        if (isAvail(map)) {
                            jobs.setMappingToExtraMappingSection(map.Value.Value, i, map);
                        }
                    }
                    jobs.state["mapping"] = JSON.parse(JSON.stringify(jobs.state.loadedJob.mapping));
                }
                else
                    jobs.state["mapping"] = null;

                $('#floatingInput').on('keyup', function () {
                    var searchText = $(this).val().toLowerCase();
                    var listItems = $('.list-group-item');

                    listItems.each(function () {
                        var text = $(this).text().toLowerCase();

                        if (text.indexOf(searchText) !== -1) {
                            $(this).show();
                        } else {
                            $(this).hide();
                        }
                    });
                });
                $('.list-group-item').on('click', function () {
                    jobs.setMappingToExtraMappingSection($(this).data("id"), jobs.state.selectedSfFieldIndex);
                    $('#backDropModal').modal('hide');
                });

                $('.btnSfDb').on('click', function () {
                    jobs.state.selectedSfFieldIndex = $(this).data("buttonindex");
                });
            },
            data: function () {
                return ({
                    fields: jobs.state.selectedSource.details.fields,
                    sfObjectData: jobs.state.selectedSfObject.data
                });
            },
            postUnload: function (data) {
            }
        }
    },
    setMappingToExtraMappingSection: function (id, index, mapData) {
        var sfFieldId = id;
        var dbField = jobs.state.selectedSource.details.fields[index].fieldId;

        // todo: add mapping - [DONE]
        jobs.setMapping(index, "SFFIELD", true, dbField, sfFieldId);

        var fieldData = jobs.state.selectedSfObject.data.find(obj => obj.id === sfFieldId);
        var selectedItemText = fieldData.name;
        if (fieldData.isExternalId || fieldData.isLookup) {
            var data = {
                index: index,
                isExternalId: fieldData.isExternalId,
                isLookup: fieldData.isLookup
            };

            var source = document.getElementById("hbs" + "ExtraMappingDetail").innerHTML;
            var template = Handlebars.compile(source);
            $(".hbsOutersection" + "ExtraMappingDetail" + index).html(template(data));

            if (fieldData.isExternalId) {
                $('.chkbxExternalId' + index).change(function () {
                    if (this.checked) {
                        // todo: set ExtId - [DONE]
                        jobs.setMapping(index, "EXTID", true, dbField, sfFieldId);
                    }
                    else {
                        // todo: unset ExtId - [DONE]
                        jobs.setMapping(index, "EXTID", false, dbField, sfFieldId);
                    }
                });

                if (isAvail(mapData) && mapData.Value.ExternalFlag)
                    $('.chkbxExternalId' + index).prop('checked', true).trigger("change");
            }

            if (fieldData.isLookup) {
                var lookupName = fieldData.lookupName;
                $('.chkbxLookup' + index).change(function () {
                    if (this.checked) {
                        // todo: set lookup flag
                        StartPageLoader();
                        var lookupDataReq = {
                            descr: lookupName,
                            lookupName: lookupName,
                            selIdentifier: '.selLookup' + index,
                            resKey: 'id',
                            resValue: 'name',
                            parentIdentifier: ".hbsOutersection" + "ExtraMappingDetail" + index + ' .card-body',
                            changeEvent: function () {
                                // todo: add lookup mapping - [DONE]
                                jobs.setMapping(index, "LOOKUP", true, dbField, sfFieldId, $('.selLookup' + index).val(), lookupName);
                            },
                            successCb: function (data) {
                                if (isAvail(mapData) && mapData.Value.LookupFlag) {
                                    $('.selLookup' + index).val(mapData.Value.LookupId).trigger("change");
                                }
                            }
                        };
                        jobs.setLookupToSelect(lookupDataReq);

                    }
                    else {
                        $('.selLookup' + index).select2('destroy');
                        // todo: remove lookup mapping, unset lookup flag - [DONE]
                        jobs.setMapping(index, "LOOKUP", false, dbField, sfFieldId);
                    }
                });

                if (isAvail(mapData) && mapData.Value.LookupFlag)
                    $('.chkbxLookup' + index).prop('checked', true).trigger("change");
            }
        }
        $(".sfField" + index).html(selectedItemText);
    },
    customDestSection: {
        extraMapping: {
            title: "ExtraMapping",
            identifier: "ExtraMapping",
            isIteratable: true,
            postLoad: function () {

            }
        }
    },
    getCustomSection: function (name, parentIdentifier) {
        var section = JSON.parse(JSON.stringify(jobs.customDestSection[name]));
        section.parentIdentifier = parentIdentifier;
        return section;
    },
    lookups: {

    },
    setLookup: function (name, callback) {
        if (isAvail(jobs.lookups[name])) {
            callback(null, jobs.lookups[name]);
        }
        else {
            var jsonData = {
                salesforceConnectionId: jobs.state.selectedProject.destConnId,
                objectName: name,
            };
            $.ajax({
                url: '/api/SalesforceOps/getsfobjectdetails',
                method: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(jsonData),
                success: function (data) {
                    jobs.lookups[name] = data;
                    callback(null, jobs.lookups[name]);
                },
                error: function (error) {
                    callback('Failed to fetch data', null);
                }
            });
        }
    },
    setLookupToSelect: function (data) {
        var callback = function (errMsg, response) {
            if (isAvail(errMsg)) {
                Swal.fire({
                    title: 'Error!',
                    text: 'There was an error loading lookup : ' + errMsg,
                    icon: 'error',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    buttonsStyling: false
                });
            }
            else {
                if (Array.isArray(response.data) && response.data.length > 0) {
                    var selectBox = $(data.selIdentifier);
                    response.data.forEach(function (option) {
                        if (isFunction(data.title)) {
                            var $optionElement = $('<option/>', {
                                value: option[data.resKey],
                                title: data.title(option)
                            }).text(option[data.resValue]);

                            if (Array.isArray(data.attributes) && data.attributes.length > 0) {
                                data.attributes.forEach(function (o) {
                                    $optionElement.data(o.key, option[o.value]);
                                });
                            }

                            selectBox.append($optionElement);
                        }
                        else {
                            var $optionElement = $('<option/>', {
                                value: option[data.resKey]
                            }).text(option[data.resValue]);

                            if (Array.isArray(data.attributes) && data.attributes.length > 0) {
                                data.attributes.forEach(function (o) {
                                    $optionElement.data(o.key, option[o.value]);
                                });
                            }

                            selectBox.append($optionElement);
                        }
                    });

                    if (isFunction(data.changeEvent))
                        selectBox.on('change', data.changeEvent);

                    if (isFunction(data.template)) {
                        selectBox.select2({
                            placeholder: "Select an " + data.descr,
                            dropdownParent: $(data.parentIdentifier),
                            templateResult: data.template,
                            dropdownCssClass: 'auto-height'
                        });
                    }
                    else
                        selectBox.select2({
                            placeholder: "Select an " + data.descr,
                            dropdownParent: $(data.parentIdentifier),
                            dropdownCssClass: 'auto-height'
                        });
                }

                if (isFunction(data.successCb))
                    data.successCb(response);
            }

            //selectBox.on('select2:select', function (e) {
            //    var data = e.params.data;
            //    setCookie("selectedOrg", data.id);
            //    location.reload();
            //});

            StopPageLoader();
        }

        jobs.setLookup(data.lookupName, callback);
    }
}

function loadProjects() {
    parameterCtrl.loadSectionWithCallbacks(jobs.genSections.projects);
}

function loadJobs() {
    $(function () {
        parameterCtrl.loadSectionWithCallbacks(jobs.genSections.jobs);

        // Delete Record
        $('.datatables-jobs tbody').on('click', '.delete-record', function () {
            dt_permission.row($(this).parents('tr')).remove().draw();
        });
    });
}

'use strict';


var fileSourceDetail = {
    load: function () {
        var data1 = {
            title: "File Source Detail",
            ajaxData: null,
            type: 'Add',
            identifier: "FileSourceDetail",
            columns: ['fileSourceDetailName', 'fileName', 'delimiter', 'dateFieldFormat', 'timeFieldFormat', 'archiveFileName', 'hasHeader']
        };
        var dt1 = parameterCtrl.staticDatatable.loadDataTable(data1);
        fileSourceDetail.state.addDatatable = dt1;

        var data3 = {
            title: "File Source Detail",
            ajaxData: null,
            type: 'Edit',
            identifier: "FileSourceDetail",
            columns: ['fileSourceDetailName', 'fileName', 'delimiter', 'dateFieldFormat', 'timeFieldFormat', 'archiveFileName', 'hasHeader']
        };
        var dt2 = parameterCtrl.staticDatatable.loadDataTable(data3);
        fileSourceDetail.state.editDatatable = dt2;

        var data2 = {
            title: "File Source Detail",
            identifier: "FileSourceDetail",
            type: "Add",
            datatable: {
                getInstance: function () {
                    if (fileSourceConnection.state.actionType == "Add")
                        return fileSourceDetail.state.addDatatable;
                    return fileSourceDetail.state.editDatatable;
                }
            },
            fields: fileSourceDetail.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAddToTable(data2);

        var data2 = {
            title: "File Source Detail",
            identifier: "FileSourceDetail",
            type: "Edit",
            datatable: {
                getInstance: function () {
                    if (fileSourceConnection.state.actionType == "Add")
                        return fileSourceDetail.state.addDatatable;
                    return fileSourceDetail.state.editDatatable;
                }
            },
            fields: fileSourceDetail.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            },
            getRowIndex: function () {
                return fileSourceDetail.state.rowIndex;
            }
        };
        parameterCtrl.fvEditToTable(data2);

        $(['#dtAddFileSourceDetail', '#dtEditFileSourceDetail']).each(function (index, item) {
            $(item + ' tbody').on('click', '.delete-record', function () {
                var row = $(this).closest('tr');
                fileSourceDetail.state.getDatatable().row(row).remove().draw(false);
            });

            $(item + ' tbody').on('click', '.edit-record', function () {
                var row = $(this).closest('tr');
                var rowData = fileSourceDetail.state.getDatatable().row(row).data();
                fileSourceDetail.state.rowIndex = fileSourceDetail.state.getDatatable().row(this).index();
                var $editForm = $('#frmEditFileSourceDetail');
                // Load data to form
                $editForm.find('input[name="fileSourceDetailName"]').val(rowData.fileSourceDetailName);
                $editForm.find('input[name="fileName"]').val(rowData.fileName);
                $editForm.find('input[name="delimiter"]').val(rowData.delimiter);
                $editForm.find('input[name="dateFieldFormat"]').val(rowData.dateFieldFormat);
                $editForm.find('input[name="timeFieldFormat"]').val(rowData.timeFieldFormat);
                $editForm.find('input[name="archiveFileName"]').val(rowData.archiveFileName);
                $editForm.find('input[name="hasHeader"]').prop('checked', rowData.hasHeader);
            });
        });
    },
    fvFields: function () {
        return {
            // ['fileSourceDetailName', 'fileName', 'delimiter', 'dateFieldFormat', 'timeFieldFormat', 'archiveFileName', 'hasHeader']
            fields: {
                "fileSourceDetailName": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter file source detail name'
                        }
                    }
                },
                "fileName": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter file name'
                        }
                    }
                },
                "delimiter": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter delimiter'
                        }
                    }
                },
                "dateFieldFormat": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter date field format'
                        }
                    }
                },
                "timeFieldFormat": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter time field format'
                        }
                    }
                },
                "archiveFileName": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter date archive file name'
                        }
                    }
                },
                hasHeader: {
                    validators: {
                    }
                },
            },
            plugins: parameterCtrl.fv.plugins(),
            init: parameterCtrl.fv.init()
        }
    },
    state: {
        rowIndex: null,
        editDatatable: null,
        addDatatable: null,
        getDatatable: function () {
            if (fileSourceConnection.state.actionType == "Add")
                return fileSourceDetail.state.addDatatable;
            return fileSourceDetail.state.editDatatable;
        }
    }
};


var fileSourceConnection = {
    load: function () {
        var data1 = {
            title: "File Source Connection",
            uri: function () {
                return '/api/data/fileSourceConnection';
            },
            identifier: "FileSourceConnection",
            type: "Add",
            datatable: {
                getInstance: function () { return fileSourceDetail.state.addDatatable },
                name: "FileSourceDetails"
            },
            fields: fileSourceConnection.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAdd(data1);

        var data2 = {
            title: "File Source Connection",
            uri: function () {
                return '/api/data/filesourceconnection/' + fileSourceConnection.state.editId;
            },
            identifier: "FileSourceConnection",
            type: "Edit",
            datatable: {
                getInstance: function () { return fileSourceDetail.state.editDatatable },
                name: "FileSourceDetails"
            },
            fields: fileSourceConnection.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvEdit(data2);

        var data3 = {
            title: "File Source Connection",
            uri: function () {
                return '/api/data/filesourceconnection';
            },
            identifier: "FileSourceConnection",
            columns: ['id', 'name', 'fileLocation', 'archiveLocation']
        };
        parameterCtrl.datatable.loadDataTable(data3);

        $('#dtFileSourceConnection tbody').on('click', '.edit-record', function () {
            var dataId = $(this).data('id');
            var $editForm = $('#frmEditFileSourceConnection');
            $.ajax({
                url: '/api/data/filesourceconnection/' + dataId,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    fileSourceConnection.state.editId = response.id;

                    // Load data to form 
                    $editForm.find('input[name="Name"]').val(response.name);
                    $editForm.find('input[name="FileLocation"]').val(response.fileLocation);
                    $editForm.find('input[name="ArchiveLocation"]').val(response.archiveLocation);

                    if (Array.isArray(response.fileSourceDetails)) {
                        fileSourceDetail.state.editDatatable.clear().draw();
                        fileSourceDetail.state.editDatatable.rows.add(response.fileSourceDetails).draw();
                    }
                }
            });
        });

        $('#mdlAddFileSourceConnection').on('shown.bs.modal', function () {
            fileSourceConnection.state.actionType = "Add";
        });

        $('#mdlEditFileSourceConnection').on('shown.bs.modal', function () {
            fileSourceConnection.state.actionType = "Edit";
        });
        fileSourceDetail.load();
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
                "FileLocation": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter file location'
                        }
                    }
                },
                "ArchiveLocation": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter archive location'
                        }
                    }
                }
            },
            plugins: parameterCtrl.fv.plugins(),
            init: parameterCtrl.fv.init()
        }
    },
    state: {
        editId: null,
        getEditId: function () {
            return fileSourceConnection.state.editId;
        }
    }
};

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        fileSourceConnection.load();
    })();
});

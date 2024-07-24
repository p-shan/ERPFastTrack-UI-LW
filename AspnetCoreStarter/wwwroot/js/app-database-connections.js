'use strict';


var queryConfiguration = {
    load: function () {
        var data1 = {
            title: "Query Configuration",
            ajaxData: null,
            type: 'Add',
            identifier: "QueryConfiguration",
            columns: ['queryName', 'queryDetails']
        };
        var dt1 = parameterCtrl.staticDatatable.loadDataTable(data1);
        queryConfiguration.state.addDatatable = dt1;

        var data3 = {
            title: "Query Configuration",
            ajaxData: null,
            type: 'Edit',
            identifier: "QueryConfiguration",
            columns: ['queryName', 'queryDetails']
        };
        var dt2 = parameterCtrl.staticDatatable.loadDataTable(data3);
        queryConfiguration.state.editDatatable = dt2;

        var data2 = {
            title: "Query Configuration",
            identifier: "QueryConfiguration",
            type: "Add",
            datatable: {
                getInstance: function () {
                    if (databaseConnection.state.actionType == "Add")
                        return queryConfiguration.state.addDatatable;
                    return queryConfiguration.state.editDatatable;
                }
            },
            fields: queryConfiguration.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAddToTable(data2);

        var data2 = {
            title: "Query Configuration",
            identifier: "QueryConfiguration",
            type: "Edit",
            datatable: {
                getInstance: function () {
                    if (databaseConnection.state.actionType == "Add")
                        return queryConfiguration.state.addDatatable;
                    return queryConfiguration.state.editDatatable;
                }
            },
            fields: queryConfiguration.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            },
            getRowIndex: function () {
                return queryConfiguration.state.rowIndex;
            }
        };
        parameterCtrl.fvEditToTable(data2);

        $(['#dtAddQueryConfiguration', '#dtEditQueryConfiguration']).each(function (index, item) {
            $(item + ' tbody').on('click', '.delete-record', function () {
                var row = $(this).closest('tr');
                queryConfiguration.state.getDatatable().row(row).remove().draw(false);
            });

            $(item + ' tbody').on('click', '.edit-record', function () {
                var row = $(this).closest('tr');
                debugger;
                var rowData = queryConfiguration.state.getDatatable().row(row).data();
                queryConfiguration.state.rowIndex = queryConfiguration.state.getDatatable().row(this).index();
                var $editForm = $('#frmEditQueryConfiguration');
                // Load data to form 
                $editForm.find('input[name="queryName"]').val(rowData.queryName);
                $editForm.find('textarea[name="queryDetails"]').val(rowData.queryDetails);
            });
        });
    },
    fvFields: function () {
        return {
            fields: {
                "queryName": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter query name'
                        }
                    }
                },
                "queryDetails": {
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
        rowIndex: null,
        editDatatable: null,
        addDatatable: null,
        getDatatable: function () {
            if (databaseConnection.state.actionType == "Add")
                return queryConfiguration.state.addDatatable;
            return queryConfiguration.state.editDatatable;
        }
    }
};

var databaseConnection = {
    load: function () {
        var data1 = {
            title: "Database Connection",
            uri: function () {
                return '/api/data/databaseconnection';
            },
            identifier: "DatabaseConnection",
            type: "Add",
            datatable: {
                getInstance: function () { return queryConfiguration.state.addDatatable },
                name: "QueryConfigurations"
            },
            fields: databaseConnection.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAdd(data1);

        var data2 = {
            title: "Database Connection",
            uri: function () {
                return '/api/data/databaseconnection/' + databaseConnection.state.editId;
            },
            identifier: "DatabaseConnection",
            type: "Edit",
            datatable: {
                getInstance: function () { return queryConfiguration.state.editDatatable },
                name: "QueryConfigurations"
            },
            fields: databaseConnection.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvEdit(data2);

        var data3 = {
            title: "Database Connection",
            uri: function () {
                return '/api/data/databaseconnection';
            },
            identifier: "DatabaseConnection",
            columns: ['id', 'name', 'connectionString']
        };
        parameterCtrl.datatable.loadDataTable(data3);

        $('#dtDatabaseConnection tbody').on('click', '.edit-record', function () {
            var dataId = $(this).data('id');
            var $editForm = $('#frmEditDatabaseConnection');
            $.ajax({
                url: '/api/data/databaseconnection/' + dataId,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    databaseConnection.state.editId = response.id;
                    // Load data to form 
                    $editForm.find('input[name="Name"]').val(response.name);
                    $editForm.find('textarea[name="ConnectionString"]').val(response.connectionString);

                    if (Array.isArray(response.queryConfigurations))
                        queryConfiguration.state.editDatatable.rows.add(response.queryConfigurations).draw();
                }
            });
        });

        $("#btnEdtTestConnection").on('click', function () {
            var $editForm = $('#frmEditDatabaseConnection');
            var connectionStr = $editForm.find('textarea[name="ConnectionString"]').val();
            var data = {
                connectionString: connectionStr
            };
            $.ajax({
                url: '/api/operations/utility/testconnection',
                data: JSON.stringify(data),
                type: 'POST',
                contentType: 'application/json',
                success: function (response) {
                    if (response.status) {
                        Swal.fire({
                            title: 'Success!',
                            text: 'Connection Successful!',
                            icon: 'success',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Error!',
                            text: 'Connection Successful. Reason' + response.errorMessage + '!',
                            icon: 'error',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        });
                    }
                }
            });
        });

        $("#btnAddTestConnection").on('click', function () {
            var $addForm = $('#frmAddDatabaseConnection');
            var connectionStr = $addForm.find('textarea[name="ConnectionString"]').val();
            var data = {
                connectionString: connectionStr
            };
            $.ajax({
                url: '/api/operations/utility/testconnection',
                data: JSON.stringify(data),
                type: 'POST',
                contentType: 'application/json',
                success: function (response) {
                    if (response.status) {
                        Swal.fire({
                            title: 'Success!',
                            text: 'Connection Successful!',
                            icon: 'success',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        });
                    }
                    else {
                        Swal.fire({
                            title: 'Error!',
                            text: 'Connection unsuccessful. Reason : ' + response.errorMessage + '!',
                            icon: 'error',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        });
                    }
                }
            });
        });

        $('#mdlAddDatabaseConnection').on('shown.bs.modal', function () {
            databaseConnection.state.actionType = "Add";
        });

        $('#mdlEditDatabaseConnection').on('shown.bs.modal', function () {
            databaseConnection.state.actionType = "Edit";
        });
        queryConfiguration.load();
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
                "ConnectionString": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter connection string'
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
            return databaseConnection.state.editId;
        }
    }
};

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        databaseConnection.load();
    })();
});

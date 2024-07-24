'use strict';

var manageOrganization = {
    load: function () {
        var data1 = {
            title: "Manage Organization",
            uri: function () {
                return '/api/Organization/postusers';
            },
            identifier: "ManageOrganization",
            type: "Add",
            fields: manageOrganization.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAdd(data1);

        var data3 = {
            title: "User",
            data: "users",
            uri: function () {
                return '/api/Organization/getusers';
            }, //dtQueryConfiguration, mdlAddQueryConfiguration
            identifier: "ManageOrganization",
            columns: ['id', 'email', 'name', 'role'],
            customActionColumnRender: function (d, type, full, meta) {
                return (
                    '<span class="text-nowrap"><button class="btn btn-sm btn-icon me-2 view-record"><i class="ti ti-eye"></i></button><span>'
                );
            },
            customColumnDefs: [
                {
                    // User Role
                    targets: 4,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        var $assignedTo = full['role'],
                            $output = '';
                        var roleBadgeObj = {
                            ADMINISTRATOR: '<span class="badge bg-label-primary m-1">Administrator</span>',
                            EDITOR: '<span class="badge bg-label-warning m-1">Editor</span>',
                            EXECUTOR: '<span class="badge bg-label-success m-1">Executor</span>'
                        };

                        $output += roleBadgeObj[$assignedTo];
                        return '<span class="text-nowrap">' + $output + '</span>';
                    }
                }
            ]
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
                    manageOrganization.state.editId = response.id;
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
                "Name": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter query name'
                        }
                    }
                },
                "EmailId": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter email address'
                        },
                        emailAddress: {
                            message: 'The value is not a valid email address'
                        }
                    }
                },
                "UserPassword": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter password'
                        }
                    }
                },
                "OrgRole": {
                    validators: {
                        callback: {
                            message: 'Please select the user role',
                            callback: function (input) {
                                // Get the selected options
                                var data = orgRoleField.val();
                                return data != null && data != "" && data != undefined;
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
        editId: null
    }
};

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        manageOrganization.load();
    })();
});

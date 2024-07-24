/**
 * App user list (jquery)
 */

'use strict';

$(function () {
    var dataTablePermissions = $('.datatables-manageOrg'),
        dt_permission,
        userList = '/Apps/Users/List';

    // Users List datatable
    if (dataTablePermissions.length) {
        dt_permission = dataTablePermissions.DataTable({
            ajax: {
                url: "/api/Organization/getusers",
                dataSrc: 'users',
                complete: function () {
                }
            },
            columns: [
                // columns according to JSON
                { data: '' },
                { data: 'name' },
                { data: 'email' },
                { data: 'role' },
                { data: '' }
            ],
            columnDefs: [
                {
                    // For Responsive
                    className: 'control',
                    orderable: false,
                    searchable: false,
                    responsivePriority: 2,
                    targets: 0,
                    render: function (data, type, full, meta) {
                        return '';
                    }
                },
                {
                    // Name
                    targets: 1,
                    render: function (data, type, full, meta) {
                        var $name = full['name'];
                        return '<span class="text-nowrap">' + $name + '</span>';
                    }
                },
                {
                    // Name
                    targets: 2,
                    render: function (data, type, full, meta) {
                        var $name = full['email'];
                        return '<span class="text-nowrap">' + $name + '</span>';
                    }
                },
                {
                    // User Role
                    targets: 3,
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
                },
                {
                    // Actions
                    targets: -1,
                    searchable: false,
                    title: 'Actions',
                    orderable: false,
                    render: function (data, type, full, meta) {
                        return (
                            '<span class="text-nowrap"><button class="btn btn-sm btn-icon me-2" data-bs-target="#editPermissionModal" data-bs-toggle="modal" data-bs-dismiss="modal"><i class="ti ti-edit"></i></button>' +
                            '<button class="btn btn-sm btn-icon delete-record"><i class="ti ti-trash"></i></button></span>'
                        );
                    }
                }
            ],
            order: [[1, 'asc']],
            dom:
                '<"row mx-1"' +
                '<"col-sm-12 col-md-3" l>' +
                '<"col-sm-12 col-md-9"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-md-end justify-content-center flex-wrap me-1"<"me-3"f>B>>' +
                '>t' +
                '<"row mx-2"' +
                '<"col-sm-12 col-md-6"i>' +
                '<"col-sm-12 col-md-6"p>' +
                '>',
            language: {
                sLengthMenu: 'Show _MENU_',
                search: 'Search',
                searchPlaceholder: 'Search..'
            },
            // Buttons with Dropdown
            buttons: [
                {
                    text: 'Add User to Organization',
                    className: 'add-new btn btn-primary mb-3 mb-md-0',
                    attr: {
                        'data-bs-toggle': 'modal',
                        'data-bs-target': '#addUserToOrgModal'
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('btn-secondary');
                    }
                }
            ],
            // For responsive popup
            responsive: {
                details: {
                    display: $.fn.dataTable.Responsive.display.modal({
                        header: function (row) {
                            var data = row.data();
                            return 'Details of ' + data['name'];
                        }
                    }),
                    type: 'column',
                    renderer: function (api, rowIdx, columns) {
                        var data = $.map(columns, function (col, i) {
                            return col.title !== '' // ? Do not show row in modal popup if title is blank (for check box)
                                ? '<tr data-dt-row="' +
                                col.rowIndex +
                                '" data-dt-column="' +
                                col.columnIndex +
                                '">' +
                                '<td>' +
                                col.title +
                                ':' +
                                '</td> ' +
                                '<td>' +
                                col.data +
                                '</td>' +
                                '</tr>'
                                : '';
                        }).join('');

                        return data ? $('<table class="table"/><tbody />').append(data) : false;
                    }
                }
            },
            initComplete: function () {
                // Adding role filter once table initialized
                this.api()
                    .columns(3)
                    .every(function () {
                        var column = this;
                        var select = $(
                            '<select id="UserRole" class="form-select text-capitalize"><option value=""> Select Role </option></select>'
                        )
                            .appendTo('.user_role')
                            .on('change', function () {
                                var val = $.fn.dataTable.util.escapeRegex($(this).val());
                                column.search(val ? '^' + val + '$' : '', true, false).draw();
                            });

                        column
                            .data()
                            .unique()
                            .sort()
                            .each(function (d, j) {
                                select.append('<option value="' + d + '" class="text-capitalize">' + d + '</option>');
                            });
                    });
            }
        });
    }

    // Delete Record
    $('.datatables-manageOrg tbody').on('click', '.delete-record', function () {
        dt_permission.row($(this).parents('tr')).remove().draw();
    });

    // Filter form control to default size
    // ? setTimeout used for multilingual table initialization
    setTimeout(() => {
        $('.dataTables_filter .form-control').removeClass('form-control-sm');
        $('.dataTables_length .form-select').removeClass('form-select-sm');
    }, 300);
});

$(document).on('ready', function (e) {
    $("#selOrgRole").select2({
        placeholder: 'Select the user role'
    });
});

// Add user to organization form validation
document.addEventListener('DOMContentLoaded', function (e) {    
    (function () {
        const form = document.getElementById('addUserToOrgForm');
        const orgRoleField = jQuery(form.querySelector('[name="OrgRole"]'));
        FormValidation.formValidation(form, {
            fields: {
                "UserEmail": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter organization name'
                        }
                    }
                },
                "OrgRole": {
                    validators: {
                        callback: {
                            message: 'Please select the user role',
                            callback: function (input) {
                                debugger;
                                // Get the selected options
                                var data = orgRoleField.val();
                                return data != null && data != "" && data != undefined;
                            },
                        },
                    },
                }
            },
            plugins: {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap5: new FormValidation.plugins.Bootstrap5({
                    // Use this for enabling/changing valid/invalid class
                    // eleInvalidClass: '',
                    eleValidClass: '',
                    rowSelector: ".form-field"
                }),
                submitButton: new FormValidation.plugins.SubmitButton(),
                // Submit the form when all fields are valid
                // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
                autoFocus: new FormValidation.plugins.AutoFocus()
            },
            init: instance => {
                instance.on('plugins.message.placed', function (e) {
                    if (e.element.parentElement.classList.contains('input-group')) {
                        e.element.parentElement.insertAdjacentElement('afterend', e.messageElement);
                    }
                });
            }
        }).on('core.form.valid', function (e) {
            var json = {};
            $.each(e.formValidation.form, function (index, item) {
                if (e.formValidation.fields[item.name] != undefined) {
                    json[item.name] = item.value;
                }
            });

            $.ajax({
                url: '/api/Organization/postusers',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(json),
                success: function (response) {
                    if (response.status) {
                        Swal.fire({
                            title: 'Success!',
                            text: 'User has been added to the Organization!',
                            icon: 'success',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        }).then(() => {
                            $('.datatables-manageOrg').DataTable().ajax.reload();
                        });
                        $('#addUserToOrgModal').modal('hide');
                    }
                    else {
                        Swal.fire({
                            title: 'Error!',
                            text: response.errorMessage,
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
                        text: 'There was an error while adding user organization!',
                        icon: 'error',
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false
                    });
                }
            });
        });;
    })();
});

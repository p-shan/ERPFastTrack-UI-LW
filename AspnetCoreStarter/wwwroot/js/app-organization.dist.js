'use strict';

$(function () {
    var dataTablePermissions = $('.datatables-organizations'),
        dt_permission,
        userList = '/Apps/Users/List';

    // Users List datatable
    if (dataTablePermissions.length) {
        dt_permission = dataTablePermissions.DataTable({
            ajax: {
                url: "/api/Organization",
                dataSrc: '',
                complete: function () {
                }
            },
            columns: [
                // columns according to JSON
                { data: '' },
                { data: 'id' },
                { data: 'organizationName' },
                { data: 'orgRole' },
                { data: 'license' },
                { data: 'licenseValidTill' }
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
                    targets: 1,
                    searchable: false,
                    visible: false
                },
                {
                    // Name
                    targets: 2,
                    render: function (data, type, full, meta) {
                        var $name = full['organizationName'];
                        return '<span class="text-nowrap">' + $name + '</span>';
                    }
                },
                {
                    // User Role
                    targets: 3,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        var $assignedTo = full['orgRole'],
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
                    // User Role
                    targets: 4,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        var licenseFlag = full['license'],
                            $output = '';

                        var licenseValidTill = full['licenseValidTill'];

                        var roleBadgeObj = {
                            EXPIRED: '<span class="badge bg-label-danger m-1">EXPIRED</span>',
                            VALID: '<span class="badge bg-label-success m-1">VALID</span>',
                            NOTAVAIL: '<span class="badge bg-label-warning m-1">NO LICENSE</span>'
                        };
                        if (licenseFlag)
                            $output += roleBadgeObj.VALID;
                        else if (isAvail(licenseValidTill) && licenseValidTill != "")
                            $output += roleBadgeObj.EXPIRED;
                        else
                            $output += roleBadgeObj.NOTAVAIL;
                        return '<span class="text-nowrap">' + $output + '</span>';
                    }
                },
                {
                    // User Role
                    targets: 5,
                    orderable: false,
                    render: function (data, type, full, meta) {
                        var licenseValidTill = full['licenseValidTill'];
                        var $output = '';
                        if (isAvail(licenseValidTill) && licenseValidTill != "")
                            $output = moment(licenseValidTill).format("MMM Do YY");

                        return '<span class="text-nowrap">' + $output + '</span>';
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
                //{
                //    text: 'Add New Organization',
                //    className: 'add-new btn btn-primary mb-3 mb-md-0',
                //    attr: {
                //        'data-bs-toggle': 'modal',
                //        'data-bs-target': '#addOrganizationModal'
                //    },
                //    init: function (api, node, config) {
                //        $(node).removeClass('btn-secondary');
                //    }
                //}
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
    $('.datatables-organizations tbody').on('click', '.delete-record', function () {
        dt_permission.row($(this).parents('tr')).remove().draw();
    });

    // Filter form control to default size
    // ? setTimeout used for multilingual table initialization
    setTimeout(() => {
        $('.dataTables_filter .form-control').removeClass('form-control-sm');
        $('.dataTables_length .form-select').removeClass('form-select-sm');
    }, 300);
});

// Add organization form validation
document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        FormValidation.formValidation(document.getElementById('addOrganizationForm'), {
            fields: {
                "Name": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter organization name'
                        }
                    }
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
                url: '/api/Organization',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(json),
                success: function (response) {
                    if (response.status) {
                        Swal.fire({
                            title: 'Success!',
                            text: 'Organization has been created!',
                            icon: 'success',
                            customClass: {
                                confirmButton: 'btn btn-primary'
                            },
                            buttonsStyling: false
                        }).then(() => {
                            $('.datatables-organizations').DataTable().ajax.reload();
                            location.reload();
                        });
                        $('#addOrganizationModal').modal('hide');
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
                        text: 'There was an error while adding organization!',
                        icon: 'error',
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false
                    });
                }
            });
        });
    })();
});

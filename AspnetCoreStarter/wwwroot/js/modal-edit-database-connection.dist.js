'use strict';

document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        FormValidation.formValidation(document.getElementById('editDatabaseConnectionForm'), {
            fields: {
                "Name": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter connection name'
                        }
                    }
                },
                "ConnectionUrl": {
                    validators: {
                        notEmpty: {
                            message: 'Please enter URL'
                        },
                        uri: {
                            message: 'Please enter a valid URL'
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
            json.Id = parseInt(document.querySelector('#editDatabaseConnectionForm #editDatabaseConnectionId').value);
            json.OrgId = getCookie("selectedOrg");

            $.ajax({
                url: '/api/Database/' + json['Id'],
                type: 'PUT',
                contentType: 'application/json',
                data: JSON.stringify(json),
                success: function (response) {
                    Swal.fire({
                        title: 'Success!',
                        text: 'Database connection changes have been saved!',
                        icon: 'success',
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false
                    }).then(() => {
                        $('.datatables-database-connection').DataTable().ajax.reload();
                    });
                    $('#editDatabaseConnectionModal').modal('hide');
                },
                error: function (error) {
                    // Handle errors here
                    Swal.fire({
                        title: 'Error!',
                        text: 'There was an error while saving Database connection!',
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

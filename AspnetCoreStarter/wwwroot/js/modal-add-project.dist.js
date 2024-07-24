/**
 * Add Permission Modal JS
 */

'use strict';

// Add permission form validation
document.addEventListener('DOMContentLoaded', function (e) {
    (function () {
        FormValidation.formValidation(document.getElementById('addProjectForm'), {
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
                        notEmpty: {
                            message: 'Please select salesforce database mapping'
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

            json.OrgId = getCookie("selectedOrg");
            json.SfDbMappingId = 1;

            $.ajax({
                url: '/api/Projects',
                type: 'POST',
                contentType: 'application/json',
                data: JSON.stringify(json),
                success: function (response) {
                    Swal.fire({
                        title: 'Good job!',
                        text: 'You clicked the button!',
                        icon: 'success',
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false
                    });
                },
                error: function (error) {
                    // Handle errors here
                    Swal.fire({
                        title: 'Good Error!',
                        text: 'You clicked the button!',
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

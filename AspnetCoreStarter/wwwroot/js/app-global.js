'use strict';

const driver = window.driver.js.driver;

function StartPageLoader() {
    $.blockUI({
        message: '<div class="spinner-border text-white" role="status"></div>',
        css: {
            backgroundColor: 'transparent',
            border: '0'
        },
        overlayCSS: {
            opacity: 0.5
        }
    });
}

function StopPageLoader() {
    $.unblockUI();
}
$(document).ajaxStart(StartPageLoader).ajaxStop($.unblockUI);

function getRoleBadge(text) {
    var roleBadgeObj = {
        ADMINISTRATOR: '<small class="text-sm"><span class="badge rounded-pill bg-label-primary">ADMINISTRATOR</span></small>',
        EDITOR: '<small class="text-sm"><span class="badge rounded-pill bg-label-warning m-1">EDITOR</span></small>',
        EXECUTOR: '<small class="text-sm"><span class="badge rounded-pill bg-label-success m-1">EXECUTOR</span></small>'
    };

    var $output = roleBadgeObj[text];

    return $output == undefined ? "" : $output;
}

function loadGlobalOrganization() {
    $.ajax({
        url: '/api/Organization',
        type: 'GET',
        contentType: 'application/json',
        success: function (response) {
            var selectBox = $('#selectGlobalOrganization');
            var selectedOrg = getCookie("selectedOrg");
            response.forEach(function (option) {
                selectBox.append('<option value="' + option.id + '" title="' + option.orgRole + '">' + option.organizationName + '</option>');
            });

            if (selectedOrg != null) {
                selectBox.val(selectedOrg);

                if (!IsIntroComplete("selected-org")) {
                    const driverObj = driver({
                        showProgress: true,
                        steps: [
                            { element: '#ulNavMenu li:nth-child(11)', popover: { title: 'Parameter Controls', description: 'Setup different data sources and it\'s destination. For example : Load the data from Database (SOURCE) and Write to Salesforce (DESTINATION).', side: "right", align: 'start' } },
                            { element: '#ulNavMenu li:nth-child(6)', popover: { title: 'Setup Project', description: 'After source and destination, start defining your project for linking source and destination connections.', side: "bottom", align: 'start' } },
                            { element: '#ulNavMenu li:nth-child(7)', popover: { title: 'Setup Jobs', description: 'Once project is defined, create multiple jobs to map different schemas. For Example : Map database account table to Salesforce account object.', side: "bottom", align: 'start' } },
                            { element: '#ulNavMenu li:nth-child(8)', popover: { title: 'Setup Schedules', description: 'Here, you can define the execution order of jobs and it\s schedule when to execute.', side: "bottom", align: 'start' } },
                            { element: '#ulNavMenu li:nth-child(9)', popover: { title: 'Check Executions', description: 'One stop to check all your executions and it\'s history.', side: "bottom", align: 'start' } },
                            { popover: { title: 'Catch you later', description: 'And that is all for now, go ahead and I will meet you later.' } }
                        ],
                        onDestroyStarted: () => {
                            SetIntroComplete("selected-org");
                            driverObj.destroy();
                        }
                    });

                    driverObj.drive();
                }
            }

            if (!IsIntroComplete("first-login")) {
                const driverObj = driver({
                    showProgress: true,
                    steps: [
                        { popover: { title: 'Welcome to ERPFastTrack', description: 'Let\'s take a tour.' } },
                        { element: '#layout-menu .menu-inner', popover: { title: 'Navigation Menu', description: 'Intuitive Menu for easy access of the application controls.', side: "right", align: 'start' } },
                        { element: '#ulNavMenu li:nth-child(3)', popover: { title: 'Your Organizations', description: 'Find all your organizations here and check their roles, license and other details.', side: "right", align: 'start' } },
                        { element: '#divGlobalOrganization', popover: { title: 'Organization Selection', description: 'Select your organization from here.', side: "bottom", align: 'start' } },
                        { popover: { title: 'Catch you later', description: 'And that is all, go ahead and I will meet you later.' } }
                    ],
                    onDestroyStarted: () => {
                        SetIntroComplete("first-login");
                        driverObj.destroy();
                    }
                });

                driverObj.drive();
            }

            selectBox.select2({
                placeholder: "Select an organization",
                dropdownParent: $("#layout-navbar"),
                templateSelection: function (data, container) {
                    return $(`<span>${data.text} ${getRoleBadge(data.title)}</span>`);
                },
                templateResult: function (data) {
                    return $(`<span>${data.text} ${getRoleBadge(data.title)}</span>`);
                }
            });

            selectBox.on('select2:select', function (e) {
                var data = e.params.data;
                setCookie("selectedOrg", data.id);
                location.reload();
            });
        }
    });
}

$(document).ready(function () {
    if ($('#selectGlobalOrganization').length) {
        loadGlobalOrganization();
    }
});

function setCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + value + expires + "; path=/";
}

function getCookie(name) {
    const cookies = document.cookie.split(';').map(cookie => cookie.trim());
    for (const cookie of cookies) {
        const [cookieName, cookieValue] = cookie.split('=');
        if (cookieName === name) {
            return cookieValue;
        }
    }
    return null;
}
function deleteCookie(name) {
    document.cookie = name + '=; Max-Age=-99999999;';
}

function isFunction(variable) {
    return typeof variable === 'function' && !(variable === null || variable === undefined);
}

function isAvail(variable) {
    return !(variable === null || variable === undefined);
}

$(function () {
    // Form sticky actions
    var topSpacing;
    const stickyEl = $('.sticky-element');
    topSpacing = $('.layout-navbar').height() + 7;

    // sticky element init (Sticky Layout)
    if (stickyEl.length) {
        stickyEl.sticky({
            topSpacing: topSpacing,
            zIndex: 9
        });
    }
});
var vs;
var fms;
var parameterCtrl = {
    fvWizardAdd: function (data) {
        var formId = "frm" + data.type + data.identifier;
        //wzdAddProject
        var wizardId = "wzd" + data.type + data.identifier;
        //wzdAddProjectForm
        var wizardFormId = "wzd" + data.type + data.identifier + "Form";

        const wizard = document.querySelector("#" + wizardId);
        const wizardForm = wizard.querySelector("#" + wizardFormId);

        const wizardNext = [].slice.call(wizardForm.querySelectorAll('.btn-next'));
        const wizardPrev = [].slice.call(wizardForm.querySelectorAll('.btn-prev'));

        const validationStepper = new Stepper(wizard, {
            linear: true
        });
        vs = validationStepper;
        var forms = [];
        $.each(data.fields, function (index, item) {

            var wizardPageFormId = "wzd" + data.type + data.identifier + "Form" + (index + 1);
            var fv = FormValidation.formValidation(document.querySelector("#" + wizardFormId).querySelector("#" + wizardPageFormId), item.fvFields).on('core.form.valid', function (e) {
                //// Create an empty array to store unique names
                //var uniqueNames = [];

                //// Iterate over the input elements
                //inputElements.forEach(function (input) {
                //    // Check if the input type is radio
                //    if (input.type === 'radio') {
                //        // Get the name attribute of the radio button
                //        var name = input.getAttribute('name');
                //        // Get the value of the radio button
                //        var value = input.value;

                //        // Check if the name attribute is not null or empty
                //        if (name && name.trim() !== '') {
                //            // Check if the name already exists in the object
                //            if (!uniqueNames.hasOwnProperty(name)) {
                //                // If the name doesn't exist, initialize it with an empty array
                //                uniqueNames[name] = [];
                //            }

                //            // Add the value to the array corresponding to the name
                //            uniqueNames[name].push(value);
                //        }
                //    }

                //    // Get the name attribute of the input element
                //    var name = input.getAttribute('name');

                //    // Check if the name attribute is not null or empty
                //    if (name && name.trim() !== '') {
                //        // Check if the name is not already in the uniqueNames array
                //        if (!uniqueNames.includes(name)) {
                //            // Add the name to the uniqueNames array
                //            uniqueNames.push(name);
                //        }
                //    }
                //});

                //var json = {};
                //$.each(inputElements, function (index, item) {
                //    if (e.formValidation.fields[item.name] != undefined) {
                //        json[item.name] = item.value;
                //    }
                //});

                var res = false;
                var formData = {};
                if (isAvail(item.generateFormData) && item.generateFormData == true) {
                    $.each(e.formValidation.form.parentElement.parentElement.parentElement.parentElement, function (index, item) {
                        $.each(forms, function (k, v) {
                            if (v.fields[item.name] != undefined) {
                                if (item.type == "radio") {
                                    if (item.checked)
                                        formData[item.name] = item.value;
                                }
                                else if (item.type == "checkbox") {
                                    if (item.checked)
                                        formData[item.name] = true;
                                    else
                                        formData[item.name] = false;
                                }
                                else
                                    formData[item.name] = item.value;
                            }
                        });
                    });
                }

                // Jump to the next step when all fields in the current step are valid
                if (isFunction(item.postValidate))
                    res = item.postValidate(e, formData, forms[index], forms[index + 1], index, forms);

                if (!res)
                    validationStepper.next();
                else {
                    data.json = formData;
                    parameterCtrl.ajaxAdd(data);
                }
            });

            forms.push(fv);

        });
        fms = forms;
        wizardNext.forEach(item => {
            item.addEventListener('click', event => {
                // When click the Next button, we will validate the current step
                var currIndex = validationStepper._currentIndex;
                forms[currIndex].validate();
            });
        });

        wizardPrev.forEach(item => {
            item.addEventListener('click', event => {
                var currIndex = validationStepper._currentIndex;
                if (currIndex != 0) {
                    validationStepper.previous();
                }
            });
        });
        return forms;
    },
    fvAddToTable: function (data) {
        var formId = "frm" + data.type + data.identifier;
        const fv1 = FormValidation.formValidation(document.getElementById(formId), data.fields).on('core.form.valid', function (e) {
            var json = {};
            var arr = [];
            $.each(e.formValidation.form, function (index, item) {
                if (e.formValidation.fields[item.name] != undefined) {
                    if (item.type == "radio") {
                        if (item.checked)
                            json[item.name] = item.value;
                    }
                    else if (item.type == "checkbox") {
                        if (item.checked)
                            json[item.name] = true;
                        else
                            json[item.name] = false;
                    }
                    else
                        json[item.name] = item.value;
                }
            });

            if (data.customFieldMapper != null && data.customFieldMapper != undefined)
                json = data.customFieldMapper(e, json);

            arr.push(json);
            data.datatable.getInstance().rows.add(arr).draw();
            $('#ocAdd' + data.identifier).offcanvas('hide');
            var formId = "frm" + data.type + data.identifier;
            $("#" + formId).trigger("reset");
        });
        return fv1;
    },
    fvEditToTable: function (data) {
        var formId = "frm" + data.type + data.identifier;
        const fv1 = FormValidation.formValidation(document.getElementById(formId), data.fields).on('core.form.valid', function (e) {
            var json = {};
            $.each(e.formValidation.form, function (index, item) {
                if (e.formValidation.fields[item.name] != undefined) {
                    if (item.type == "radio") {
                        if (item.checked)
                            json[item.name] = item.value;
                    }
                    else if (item.type == "checkbox") {
                        if (item.checked)
                            json[item.name] = true;
                        else
                            json[item.name] = false;
                    }
                    else
                        json[item.name] = item.value;
                }
            });

            if (data.customFieldMapper != null && data.customFieldMapper != undefined)
                json = data.customFieldMapper(e, json);
            debugger;
            data.datatable.getInstance().row(data.getRowIndex()).data(json).draw();
            $('#ocEdit' + data.identifier).offcanvas('hide');
            var formId = "frm" + data.type + data.identifier;
            $("#" + formId).trigger("reset");
        });
        return fv1;
    },
    fvAdd: function (data) {
        var formId = "frm" + data.type + data.identifier;
        const fv1 = FormValidation.formValidation(document.getElementById(formId), data.fields).on('core.form.valid', function (e) {
            var json = {};
            $.each(e.formValidation.form, function (index, item) {
                if (e.formValidation.fields[item.name] != undefined) {
                    if (item.type == "radio") {
                        if (item.checked)
                            json[item.name] = item.value;
                    }
                    else if (item.type == "checkbox") {
                        if (item.checked)
                            json[item.name] = true;
                        else
                            json[item.name] = false;
                    }
                    else
                        json[item.name] = item.value;
                }
            });

            if (isAvail(data.datatable)) {
                json[data.datatable.name] = data.datatable.getInstance().rows().data().toArray();
            }

            if (data.customFieldMapper != null && data.customFieldMapper != undefined)
                json = data.customFieldMapper(e, json);

            data.json = json;
            parameterCtrl.ajaxAdd(data);
        });
        return fv1;
    },
    fvEdit: function (data) {
        var formId = "frm" + data.type + data.identifier;

        const fv2 = FormValidation.formValidation(document.getElementById(formId), data.fields).on('core.form.valid', function (e) {
            var json = {};
            $.each(e.formValidation.form, function (index, item) {
                if (e.formValidation.fields[item.name] != undefined) {
                    if (item.type == "radio") {
                        if (item.checked)
                            json[item.name] = item.value;
                    }
                    else if (item.type == "checkbox") {
                        if (item.checked)
                            json[item.name] = true;
                        else
                            json[item.name] = false;
                    }
                    else
                        json[item.name] = item.value;
                }
            });

            if (isAvail(data.datatable)) {
                json[data.datatable.name] = data.datatable.getInstance().rows().data().toArray();
            }

            if (data.customFieldMapper != null && data.customFieldMapper != undefined)
                json = data.customFieldMapper(e, json);

            data.json = json;
            parameterCtrl.ajaxEdit(data);
        });
        return fv2;
    },
    ajaxEdit: function (data, successCb, errorCb) {
        $.ajax({
            url: data.uri(),
            type: 'PUT',
            contentType: 'application/json',
            data: JSON.stringify(data.json),
            success: function (response) {
                if (response.status) {
                    Swal.fire({
                        title: 'Success!',
                        text: data.title + ' changes have been saved!',
                        icon: 'success',
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false
                    }).then(() => {
                        $('#dt' + data.identifier).DataTable().ajax.reload();
                    });
                    $('#mdlEdit' + data.identifier).modal('hide');
                    var formId = "frm" + data.type + data.identifier;
                    $("#" + formId).trigger("reset");
                    if (isAvail(data.datatable)) {
                        data.datatable.getInstance().clear().draw();
                    }
                }
                else {
                    // Handle errors here
                    Swal.fire({
                        title: 'Error!',
                        text: 'There was an error while saving ' + data.title + ' - ' + response.errorMessage,
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
                    text: 'There was an error while saving ' + data.title + '!',
                    icon: 'error',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    buttonsStyling: false
                });
            }
        });
    },
    ajaxAdd: function (data, successCb, errorCb) {
        $.ajax({
            url: data.uri(),
            type: 'POST',
            contentType: 'application/json',
            data: JSON.stringify(data.json),
            success: function (response) {
                if (response.status) {
                    Swal.fire({
                        title: 'Success!',
                        text: data.title + ' has been created!',
                        icon: 'success',
                        customClass: {
                            confirmButton: 'btn btn-primary'
                        },
                        buttonsStyling: false
                    }).then(() => {
                        $('#dt' + data.identifier).DataTable().ajax.reload();
                    });
                    $('#mdlAdd' + data.identifier).modal('hide');
                    var formId = "frm" + data.type + data.identifier;
                    $("#" + formId).trigger("reset");

                    if (isAvail(data.datatable)) {
                        data.datatable.getInstance().clear().draw();
                    }
                }
                else {
                    // Handle errors here
                    Swal.fire({
                        title: 'Error!',
                        text: 'There was an error while adding ' + data.title + ' - ' + response.errorMessage,
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
                    text: 'There was an error while adding ' + data.title + '!',
                    icon: 'error',
                    customClass: {
                        confirmButton: 'btn btn-primary'
                    },
                    buttonsStyling: false
                });
            }
        });
    },
    loadSection: function (data) {
        var source = document.getElementById("hbs" + data.identifier).innerHTML;
        var template = Handlebars.compile(source);
        $("#hbsOutersection" + data.identifier).html(template(data.data));
    },
    loadSectionWithCallbacks: function (data) {
        if (!data.isIteratable) {
            var source = document.getElementById("hbs" + data.identifier).innerHTML;
            var template = Handlebars.compile(source);
            if (isAvail(data.parentIdentifier)) {
                $(data.parentIdentifier).html(template(isFunction(data.data) ? data.data() : null));
            }
            else {
                $("#hbsOutersection" + data.identifier).html(template(isFunction(data.data) ? data.data() : null));
            }
            if (isFunction(data.postLoad)) {
                data.postLoad();
            }
        }
    },
    unloadSectionWithCallbacks: function (data) {
        $("#hbsOutersection" + data.identifier).html("");
        if (isFunction(data.postUnload)) {
            data.postUnload();
        }
    },
    selectAjaxPost: function (data, successCb, failureCb) {
        $.ajax({
            url: data.uri,
            data: JSON.stringify(data.data),
            type: 'POST',
            contentType: 'application/json',
            success: function (response) {
                if (Array.isArray(response.data) && response.data.length > 0) {
                    data.fields.forEach(function (item, index) {
                        var selectBox = $(item.selIdentifier);

                        if (item.destroyAtStart) {
                            selectBox.select2("destroy");
                        }

                        selectBox.empty();
                        var $defOptionElement = $('<option/>');
                        selectBox.append($defOptionElement);
                        response.data.forEach(function (option) {
                            if (isFunction(item.title)) {
                                var $optionElement = $('<option/>', {
                                    value: option[item.resKey],
                                    title: item.title(option)
                                }).text(option[item.resValue]);

                                if (Array.isArray(item.attributes) && item.attributes.length > 0) {
                                    item.attributes.forEach(function (o) {
                                        $optionElement.data(o.key, option[o.value]);
                                    });
                                }

                                selectBox.append($optionElement);
                            }
                            else {
                                var $optionElement = $('<option/>', {
                                    value: option[item.resKey]
                                }).text(option[item.resValue]);

                                if (Array.isArray(item.attributes) && item.attributes.length > 0) {
                                    item.attributes.forEach(function (o) {
                                        $optionElement.data(o.key, o.value);
                                    });
                                }

                                selectBox.append($optionElement);
                            }
                        });

                        if (isAvail(item.form)) {
                            selectBox.on('change', function () {
                                item.form.revalidateField(item.name);
                            });
                        }

                        if (isFunction(item.changeEvent))
                            selectBox.on('change', item.changeEvent);

                        if (isFunction(item.template)) {
                            selectBox.select2({
                                placeholder: "Select an " + data.title,
                                dropdownParent: $(item.parentIdentifier),
                                templateResult: item.template,
                                dropdownCssClass: 'auto-height'
                            });
                        }
                        else
                            selectBox.select2({
                                placeholder: "Select an " + data.title,
                                dropdownParent: $(item.parentIdentifier),
                                dropdownCssClass: 'auto-height'
                            });
                    });

                    if (isFunction(successCb))
                        successCb(response);

                    //selectBox.on('select2:select', function (e) {
                    //    var data = e.params.data;
                    //    setCookie("selectedOrg", data.id);
                    //    location.reload();
                    //});
                }
                else {
                    if (isFunction(failureCb))
                        failureCb(response);
                }
            }
        });
    },
    selectAjax: function (data, successCb, failureCb) {

        $.ajax({
            url: data.uri,
            type: 'GET',
            contentType: 'application/json',
            success: function (response) {
                if (Array.isArray(response.data) && response.data.length > 0) {
                    data.fields.forEach(function (item, index) {
                        var selectBox = $(item.selIdentifier);

                        if (item.destroyAtStart) {
                            selectBox.select2("destroy");
                        }

                        selectBox.empty();
                        var $defOptionElement = $('<option/>');
                        selectBox.append($defOptionElement);
                        response.data.forEach(function (option) {
                            if (isFunction(item.title)) {
                                var $optionElement = $('<option/>', {
                                    value: option[item.resKey],
                                    title: item.title(option)
                                }).text(option[item.resValue]);

                                if (Array.isArray(item.attributes) && item.attributes.length > 0) {
                                    item.attributes.forEach(function (o) {
                                        $optionElement.data(o.key, option[o.value]);
                                    });
                                }

                                selectBox.append($optionElement);
                            }
                            else {
                                var $optionElement = $('<option/>', {
                                    value: option[item.resKey]
                                }).text(option[item.resValue]);

                                if (Array.isArray(item.attributes) && item.attributes.length > 0) {
                                    item.attributes.forEach(function (o) {
                                        $optionElement.data(o.key, o.value);
                                    });
                                }

                                selectBox.append($optionElement);
                            }
                        });

                        if (isAvail(item.form)) {
                            selectBox.on('change', function () {
                                item.form.revalidateField(item.name);
                            });
                        }

                        if (isFunction(item.changeEvent))
                            selectBox.on('change', item.changeEvent);

                        if (isFunction(item.template)) {
                            selectBox.select2({
                                placeholder: "Select an " + data.title,
                                dropdownParent: $(item.parentIdentifier),
                                templateResult: item.template,
                                dropdownCssClass: 'auto-height'
                            });
                        }
                        else
                            selectBox.select2({
                                placeholder: "Select an " + data.title,
                                dropdownParent: $(item.parentIdentifier),
                                dropdownCssClass: 'auto-height'
                            });
                    });

                    if (isFunction(successCb))
                        successCb(response);

                    //selectBox.on('select2:select', function (e) {
                    //    var data = e.params.data;
                    //    setCookie("selectedOrg", data.id);
                    //    location.reload();
                    //});
                }
                else {
                    if (isFunction(failureCb))
                        failureCb(response);
                }
            }
        });
    },
    staticDatatable: {
        loadDataTable: function (data) {
            var table = $("#dt" + data.type + data.identifier).DataTable({
                dom:
                    '<"row mx-1"' +
                    '<"col-sm-12 col-md-3" l>' +
                    '<"col-sm-12 col-md-9"<"dt-action-buttons text-xl-end text-lg-start text-md-end text-start d-flex align-items-center justify-content-md-end justify-content-center flex-wrap me-1"<"me-3"f>B>>' +
                    '>t' +
                    '<"row mx-2"' +
                    '<"col-sm-12 col-md-6"i>' +
                    '<"col-sm-12 col-md-6"p>' +
                    '>',
                data: data.ajaxData,
                columns: parameterCtrl.staticDatatable.columns(data.columns),
                buttons: isFunction(data.buttonFunc) ? data.buttonFunc() : parameterCtrl.staticDatatable.buttons("Add", "#ocAdd" + data.identifier),
                columnDefs: [{
                    // Actions
                    targets: -1,
                    searchable: false,
                    title: 'Actions',
                    orderable: false,
                    render: function (d, type, full, meta) {
                        return (
                            '<span class="text-nowrap"><span class="btn btn-sm btn-icon me-2 edit-record" data-id="' + full['id'] + '" data-bs-target="#ocEdit' + data.identifier + '" data-bs-toggle="offcanvas"><i class="ti ti-edit"></i></span>' +
                            '<span class="btn btn-sm btn-icon delete-record" data-id="' + full['id'] + '"><i class="ti ti-trash"></i></span></span>'
                        );
                    }
                }]
            });

            return table;
        },
        columns: function (colmArr) {
            let newColumns = [];

            colmArr.forEach(item => {
                newColumns.push({ data: item });
            });
            newColumns.push('');

            return newColumns;
        },
        // Buttons with Dropdown
        buttons: function (title, targetId) {

            return [
                {
                    text: title,
                    className: 'add-new btn btn-primary mb-3 mb-md-0',
                    attr: {
                        'data-bs-toggle': 'offcanvas',
                        'data-bs-target': targetId
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('btn-secondary');
                    }
                }
            ];
        }
    },
    datatable: {
        loadDataTable: function (data) {
            var columnDefs = [];

            if (!(isAvail(data.isResponsiveDisabled) && data.isResponsiveDisabled)) {
                columnDefs.push({
                    // For Responsive
                    className: 'control',
                    orderable: false,
                    searchable: false,
                    visible: false,
                    responsivePriority: 2,
                    targets: 0,
                    render: function (d, type, full, meta) {
                        return '';
                    }
                });
            }

            columnDefs.push({
                targets: 1,
                searchable: false,
                visible: false
            });

            if (!(isAvail(data.isActionHidden) && data.isActionHidden)) {
                columnDefs.push({
                    // Actions
                    targets: -1,
                    searchable: false,
                    title: 'Actions',
                    orderable: false,
                    render: function (d, type, full, meta) {
                        if (isFunction(data.customActionColumnRender)) {
                            return data.customActionColumnRender(d, type, full, meta);
                        } else {
                            return (
                                '<span class="text-nowrap"><button class="btn btn-sm btn-icon me-2 edit-record" data-id="' + full['id'] + '" data-bs-target="#mdlEdit' + data.identifier + '" data-bs-toggle="modal" data-bs-dismiss="modal"><i class="ti ti-edit"></i></button>' +
                                '<button class="btn btn-sm btn-icon delete-record" data-id="' + full['id'] + '"><i class="ti ti-trash"></i></button></span>'
                            );
                        }
                    }
                });
            }

            if (isAvail(data.customColumnDefs) && data.customColumnDefs.length > 0) {
                data.customColumnDefs.forEach(function (i) {
                    columnDefs.push(i);
                });
            }

            $("#dt" + data.identifier).on('error.dt',
                function (e, settings, techNote, message) {
                    toastr.options.progressBar = true;
                    toastr.warning(data.title + " - no data exists !")
                }).DataTable({
                    ajax: {
                        url: data.uri(),
                        dataSrc: function (d) {
                            if (data.data == null) {
                                return [];
                            } else {
                                return d[data.data];
                            }
                        },
                        complete: function (d) {
                            if (isFunction(data.successCb))
                                data.successCb(data);
                        }
                    },
                    rowCallback: isFunction(data.customRowCallback) ? data.customRowCallback : function (row, data, index) { },
                    columns: parameterCtrl.datatable.columns(data.columns),
                    columnDefs: (!(isAvail(data.isColumnDefDisabled) && data.isColumnDefDisabled)) ? columnDefs : null,
                    order: parameterCtrl.datatable.order,
                    dom: isAvail(data.dom) ? data.dom : parameterCtrl.datatable.dom,
                    language: parameterCtrl.datatable.language,
                    buttons: isFunction(data.buttonFunc) ? data.buttonFunc() : parameterCtrl.datatable.buttons("Add New " + data.title, "#mdlAdd" + data.identifier),
                    responsive: (!(isAvail(data.isResponsiveDisabled) && data.isResponsiveDisabled)) ? parameterCtrl.datatable.responsive() : null,
                    initComplete: function () {
                        if (isFunction(data.completeFunc)) {
                            data.completeFunc();
                        }
                    }
                });

            $('#dt' + data.identifier + ' tbody').on('click', '.delete-record', isFunction(data.deleteFunc) ? data.deleteFunc() : function () {
                var dataId = $(this).data('id');
                Swal.fire({
                    title: 'Delete record?',
                    text: 'Do you want to delete this record?',
                    icon: 'warning',
                    showCancelButton: true,
                    confirmButtonText: "Yes, delete it!",
                    cancelButtonText: "No, cancel!",
                    customClass: {
                        confirmButton: "btn btn-success",
                        cancelButton: "btn btn-danger"
                    },
                    buttonsStyling: false
                }).then((result) => {
                    if (result.isConfirmed) {
                        $.ajax({
                            url: isFunction(data.deleteUri) ? data.deleteUri() + '/' + dataId : data.uri() + '/' + dataId,
                            type: 'DELETE',
                            contentType: 'application/json',
                            success: function (response) {
                                Swal.fire({
                                    title: 'Success!',
                                    icon: 'success',
                                    customClass: {
                                        confirmButton: 'btn btn-primary'
                                    },
                                    buttonsStyling: false
                                }).then(() => {
                                    $('#dt' + data.identifier).DataTable().ajax.reload();
                                });
                            },
                            error: function (error) {
                                // Handle errors here
                                Swal.fire({
                                    title: 'Error!',
                                    text: 'There was an error while deleting ' + data.title + '!',
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
            });

            // Filter form control to default size
            // ? setTimeout used for multilingual table initialization
            setTimeout(() => {
                $('.dataTables_filter .form-control').removeClass('form-control-sm');
                $('.dataTables_length .form-select').removeClass('form-select-sm');
            }, 300);
        },
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
        buttons: function (title, targetId) {
            return [
                {
                    text: title,
                    className: 'add-new btn btn-primary mb-3 mb-md-0',
                    attr: {
                        'data-bs-toggle': 'modal',
                        'data-bs-target': targetId
                    },
                    init: function (api, node, config) {
                        $(node).removeClass('btn-secondary');
                    }
                }, {
                    text: 'Refresh',
                    className: 'add-new btn btn-warning mb-3 ms-1 mb-md-0',
                    action: function (e, dt, node, config) {
                        dt.clear().draw();
                        dt.ajax.reload();
                    }
                }
            ];
        },
        // For responsive popup
        responsive: function () {
            var data =
            {
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
            }
            return data;
        },
        columns: function (colmArr) {
            let newColumns = [];

            newColumns.push('');
            colmArr.forEach(item => {
                newColumns.push({ data: item });
            });
            newColumns.push('');

            return newColumns;
        }
    },
    fv: {
        plugins: function () {
            var data = {
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
            };
            return data;
        },
        init: function () {
            return (instance => {
                instance.on('plugins.message.placed', function (e) {
                    if (e.element.parentElement.classList.contains('input-group')) {
                        e.element.parentElement.insertAdjacentElement('afterend', e.messageElement);
                    }
                })
            });
        }
    },
    fvWizard: {
        plugins: function () {
            var data = {
                trigger: new FormValidation.plugins.Trigger(),
                bootstrap5: new FormValidation.plugins.Bootstrap5(),
                submitButton: new FormValidation.plugins.SubmitButton(),
                // Submit the form when all fields are valid
                // defaultSubmit: new FormValidation.plugins.DefaultSubmit(),
                autoFocus: new FormValidation.plugins.AutoFocus()
            };
            return data;
        }
    }
};
if (isAvail($.fn.dataTable)) {
    $.fn.dataTable.ext.errMode = 'none';
}
Handlebars.registerHelper('eq', function (a, b, options) {
    return a === b;
});

function ExtractDate(dateTimeString) {
    if (isAvail(dateTimeString)) {
        // Split the dateTimeString by "T" to separate the date and time parts
        const parts = dateTimeString.split('T');

        // Return only the date part (e.g., "2024-01-12")
        return parts[0];
    }
    else return "";
}

function IsIntroComplete(item) {
    return localStorage.getItem('intro_' + item) == "1";
}

function SetIntroComplete(item) {
    localStorage.setItem('intro_' + item, "1");
}

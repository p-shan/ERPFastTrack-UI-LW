'use strict';

var salesforceConnection = {
    load: function () {
        var data1 = {
            title: "Salesforce Connection",
            uri: function () {
                return '/api/data/salesforceconnection';
            },
            identifier: "SalesforceConnection",
            type: "Add",
            fields: salesforceConnection.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvAdd(data1);

        var data2 = {
            title: "Salesforce Connection",
            uri: function () {
                return '/api/data/salesforceconnection/' + salesforceConnection.state.editId;
            },
            identifier: "SalesforceConnection",
            type: "Edit",
            fields: salesforceConnection.fvFields(),
            customFieldMapper: function (e, json) {
                return json;
            }
        };
        parameterCtrl.fvEdit(data2);

        var data3 = {
            title: "Salesforce Connection",
            uri: function () {
                return '/api/data/salesforceconnection';
            },
            identifier: "SalesforceConnection",
            columns: ['id', 'name', 'url']
        };
        parameterCtrl.datatable.loadDataTable(data3);

        $('#dtSalesforceConnection tbody').on('click', '.edit-record', function () {
            var dataId = $(this).data('id');
            var $editForm = $('#frmEditSalesforceConnection');
            $.ajax({
                url: '/api/data/salesforceconnection/' + dataId,
                type: 'GET',
                contentType: 'application/json',
                success: function (response) {
                    salesforceConnection.state.editId = response.id;
                    // Load data to form 
                    $editForm.find('#editSalesforceConnectionId').val(response.id);
                    $.timeago.settings.allowFuture = true;
                    // $editForm.find('#tokenExpirySpan').html((response.tokenExpiry) ? $.timeago(response.tokenExpiry + "Z") : "Not yet set");

                    $editForm.find('input[name="Name"]').val(response.name);
                    $editForm.find('input[name="Url"]').val(response.url);
                    $editForm.find('input[name="TokenEndpoint"]').val(response.tokenEndpoint);
                    $editForm.find('input[name="Username"]').val(response.username);
                    $editForm.find('input[name="ClientId"]').val(response.clientId);
                    $editForm.find('input[name="ClientSecret"]').val(response.clientSecret);
                    $editForm.find('input[name="Password"]').val(response.password);

                    if (!isAvail(response.tokenExpiry)) {
                        $("#connectionAlert").hide();
                    }

                    /*
                    var tokenExpirySpan = response.tokenExpiry + "Z";
                    var tokenExpiryTime = Date.parse(tokenExpirySpan);
                    var currentTime = new Date().getTime();
                    tokenExpirySpan = new Date(tokenExpiryTime).toLocaleString();
                    if (tokenExpiryTime < currentTime || !tokenExpiryTime) {
                        var connectionAlert = document.getElementById('connectionAlert');
                        connectionAlert.classList.remove('alert-success');
                        connectionAlert.classList.add('alert-danger');
                    }
                    */
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
                            message: 'Please enter connection name'
                        }
                    }
                },
                "Url": {
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
        salesforceConnection.load();
    })();
});


 //const tabs = document.querySelectorAll('[data-bs-toggle="tab"]');
        //tabs.forEach((tab) => {
        //    tab.addEventListener('click', function () {
        //        const activeTabId = tab.getAttribute('data-bs-target').substr(1);

        //        if (activeTabId === 'navs-pills-top-m2m') {
        //            fv.enableValidator('Url');
        //            fv.enableValidator('TokenEndpoint');
        //            fv.enableValidator('Password');
        //            fv.enableValidator('Username');
        //            fv.enableValidator('ClientId');
        //            fv.enableValidator('ClientSecret');
        //        }
        //        else if (activeTabId === 'navs-pills-top-oauth') {
        //            fv.disableValidator('TokenEndpoint');
        //            fv.disableValidator('Password');
        //            fv.disableValidator('Username');
        //            fv.disableValidator('ClientId');
        //            fv.disableValidator('ClientSecret');
        //            fv.enableValidator('Url');

        //        }
        //    });
        //});

'use strict';

$(document).ready(function () {
    loadExecutions();
});

var executions = {
    state: {},
    load: function () {
    },
    charts: {
        leadsChart: function (executionChartLabels, executionChartSeries, executionChartColors, totalTimeInSecs) {
            let labelColor, headingColor, borderColor;

            if (isDarkStyle) {
                labelColor = config.colors_dark.textMuted;
                headingColor = config.colors_dark.headingColor;
                borderColor = config.colors_dark.borderColor;
            } else {
                labelColor = config.colors.textMuted;
                headingColor = config.colors.headingColor;
                borderColor = config.colors.borderColor;
            }

            const leadsReportChartEl = document.querySelector('#leadsReportChart'),
                leadsReportChartConfig = {
                    chart: {
                        height: 157,
                        width: 130,
                        parentHeightOffset: 0,
                        type: 'donut'
                    },
                    labels: executionChartLabels,
                    series: executionChartSeries,
                    colors: executionChartColors,
                    stroke: {
                        width: 0
                    },
                    dataLabels: {
                        enabled: false,
                        formatter: function (val, opt) {
                            return parseInt(val) + '%';
                        }
                    },
                    legend: {
                        show: false
                    },
                    tooltip: {
                        theme: false
                    },
                    grid: {
                        padding: {
                            top: 0
                        }
                    },
                    plotOptions: {
                        pie: {
                            donut: {
                                size: '75%',
                                labels: {
                                    show: true,
                                    value: {
                                        fontSize: '1.5rem',
                                        fontFamily: 'Public Sans',
                                        color: headingColor,
                                        fontWeight: 500,
                                        offsetY: -15,
                                        formatter: function (val) {
                                            return parseInt(val) + '%';
                                        }
                                    },
                                    name: {
                                        offsetY: 20,
                                        fontFamily: 'Public Sans'
                                    },
                                    total: {
                                        show: true,
                                        fontSize: '.7rem',
                                        label: 'Total',
                                        color: labelColor,
                                        formatter: function (w) {
                                            return totalTimeInSecs;
                                        }
                                    }
                                }
                            }
                        }
                    }
                };
            if (typeof leadsReportChartEl !== undefined && leadsReportChartEl !== null) {
                const leadsReportChart = new ApexCharts(leadsReportChartEl, leadsReportChartConfig);
                leadsReportChart.render();
            }
        },
        doughnutChart: function (data, index) {

            let cardColor, headingColor, labelColor, borderColor, legendColor;

            if (isDarkStyle) {
                cardColor = config.colors_dark.cardColor;
                headingColor = config.colors_dark.headingColor;
                labelColor = config.colors_dark.textMuted;
                legendColor = config.colors_dark.bodyColor;
                borderColor = config.colors_dark.borderColor;
            } else {
                cardColor = config.colors.cardColor;
                headingColor = config.colors.headingColor;
                labelColor = config.colors.textMuted;
                legendColor = config.colors.bodyColor;
                borderColor = config.colors.borderColor;
            }

            // Set height according to their data-height
            // --------------------------------------------------------------------
            const chartList = document.querySelectorAll('.chartjs');
            chartList.forEach(function (chartListItem) {
                chartListItem.height = chartListItem.dataset.height;
            });
            const doughnutChart = document.getElementById('doughnutChart' + index);
            if (doughnutChart) {
                const doughnutChartVar = new Chart(doughnutChart, {
                    type: 'doughnut',
                    data: {
                        labels: ['Processed', 'Failed'],
                        datasets: [
                            {
                                data: data,
                                backgroundColor: [config.colors.success, config.colors.danger],
                                borderWidth: 0,
                                pointStyle: 'rectRounded'
                            }
                        ]
                    },
                    options: {
                        responsive: true,
                        animation: {
                            duration: 500
                        },
                        cutout: '68%',
                        plugins: {
                            legend: {
                                display: false
                            },
                            tooltip: {
                                callbacks: {
                                    label: function (context) {
                                        const label = context.labels || '',
                                            value = context.parsed;
                                        const output = ' ' + label + ' : ' + value + ' records';
                                        return output;
                                    }
                                },
                                // Updated default tooltip UI
                                rtl: isRtl,
                                backgroundColor: cardColor,
                                titleColor: headingColor,
                                bodyColor: legendColor,
                                borderWidth: 1,
                                borderColor: borderColor
                            }
                        }
                    }
                });
            }
        }
    },
    genSections: {
        executions: {
            title: "Executions",
            identifier: "Executions",
            postLoad: function (data) {
                var data3 = {
                    title: "Executions",
                    uri: function () {
                        return '/api/operations/execution'
                    },
                    identifier: "Executions",
                    columns: ['id', 'scheduleName', 'numberOfJobs', 'status', 'scheduledAt'],
                    customColumnDefs: [
                        {
                            // Active
                            targets: 4,
                            searchable: true,
                            title: 'Status',
                            orderable: true,
                            render: function (d, type, full, meta) {
                                switch (full["executionStatus"]) {
                                    case 1:
                                        var data = '<span class="badge bg-label-secondary">SCHEDULED</span>';
                                        return data;
                                        break;
                                    case 2:
                                        var data = '<span class="badge  bg-label-info">IN-PROGRESS</span>';
                                        return data;
                                        break;
                                    case 3:
                                        var data = '<span class="badge  bg-label-success">COMPLETED</span>';
                                        return data;
                                        break;
                                    case 4:
                                        var data = '<span class="badge  bg-label-danger">FAILED</span>';
                                        return data;
                                        break;
                                }
                            }
                        },

                        {
                            // Active
                            targets: 5,
                            searchable: true,
                            title: 'Scheduled at',
                            orderable: true,
                            render: function (d, type, full, meta) {
                                return moment(full["scheduledAt"]).format('lll');
                            }
                        }
                    ],
                    customActionColumnRender: function (d, type, full, meta) {
                        return (
                            '<span class="text-nowrap"><button class="btn btn-sm btn-icon me-2 view-record"  data-status="' + full['executionStatus'] + '" data-id="' + full['id'] + '"><i class="ti ti-eye"></i></button>'
                        );
                    },
                    buttonFunc: function () {
                        return ([{
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

                $('#dtExecutions tbody').on('click', '.view-record', function () {

                    var status = $(this).data('status');
                    var dataId = $(this).data('id');

                    if (status == 3 || status == 4) {
                        $.ajax({
                            url: '/api/operations/execution/' + dataId,
                            type: 'GET',
                            contentType: 'application/json',
                            success: function (response) {
                                executions.state["loadedExecution"] = response;
                                parameterCtrl.unloadSectionWithCallbacks(executions.genSections.executionDetails);
                                if (status == 4) {
                                    Swal.fire({
                                        title: 'Error!',
                                        text: 'There was an error while running the execution' + ' - ' + response.failedReason,
                                        icon: 'error',
                                        customClass: {
                                            confirmButton: 'btn btn-primary'
                                        },
                                        buttonsStyling: false
                                    });
                                }
                                else parameterCtrl.loadSectionWithCallbacks(executions.genSections.executionDetails);
                            }
                        });
                    }
                    else {
                        toastr.warning("Job is not completed");
                    }
                });

            },
            postUnload: function (data) {

            }
        },
        executionDetsTable: {
            title: "ExecutionDetsTable",
            identifier: "ExecutionDetsTable",
            postLoad: function (data) {
                StartPageLoader();
                var executionId = executions.state.ExecutionDetailId;
                var data3 = {
                    title: "ExecutionDets",
                    uri: function () {
                        return '/api/operations/execution/details/' + executionId;
                    },
                    identifier: "ExecutionDets",
                    /*
                     * 
                    <th>Salesforce Id</th>
                    <th>Details</th>
                    <th>Status</th>
                    <th>Message</th>
                    <th>MessageCode</th>
                    columns: ['id', 'externalIdName', 'externalIdValue', 'created', 'salesforceId', 'schemaFailure', 'message', 'messageCode', 'jsonReq', 'jsonRes'],
                    */
                    columns: ['id', 'salesforceId', 'externalIdName', 'status', 'message', 'messageCode'],
                    customColumnDefs: [
                        {
                            // Details
                            targets: 3,
                            searchable: true,
                            title: 'Details',
                            orderable: false,
                            render: function (d, type, full, meta) {
                                if (isAvail(full) && isAvail(full.externalIdName) && isAvail(full.externalIdValue)) {
                                    return '<div class="d-flex justify-content-start align-items-center user-name"><div class="d-flex flex-column"><span class="text-truncate">External Id</span><small class="text-truncate text-muted">' + full.externalIdName + ': ' + full.externalIdValue + '</small></div></div>'
                                }
                                return "";
                            }
                        },
                        {
                            // Status
                            targets: 4,
                            searchable: true,
                            title: 'Status',
                            orderable: true,
                            render: function (data, type, full, meta) {
                                var success = '<i class="ti ti-checks ti-sm"/>';
                                var failed = '<i class="ti ti-x ti-sm"/>';

                                var isSuccess = full.schemaFailure != 'true' && full.created == 'true';
                                var icon = isSuccess ? success : failed;

                                var schemaStatus = full.schemaFailure != 'true' ? 'Success' : 'Failed';
                                var createdStatus = full.created == 'true' ? 'Created' : 'Error';

                                if (isAvail(full) && isAvail(full.created) && isAvail(full.schemaFailure)) {
                                    var text = '<span data-bs-toggle="tooltip" data-bs-html="true" aria-label="<span><strong>Status</strong><br> <span class=&quot;fw-medium fw-medium&quot;>Created:</span> ' + createdStatus + '<br> <span class=&quot;fw-medium fw-medium&quot;>Schema:</span> ' + schemaStatus + '</span>" data-bs-original-title="<span><strong>Status</strong><br> <span class=&quot;fw-medium fw-medium&quot;>Created:</span> ' + createdStatus + '<br> <span class=&quot;fw-medium fw-medium&quot;>Schema:</span> ' + schemaStatus + '</span>"><span class="badge badge-center rounded-pill bg-label-' + (isSuccess ? 'success' : 'danger') + ' w-px-30 h-px-30">' + icon + '</span></span>';
                                    return text;
                                }
                                return "";
                            }
                        }
                    ],
                    customRowCallback: function (row, data, index) {
                        $($(row).find('td:eq(3)').find('span[data-bs-toggle="tooltip"]')[0]).tooltip();
                    },
                    buttonFunc: function () {
                        return (null);
                    },
                    customActionColumnRender: function (d, type, full, meta) {
                        return (
                            '<span class="text-nowrap"><button class="btn btn-sm btn-icon me-2 view-record" data-id="' + full['id'] + '"><i class="ti ti-eye"></i></button>'
                        );
                    },
                    completeFunc: function () {
                        $("#hbsOutersectionExecutionDetsRecord").html("");
                        $('#exLargeModal').modal('show');
                        StopPageLoader();
                    }
                };
                parameterCtrl.datatable.loadDataTable(data3);

                $('#dtExecutionDets tbody').on('click', '.view-record', function () {
                    var dataId = $(this).data('id');

                    $.ajax({
                        url: '/api/operations/execution/details/history/' + dataId,
                        type: 'GET',
                        contentType: 'application/json',
                        success: function (response) {
                            executions.state["loadedExecutionHistory"] = response;

                            parameterCtrl.loadSectionWithCallbacks(executions.genSections.executionDetsRecord);
                        }
                    });
                });
            }
        },
        executionDetsRecord: {
            title: "ExecutionDetsRecord",
            identifier: "ExecutionDetsRecord",
            postLoad: function (data) {
                $('#json-request').jsonViewer(JSON.parse(executions.state.loadedExecutionHistory.jsonReq));
                $('#json-response').jsonViewer(JSON.parse(executions.state.loadedExecutionHistory.jsonRes));
                $("#exLargeModal .modal-body").animate({ scrollTop: $('#exLargeModal .modal-body').prop("scrollHeight") }, 'slow');
            },
            data: function () {
                return ({
                    jsonReq: executions.state.loadedExecutionHistory.jsonReq,
                    jsonRes: executions.state.loadedExecutionHistory.jsonRes
                });
            },
            postUnload: function (data) {

            }
        },
        executionDetails: {
            title: "ExecutionDets",
            identifier: "ExecutionDets",
            postLoad: function (data) {
                $([document.documentElement, document.body]).animate({
                    scrollTop: $("#hbsOutersectionExecutionDets").offset().top
                }, 100);
                if (isAvail(executions.state.loadedExecution)) {
                    var timeDifferenceInSeconds = (new Date(executions.state.loadedExecution.completionTime) - new Date(executions.state.loadedExecution.startTime)) / 1000;
                    let scheduledAtDate = moment(executions.state.loadedExecution.scheduledAt).format('lll');

                    var data = {
                        ScheduleName: executions.state.loadedExecution.scheduleName,
                        ScheduleStatus: executions.state.loadedExecution.executionStatus,
                        ScheduledAt: scheduledAtDate,
                        NumberOfJobs: executions.state.loadedExecution.numberOfJobs,
                        SuccessfulJobs: executions.state.loadedExecution.successfulJobs,
                        FailedJobs: executions.state.loadedExecution.failedJobs,
                        TimeSpending: timeDifferenceInSeconds
                    };
                    var source = document.getElementById("hbsExecutionDets").innerHTML;
                    var template = Handlebars.compile(source);
                    $('#ExecutionCardDetail').html(template(data));
                    var jobDetails = [];

                    var executionChartSeries = [];
                    var executionChartLabels = [];
                    var executionChartColors = [];
                    var totalTimeInSecs = timeDifferenceInSeconds + "s";
                    var green = [
                        '#ccff33',
                        '#9ef01a',
                        '#70e000',
                        '#38b000',
                        '#008000',
                        '#007200',
                        '#006400',
                        '#004b23'];
                    var red = [
                        '#fc9ca2',
                        '#fb747d',
                        '#fa4c58',
                        '#f92432',
                        '#e30613',
                        '#c70512',
                        '#9f040e',
                        '#77030b'
                    ];
                    var gI = 0, rI = 0;

                    executions.state.loadedExecution.jobDetails.forEach(function (item, index) {
                        executionChartLabels.push(item.name);
                        executionChartSeries.push((new Date(item.completionTime) - new Date(item.startTime)) / 1000);
                        if (item.status) {
                            executionChartColors.push(green[gI++]);
                        }
                        else {
                            executionChartColors.push(red[rI++]);
                        }

                        var jobDetail = {
                            Initial: item.name[0],
                            Index: index,
                            ExecutionDetailId: item.id,
                            JobName: item.name,
                            JobStatus: item.status ? "Completed" : "Failed",
                            JobCreatedTime: scheduledAtDate,
                            JobStartTime: moment(item.startTime).format('lll'),
                            JobCompletionTime: moment(item.completionTime).format('lll'),
                            JobObservations: item.observations,
                            JobProcessedRecords: item.processedRecords,
                            JobFailedRecords: item.failedRecords,
                            JobTotalRecords: item.totalRecords,
                            SuccessPercent: Math.round(item.processedRecords / item.totalRecords * 100),
                            FailurePercent: Math.round(item.failedRecords / item.totalRecords * 100),
                            ExternalId: item.externalId,
                            SourceName: item.sourceName,
                            ProjectTypeId: item.projectType,
                            DestinationTypeId: item.destinationType,
                            SourceDetName: item.sourceDetName,
                            DestName: item.destName,
                            DestDetName: item.destDetName
                        }
                        jobDetails.push(jobDetail);

                    });

                    var source = document.getElementById("hbsExecutionJobDetails").innerHTML;
                    var template = Handlebars.compile(source);
                    $('#divJobDetails').html(template(jobDetails));

                    executions.state.loadedExecution.jobDetails.forEach(function (item, index) {
                        var chartData = [item.processedRecords, item.failedRecords];
                        executions.charts.doughnutChart(chartData, index);
                    });

                    executions.charts.leadsChart(executionChartLabels, executionChartSeries, executionChartColors, totalTimeInSecs);

                    $(".btnExploreDets").on('click', function () {
                        executions.state.ExecutionDetailId = $(this).data("executiondetailid");
                        parameterCtrl.loadSectionWithCallbacks(executions.genSections.executionDetsTable);
                        $("#detTitle").html($(this).data("jobname"));
                    });

                    $(".btnExtDtModalClose").click(function () {
                        $('#exLargeModal').modal('hide');
                    });

                }
            },
            data: function () {
            },
            postUnload: function (data) {

            }
        }
    }
}

function loadExecutions() {
    parameterCtrl.loadSectionWithCallbacks(executions.genSections.executions);
}

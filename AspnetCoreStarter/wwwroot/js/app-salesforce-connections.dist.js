"use strict";var salesforceConnection={load:function(){var e={title:"Salesforce Connection",uri:function(){return"/api/data/salesforceconnection"},identifier:"SalesforceConnection",type:"Add",fields:salesforceConnection.fvFields(),customFieldMapper:function(e,n){return n}},e=(parameterCtrl.fvAdd(e),{title:"Salesforce Connection",uri:function(){return"/api/data/salesforceconnection/"+salesforceConnection.state.editId},identifier:"SalesforceConnection",type:"Edit",fields:salesforceConnection.fvFields(),customFieldMapper:function(e,n){return n}});parameterCtrl.fvEdit(e);parameterCtrl.datatable.loadDataTable({title:"Salesforce Connection",uri:function(){return"/api/data/salesforceconnection"},identifier:"SalesforceConnection",columns:["id","name","url"]}),$("#dtSalesforceConnection tbody").on("click",".edit-record",function(){var e=$(this).data("id"),n=$("#frmEditSalesforceConnection");$.ajax({url:"/api/data/salesforceconnection/"+e,type:"GET",contentType:"application/json",success:function(e){salesforceConnection.state.editId=e.id,n.find("#editSalesforceConnectionId").val(e.id),$.timeago.settings.allowFuture=!0,n.find('input[name="Name"]').val(e.name),n.find('input[name="Url"]').val(e.url),n.find('input[name="TokenEndpoint"]').val(e.tokenEndpoint),n.find('input[name="Username"]').val(e.username),n.find('input[name="ClientId"]').val(e.clientId),n.find('input[name="ClientSecret"]').val(e.clientSecret),n.find('input[name="Password"]').val(e.password),isAvail(e.tokenExpiry)||$("#connectionAlert").hide()}})})},fvFields:function(){return{fields:{Name:{validators:{notEmpty:{message:"Please enter connection name"}}},Url:{validators:{notEmpty:{message:"Please enter URL"},uri:{message:"Please enter a valid URL"}}}},plugins:parameterCtrl.fv.plugins(),init:parameterCtrl.fv.init()}},state:{editId:null}};document.addEventListener("DOMContentLoaded",function(e){salesforceConnection.load()});
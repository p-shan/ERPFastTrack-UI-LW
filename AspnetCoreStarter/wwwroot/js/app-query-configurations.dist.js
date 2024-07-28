"use strict";var queryConfiguration={load:function(){var e={title:"Query Configuration",uri:function(){return"/api/data/queryconfiguration"},identifier:"QueryConfiguration",type:"Add",fields:queryConfiguration.fvFields(),customFieldMapper:function(e,t){return t}},e=(parameterCtrl.fvAdd(e),{title:"Query Configuration",uri:function(){return"/api/data/queryconfiguration/"+queryConfiguration.state.editId},identifier:"QueryConfiguration",type:"Edit",fields:queryConfiguration.fvFields(),customFieldMapper:function(e,t){return t}});parameterCtrl.fvEdit(e);parameterCtrl.datatable.loadDataTable({title:"Query Configuration",uri:function(){return"/api/data/queryconfiguration"},identifier:"QueryConfiguration",columns:["id","queryName","queryDetails"]}),$("#dtQueryConfiguration tbody").on("click",".edit-record",function(){var e=$(this).data("id"),t=$("#frmEditQueryConfiguration");$.ajax({url:"/api/data/queryconfiguration/"+e,type:"GET",contentType:"application/json",success:function(e){queryConfiguration.state.editId=e.id,t.find('input[name="QueryName"]').val(e.queryName),t.find('textarea[name="QueryDetails"]').val(e.queryDetails)}})})},fvFields:function(){return{fields:{QueryName:{validators:{notEmpty:{message:"Please enter query name"}}},QueryDetails:{validators:{notEmpty:{message:"Please enter query details"}}}},plugins:parameterCtrl.fv.plugins(),init:parameterCtrl.fv.init()}},state:{editId:null}};document.addEventListener("DOMContentLoaded",function(e){queryConfiguration.load()});
"use strict";document.addEventListener("DOMContentLoaded",function(t){FormValidation.formValidation(document.getElementById("addProjectForm"),{fields:{Name:{validators:{notEmpty:{message:"Please enter project name"}}},SfDbMappingId:{validators:{notEmpty:{message:"Please select salesforce database mapping"}}}},plugins:{trigger:new FormValidation.plugins.Trigger,bootstrap5:new FormValidation.plugins.Bootstrap5({eleValidClass:"",rowSelector:".form-field"}),submitButton:new FormValidation.plugins.SubmitButton,autoFocus:new FormValidation.plugins.AutoFocus},init:t=>{t.on("plugins.message.placed",function(t){t.element.parentElement.classList.contains("input-group")&&t.element.parentElement.insertAdjacentElement("afterend",t.messageElement)})}}).on("core.form.valid",function(n){var o={};$.each(n.formValidation.form,function(t,e){null!=n.formValidation.fields[e.name]&&(o[e.name]=e.value)}),o.OrgId=getCookie("selectedOrg"),o.SfDbMappingId=1,$.ajax({url:"/api/Projects",type:"POST",contentType:"application/json",data:JSON.stringify(o),success:function(t){Swal.fire({title:"Good job!",text:"You clicked the button!",icon:"success",customClass:{confirmButton:"btn btn-primary"},buttonsStyling:!1})},error:function(t){Swal.fire({title:"Good Error!",text:"You clicked the button!",icon:"error",customClass:{confirmButton:"btn btn-primary"},buttonsStyling:!1})}})})});
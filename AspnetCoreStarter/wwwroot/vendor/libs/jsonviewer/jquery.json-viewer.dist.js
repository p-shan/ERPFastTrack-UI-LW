!function(e,t){if("object"==typeof exports&&"object"==typeof module)module.exports=t();else if("function"==typeof define&&define.amd)define([],t);else{var s=t();for(var o in s)("object"==typeof exports?exports:e)[o]=s[o]}}(self,(()=>(()=>{function e(t){return e="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},e(t)}return function(t){function s(e){return e instanceof Object&&Object.keys(e).length>0}function o(e){return e.replace(/&/g,"&amp;").replace(/</g,"&lt;").replace(/>/g,"&gt;").replace(/'/g,"&apos;").replace(/"/g,"&quot;")}function n(t,l){var i="";if("string"==typeof t)t=o(t),l.withLinks&&function(e){for(var t=["http","https","ftp","ftps"],s=0;s<t.length;++s)if(e.startsWith(t[s]+"://"))return!0;return!1}(t)?i+='<a href="'+t+'" class="json-string" target="_blank">'+t+"</a>":i+='<span class="json-string">"'+(t=t.replace(/&quot;/g,"\\&quot;"))+'"</span>';else if("number"==typeof t||"bigint"==typeof t)i+='<span class="json-literal">'+t+"</span>";else if("boolean"==typeof t)i+='<span class="json-literal">'+t+"</span>";else if(null===t)i+='<span class="json-literal">null</span>';else if(t instanceof Array)if(t.length>0){i+='[<ol class="json-array">';for(var a=0;a<t.length;++a)i+="<li>",s(t[a])&&(i+='<a href class="json-toggle"></a>'),i+=n(t[a],l),a<t.length-1&&(i+=","),i+="</li>";i+="</ol>]"}else i+="[]";else if("object"===e(t))if(l.bigNumbers&&("function"==typeof t.toExponential||t.isLosslessNumber))i+='<span class="json-literal">'+t.toString()+"</span>";else{var r=Object.keys(t).length;if(r>0){for(var c in i+='{<ul class="json-dict">',t)if(Object.prototype.hasOwnProperty.call(t,c)){c=o(c);var f=l.withQuotes?'<span class="json-string">"'+c+'"</span>':c;i+="<li>",s(t[c])?i+='<a href class="json-toggle">'+f+"</a>":i+=f,i+=": "+n(t[c],l),--r>0&&(i+=","),i+="</li>"}i+="</ul>}"}else i+="{}"}return i}t.fn.jsonViewer=function(e,o){return o=Object.assign({},{collapsed:!1,rootCollapsable:!0,withQuotes:!1,withLinks:!0,bigNumbers:!1},o),this.each((function(){var l=n(e,o);o.rootCollapsable&&s(e)&&(l='<a href class="json-toggle"></a>'+l),t(this).html(l),t(this).addClass("json-document"),t(this).off("click"),t(this).on("click","a.json-toggle",(function(){var e=t(this).toggleClass("collapsed").siblings("ul.json-dict, ol.json-array");if(e.toggle(),e.is(":visible"))e.siblings(".json-placeholder").remove();else{var s=e.children("li").length,o=s+(s>1?" items":" item");e.after('<a href class="json-placeholder">'+o+"</a>")}return!1})),t(this).on("click","a.json-placeholder",(function(){return t(this).siblings("a.json-toggle").click(),!1})),1==o.collapsed&&t(this).find("a.json-toggle").click()}))}}(jQuery),{}})()));
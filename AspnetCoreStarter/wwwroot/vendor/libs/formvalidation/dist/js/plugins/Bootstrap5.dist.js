!function(e,t){if("object"==typeof exports&&"object"==typeof module)module.exports=t();else if("function"==typeof define&&define.amd)define([],t);else{var n=t();for(var r in n)("object"==typeof exports?exports:e)[r]=n[r]}}(self,(()=>{return e={80016:function(e,t,n){var r,o,i;function l(e){return l="function"==typeof Symbol&&"symbol"==typeof Symbol.iterator?function(e){return typeof e}:function(e){return e&&"function"==typeof Symbol&&e.constructor===Symbol&&e!==Symbol.prototype?"symbol":typeof e},l(e)}i=function(){"use strict";function e(e,t){for(var n=0;n<t.length;n++){var r=t[n];r.enumerable=r.enumerable||!1,r.configurable=!0,"value"in r&&(r.writable=!0),Object.defineProperty(e,r.key,r)}}function t(e){return t=Object.setPrototypeOf?Object.getPrototypeOf.bind():function(e){return e.__proto__||Object.getPrototypeOf(e)},t(e)}function n(e,t){return n=Object.setPrototypeOf?Object.setPrototypeOf.bind():function(e,t){return e.__proto__=t,e},n(e,t)}function r(e){if(void 0===e)throw new ReferenceError("this hasn't been initialised - super() hasn't been called");return e}function o(){return o="undefined"!=typeof Reflect&&Reflect.get?Reflect.get.bind():function(e,n,r){var o=function(e,n){for(;!Object.prototype.hasOwnProperty.call(e,n)&&null!==(e=t(e)););return e}(e,n);if(o){var i=Object.getOwnPropertyDescriptor(o,n);return i.get?i.get.call(arguments.length<3?e:r):i.value}},o.apply(this,arguments)}var i=FormValidation.utils.classSet,a=FormValidation.utils.hasClass,c=function(c){!function(e,t){if("function"!=typeof t&&null!==t)throw new TypeError("Super expression must either be null or a function");e.prototype=Object.create(t&&t.prototype,{constructor:{value:e,writable:!0,configurable:!0}}),Object.defineProperty(e,"prototype",{writable:!1}),t&&n(e,t)}(m,FormValidation.plugins.Framework);var f,u,s,p,d=(s=m,p=function(){if("undefined"==typeof Reflect||!Reflect.construct)return!1;if(Reflect.construct.sham)return!1;if("function"==typeof Proxy)return!0;try{return Boolean.prototype.valueOf.call(Reflect.construct(Boolean,[],(function(){}))),!0}catch(e){return!1}}(),function(){var e,n=t(s);if(p){var o=t(this).constructor;e=Reflect.construct(n,arguments,o)}else e=n.apply(this,arguments);return function(e,t){if(t&&("object"===l(t)||"function"==typeof t))return t;if(void 0!==t)throw new TypeError("Derived constructors may only return object or undefined");return r(e)}(this,e)});function m(e){var t;return function(e,t){if(!(e instanceof t))throw new TypeError("Cannot call a class as a function")}(this,m),(t=d.call(this,Object.assign({},{eleInvalidClass:"is-invalid",eleValidClass:"is-valid",formClass:"fv-plugins-bootstrap5",rowInvalidClass:"fv-plugins-bootstrap5-row-invalid",rowPattern:/^(.*)(col|offset)(-(sm|md|lg|xl))*-[0-9]+(.*)$/,rowSelector:".row",rowValidClass:"fv-plugins-bootstrap5-row-valid"},e))).eleValidatedHandler=t.handleElementValidated.bind(r(t)),t}return f=m,(u=[{key:"install",value:function(){o(t(m.prototype),"install",this).call(this),this.core.on("core.element.validated",this.eleValidatedHandler)}},{key:"uninstall",value:function(){o(t(m.prototype),"install",this).call(this),this.core.off("core.element.validated",this.eleValidatedHandler)}},{key:"handleElementValidated",value:function(e){var t=e.element.getAttribute("type");if(("checkbox"===t||"radio"===t)&&e.elements.length>1&&a(e.element,"form-check-input")){var n=e.element.parentElement;a(n,"form-check")&&a(n,"form-check-inline")&&i(n,{"is-invalid":!e.valid,"is-valid":e.valid})}}},{key:"onIconPlaced",value:function(e){i(e.element,{"fv-plugins-icon-input":!0});var t=e.element.parentElement;a(t,"input-group")&&(t.parentElement.insertBefore(e.iconElement,t.nextSibling),e.element.nextElementSibling&&a(e.element.nextElementSibling,"input-group-text")&&i(e.iconElement,{"fv-plugins-icon-input-group":!0}));var n=e.element.getAttribute("type");if("checkbox"===n||"radio"===n){var r=t.parentElement;a(t,"form-check")?(i(e.iconElement,{"fv-plugins-icon-check":!0}),t.parentElement.insertBefore(e.iconElement,t.nextSibling)):a(t.parentElement,"form-check")&&(i(e.iconElement,{"fv-plugins-icon-check":!0}),r.parentElement.insertBefore(e.iconElement,r.nextSibling))}}},{key:"onMessagePlaced",value:function(e){e.messageElement.classList.add("invalid-feedback");var t=e.element.parentElement;if(a(t,"input-group"))return t.appendChild(e.messageElement),void i(t,{"has-validation":!0});var n=e.element.getAttribute("type");"checkbox"!==n&&"radio"!==n||!a(e.element,"form-check-input")||!a(t,"form-check")||a(t,"form-check-inline")||e.elements[e.elements.length-1].parentElement.appendChild(e.messageElement)}}])&&e(f.prototype,u),Object.defineProperty(f,"prototype",{writable:!1}),m}();return c},"object"===l(t)?e.exports=i():void 0===(o="function"==typeof(r=i)?r.call(t,n,t,e):r)||(e.exports=o)}},t={},function n(r){var o=t[r];if(void 0!==o)return o.exports;var i=t[r]={exports:{}};return e[r].call(i.exports,i,i.exports,n),i.exports}(80016);var e,t}));
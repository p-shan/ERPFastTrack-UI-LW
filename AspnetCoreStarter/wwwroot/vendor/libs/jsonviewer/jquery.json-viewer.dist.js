/*
 * ATTENTION: The "eval" devtool has been used (maybe by default in mode: "development").
 * This devtool is neither made for production nor for readable output files.
 * It uses "eval()" calls to create a separate source file in the browser devtools.
 * If you are trying to read the output file, select a different devtool (https://webpack.js.org/configuration/devtool/)
 * or disable the default devtool with "devtool: false".
 * If you are looking for production-ready output files, see mode: "production" (https://webpack.js.org/configuration/mode/).
 */
(function webpackUniversalModuleDefinition(root, factory) {
	if(typeof exports === 'object' && typeof module === 'object')
		module.exports = factory();
	else if(typeof define === 'function' && define.amd)
		define([], factory);
	else {
		var a = factory();
		for(var i in a) (typeof exports === 'object' ? exports : root)[i] = a[i];
	}
})(self, () => {
return /******/ (() => { // webpackBootstrap
/******/ 	var __webpack_modules__ = ({

/***/ "./wwwroot/vendor/libs/jsonviewer/jquery.json-viewer.js":
/*!**************************************************************!*\
  !*** ./wwwroot/vendor/libs/jsonviewer/jquery.json-viewer.js ***!
  \**************************************************************/
/***/ (() => {

eval("function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n/**\n * jQuery json-viewer\n * @author: Alexandre Bodelot <alexandre.bodelot@gmail.com>\n * @link: https://github.com/abodelot/jquery.json-viewer\n */\n(function ($) {\n  /**\n   * Check if arg is either an array with at least 1 element, or a dict with at least 1 key\n   * @return boolean\n   */\n  function isCollapsable(arg) {\n    return arg instanceof Object && Object.keys(arg).length > 0;\n  }\n\n  /**\n   * Check if a string looks like a URL, based on protocol\n   * This doesn't attempt to validate URLs, there's no use and syntax can be too complex\n   * @return boolean\n   */\n  function isUrl(string) {\n    var protocols = ['http', 'https', 'ftp', 'ftps'];\n    for (var i = 0; i < protocols.length; ++i) {\n      if (string.startsWith(protocols[i] + '://')) {\n        return true;\n      }\n    }\n    return false;\n  }\n\n  /**\n   * Return the input string html escaped\n   * @return string\n   */\n  function htmlEscape(s) {\n    return s.replace(/&/g, '&amp;').replace(/</g, '&lt;').replace(/>/g, '&gt;').replace(/'/g, '&apos;').replace(/\"/g, '&quot;');\n  }\n\n  /**\n   * Transform a json object into html representation\n   * @return string\n   */\n  function json2html(json, options) {\n    var html = '';\n    if (typeof json === 'string') {\n      // Escape tags and quotes\n      json = htmlEscape(json);\n      if (options.withLinks && isUrl(json)) {\n        html += '<a href=\"' + json + '\" class=\"json-string\" target=\"_blank\">' + json + '</a>';\n      } else {\n        // Escape double quotes in the rendered non-URL string.\n        json = json.replace(/&quot;/g, '\\\\&quot;');\n        html += '<span class=\"json-string\">\"' + json + '\"</span>';\n      }\n    } else if (typeof json === 'number' || typeof json === 'bigint') {\n      html += '<span class=\"json-literal\">' + json + '</span>';\n    } else if (typeof json === 'boolean') {\n      html += '<span class=\"json-literal\">' + json + '</span>';\n    } else if (json === null) {\n      html += '<span class=\"json-literal\">null</span>';\n    } else if (json instanceof Array) {\n      if (json.length > 0) {\n        html += '[<ol class=\"json-array\">';\n        for (var i = 0; i < json.length; ++i) {\n          html += '<li>';\n          // Add toggle button if item is collapsable\n          if (isCollapsable(json[i])) {\n            html += '<a href class=\"json-toggle\"></a>';\n          }\n          html += json2html(json[i], options);\n          // Add comma if item is not last\n          if (i < json.length - 1) {\n            html += ',';\n          }\n          html += '</li>';\n        }\n        html += '</ol>]';\n      } else {\n        html += '[]';\n      }\n    } else if (_typeof(json) === 'object') {\n      // Optional support different libraries for big numbers\n      // json.isLosslessNumber: package lossless-json\n      // json.toExponential(): packages bignumber.js, big.js, decimal.js, decimal.js-light, others?\n      if (options.bigNumbers && (typeof json.toExponential === 'function' || json.isLosslessNumber)) {\n        html += '<span class=\"json-literal\">' + json.toString() + '</span>';\n      } else {\n        var keyCount = Object.keys(json).length;\n        if (keyCount > 0) {\n          html += '{<ul class=\"json-dict\">';\n          for (var key in json) {\n            if (Object.prototype.hasOwnProperty.call(json, key)) {\n              key = htmlEscape(key);\n              var keyRepr = options.withQuotes ? '<span class=\"json-string\">\"' + key + '\"</span>' : key;\n              html += '<li>';\n              // Add toggle button if item is collapsable\n              if (isCollapsable(json[key])) {\n                html += '<a href class=\"json-toggle\">' + keyRepr + '</a>';\n              } else {\n                html += keyRepr;\n              }\n              html += ': ' + json2html(json[key], options);\n              // Add comma if item is not last\n              if (--keyCount > 0) {\n                html += ',';\n              }\n              html += '</li>';\n            }\n          }\n          html += '</ul>}';\n        } else {\n          html += '{}';\n        }\n      }\n    }\n    return html;\n  }\n\n  /**\n   * jQuery plugin method\n   * @param json: a javascript object\n   * @param options: an optional options hash\n   */\n  $.fn.jsonViewer = function (json, options) {\n    // Merge user options with default options\n    options = Object.assign({}, {\n      collapsed: false,\n      rootCollapsable: true,\n      withQuotes: false,\n      withLinks: true,\n      bigNumbers: false\n    }, options);\n\n    // jQuery chaining\n    return this.each(function () {\n      // Transform to HTML\n      var html = json2html(json, options);\n      if (options.rootCollapsable && isCollapsable(json)) {\n        html = '<a href class=\"json-toggle\"></a>' + html;\n      }\n\n      // Insert HTML in target DOM element\n      $(this).html(html);\n      $(this).addClass('json-document');\n\n      // Bind click on toggle buttons\n      $(this).off('click');\n      $(this).on('click', 'a.json-toggle', function () {\n        var target = $(this).toggleClass('collapsed').siblings('ul.json-dict, ol.json-array');\n        target.toggle();\n        if (target.is(':visible')) {\n          target.siblings('.json-placeholder').remove();\n        } else {\n          var count = target.children('li').length;\n          var placeholder = count + (count > 1 ? ' items' : ' item');\n          target.after('<a href class=\"json-placeholder\">' + placeholder + '</a>');\n        }\n        return false;\n      });\n\n      // Simulate click on toggle button when placeholder is clicked\n      $(this).on('click', 'a.json-placeholder', function () {\n        $(this).siblings('a.json-toggle').click();\n        return false;\n      });\n      if (options.collapsed == true) {\n        // Trigger click to collapse all nodes\n        $(this).find('a.json-toggle').click();\n      }\n    });\n  };\n})(jQuery);\n\n//# sourceURL=webpack://erpfasttrackui/./wwwroot/vendor/libs/jsonviewer/jquery.json-viewer.js?");

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = {};
/******/ 	__webpack_modules__["./wwwroot/vendor/libs/jsonviewer/jquery.json-viewer.js"]();
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});
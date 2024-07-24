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
		module.exports = factory(require("jQuery"));
	else if(typeof define === 'function' && define.amd)
		define(["jQuery"], factory);
	else {
		var a = typeof exports === 'object' ? factory(require("jQuery")) : factory(root["jQuery"]);
		for(var i in a) (typeof exports === 'object' ? exports : root)[i] = a[i];
	}
})(self, (__WEBPACK_EXTERNAL_MODULE_jquery__) => {
return /******/ (() => { // webpackBootstrap
/******/ 	var __webpack_modules__ = ({

/***/ "./wwwroot/vendor/libs/timeago/jquery.timeago.js":
/*!*******************************************************!*\
  !*** ./wwwroot/vendor/libs/timeago/jquery.timeago.js ***!
  \*******************************************************/
/***/ ((module, exports, __webpack_require__) => {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_ARRAY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n/**\n * Timeago is a jQuery plugin that makes it easy to support automatically\n * updating fuzzy timestamps (e.g. \"4 minutes ago\" or \"about 1 day ago\").\n *\n * @name timeago\n * @version 1.6.7\n * @requires jQuery >=1.5.0 <4.0\n * @author Ryan McGeary\n * @license MIT License - http://www.opensource.org/licenses/mit-license.php\n *\n * For usage and examples, visit:\n * http://timeago.yarp.com/\n *\n * Copyright (c) 2008-2019, Ryan McGeary (ryan -[at]- mcgeary [*dot*] org)\n */\n\n(function (factory) {\n  if (true) {\n    // AMD. Register as an anonymous module.\n    !(__WEBPACK_AMD_DEFINE_ARRAY__ = [__webpack_require__(/*! jquery */ \"jquery\")], __WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.apply(exports, __WEBPACK_AMD_DEFINE_ARRAY__)) : __WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__));\n  } else {}\n})(function ($) {\n  $.timeago = function (timestamp) {\n    if (timestamp instanceof Date) {\n      return inWords(timestamp);\n    } else if (typeof timestamp === \"string\") {\n      return inWords($.timeago.parse(timestamp));\n    } else if (typeof timestamp === \"number\") {\n      return inWords(new Date(timestamp));\n    } else {\n      return inWords($.timeago.datetime(timestamp));\n    }\n  };\n  var $t = $.timeago;\n  $.extend($.timeago, {\n    settings: {\n      refreshMillis: 60000,\n      allowPast: true,\n      allowFuture: false,\n      localeTitle: false,\n      cutoff: 0,\n      autoDispose: true,\n      strings: {\n        prefixAgo: null,\n        prefixFromNow: null,\n        suffixAgo: \"ago\",\n        suffixFromNow: \"from now\",\n        inPast: \"any moment now\",\n        seconds: \"less than a minute\",\n        minute: \"about a minute\",\n        minutes: \"%d minutes\",\n        hour: \"about an hour\",\n        hours: \"about %d hours\",\n        day: \"a day\",\n        days: \"%d days\",\n        month: \"about a month\",\n        months: \"%d months\",\n        year: \"about a year\",\n        years: \"%d years\",\n        wordSeparator: \" \",\n        numbers: []\n      }\n    },\n    inWords: function inWords(distanceMillis) {\n      if (!this.settings.allowPast && !this.settings.allowFuture) {\n        throw 'timeago allowPast and allowFuture settings can not both be set to false.';\n      }\n      var $l = this.settings.strings;\n      var prefix = $l.prefixAgo;\n      var suffix = $l.suffixAgo;\n      if (this.settings.allowFuture) {\n        if (distanceMillis < 0) {\n          prefix = $l.prefixFromNow;\n          suffix = $l.suffixFromNow;\n        }\n      }\n      if (!this.settings.allowPast && distanceMillis >= 0) {\n        return this.settings.strings.inPast;\n      }\n      var seconds = Math.abs(distanceMillis) / 1000;\n      var minutes = seconds / 60;\n      var hours = minutes / 60;\n      var days = hours / 24;\n      var years = days / 365;\n      function substitute(stringOrFunction, number) {\n        var string = $.isFunction(stringOrFunction) ? stringOrFunction(number, distanceMillis) : stringOrFunction;\n        var value = $l.numbers && $l.numbers[number] || number;\n        return string.replace(/%d/i, value);\n      }\n      var words = seconds < 45 && substitute($l.seconds, Math.round(seconds)) || seconds < 90 && substitute($l.minute, 1) || minutes < 45 && substitute($l.minutes, Math.round(minutes)) || minutes < 90 && substitute($l.hour, 1) || hours < 24 && substitute($l.hours, Math.round(hours)) || hours < 42 && substitute($l.day, 1) || days < 30 && substitute($l.days, Math.round(days)) || days < 45 && substitute($l.month, 1) || days < 365 && substitute($l.months, Math.round(days / 30)) || years < 1.5 && substitute($l.year, 1) || substitute($l.years, Math.round(years));\n      var separator = $l.wordSeparator || \"\";\n      if ($l.wordSeparator === undefined) {\n        separator = \" \";\n      }\n      return $.trim([prefix, words, suffix].join(separator));\n    },\n    parse: function parse(iso8601) {\n      var s = $.trim(iso8601);\n      s = s.replace(/\\.\\d+/, \"\"); // remove milliseconds\n      s = s.replace(/-/, \"/\").replace(/-/, \"/\");\n      s = s.replace(/T/, \" \").replace(/Z/, \" UTC\");\n      s = s.replace(/([\\+\\-]\\d\\d)\\:?(\\d\\d)/, \" $1$2\"); // -04:00 -> -0400\n      s = s.replace(/([\\+\\-]\\d\\d)$/, \" $100\"); // +09 -> +0900\n      return new Date(s);\n    },\n    datetime: function datetime(elem) {\n      var iso8601 = $t.isTime(elem) ? $(elem).attr(\"datetime\") : $(elem).attr(\"title\");\n      return $t.parse(iso8601);\n    },\n    isTime: function isTime(elem) {\n      // jQuery's `is()` doesn't play well with HTML5 in IE\n      return $(elem).get(0).tagName.toLowerCase() === \"time\"; // $(elem).is(\"time\");\n    }\n  });\n\n  // functions that can be called via $(el).timeago('action')\n  // init is default when no action is given\n  // functions are called with context of a single element\n  var functions = {\n    init: function init() {\n      functions.dispose.call(this);\n      var refresh_el = $.proxy(refresh, this);\n      refresh_el();\n      var $s = $t.settings;\n      if ($s.refreshMillis > 0) {\n        this._timeagoInterval = setInterval(refresh_el, $s.refreshMillis);\n      }\n    },\n    update: function update(timestamp) {\n      var date = timestamp instanceof Date ? timestamp : $t.parse(timestamp);\n      $(this).data('timeago', {\n        datetime: date\n      });\n      if ($t.settings.localeTitle) {\n        $(this).attr(\"title\", date.toLocaleString());\n      }\n      refresh.apply(this);\n    },\n    updateFromDOM: function updateFromDOM() {\n      $(this).data('timeago', {\n        datetime: $t.parse($t.isTime(this) ? $(this).attr(\"datetime\") : $(this).attr(\"title\"))\n      });\n      refresh.apply(this);\n    },\n    dispose: function dispose() {\n      if (this._timeagoInterval) {\n        window.clearInterval(this._timeagoInterval);\n        this._timeagoInterval = null;\n      }\n    }\n  };\n  $.fn.timeago = function (action, options) {\n    var fn = action ? functions[action] : functions.init;\n    if (!fn) {\n      throw new Error(\"Unknown function name '\" + action + \"' for timeago\");\n    }\n    // each over objects here and call the requested function\n    this.each(function () {\n      fn.call(this, options);\n    });\n    return this;\n  };\n  function refresh() {\n    var $s = $t.settings;\n\n    //check if it's still visible\n    if ($s.autoDispose && !$.contains(document.documentElement, this)) {\n      //stop if it has been removed\n      $(this).timeago(\"dispose\");\n      return this;\n    }\n    var data = prepareData(this);\n    if (!isNaN(data.datetime)) {\n      if ($s.cutoff === 0 || Math.abs(distance(data.datetime)) < $s.cutoff) {\n        $(this).text(inWords(data.datetime));\n      } else {\n        if ($(this).attr('title').length > 0) {\n          $(this).text($(this).attr('title'));\n        }\n      }\n    }\n    return this;\n  }\n  function prepareData(element) {\n    element = $(element);\n    if (!element.data(\"timeago\")) {\n      element.data(\"timeago\", {\n        datetime: $t.datetime(element)\n      });\n      var text = $.trim(element.text());\n      if ($t.settings.localeTitle) {\n        element.attr(\"title\", element.data('timeago').datetime.toLocaleString());\n      } else if (text.length > 0 && !($t.isTime(element) && element.attr(\"title\"))) {\n        element.attr(\"title\", text);\n      }\n    }\n    return element.data(\"timeago\");\n  }\n  function inWords(date) {\n    return $t.inWords(distance(date));\n  }\n  function distance(date) {\n    return new Date().getTime() - date.getTime();\n  }\n\n  // fix for IE6 suckage\n  document.createElement(\"abbr\");\n  document.createElement(\"time\");\n});\n\n//# sourceURL=webpack://erpfasttrackui/./wwwroot/vendor/libs/timeago/jquery.timeago.js?");

/***/ }),

/***/ "jquery":
/*!*************************!*\
  !*** external "jQuery" ***!
  \*************************/
/***/ ((module) => {

"use strict";
module.exports = __WEBPACK_EXTERNAL_MODULE_jquery__;

/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module can't be inlined because the eval devtool is used.
/******/ 	var __webpack_exports__ = __webpack_require__("./wwwroot/vendor/libs/timeago/jquery.timeago.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});
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

/***/ "./wwwroot/vendor/libs/formvalidation/dist/js/plugins/Wizard.js":
/*!**********************************************************************!*\
  !*** ./wwwroot/vendor/libs/formvalidation/dist/js/plugins/Wizard.js ***!
  \**********************************************************************/
/***/ (function(module, exports, __webpack_require__) {

eval("var __WEBPACK_AMD_DEFINE_FACTORY__, __WEBPACK_AMD_DEFINE_RESULT__;function _typeof(o) { \"@babel/helpers - typeof\"; return _typeof = \"function\" == typeof Symbol && \"symbol\" == typeof Symbol.iterator ? function (o) { return typeof o; } : function (o) { return o && \"function\" == typeof Symbol && o.constructor === Symbol && o !== Symbol.prototype ? \"symbol\" : typeof o; }, _typeof(o); }\n/**\n * FormValidation (https://formvalidation.io), v1.10.0 (2236098)\n * The best validation library for JavaScript\n * (c) 2013 - 2021 Nguyen Huu Phuoc <me@phuoc.ng>\n */\n\n(function (global, factory) {\n  ( false ? 0 : _typeof(exports)) === 'object' && \"object\" !== 'undefined' ? module.exports = factory() :  true ? !(__WEBPACK_AMD_DEFINE_FACTORY__ = (factory),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ = (typeof __WEBPACK_AMD_DEFINE_FACTORY__ === 'function' ?\n\t\t(__WEBPACK_AMD_DEFINE_FACTORY__.call(exports, __webpack_require__, exports, module)) :\n\t\t__WEBPACK_AMD_DEFINE_FACTORY__),\n\t\t__WEBPACK_AMD_DEFINE_RESULT__ !== undefined && (module.exports = __WEBPACK_AMD_DEFINE_RESULT__)) : (0);\n})(this, function () {\n  'use strict';\n\n  function _classCallCheck(instance, Constructor) {\n    if (!(instance instanceof Constructor)) {\n      throw new TypeError(\"Cannot call a class as a function\");\n    }\n  }\n  function _defineProperties(target, props) {\n    for (var i = 0; i < props.length; i++) {\n      var descriptor = props[i];\n      descriptor.enumerable = descriptor.enumerable || false;\n      descriptor.configurable = true;\n      if (\"value\" in descriptor) descriptor.writable = true;\n      Object.defineProperty(target, descriptor.key, descriptor);\n    }\n  }\n  function _createClass(Constructor, protoProps, staticProps) {\n    if (protoProps) _defineProperties(Constructor.prototype, protoProps);\n    if (staticProps) _defineProperties(Constructor, staticProps);\n    Object.defineProperty(Constructor, \"prototype\", {\n      writable: false\n    });\n    return Constructor;\n  }\n  function _defineProperty(obj, key, value) {\n    if (key in obj) {\n      Object.defineProperty(obj, key, {\n        value: value,\n        enumerable: true,\n        configurable: true,\n        writable: true\n      });\n    } else {\n      obj[key] = value;\n    }\n    return obj;\n  }\n  function _inherits(subClass, superClass) {\n    if (typeof superClass !== \"function\" && superClass !== null) {\n      throw new TypeError(\"Super expression must either be null or a function\");\n    }\n    subClass.prototype = Object.create(superClass && superClass.prototype, {\n      constructor: {\n        value: subClass,\n        writable: true,\n        configurable: true\n      }\n    });\n    Object.defineProperty(subClass, \"prototype\", {\n      writable: false\n    });\n    if (superClass) _setPrototypeOf(subClass, superClass);\n  }\n  function _getPrototypeOf(o) {\n    _getPrototypeOf = Object.setPrototypeOf ? Object.getPrototypeOf.bind() : function _getPrototypeOf(o) {\n      return o.__proto__ || Object.getPrototypeOf(o);\n    };\n    return _getPrototypeOf(o);\n  }\n  function _setPrototypeOf(o, p) {\n    _setPrototypeOf = Object.setPrototypeOf ? Object.setPrototypeOf.bind() : function _setPrototypeOf(o, p) {\n      o.__proto__ = p;\n      return o;\n    };\n    return _setPrototypeOf(o, p);\n  }\n  function _isNativeReflectConstruct() {\n    if (typeof Reflect === \"undefined\" || !Reflect.construct) return false;\n    if (Reflect.construct.sham) return false;\n    if (typeof Proxy === \"function\") return true;\n    try {\n      Boolean.prototype.valueOf.call(Reflect.construct(Boolean, [], function () {}));\n      return true;\n    } catch (e) {\n      return false;\n    }\n  }\n  function _assertThisInitialized(self) {\n    if (self === void 0) {\n      throw new ReferenceError(\"this hasn't been initialised - super() hasn't been called\");\n    }\n    return self;\n  }\n  function _possibleConstructorReturn(self, call) {\n    if (call && (_typeof(call) === \"object\" || typeof call === \"function\")) {\n      return call;\n    } else if (call !== void 0) {\n      throw new TypeError(\"Derived constructors may only return object or undefined\");\n    }\n    return _assertThisInitialized(self);\n  }\n  function _createSuper(Derived) {\n    var hasNativeReflectConstruct = _isNativeReflectConstruct();\n    return function _createSuperInternal() {\n      var Super = _getPrototypeOf(Derived),\n        result;\n      if (hasNativeReflectConstruct) {\n        var NewTarget = _getPrototypeOf(this).constructor;\n        result = Reflect.construct(Super, arguments, NewTarget);\n      } else {\n        result = Super.apply(this, arguments);\n      }\n      return _possibleConstructorReturn(this, result);\n    };\n  }\n  var t = FormValidation.Plugin;\n  var e = FormValidation.utils.classSet;\n  var s = FormValidation.plugins.Excluded;\n  var i = /*#__PURE__*/function (_t) {\n    _inherits(i, _t);\n    var _super = _createSuper(i);\n    function i(t) {\n      var _this;\n      _classCallCheck(this, i);\n      _this = _super.call(this, t);\n      _this.currentStep = 0;\n      _this.numSteps = 0;\n      _this.stepIndexes = [];\n      _this.opts = Object.assign({}, {\n        activeStepClass: \"fv-plugins-wizard--active\",\n        onStepActive: function onStepActive() {},\n        onStepInvalid: function onStepInvalid() {},\n        onStepValid: function onStepValid() {},\n        onValid: function onValid() {},\n        stepClass: \"fv-plugins-wizard--step\"\n      }, t);\n      _this.prevStepHandler = _this.onClickPrev.bind(_assertThisInitialized(_this));\n      _this.nextStepHandler = _this.onClickNext.bind(_assertThisInitialized(_this));\n      return _this;\n    }\n    _createClass(i, [{\n      key: \"install\",\n      value: function install() {\n        var _this2 = this;\n        this.core.registerPlugin(i.EXCLUDED_PLUGIN, this.opts.isFieldExcluded ? new s({\n          excluded: this.opts.isFieldExcluded\n        }) : new s());\n        var t = this.core.getFormElement();\n        this.steps = [].slice.call(t.querySelectorAll(this.opts.stepSelector));\n        this.numSteps = this.steps.length;\n        this.steps.forEach(function (t) {\n          e(t, _defineProperty({}, _this2.opts.stepClass, true));\n        });\n        e(this.steps[0], _defineProperty({}, this.opts.activeStepClass, true));\n        this.stepIndexes = Array(this.numSteps).fill(0).map(function (t, e) {\n          return e;\n        });\n        this.prevButton = typeof this.opts.prevButton === \"string\" ? this.opts.prevButton.substring(0, 1) === \"#\" ? document.getElementById(this.opts.prevButton.substring(1)) : t.querySelector(this.opts.prevButton) : this.opts.prevButton;\n        this.nextButton = typeof this.opts.nextButton === \"string\" ? this.opts.nextButton.substring(0, 1) === \"#\" ? document.getElementById(this.opts.nextButton.substring(1)) : t.querySelector(this.opts.nextButton) : this.opts.nextButton;\n        this.prevButton.addEventListener(\"click\", this.prevStepHandler);\n        this.nextButton.addEventListener(\"click\", this.nextStepHandler);\n      }\n    }, {\n      key: \"uninstall\",\n      value: function uninstall() {\n        this.core.deregisterPlugin(i.EXCLUDED_PLUGIN);\n        this.prevButton.removeEventListener(\"click\", this.prevStepHandler);\n        this.nextButton.removeEventListener(\"click\", this.nextStepHandler);\n        this.stepIndexes.length = 0;\n      }\n    }, {\n      key: \"getCurrentStep\",\n      value: function getCurrentStep() {\n        return this.currentStep;\n      }\n    }, {\n      key: \"goToPrevStep\",\n      value: function goToPrevStep() {\n        var _this3 = this;\n        var t = this.currentStep - 1;\n        if (t < 0) {\n          return;\n        }\n        var e = this.opts.isStepSkipped ? this.stepIndexes.slice(0, this.currentStep).reverse().find(function (t, e) {\n          return !_this3.opts.isStepSkipped({\n            currentStep: _this3.currentStep,\n            numSteps: _this3.numSteps,\n            targetStep: t\n          });\n        }) : t;\n        this.goToStep(e);\n        this.onStepActive();\n      }\n    }, {\n      key: \"goToNextStep\",\n      value: function goToNextStep() {\n        var _this4 = this;\n        this.core.validate().then(function (t) {\n          if (t === \"Valid\") {\n            var _t2 = _this4.currentStep + 1;\n            if (_t2 >= _this4.numSteps) {\n              _this4.currentStep = _this4.numSteps - 1;\n            } else {\n              var _e3 = _this4.opts.isStepSkipped ? _this4.stepIndexes.slice(_t2, _this4.numSteps).find(function (t, e) {\n                return !_this4.opts.isStepSkipped({\n                  currentStep: _this4.currentStep,\n                  numSteps: _this4.numSteps,\n                  targetStep: t\n                });\n              }) : _t2;\n              _t2 = _e3;\n              _this4.goToStep(_t2);\n            }\n            _this4.onStepActive();\n            _this4.onStepValid();\n            if (_t2 === _this4.numSteps) {\n              _this4.onValid();\n            }\n          } else if (t === \"Invalid\") {\n            _this4.onStepInvalid();\n          }\n        });\n      }\n    }, {\n      key: \"goToStep\",\n      value: function goToStep(t) {\n        e(this.steps[this.currentStep], _defineProperty({}, this.opts.activeStepClass, false));\n        e(this.steps[t], _defineProperty({}, this.opts.activeStepClass, true));\n        this.currentStep = t;\n      }\n    }, {\n      key: \"onClickPrev\",\n      value: function onClickPrev() {\n        this.goToPrevStep();\n      }\n    }, {\n      key: \"onClickNext\",\n      value: function onClickNext() {\n        this.goToNextStep();\n      }\n    }, {\n      key: \"onStepActive\",\n      value: function onStepActive() {\n        var t = {\n          numSteps: this.numSteps,\n          step: this.currentStep\n        };\n        this.core.emit(\"plugins.wizard.step.active\", t);\n        this.opts.onStepActive(t);\n      }\n    }, {\n      key: \"onStepValid\",\n      value: function onStepValid() {\n        var t = {\n          numSteps: this.numSteps,\n          step: this.currentStep\n        };\n        this.core.emit(\"plugins.wizard.step.valid\", t);\n        this.opts.onStepValid(t);\n      }\n    }, {\n      key: \"onStepInvalid\",\n      value: function onStepInvalid() {\n        var t = {\n          numSteps: this.numSteps,\n          step: this.currentStep\n        };\n        this.core.emit(\"plugins.wizard.step.invalid\", t);\n        this.opts.onStepInvalid(t);\n      }\n    }, {\n      key: \"onValid\",\n      value: function onValid() {\n        var t = {\n          numSteps: this.numSteps\n        };\n        this.core.emit(\"plugins.wizard.valid\", t);\n        this.opts.onValid(t);\n      }\n    }]);\n    return i;\n  }(t);\n  i.EXCLUDED_PLUGIN = \"___wizardExcluded\";\n  return i;\n});\n\n//# sourceURL=webpack://erpfasttrackui/./wwwroot/vendor/libs/formvalidation/dist/js/plugins/Wizard.js?");

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
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = __webpack_require__("./wwwroot/vendor/libs/formvalidation/dist/js/plugins/Wizard.js");
/******/ 	
/******/ 	return __webpack_exports__;
/******/ })()
;
});
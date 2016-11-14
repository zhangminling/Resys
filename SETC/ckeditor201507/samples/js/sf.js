﻿/*
 Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 For licensing, see LICENSE.md or http://ckeditor.com/license
*/
var SF=function(){function d(a){return(a=a.attributes?a.attributes.getNamedItem("class"):null)?a.value.split(" "):[]}function c(a){var e=document.createAttribute("class");e.value=a.join(" ");return e}var b={attachListener:function(a,e,b){if(a.addEventListener)a.addEventListener(e,b,!1);else if(a.attachEvent)a.attachEvent("on"+e,function(){b.apply(a,arguments)});else throw Error("Could not attach event.");}};b.indexOf=function(){var a=Array.prototype.indexOf;return"function"===a?function(e,b){return a.call(e,
b)}:function(a,b){for(var c=a.length,d=0;d<c;d++)if(a[d]===b)return d;return-1}}();b.accept=function(a,e){var c;a.children?(c=a.children,e(a)):"number"===typeof a.length&&(c=a);for(var d=c?c.length||0:0;d--;)b.accept(c[d],e)};b.getByClass=function(){var a=document.getElementsByClassName;return"function"===typeof a?function(e,b){"string"===typeof e&&(b=e,e=document);return a.call(e,b)}:function(a,c){"string"===typeof a&&(c=a,a=document.getElementsByTagName("html")[0]);var d=[];b.accept(a,function(a){b.classList.contains(a,
c)&&d.push(a)});return d}}();b.classList={};b.classList.add=function(a,b){var f=d(a);f.push(b);a.attributes.setNamedItem(c(f))};b.classList.remove=function(a,e){var f=d(a,e),l=b.indexOf(f,e);-1!==l&&(f.splice(l,1),a.attributes.setNamedItem(c(f)))};b.classList.contains=function(a,c){return-1!==b.indexOf(d(a),c)};b.classList.toggle=function(a,b){this.contains(a,b)?this.remove(a,b):this.add(a,b)};return b}();"use strict";
(function(){function d(c){for(var b in c)delete c[b]}SF.modal=function(c){function b(a){27==a.keyCode&&f.close()}c.modalClass="modal content";c.closeClass="modal-close";c.modalStyles=d;c.closeStyles=d;var a=c.afterCreate,e=c.afterClose;c.afterCreate=function(c){a&&a(c);window.addEventListener("keydown",b)};c.afterClose=function(a){e&&e(a);window.removeEventListener("keydown",b)};var f=(new picoModal(c)).afterCreate(c.afterCreate).afterClose(c.afterClose);return f}})();"use strict";
(function(){for(var d=SF.getByClass("toggler"),c=d.length;c--;)SF.attachListener(d[c],"click",function(){var b=SF.classList.contains(this,"icon-toggler-expanded")||SF.classList.contains(this,"icon-toggler-collapsed"),a=document.getElementById(this.getAttribute("data-for"));SF.classList.toggle(this,"collapsed");SF.classList.contains(this,"collapsed")?(SF.classList.add(a,"collapsed"),b&&(SF.classList.remove(this,"icon-toggler-expanded"),SF.classList.add(this,"icon-toggler-collapsed"))):(SF.classList.remove(a,
"collapsed"),b&&(SF.classList.remove(this,"icon-toggler-collapsed"),SF.classList.add(this,"icon-toggler-expanded")))})})();"use strict";(function(){for(var d=SF.getByClass("tree-a"),c=d.length;c--;)SF.attachListener(d[c],"click",function(b){b=b.target||b.srcElement;"H2"===b.nodeName&&!SF.classList.contains(b,"tree-a-no-sub")&&SF.classList.toggle(b,"tree-a-active")})})();
(function(d,c){function b(a){return"object"===typeof Node?a instanceof Node:a&&"object"===typeof a&&"number"===typeof a.nodeType}function a(){var a=[];return{watch:a.push.bind(a),trigger:function(b){for(var c=!0,d={preventDefault:function(){c=!1}},e=0;e<a.length;e++)a[e](b,d);return c}}}function e(a){this.elem=a}function f(a,b){return e.div().clazz("pico-overlay").clazz(a("overlayClass","")).stylize({display:"block",position:"fixed",top:"0px",left:"0px",height:"100%",width:"100%",zIndex:1E4}).stylize(a("overlayStyles",
{opacity:0.5,background:"#000"})).onClick(function(){a("overlayClose",!0)&&b()})}function l(a,b){var c=a("width","auto");"number"===typeof c&&(c=""+c+"px");return e.div().clazz("pico-content").clazz(a("modalClass","")).stylize({display:"block",position:"fixed",zIndex:10001,left:"50%",top:"50px",width:c,"-ms-transform":"translateX(-50%)","-moz-transform":"translateX(-50%)","-webkit-transform":"translateX(-50%)","-o-transform":"translateX(-50%)",transform:"translateX(-50%)"}).stylize(a("modalStyles",
{backgroundColor:"white",padding:"20px",borderRadius:"5px"})).html(a("content")).attr("role","dialog").onClick(function(a){(new e(a.target)).anyAncestor(function(a){return/\bpico-close\b/.test(a.elem.className)})&&b()})}function n(a){return function(){return a().elem}}function i(c){function e(a,b){var d=c[a];"function"===typeof d&&(d=d(b));return void 0===d?b:d}function i(){j().hide();k().hide();s.trigger(h)}function o(){t.trigger(h)&&i()}function g(a){return function(){a.apply(this,arguments);return h}}
function p(a){if(!q){var c=l(e,o),b=f(e,o),d;var m=e;m("closeButton",!0)&&(d=c.child().html(m("closeHtml","&#xD7;")).clazz("pico-close").clazz(m("closeClass")).stylize(m("closeStyles",{borderRadius:"2px",cursor:"pointer",height:"15px",width:"15px",position:"absolute",top:"5px",right:"5px",fontSize:"16px",textAlign:"center",lineHeight:"15px",background:"#CCC"})));q={modal:c,overlay:b,close:d};u.trigger(h)}return q[a]}if("string"===typeof c||b(c))c={content:c};var u=a(),v=a(),w=a(),t=a(),s=a(),q,k=
p.bind(d,"modal"),j=p.bind(d,"overlay"),r=p.bind(d,"close"),h={modalElem:n(k),closeElem:n(r),overlayElem:n(j),show:function(){v.trigger(h)&&(j().show(),r(),k().show(),w.trigger(h));return this},close:g(o),forceClose:g(i),destroy:function(){k=k().destroy();j=j().destroy();r=void 0},options:function(a){c=a},afterCreate:g(u.watch),beforeShow:g(v.watch),afterShow:g(w.watch),beforeClose:g(t.watch),afterClose:g(s.watch)};return h}e.div=function(a){var b=c.createElement("div");(a||c.body).appendChild(b);
return new e(b)};e.prototype={child:function(){return e.div(this.elem)},stylize:function(a){a=a||{};"undefined"!==typeof a.opacity&&(a.filter="alpha(opacity="+100*a.opacity+")");for(var b in a)a.hasOwnProperty(b)&&(this.elem.style[b]=a[b]);return this},clazz:function(a){this.elem.className+=" "+a;return this},html:function(a){b(a)?this.elem.appendChild(a):this.elem.innerHTML=a;return this},onClick:function(a){this.elem.addEventListener("click",a);return this},destroy:function(){c.body.removeChild(this.elem)},
hide:function(){this.elem.style.display="none"},show:function(){this.elem.style.display="block"},attr:function(a,b){this.elem.setAttribute(a,b);return this},anyAncestor:function(a){for(var b=this.elem;b;){if(a(new e(b)))return!0;b=b.parentNode}return!1}};"function"===typeof d.define&&d.define.amd?d.define(function(){return i}):d.picoModal=i})(window,document);
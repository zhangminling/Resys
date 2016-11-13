/**
 * @license Copyright (c) 2003-2015, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function(config) {
    //config.enterMode = CKEDITOR.ENTER_BR;
    /* 添加新的插件 */
    config.extraPlugins = 'timestamp,video';
    config.allowedContent = {
        $1: {
            // Use the ability to specify elements as an object.
            elements: CKEDITOR.dtd,
            attributes: true,
            styles: true,
            classes: true
        }
    };
    config.disallowedContent = 'script; *[on*]';
    
    
    /* 配置工具条 */
    config.toolbar = [
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'NewPage'] },
        { name: 'styles', items: ['Styles', 'Format', 'Font', 'FontSize'] },
        { name: 'colors', items: ['TextColor', 'BGColor'] },
        '/',
        { name: 'paragraph', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Smiley', '-', 'ShowBlocks', 'Source'] }
    ];
};

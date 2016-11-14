/**
 * @license Copyright (c) 2003-2016, CKSource - Frederico Knabben. All rights reserved.
 * For licensing, see LICENSE.md or http://ckeditor.com/license
 */

CKEDITOR.editorConfig = function( config ) {
	// Define changes to default configuration here. For example:
	// config.language = 'fr';
    // config.uiColor = '#AADC6E';   , 'LinkButton'

    //config.extraPlugins = 'LinkButton';
    //config.extraPlugins = 'code';
    //config.skin = 'kama';

    
    config.toolbar = [
        { name: 'basicstyles', items: ['Bold', 'Italic', 'Underline', '-', 'NewPage'] },
        { name: 'styles', items: ['Format', 'Font', 'FontSize'] },
        { name: 'colors', items: ['TextColor', 'BGColor'] },
        { name: 'paragraph', items: ['JustifyLeft', 'JustifyCenter', 'JustifyRight', 'JustifyBlock', '-', 'Smiley', '-', 'ShowBlocks'] }
    ];
};

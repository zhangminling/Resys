/*CKEDITOR.plugins.add('timestamp', {
    icons: 'timestamp',
    init: function (editor) {
        //Plugin logic goes here.
        /*
        editor.addCommand('insertTimestamp', {
            exec: function (editor) {
                var now = new Date();
                editor.insertHtml('The current date and time is: <em>' + now.toString() + '</em>');
            }
        });
        */


        /*editor.addCommand('code', {
            exec: function showMyDialog(e) {
            var str = 'width=980,height=650,left=' + ((screen.width - 900) / 2) + ',top=' + ((screen.height - 650) / 2) + ',scrollbars=no,scrolling=no,location=no,toolbar=no'
                var w = window.open('UserSpace.aspx', 'MyWindow', str);                
            }
        });
        

        editor.ui.addButton('code', {
            label: '插入资源',
            command: 'insertcode',
            toolbar: 'insert'
        });     
    }
});*/
/*(function () {
    //Section 1 : 按下自定义按钮时执行的代码
    var a = {
        exec: function (editor) {
            alert("这是自定义按钮");
        }
    },
    //Section 2 : 创建自定义按钮、绑定方法
    b = 'LinkButton';
    CKEDITOR.plugins.add(b, {
        init: function (editor) {
            editor.addCommand(b, a);
            editor.ui.addButton('LinkButton', {
                label: 'LinkButton',
                icon: this.path + 'timestamp.png',
                command: b
            });
        }
    });
});*/
CKEDITOR.dialog.add(
    "picuploader",
    function (editor) {
        var timestamp = Math.round(new Date().getTime() / 1000);
        var ckeditorPage = '/picture/CustomerPicList?from=ckeditor&timestamp=' + timestamp;
        return {
            title: "插入代码",
            minWidth: 800,
            minHeight: 600,
            contents:
            [
                {
                    id: "tab1",
                    label: "",
                    title: "",
                    expand: true,
                    padding: 0,
                    elements:
                    [
                        {
                            type: "html",
                            html: "<iframe id='img_browser'name='img_browser' src='" + ckeditorPage + "'></iframe>",
                            style: "width:100%;height:600px;padding:0;"//  style='width:800px;height:600px'
                        }
                    ]
                }
            ],
            onOk: function () {
                //插入富文本编辑器内容 window.frames["img_browser"].document.getElementById("hf_imgsrc");//
                var hf = document.getElementById('img_browser').contentWindow.document.getElementByClassName("selected_imgsrc");
                if (hf != null) {
                    var imgSrc = hf.value;
                    editor.insertHtml("<img src='" + imgSrc + "' />"); //将select插入编辑器

                } else {
                    alert("hf is null");
                }

            },
            //onHide: function () { document.getElementById('img_browser').contentDocument.location.reload(); },
            //resizable: CKEDITOR.DIALOG_RESIZE_HEIGHT
        };
    }
);
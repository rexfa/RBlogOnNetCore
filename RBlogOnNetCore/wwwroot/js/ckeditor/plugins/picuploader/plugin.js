CKEDITOR.plugins.add(
    "picuploader",
    {
        requires: ["dialog"],
        //lang: ["zh-cn"],
        init: function (editor) {
            editor.addCommand("picuploader", new CKEDITOR.dialogCommand("picuploader"));
            editor.ui.addButton(
                "picuploader", {
                    label: "上传图片",
                    command: "picuploader",
                    icon: this.path + "images/pic.png",
                    toolbar: "insert"
                }
            );
            CKEDITOR.dialog.add("picuploader", this.path + "dialogs/code.js");
        }
    }
);
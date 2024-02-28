// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
  if (
    localStorage.getItem("color-theme") === "dark" ||
    (!("color-theme" in localStorage) &&
      window.matchMedia("(prefers-color-scheme: dark)").matches)
  ) {
    document.documentElement.classList.remove("dark");
  }

  var config = {
    toolbarGroups: [
      { name: "document", groups: ["mode", "document", "doctools"] },
      { name: "clipboard", groups: ["clipboard", "undo"] },
      {
        name: "editing",
        groups: ["find", "selection", "spellchecker", "editing"],
      },
      { name: "forms", groups: ["forms"] },
      { name: "basicstyles", groups: ["basicstyles", "cleanup"] },
      {
        name: "paragraph",
        groups: ["list", "indent", "blocks", "align", "bidi", "paragraph"],
      },
      { name: "links", groups: ["links"] },
      { name: "insert", groups: ["insert"] },
      { name: "styles", groups: ["styles"] },
      { name: "colors", groups: ["colors"] },
      { name: "tools", groups: ["tools"] },
      { name: "others", groups: ["others"] },
      { name: "about", groups: ["about"] },
    ],

    removeButtons:
      "Source,Save,NewPage,ExportPdf,Preview,Print,Templates,Cut,Copy,Paste,PasteText,PasteFromWord,SelectAll,Find,Replace,Scayt,Form,Checkbox,Radio,TextField,Textarea,Select,Button,ImageButton,HiddenField,Maximize,ShowBlocks",
  };
  $(".editor").ckeditor(config);
});

function getImgURL(originURL) {
  var url = "";
  if (originURL != undefined) {
    if (originURL[0] == "~")
      originURL = originURL.replace("~", window.location.origin);
    url = originURL;
  }
  return url;
}

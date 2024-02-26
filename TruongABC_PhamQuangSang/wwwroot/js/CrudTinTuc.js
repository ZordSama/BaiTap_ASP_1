$(document).ready(function () {});
function getTinTuc(ID) {
  $.ajax({
    url: "/CRUD/GetTinTuc/" + ID,
    type: "GET",
    datatype: "json",
    success: function (response) {
      if (response.success) {
        var TinTuc = JSON.parse(response.data);
        var Form = $("#drawer-update-TinTuc");
        for (var key in TinTuc) {
          console.log(key);
          console.log(Form.find("[name=" + key + "]"));
          var inp = Form.find("[name=" + key + "]");
          if (inp.is("input") && inp.attr("type") !== "file")
            inp.val(TinTuc[key]);
          if (inp.is("textarea")) {
            console.log(TinTuc[key]);
            inp.html(TinTuc[key]);
          }
          if (inp.attr("type") === "file")
            loadURLToInputFiled(TinTuc[key], inp);
        }
        $("#editorCover").hide();
      }
    },
    error: function (response) {
      result = response;
    },
  });
}

function loadURLToInputFiled(url, ele) {
  getImgURL(url, (imgBlob) => {
    // Load img blob to input
    // WIP: UTF8 character error
    let fileName = url.split("/").pop();
    let file = new File(
      [imgBlob],
      fileName,
      { type: "image/jpeg", lastModified: new Date().getTime() },
      "utf-8"
    );
    let container = new DataTransfer();
    container.items.add(file);
    ele.files = container.files;
  });
}
// xmlHTTP return blob respond
function getImgURL(url, callback) {
  var xhr = new XMLHttpRequest();
  xhr.onload = function () {
    callback(xhr.response);
  };
  xhr.open("GET", url);
  xhr.responseType = "blob";
  xhr.send();
}

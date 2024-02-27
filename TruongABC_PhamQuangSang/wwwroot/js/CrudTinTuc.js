// var isUploaded = false;
$(document).ready(function () {});
function getTinTuc(ID) {
  $.ajax({
    url: "/CRUD/GetTinTuc/" + ID,
    type: "GET",
    datatype: "json",
    success: function (response) {
      if (response.success) {
        var TinTuc = JSON.parse(response.data);
        // console.log(TinTuc);
        var Form = $("#drawer-update-TinTuc");
        for (var key in TinTuc) {
          var inp = Form.find("[name=" + key + "]");
          if (inp.is("input") && inp.attr("type") !== "file")
            inp.val(TinTuc[key]);
          if (inp.is("textarea")) {
            inp.html(TinTuc[key]);
          }
          if (inp.attr("type") === "file") {
            var url = TinTuc[key];
            if (url[0] === "~") {
              url = url.replace("~", window.location.origin);
            }
            var imgPreview = $("#imgInputPreview-2");
            imgPreview.attr("src", url);
            imgPreview.show();
            $("#dropzone-file-2").val("");
          }
        }
        $("#editorCover").hide();
      }
    },
    error: function (response) {
      result = response;
    },
  });
}
function createTinTuc() {
  var formData = new FormData(document.getElementById("fcreateTinTuc"));
  var file = $("#dropzone-file").prop("files")[0];
  if (file != undefined) formData.append("file", file);
  $.ajax({
    url: "/CRUD/CreateTinTuc/",
    type: "POST",
    contentType: false,
    processData: false,
    cache: false,
    data: formData,
    success: function (response) {
      if (response.success) {
        // toast
        iziToast.success({
          title: "Thành công",
          message: response.message,
          position: "topCenter",
        });
        window.location.reload();
      } else {
        iziToast.error({
          title: "Thông báo",
          message: response.message,
          position: "topCenter",
        });
      }
    },
  });
}

function editTinTuc() {
  var formData = new FormData(document.getElementById("drawer-update-TinTuc"));
  var file = $("#dropzone-file-2").prop("files")[0];
  if (file != undefined) formData.append("file", file);
  $.ajax({
    url: "/CRUD/UpdateTinTuc/" ,
    type: "PUT",
    contentType: false,
    processData: false,
    cache: false,
    data: formData,
    success: function (response) {
      if (response.success) {
        // toast
        iziToast.success({
          title: "Thành công",
          message: response.message,
          position: "topCenter",
        });
        window.location.reload();
      } else {
        iziToast.error({
          title: "Thông báo",
          message: response.message,
          position: "topCenter",
        });
      }
    },
  });
}

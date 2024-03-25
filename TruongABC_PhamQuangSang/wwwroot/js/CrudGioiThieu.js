// var isUploaded = false;
$(document).ready(function () {
  $("#drawer-update-GioiThieu").on("submit", function (e) {
    e.preventDefault();
    // console.log(CKEDITOR.instances["NoiDungU"].getData());
    editGioiThieu();
  });
  $("#fcreateGioiThieu").on("submit", function (e) {
    e.preventDefault();
    createGioiThieu();
  });
});
function getGioiThieu(ID) {
  var GioiThieu;
  $.ajax({
    url: "/CRUD/GetGioiThieu/" + ID,
    async: false,
    type: "GET",
    datatype: "json",
    success: function (response) {
      if (response.success) {
        GioiThieu = JSON.parse(response.data);
        // console.log(GioiThieu);
        fillGioiThieu(GioiThieu);
        $("#editorCover").hide();
        CKEDITOR.instances["NoiDungGtU"].setData(GioiThieu["NoiDung"]);
      }
    },
    error: function (response) {
      result = response;
    },
  });
  // console.log(GioiThieu);
  return GioiThieu;
}
function fillGioiThieu(GioiThieu, url) {
  var Form = $("#drawer-update-GioiThieu");
  $("#alterID").html(GioiThieu["TenGt"]);
  for (var key in GioiThieu) {
    var inp = Form.find("[name=" + key + "]");
    if (inp.is("input") && inp.attr("type") !== "file") inp.val(GioiThieu[key]);
    if (inp.is("textarea")) {
      inp.html(GioiThieu[key]);
    }
    if (inp.attr("type") === "file") {
      var url = getImgURL(GioiThieu[key]);
      var imgPreview = $("#imgInputPreview-2");
      imgPreview.attr("src", url);
      imgPreview.show();
      $("#dropzone-file-2").val("");
    }
  }
}
function createGioiThieu() {
  var formData = new FormData(document.getElementById("fcreateGioiThieu"));
  // var file = $("#dropzone-file").prop("files")[0];
  // if (file != undefined) formData.append("file", file);
  formData.set("NoiDungGt", CKEDITOR.instances["NoiDungGtC"].getData());
  $.ajax({
    url: "/CRUD/CreateGioiThieu/",
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
        window.setTimeout(reloadPage, 1000);
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

function editGioiThieu() {
  var formData = new FormData(document.getElementById("drawer-update-GioiThieu"));
  // var payload = {}
  // formData.forEach((value, key) => {
  //   payload[key] = value;
  // });
  // console.log(payload);
  // var file = $("#dropzone-file-2").prop("files")[0];
  // if (file != undefined) formData.append("file", file);
  formData.set("NoiDungGt", CKEDITOR.instances["NoiDungGtU"].getData());
  $.ajax({
    url: "/CRUD/UpdateGioiThieu/",
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
        window.setTimeout(reloadPage, 1000);
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

function deleteGioiThieu(id) {
  $.ajax({
    url: "/CRUD/DeleteGioiThieu/",
    type: "DELETE",
    data: { id: id },
    success: function (response) {
      if (response.success) {
        // toast
        iziToast.success({
          title: "Thành công",
          message: response.message,
          position: "topCenter",
        });
        window.setTimeout(reloadPage, 1000);
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
function setDelId(id) {
  $("#delId").val(id);
}
function delBtnConfirmed() {
  var id = $("#delId").val();
  deleteGioiThieu(id);
}

function reloadPage(){
  window.location.reload();
}
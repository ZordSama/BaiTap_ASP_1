// var isUploaded = false;
$(document).ready(function () {
    $("#drawer-update-LichCt").on("submit", function (e) {
      e.preventDefault();
      // console.log(CKEDITOR.instances["NoiDungU"].getData());
      editLichCt();
    });
    $("#fcreateLichCt").on("submit", function (e) {
      e.preventDefault();
      createLichCt();
    });
  });
  function getLichCt(ID) {
    var LichCt;
    $.ajax({
      url: "/CRUD/GetLichCt/" + ID,
      async: false,
      type: "GET",
      datatype: "json",
      success: function (response) {
        if (response.success) {
          LichCt = JSON.parse(response.data);
          // console.log(LichCt);
          fillLichCt(LichCt);
          $("#editorCover").hide();
          CKEDITOR.instances["NoiDungGtU"].setData(LichCt["NoiDung"]);
        }
      },
      error: function (response) {
        result = response;
      },
    });
    // console.log(LichCt);
    return LichCt;
  }
  function fillLichCt(LichCt, url) {
    var Form = $("#drawer-update-LichCt");
    $("#alterID").html(LichCt["TenGt"]);
    for (var key in LichCt) {
      var inp = Form.find("[name=" + key + "]");
      if (inp.is("input") && inp.attr("type") !== "file") inp.val(LichCt[key]);
      if (inp.is("textarea")) {
        inp.html(LichCt[key]);
      }
      if (inp.attr("type") === "file") {
        var url = getImgURL(LichCt[key]);
        var imgPreview = $("#imgInputPreview-2");
        imgPreview.attr("src", url);
        imgPreview.show();
        $("#dropzone-file-2").val("");
      }
    }
  }
  function createLichCt() {
    var formData = new FormData(document.getElementById("fcreateLichCt"));
    // var file = $("#dropzone-file").prop("files")[0];
    // if (file != undefined) formData.append("file", file);
    formData.set("NoiDungGt", CKEDITOR.instances["NoiDungGtC"].getData());
    $.ajax({
      url: "/CRUD/CreateLichCt/",
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
  
  function editLichCt() {
    var formData = new FormData(document.getElementById("drawer-update-LichCt"));
    // var payload = {}
    // formData.forEach((value, key) => {
    //   payload[key] = value;
    // });
    // console.log(payload);
    // var file = $("#dropzone-file-2").prop("files")[0];
    // if (file != undefined) formData.append("file", file);
    formData.set("NoiDungGt", CKEDITOR.instances["NoiDungGtU"].getData());
    $.ajax({
      url: "/CRUD/UpdateLichCt/",
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
  
  function deleteLichCt(id) {
    $.ajax({
      url: "/CRUD/DeleteLichCt/",
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
    deleteLichCt(id);
  }
  
  function reloadPage(){
    window.location.reload();
  }
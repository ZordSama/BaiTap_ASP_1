// var isUploaded = false;
$(document).ready(function () {
  $("#drawer-update-TinTuc").on("submit", function (e) {
    e.preventDefault();
    console.log(CKEDITOR.instances["NoiDungU"].getData());
    editTinTuc();
  });
  $("#fcreateTinTuc").on("submit", function (e) {
    e.preventDefault();
    createTinTuc();
  });
});
function getTinTuc(ID) {
  var TinTuc;
  $.ajax({
    url: "/CRUD/GetTinTuc/" + ID,
    async: false,
    type: "GET",
    datatype: "json",
    success: function (response) {
      if (response.success) {
        TinTuc = JSON.parse(response.data);
        // console.log(TinTuc);
        fillTinTuc(TinTuc);
        $("#editorCover").hide();
        CKEDITOR.instances["NoiDungU"].setData(TinTuc["NoiDung"]);
      }
    },
    error: function (response) {
      result = response;
    },
  });
  // console.log(TinTuc);
  return TinTuc;
}
function fillTinTuc(TinTuc, url) {
  var Form = $("#drawer-update-TinTuc");
  $("#alterID").html(TinTuc["TenTin"]);
  for (var key in TinTuc) {
    var inp = Form.find("[name=" + key + "]");
    if (inp.is("input") && inp.attr("type") !== "file") inp.val(TinTuc[key]);
    if (inp.is("textarea")) {
      inp.html(TinTuc[key]);
    }
    if (inp.attr("type") === "file") {
      var url = getImgURL(TinTuc[key]);
      var imgPreview = $("#imgInputPreview-2");
      imgPreview.attr("src", url);
      imgPreview.show();
      $("#dropzone-file-2").val("");
    }
  }
}
function createTinTuc() {
  var formData = new FormData(document.getElementById("fcreateTinTuc"));
  var file = $("#dropzone-file").prop("files")[0];
  if (file != undefined) formData.append("file", file);
  formData.set("NoiDung", CKEDITOR.instances["NoiDungC"].getData());
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
  formData.set("NoiDung", CKEDITOR.instances["NoiDungU"].getData());
  $.ajax({
    url: "/CRUD/UpdateTinTuc/",
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

function deleteTinTuc(id) {
  $.ajax({
    url: "/CRUD/DeleteTinTuc/",
    type: "DELETE",
    data: { idTin: id },
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
function setDelId(id) {
  $("#delId").val(id);
}
function delBtnConfirmed() {
  var id = $("#delId").val();
  deleteTinTuc(id);
}

function detailsReq(id) {
  var TinTuc = getTinTuc(id);
  let TenAnh = "ảnh\nlỗi";
  if (TinTuc["Anh"]) {
    TenAnh = TinTuc["Anh"].split("/").pop();
  }
  $("#preview-drawer-label").html(TinTuc["TenTin"]);
  $("#NhomLabel").html(TinTuc["TenNhomTin"]);
  // console.log(TinTuc);
  $("#preview-box").html(
    `
    <img class="object-cover object-center w-32 h-20 truncate" src="${getImgURL(TinTuc["Anh"])}" alt=${TenAnh}>
    <div class="flex flex-col justify-center ml-2">
      <h1 class="text-lg font-semibold">${TinTuc["TenTin"]}</h1>
      <p class="text-sm text-ellipsis">${TinTuc["TomTat"]}</p>
    </div>
    `
  );
  $("#preview-content").html(
    `
    <div class="flex flex-col justify-center ml-2">
      <h1 class="text-lg font-semibold">${TinTuc["TenTin"]}</h1>
      <p class="text-sm text-ellipsis">${TinTuc["TomTat"]}</p>
      <p class="text-sm text-ellipsis">${TinTuc["NoiDung"]}</p>
    </div>
    `
  );
}
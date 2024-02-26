$(document).ready(function () {
    $("#dropzone-file").change(function (e) {
        if (e.target.files.length > 0) {
          const imgFile = e.target.files[0];
          var imgEl = $("#imgInputPreview");
          imgEl.attr("src", URL.createObjectURL(imgFile));
          imgEl.show();
          $("#imgInpLabel").html(
            "Thêm ảnh (click vào ảnh đã upload để dổi ảnh khác)"
          );
          $("#imgSeletorText").html("Click để chọn ảnh khác");
        }
      });
});
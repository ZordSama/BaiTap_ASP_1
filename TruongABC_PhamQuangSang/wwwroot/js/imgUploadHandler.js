$(document).ready(function () {
  $("#dropzone-file").change(function (e) {imgZoneHandler(e, $("#imgInputPreview"), $("#imgInpLabel"), $("#imgSeletorText"))});
  $("#dropzone-file-2").change(function (e) {imgZoneHandler(e, $("#imgInputPreview-2"), $("#imgInpLabel-2"), $("#imgSeletorText-2"))});
  //  function (e) {
  //   if (e.target.files.length > 0) {
  //     const imgFile = e.target.files[0];
  //     var imgEl = $("#imgInputPreview");
  //     imgEl.attr("src", URL.createObjectURL(imgFile));
  //     imgEl.show();
  //     $("#imgInpLabel").html(
  //       "Thêm ảnh (click vào ảnh đã upload để dổi ảnh khác)"
  //     );
  //     $("#imgSeletorText").html("Click để chọn ảnh khác");
  //     // console.log("Trigged")
  //   }
  // });
});

function imgZoneHandler(inpZone, imgEL, imgLabel,selectorText) {
  if (inpZone.target.files.length > 0) {
    const imgFile = inpZone.target.files[0];
    imgEL.attr("src", URL.createObjectURL(imgFile));
    imgEL.show();
    imgLabel.html(
      "Thêm ảnh (click vào ảnh đã upload để dổi ảnh khác)"
    );
    selectorText.html("Click để chọn ảnh khác");
    // console.log("Trigged")
  }
}

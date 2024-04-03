$(document).ready(function () {
  $("#fLogin").submit(function (e) {
    e.preventDefault();
    let data = new FormData(document.getElementById("fLogin"));
    login(data);
  });
});

function login(data) {
  $.ajax({
    url: "/Auth/Login",
    type: "post",
    contentType: false,
    processData: false,
    cache: false,
    data: data,
    success: function (response) {
      if (response.success) {
        iziToast.success({
          title: "Đăng nhập thành công",
          position: "topCenter",
        });
        window.setTimeout(()=>{window.location.href = "/CRUD/TinTuc"}, 1000);
      } else console.log("response :>> ", response);
    },
    error: function (response) {
      console.log("response :>> ", response);
    },
  });
}

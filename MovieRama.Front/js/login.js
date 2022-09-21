$(document).ready(function () {
  $(".form-body a").click(function () {
    var username = $("#username").val();
    var password = $("#password").val();
    const login = { UserName: username, Password: password };
    $("#loading").css("display", "block");
    const settings = {
      async: true,
      crossDomain: true,
      url: "https://localhost:7118/api/authentication/login",
      method: "POST",
      headers: {
        accept: "text/plain",
        "Content-Type": "application/json",
      },
      processData: false,
      data: JSON.stringify(login),
    };

    $.ajax(settings)
      .done(function (response) {
        createItem("myToken", response[0]);
        createItem("myID", response[1]);
        window.location = "index.html";
        $("#loading").css("display", "none");
      })
      .fail(function (error) {
        alert("error");
        $("#loading").css("display", "none");
      });
  });
});

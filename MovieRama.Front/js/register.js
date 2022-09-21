$(document).ready(function () {
  $(".form-body a").click(function () {
    var username = $("#username").val();
    var password = $("#password").val();
    var firstName = $("#firstName").val();
    var lastName = $("#lastName").val();
    const register = {
      UserName: username,
      Password: password,
      FirstName: firstName,
      LastName: lastName,
    };
    $("#loading").css("display", "block");
    const settings = {
      async: true,
      crossDomain: true,
      url: "https://localhost:7118/api/authentication/register",
      method: "POST",
      headers: {
        accept: "text/plain",
        "Content-Type": "application/json",
      },
      processData: false,
      data: JSON.stringify(register),
    };

    $.ajax(settings)
      .done(function (response) {
        window.location = "login.html";
        $("#loading").css("display", "none");
      })
      .fail(function (error) {
        alert("error");
        $("#loading").css("display", "none");
      });
  });
});

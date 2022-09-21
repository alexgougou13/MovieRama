$(document).ready(function () {
  var token = getValue("myToken");
  if (token == null) {
    window.location = "login.html";
  }

  $(".add-movie-container a").click(function () {
    var title = $("#title").val();
    var image = $("#image").val();
    var description = $("#description").val();
    if (title != "" && image != "" && description != "") {
      var token = getValue("myToken");
      var myID = getValue("myID");

      const movie = {
        Title: title,
        ImageUrl: image,
        Description: description,
        UserID: myID,
      };
      const settings = {
        async: true,
        crossDomain: true,
        url: "https://localhost:7118/api/Movies",
        method: "POST",
        headers: {
          accept: "text/plain",
          "Content-Type": "application/json",
          Authorization: "bearer " + token,
        },
        processData: false,
        data: JSON.stringify(movie),
      };
      $.ajax(settings)
        .done(function (response) {
          alert(response);
        })
        .fail(function (error) {
          alert(error);
        });
    } else {
      alert("Please fill the correct information");
    }
  });
});

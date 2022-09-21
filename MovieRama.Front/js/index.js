$(document).ready(function () {
  var urlParameterUser = getUrlParameter("user");
  var urlParameteSearch = getUrlParameter("search");
  var urlParameteSort = getUrlParameter("sort");
  var url = "https://localhost:7118/api/Movies?";
  if (urlParameterUser) {
    url += `userID=${urlParameterUser}&`;
  }
  if (urlParameteSearch) {
    url += `name=${urlParameteSearch}&`;
  }
  if (urlParameteSort) {
    $("#movieSorting")[0].value = urlParameteSort;
    url += `sortBy=${urlParameteSort}&`;
  }
  const settings = {
    async: true,
    crossDomain: true,
    url: url,
    method: "GET",
    headers: {
      accept: "text/plain",
    },
  };
  $("#loading").css("display", "block");
  $.ajax(settings)
    .done(function (movieList) {
      showMovies(movieList);
      $("#loading").css("display", "none");
    })
    .fail(function (error) {
      alert("error");
      $("#loading").css("display", "none");
    });
});
window.onload = function () {
  $(".thumb_up").click(function (event) {
    var myId = getValue("myID");
    var token = getValue("myToken");
    var likesNum = $(this).next();
    var vote = {
      MovieID: $(this).attr("data-attr-id"),
      UserID: myId,
      Rating: true,
    };
    const settings = {
      async: true,
      crossDomain: true,
      url: "https://localhost:7118/api/MovieRatings",
      method: "POST",
      headers: {
        accept: "text/plain",
        "Content-Type": "application/json",
        Authorization: "bearer " + token,
      },
      processData: false,
      data: JSON.stringify(vote),
    };
    $.ajax(settings)
      .done(function (response) {
        if (response == true) {
          likesNum[0].innerHTML++;
          likesNum.parent().prev().children().eq(1)[0].innerHTML--;
        } else if (response == "added") {
          likesNum[0].innerHTML++;
        } else if (response == "Not allowed") {
          alert("You can't vote your own movie");
        }
      })
      .fail(function (error) {
        if (error.status == 401) {
          window.location = "login.html";
        }
      });
  });
  $(".thumb_down").click(function (event) {
    var myId = getValue("myID");
    var token = getValue("myToken");
    var HatesNum = $(this).next();
    var vote = {
      MovieID: $(this).attr("data-attr-id"),
      UserID: myId,
      Rating: false,
    };
    const settings = {
      async: true,
      crossDomain: true,
      url: "https://localhost:7118/api/MovieRatings",
      method: "POST",
      headers: {
        accept: "text/plain",
        "Content-Type": "application/json",
        Authorization: "bearer " + token,
      },
      processData: false,
      data: JSON.stringify(vote),
    };
    $.ajax(settings)
      .done(function (response) {
        if (response == true) {
          HatesNum[0].innerHTML++;
          HatesNum.parent().next().children().eq(1)[0].innerHTML--;
        } else if (response == "added") {
          HatesNum[0].innerHTML++;
        } else if (response == "Not allowed") {
          alert("You can't vote your own movie");
        }
      })
      .fail(function (error) {
        if (error.status == 401) {
          window.location = "login.html";
        }
      });
  });
  $("#search").keyup(function (e) {
    if (e.target.value.length > 0 && e.keyCode == 13) {
      var userParam = getUrlParameter("user");
      var sortParam = getUrlParameter("sort");
      if (userParam || sortParam) {
        window.location += `&search=${e.target.value}`;
      } else {
        window.location = `index.html?search=${e.target.value}`;
      }
    }
  });

  $("#movieSorting").change(function () {
    var urlParameterUser = getUrlParameter("user");
    var urlParameteSearch = getUrlParameter("search");
    var sortingBy = $("#movieSorting").val();
    if (urlParameterUser != "" && urlParameteSearch != "") {
      window.location = `index.html?user=${urlParameterUser}&search=${urlParameteSearch}&sort=${sortingBy}`;
    } else if (urlParameterUser != "") {
      window.location = `index.html?user=${urlParameterUser}&sort=${sortingBy}`;
    } else if (urlParameteSearch != "") {
      window.location = `index.html?search=${urlParameteSearch}&sort=${sortingBy}`;
    } else {
      window.location = `index.html?sort=${sortingBy}`;
    }
  });
};
function showMovies(list) {
  $.each(list, function (i) {
    const {
      Title,
      Description,
      CreatedDate,
      HatesNum,
      Id,
      LikesNum,
      NameOfUser,
      UserId,
    } = list[i];
    var ImageUrl = list[i].ImageUrl;
    if (ImageUrl == "") {
      ImageUrl = "../images/default-movie.png";
    }
    var movieToShow = $("<div></div>").addClass("movie-wrapper");
    movieToShow[0].innerHTML = `<div class="movie-info">
            <div class="movie-image">
              <img
                src="${ImageUrl}"
              />
            </div>
            <div class="movie-text">
              <div class="movie-title">
                <span>${Title}</span>
              </div>
              <div class="added-by">
                <span onclick="window.location='index.html?user=${UserId}'">${NameOfUser}</span>
              </div>
              <div class="movie-description">
                <span
                  >${Description}</span
                >
              </div>
            </div>
          </div>
          <div class="movie-date"><span>${CreatedDate}</span></div>
          <div class="movie-likes">
            <div class="movie-minus-likes">
              <span data-attr-id="${Id}" class="material-symbols-outlined thumb_down">
                thumb_down
              </span>
              <span>${HatesNum}</span>
            </div>
            <div class="movie-plus-likes">
              <span data-attr-id="${Id}" class="material-symbols-outlined thumb_up"> thumb_up </span>
              <span>${LikesNum}</span>
            </div>
          </div>`;
    $("#main-movies-container").append(movieToShow);
  });
}

var getUrlParameter = function getUrlParameter(sParam) {
  var sPageURL = window.location.search.substring(1),
    sURLVariables = sPageURL.split("&"),
    sParameterName,
    i;

  for (i = 0; i < sURLVariables.length; i++) {
    sParameterName = sURLVariables[i].split("=");

    if (sParameterName[0] === sParam) {
      return sParameterName[1] === undefined
        ? true
        : decodeURIComponent(sParameterName[1]);
    }
  }
  return false;
};

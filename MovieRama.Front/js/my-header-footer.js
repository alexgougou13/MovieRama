class MyHeader extends HTMLElement {
  connectedCallback() {
    this.innerHTML =
      '<header><a href="/html/index.html"><img src="/images/favicon.ico"></a><nav><ul>' +
      '<li class="my-nav-menu-item"><a href="/html/index.html" class="item-wrap"><span class="material-symbols-outlined"> home </span><span class="media-title">Home</span></a></li>' +
      '<li class="my-nav-menu-item"><a href="/html/addmovie.html" class="item-wrap"><span class="material-symbols-outlined"> add_circle </span><span class="media-title">Add Movie</span></a></li>' +
      "</ul></nav></header>" +
      '<a class="secondary-button log-out hide">Log out</a>' +
      '<div class="register-login"><a class="primary-button" href="/html/login.html">Login</a><a class="secondary-button" href="/html/register.html">Register</a></div>';
  }
}
customElements.define("my-header", MyHeader);

window.createItem = function createItem(key, jwt) {
  sessionStorage.setItem(key, jwt);
};
window.getValue = function getValue(key) {
  return sessionStorage.getItem(key);
};

$(document).ready(function () {
  var token = getValue("myToken");
  if (token != null) {
    $(".log-out").removeClass("hide").addClass("show");
    $(".register-login").addClass("hide");
  } else {
    $(".log-out").removeClass("show");
  }
  var pathname = window.location.pathname;
  navBarSelected(pathname);

  $(".log-out").click(function () {
    window.sessionStorage.clear();
    location.reload();
  });
});

function navBarSelected(path) {
  $(".my-nav-menu-item").removeClass("nav-selected");
  if (
    path.includes("index.html") &&
    window.location.href.indexOf("user") === -1
  ) {
    $("ul .my-nav-menu-item").eq(0).addClass("nav-selected");
  } else if (path.includes("addmovie.html")) {
    $("ul .my-nav-menu-item").eq(1).addClass("nav-selected");
  }
}

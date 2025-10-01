// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
const SearchInput = document.getElementById("SearchInput")
SearchInput.addEventListener("keypress", (e) => {
    let xhr = new XMLHttpRequest();
    let url = `https://localhost:44352/Employee?SearchInput=${e.value}`;
    xhr.open("GET", url, true);

    xhr.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.responseText);
        }
    }
    // Sending our request 
    xhr.send();
})

var serialized = System.Text.Json.JsonSerializer.Serialize(TempData["Message"]);

//document.addEventListener('DOMContentLoaded', function () {
//    var msg = serialized;
//    document.querySelector('#createdModal .modal-body').textContent = msg;
//    var modalEl = document.getElementById('createdModal');
//    var bsModal = new bootstrap.Modal(modalEl);
//    bsModal.show();
//});



var dateControl = document.querySelector('input[type="date"]');
dateControl.value = getDate();

function getTodayDate() {
    var today = new Date();
    var dd = today.getDate();
    var mm = today.getMonth() + 1; //January is 0!
    var yyyy = today.getFullYear();

    if (dd < 10) {
        dd = '0' + dd
    }

    if (mm < 10) {
        mm = '0' + mm
    }

    today = yyyy + '-' + mm + '-' + dd;

    return today;
}

function setDate() {
    var value = document.querySelector('input[type="date"]').value;
    localStorage.setItem("user_selected_date", value);
}

function getDate() {
    if (localStorage.getItem("user_selected_date") === null)
    { return getTodayDate(); }
    return localStorage.getItem("user_selected_date");
}
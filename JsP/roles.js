$(document).ready(function () {
    $.ajax({
        type: "GET",
        url: "AdminMasterPage.master/Take",
        contentType: "application/json; charset=utf-8",
        dataType: "json",

        success: function (msg) {
            debugger
            alert("Hello");
            $('.roles').css('display', 'none');
        }

    });
});
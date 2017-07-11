$('#txtC').toggle(
    function () {
        $(this).text("Show");
        $('whatever').show();
    },
    function () {
        $(this).text("Hide");
        $('whatever').hide();
    }
);

$("#buttonName").val("New text for button");





//$('#txtC').click(function (event) {
//    event.preventDefault(); //This prevents the default action  
//    alert("Hello"); //Show the alert  

//});

window.onbeforeunload = function () {
alert("fuckin hell wtf once again");
    $.ajax({
        type: "POST",
        url: "/Order/AjaxMethod",
        data: '{name: "no" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert("fuck: " + response.responseText);
        }
    });
};
$(window).unload(function () {
    alert("fuckin hell wtf once again");
    $.ajax({
        type: "POST",
        url: "/Order/AjaxMethod",
        data: '{name: "no" }',
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
        },
        failure: function (response) {
            alert(response.responseText);
        },
        error: function (response) {
            alert("fuck: " + response.responseText);
        }
    });
});




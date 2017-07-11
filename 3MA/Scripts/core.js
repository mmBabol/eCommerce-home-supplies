function clickMe() {
    //[Route("Flooring_order/")]
    var xhr = HttpRequest('#productList');

    // configure the xhr object to fetch content
    xhr.open('get', '/order/sub/Flooring_order/', true);
    // fetch the content
    xhr.send(null);
}

function onImgClick(val) {
    $.ajax({
        type: "POST",
        url: "/Order/ProductListSave",
        data: '{id: ' +  val + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

function ChangeColor(tableRow, highLight) {
    if (highLight) {
        tableRow.style.backgroundColor = '#fc1a59';
    }
    else {
        tableRow.style.backgroundColor = 'white';
    }
}

// Update Qty value. Update value in view, send to controller to save in TempData
function QtyChange(Id, val) {
    var IdName = "Qty" + Id;
    var total = 1;
    if (val == 0) {
        // Need to Fix, check if checkbox is checked, change value to 1 if clicking on and at 0
        document.getElementById(IdName).value = 1;
    }
    else {
        total = parseInt(document.getElementById(IdName).value) + parseInt(val);
        if (total < 0) {
            total = 0;
        }
        document.getElementById(IdName).value = total;
    }

    $.ajax({
        type: "POST",
        url: "/Order/ProductQtySave",
        data: '{id: ' + Id + ', qty: ' + total + ' }',
        //data: {id: id, qty: total },
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

// Check to see if text field containing location is empty, if not, enable the 'Go' button and change border of text field to indicate that it is good for saving
function onLocationUpdate(Id) {
    var IdName = "Loc-" + Id;
    var IdImg = "Next-" + Id;
    if (document.getElementById(IdName).value == "" || document.getElementById(IdName).value == '-') {
        document.getElementById(IdImg).hidden = true;
        document.getElementById(IdName).style.borderColor = "#ffffff";
    }
    else {
        document.getElementById(IdImg).hidden = false;
        document.getElementById(IdName).style.borderColor = "#BBFFAA";
    }
}

// Retrieve location value from specific product and send the Id and location to controller to be saved
function LocationChange(Id) {
    var IdName = "Loc-" + Id;
    var loc = document.getElementById(IdName).value;
    document.getElementById(IdName).style.borderColor = "#66ee77";

    $.ajax({
        type: "POST",
        url: "/Order/ProductLocSave",
        data: '{id: ' + Id + ', loc: ' + JSON.stringify(loc) + ' }',
        contentType: "application/json; charset=utf-8"
    });
}

function DoNav(theUrl) {
    document.location.href = theUrl;
}

function PopupPicker(ctl) {
    var PopupWindow = null;
    PopupWindow = window.open('DatePicker.aspx?ctl=' + ctl, '', 'width=250,height=250');
    PopupWindow.focus();
}

/* Main category */
function onClickMain(category) {
    var xhr = HttpRequest('#subCategory');

    // configure the xhr object to fetch content
    xhr.open('get', '/' + category + '/', true);
    // fetch the content
    xhr.send(null);
}

/* Sub Category */
function onClickSub(cat, sub) {
    var xhr = HttpRequest('#productList');
    // configure the xhr object to fetch content
    xhr.open('get', '/product/sub/' + cat + '/' + sub, true);
    // fetch the content
    xhr.send(null);
}

function onClickSubOrder(cater, suber) {
    var xhr = HttpRequest('#productList');

    // configure the xhr object to fetch content
    xhr.open('get', '/order/sub/' + cater + '/' + suber, true);
    // fetch the content
    xhr.send(null);
}

/* Search */
// filterProduct 
// loc - order or product. Can't be null.
// cat - category (flooring, tiles, etc.). Can't be null.
// col - colour
// name - name of product
function filterProduct(loc, cat, col, name) {
    var xhr = HttpRequest('#productList');

    //if (col == null) {col = 'null'; }
    //if (name == null) { name = 'null'; }

    //var x = document.getElementById('dimXList').value;
    //if (x == "-") { x = 'null';}

    //var y = document.getElementById('dimYList').value;
    //if (y == "-") { y = 'null'; }

    //var z = document.getElementById('dimZList').value;
    //if (z == "-") { z = 'null'; }

    //var price = document.getElementById('PriceList').value;
    //if (price == "-") { price = 'null'; }


    //// configure the xhr object to fetch content
    //if (loc == 'product') {
    //    xhr.open('get', '/product/search/' + cat + '/' + col + '/' + name, true);
    //}
    //else {
    //    //xhr.open('get', '/order/search/' + cat + '/' + col + '/' + name + '/' + x + '/' + y + '/' + z + '/' + price, true);
    //    xhr.open('get', '/order/search/cat/col/name/x/y/z/price', true);
    //}

    xhr.open('get', '/order/search/cat/col/name/x/y/z/price', true);
    // fetch the content
    xhr.send(null);
}

function onClickSuite() {
    var id = document.getElementById('order_list').value;
    
    if (id == '-') {
        return;
    }
}

function HttpRequest(listVal) {
    // Get a reference to the DOM element
    var e = document.querySelector(listVal);

    // create an xhr object
    var xhr = new XMLHttpRequest();

    // configure its handler
    xhr.onreadystatechange = function () {

        if (xhr.readyState === 4) {
            // request-response cycle has been completed, so continue
            if (xhr.status === 200) {
                // request was successfully completed, and data was received, so continue

                // update the user interface
                e.innerHTML = xhr.responseText;

                document.getElementById('productsTable').scrollIntoView(true);
                document.getElementById('productsAnchor').scrollIntoView(true);

            } else {
                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
            }
        } else {
            e.innerHTML = "<p>Loading...</p>";
        }
        // show the content
        e.style.display = 'block';
    }
    return xhr;
}

function getSuite() {
    //return document.getElementById('order_list').value;
    var id = document.getElementById('order_list').value;

    var xhr = HttpRequest('#suite_order');

    // configure the xhr object to fetch content
    xhr.open('get', '/User_Order/' + id, true);

    // fetch the content
    xhr.send(null);
}

function getCustomerId() {
    var id = document.getElementById('order_list').value;

    var xhr = HttpRequest('#suite_order');

    // configure the xhr object to fetch content
    xhr.open('get', '/User_Order/' + id, true);

    // fetch the content
    xhr.send(null);
}

//window.onbeforeunload = function () {
//    $.ajax({
//        type: "POST",
//        url: "/Order/NameGoesHere",
//        data: '{name: "maybe" }',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert("fuck: " + response.responseText);
//        }
//    });
//};

//$(window).unload(function () {
//    $.ajax({
//        type: "POST",
//        url: "/Order/AjaxMethod",
//        data: '{name: "yes" }',
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            alert("Hello: " + response.Name + " .\nCurrent Date and Time: " + response.DateTime);
//        },
//        failure: function (response) {
//            alert(response.responseText);
//        },
//        error: function (response) {
//            alert("fuck: " + response.responseText);
//        }
//    });
//});

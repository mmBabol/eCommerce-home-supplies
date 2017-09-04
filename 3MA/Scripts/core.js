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

                //alert(xhr.responseText);

                // update the user interface
                e.innerHTML = xhr.responseText;

            } else {
                e.innerHTML = xhr.responseText;
                //e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + " " + xhr.status + ")</p>";
            }
        } else {
            e.innerHTML = "<p>Loading...</p>";
            ////e.innerHTML = "<img src=\"~/images/loadingAnimation.gif\" id=\"loadingAnim\" name=\"loadingAnim\"/>";
            //e.innerHTML = "<img src=\"~/images/loadingAnimation.gif\"/>";
        }
        // show the content
        e.style.display = 'block';
    }
    return xhr;
}

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
        data: '{id: ' + val + ' }',
        contentType: "application/json; charset=utf-8",
        dataType: "json"
    });
}

function onAccClick(val, item) {
    $.ajax({
        type: "POST",
        url: "/Order/ProductListSave",
        data: '{id: ' + val + ' }',
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
    var saving = false;

    if (val == 0) {
        total = document.getElementById(IdName).value;
        if (total <= 0) {
            document.getElementById(IdName).value = 1;
            saving = true;
        }
    }
    else {
        total = parseInt(document.getElementById(IdName).value) + parseInt(val);
        if (total < 0) {
            total = 0;
        }
        document.getElementById(IdName).value = total;
        saving = true;
    }

    if (saving) {
        $.ajax({
            type: "POST",
            url: "/Order/ProductQtySave",
            data: '{id: ' + Id + ', qty: ' + total + ' }',
            //data: {id: id, qty: total },
            contentType: "application/json; charset=utf-8",
            dataType: "json"
        });
    }

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
    //document.getElementById('loadingAnim').hidden = false;
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

function onClickSubOrder(cat, sub) {
    var xhr = HttpRequest('#productList');

    // configure the xhr object to fetch content
    xhr.open('get', '/order/sub/' + cat + '/' + sub, true);

    // fetch the content
    xhr.send(null);
}

function onClickRole(role) {
    var xhr = HttpRequest('#partialRegister');

    // configure the xhr object to fetch content
    xhr.open('get', '/Account/Select/' + role, true);
    // fetch the content
    xhr.send(null);
}

function onClickType(type) {
    var xhr = HttpRequest('#partialUserType');

    // configure the xhr object to fetch content
    xhr.open('get', '/Register/Type/' + type, true);
    // fetch the content
    xhr.send(null);
}

function onClickTypeAdmin(type) {
    var xhr = HttpRequest('#partialUserType');

    // configure the xhr object to fetch content
    xhr.open('get', '/Register_Admin/Type/' + type, true);
    // fetch the content
    xhr.send(null);
}

/* Search */
// filterProduct
// loc - order or product. Can't be null.
// cat - category (flooring, tiles, etc.). Can't be null.
// col - colour
// name - name of product
function onFilterProduct(loc, cat) {
    var name = document.getElementById('searchName').value;
    if (name == null || name == "") { name = "null"; }
    name = name.trim();

    var col = document.getElementById('searchColour').value;
    if (col == null || col == "") { col = "null"; }
    col = col.trim();

    var x = document.getElementById('dimXList').value;
    if (x == "-") { x = "null"; }
    x = x.trim();

    var y = document.getElementById('dimYList').value;
    if (y == "-") { y = "null"; }
    y = y.trim();

    var z = document.getElementById('dimZList').value;
    if (z == "-") { z = "null"; }
    z = z.trim();

    var price = document.getElementById('PriceList').value;
    if (price == "-") { price = "null"; }
    price = price.trim();

    var xhr = HttpRequest('#productList');
    // configure the xhr object to fetch content
    if (loc == 'search') {
        //var cats;
        var categs = "";
        var count = document.getElementById("myCount").innerHTML;

        for (i = 0; i < 5; i++) {
            categs += '/';
            if (i <= count - 1) {

                if (document.getElementById(i).checked == true) {
                    //cats[i] = '1';
                    //categs += '1'
                    categs += document.getElementById(i).title;
                }
                else {
                    //cats[i] = '0';
                    categs += '0';
                }
            }
            else {
                //cats[i] = '0';
                categs += '0';
            }
        }
        xhr.open('get', '/search/' + cat + '/' + col + '/' + name + '/' + x + '/' + y + '/' + z + '/' + price + categs, true);
        //xhr.open('get', '/search/' + cat + '/' + col + '/' + name + '/' + x + '/' + y + '/' + z + '/' + price +
        //    '/' + "1" + '/' + "0" + '/' + "0" + '/' + "0" + '/' + "0", true);
    }
    else if (loc == 'product') {
        xhr.open('get', '/product/search/' + cat + '/' + col + '/' + name + '/' + x + '/' + y + '/' + z + '/' + price, true);
    }
    else {
        xhr.open('get', '/order/search/' + cat + '/' + col + '/' + name + '/' + x + '/' + y + '/' + z + '/' + price, true);
    }

    // fetch the content
    //xhr.send(null);
    xhr.send(null);
}

function onClickSuite() {
    var id = document.getElementById('order_list').value;

    if (id == '-') {
        return;
    }
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

function projectSelect() {
    document.getElementById('clientForm').hidden = false;
    var item = document.getElementById('project_list').value;

    document.getElementById('ProjectName').value = item.slice(0, item.indexOf(" - ")).trim();
}

function updateCheck(check) {
    var box = document.getElementById(check).checked;
    var img = document.getElementById('lightCheck');

    if (box) {
        img.src = "../images/check-true.png";
    }
    else {
        img.src = "../images/check-false.png";
    }
}

function onCheckClick(tot, cb) {
    if (cb == 0) {
        for (i = 1; i <= tot; i++) {
            document.getElementById(i).checked = false;
        }
    }
    else {
        document.getElementById(0).checked = false;
    }
}

function updateRoleBackground(radioClick) {
    alert(selected);
    var selected = document.getElementById(radioClick);
    selected.style.backgroundColor = 'rgb(150, 200, 225)';
}

function agreeTerms() {
    var terms = document.getElementById('idTerms').checked;

    if (terms) {
        document.getElementById('btnSubmit').disabled = false;
    }
    else {
        document.getElementById('btnSubmit').disabled = true;
    }
}


// Product accessories expand panel

// expandIn - mouse over item, change background colour to hover colour
// Id - Id of the item
function expandIn(Id) {
    Id.style.backgroundColor = 'rgb(215, 235, 245)';
}

// expandOut - mouse exit item, change colour to regular colour
// Id - Id of the item
function expandOut(Id) {
    Id.style.backgroundColor = 'rgb(255, 255, 255)';
}

// expandProduct -
// Id - Id  of the item
function expandProduct(item) {

    //alert("hi i am elodie");
    alert(item);
    var area = document.getElementById(item);

    if (area.hidden) {
        area.hidden = false;
    }
    else {
        area.hidden = true;
    }
    //document.getElementById('exp-51').hidden = true;
}

// HomeCategory - onclick product category on the home page, opens up appropriate page
function onHomeCategory(cat) {
    $.ajax({
        type: "POST",
        url: "/" + cat + "_Category/",
        contentType: "application/json; charset=utf-8"
    });
}

//$(function () {
//    $("#slider").responsiveSlides({
//        auto: true,
//        nav: true,
//        speed: 2000,
//        namespace: "callbacks",
//        pager: true,
//    });
//});

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
//            alert("posting: " + response.responseText);
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
//            alert("ppppp: " + response.responseText);
//        }
//    });
//});

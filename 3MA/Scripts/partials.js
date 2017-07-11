///* Order */
//function onClickOrder() {
//    alert("help!")
//    var id = document.getElementById('order_list').value;

//    alert(id);

//    if (id == '-') {
//        return;
//    }

//    var xhr = HttpRequest('#suite_order');

//    alert(id);

//    // configure the xhr object to fetch content
//    xhr.open('get', '/order_suite/' + id, true);
//    // fetch the content
//    xhr.send(null);
//}

//function onClickSuite() {
//    alert("help!")
//    var id = document.getElementById('order_list').value;

//    alert(id);

//    if (id == '-') {
//        return;
//    }

//    var xhr = HttpRequest('#suite_order');

//    alert(id);

//    // configure the xhr object to fetch content
//    xhr.open('get', '/order_suite/' + id, true);
//    // fetch the content
//    xhr.send(null);
//}

///* Main category */
//function onClickMain(category) {
//    var xhr = HttpRequest('#subCategory');

//    // configure the xhr object to fetch content
//    xhr.open('get', '/' + category + '/', true);
//    // fetch the content
//    xhr.send(null);
//}

///* Sub Category */
//function onClickSub(cat, sub) {
//    var xhr = HttpRequest('#productList');
//    // configure the xhr object to fetch content
//    xhr.open('get', '/product/sub/' + cat + '/' + sub, true);  
//    // fetch the content
//    xhr.send(null);
//}

//function onClickSubOrder(cater, suber) {
//    var xhr = HttpRequest('#productList');

//    // configure the xhr object to fetch content
//    xhr.open('get', '/order/sub/' + cater + '/' + suber, true);
//    // fetch the content
//    xhr.send(null);
//}

///* Search */
//function filterProduct(loc, cat, col, name) {
//    var xhr = HttpRequest('#productList');

//    // configure the xhr object to fetch content
//    if (loc == 'product') {
//        xhr.open('get', '/product/search/' + cat + '/' + col + '/' + name, true);
//    }
//    else {
//        xhr.open('get', '/order/search/' + cat + '/' + col + '/' + name, true);
//    }
//    // fetch the content
//    xhr.send(null);
//}

//function HttpRequest(listVal) {
//    // Get a reference to the DOM element
//    var e = document.querySelector(listVal);

//    // create an xhr object
//    var xhr = new XMLHttpRequest();

//    // configure its handler
//    xhr.onreadystatechange = function () {

//        if (xhr.readyState === 4) {
//            // request-response cycle has been completed, so continue
//            if (xhr.status === 200) {
//                // request was successfully completed, and data was received, so continue
                
//                // update the user interface
//                e.innerHTML = xhr.responseText;
                
//                document.getElementById('productsTable').scrollIntoView(true);
//                document.getElementById('productsAnchor').scrollIntoView(true);

//            } else {
//                e.innerHTML = "<p>Request was not successful<br>(" + xhr.statusText + ")</p>";
//            }
//        } else {
//            e.innerHTML = "<p>Loading...</p>";
//        }
//        // show the content
//        e.style.display = 'block';
//    }
//    return xhr;
//}
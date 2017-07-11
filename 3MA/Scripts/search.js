function onFilterProduct(loc, cat, col, name)
{
    alert("Manilyn");
    var xhr = HttpRequest('#productList');

    if (col == null) {col = "null"; }
    if (name == null) { name = "null"; }

    var x = document.getElementById('dimXList').value;
    if (x == "-") { x = "null";}

    var y = document.getElementById('dimYList').value;
    if (y == "-") { y = "null"; }

    var z = document.getElementById('dimZList').value;
    if (z == "-") { z = "null"; }

    var price = document.getElementById('PriceList').value;
    if (price == "-") { price = "null"; }


    // configure the xhr object to fetch content
    if (loc == 'product') {
        xhr.open('get', '/product/search/' + cat + '/' + col + '/' + name, true);
    }
    else {

        alert("cat: " + cat);
        alert("col: " + col);
        alert("name: " + name);
        alert("x: " + x);
        alert("y: " + y);
        alert("z: " + z);
        alert("price: " + price);

        xhr.open('get', '/order/search/' + cat + '/' + col + '/' + name + '/' + x + '/' + y + '/' + z + '/' + price, true);
        //xhr.open('get', '/order/search/cat/col/name/x/y/z/price', true);
    }

    // fetch the content
    xhr.send(null);
}
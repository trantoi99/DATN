$(document).ready(function () {
    cart.init();
    //logout.Events();
    $(".quantity").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode

        if (!(keyCode >= 48 && keyCode <= 57)) {
            return false;
        }
        else {
            return true;
        }
    });
});
function AddItem(Id) {
    console.log(Id)
    let formData = new FormData();
    formData.append('productId', Id);
    formData.append('quantity', 1);

    postData("POST", "/Cart/AddItems", formData).then(function (response) {
        console.log(response);
        alert("success");
    })

};
function formatMoney(val) {
    while (/(\d+)(\d{3})/.test(val.toString())) {
        val = val.toString().replace(/(\d+)(\d{3})/, '$1' + '.' + '$2');
    }
    return val;
}
var cart = {
    init: function () {
        cart.regEvents();
    },
    regEvents: function () {
        postData('GET', '/Cart/CartProduct', null).then(function (result) {
            if (result != null) {
                var html = '';
                var subtotal = 0;
                var discount = 0;
                var total;
                if (result.length == 0) {
                    html += '<tr class="text-center"><td colspan="6" ><span>No products!  Please shopping </span></td></tr>';
                }
                else {
                    $.each(result, function (key, item) {
                        var price = item.Product.UnitPrice;
                        var sale = item.Product.Price_Sale;
                        var quantity = item.Quantity;
                        var mul = price * quantity;
                        var mulSale = sale * quantity;
                        subtotal += mul;
                        discount += mulSale;
                        html += '<tr>';
                        html += '<td>';
                        html += '<div class="row">' + '<div class="col-sm-2 hidden-xs"><img src="~/Photos/Admin/Product/' + item.Product.ImageURL + '" alt="' + item.Product.ProductName + '" class="img-responsive" /></div>' + '<div class="col-sm-10">' + '<h4 class="nomargin">' + item.Product.ProductName + '</h4>' + '<p>' + item.Product.Detail + '</p>' + '</div>' + '</div>';
                        html += "</td>"
                        html += ' <td>' + item.Product.UnitPrice + '</td>';
                        html += '<td>';
                        html += '<input type="text" style="font-size:18px" name="quantity" class=" form-control" onkeyup="change(' + item.Product.ProductID + ');" value=' + item.Quantity + ' min="1" max="100">';
                        html += '</td>';
                        html += '<td class="text-center">1.99</td>';
                        html += '<td class="actions">';
                        html += '<button onclick="Delete(' + item.Product.ProductID + ')" class="btn btn-danger btn-lg"><i class="fas fa-trash-alt"></i></button>'
                        html += '</tr>';
                        /*
                        html += '<tr class="text-center">';
                        html += '<td ><a href="#" onclick="Delete(' + item.Product.ProductID + ')"><span class="ion-ios-close"></span>Xóa</a></td>';
                        html += '<td><img src="~/Photos/Admin/Product/'+ item.Product.ImageURL+'"/></td>';
                        html += '<td> <h3>' + item.Product.ProductName + '</h3><p>' + item.Product.Description + '</p></td>';
                        html += '<td>' + formatMoney(item.Product.UnitPrice) + '</td>';
                        html += '<td><div class="input-group mb-3">';
                        html += '<input type="text" name="quantity" class=" form-control" onkeyup="change(' + item.Product.ProductID + ');" value=' + item.Quantity + ' min="1" max="100">';
                        html += '</div></td>';
                        html += '<td>' + formatMoney(mul) + '</td>';
                        html += '</tr>';
                        
                        html += ' < td > ' + '<div class="row">' + '<div class="col-sm-2 hidden-xs"><img src="~/Photos/Admin/Product/' + item.Product.ImageURL + '" alt="' + item.Product.ProductName + '" class="img-responsive" /></div>' + '<div class="col-sm-10">' + '<h4 class="nomargin">' + item.Product.ProductName + '</h4>' + '<p>' + item.Product.Detail + '</p>' + '</div>' + '</div>' + '</td>';
                        
                        */
                    });
                }
                total = subtotal - discount;
                $("#table").html(html);
                $("#subtotal").html(formatMoney(subtotal));
                $("#discount").html(formatMoney(discount));
                $("#total").html(formatMoney(total));
            }
            else {
                alert("Error do not perform!");
            }
        })
    }
};

function change(Id) {
    var qua = document.getElementsByName("quantity")[0].value;
    let formData = new FormData();
    formData.append('productId', Id);
    formData.append('quantity', 1);
    postData('POST', '/Cart/Update', formData).then(function (data) {
        if (data != null) {
            cart.init();
        }
        else {
            toastr["error"]("Error 404!");
        }
    });
};
function Delete(Id) {
    postData('POST', '/Cart/Delete/' + Id, null).then(function (data) {
        if (data != null) {
            document.getElementById("cart-count").innerHTML = '<span class="icon-shopping_cart"></span>][' + data + ']';
            cart.init();
        }
        else {
            toastr["error"]("Error 404");
        }
    })
};

async function postData(verb, url, data) {
    const response = await fetch(url, {
        method: verb,
        mode: 'cors',
        cache: 'default',
        credentials: 'same-origin',
        redirect: 'follow',
        referrerPolicy: 'no-referrer',
        body: data
    }).catch(error => console.error('Error', error));
    return response.json();
};
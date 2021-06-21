$(document).ready(function () {
    loadData();
})

function loadData() {
    $.ajax({
        type: "GET",
        url: "/ProductAdmin/ListProduct",
        success: function (response) {
            var html = "";
            response.map((item, key) => {
                html += `<tr><td>` + item.ProductID + `</td><td>` + item.ProductName + `</td><td>` + item.UnitPrice + `</td><td>` + item.Description + `</td><td>` + item.Detail + `</td><td>` + item.ImageURL + `</td><td>` + item.Amount + `</td><td><button type="button" class="btn btn-success" data-toggle ="modal" data-target = "#editProduct"  onclick = getInfoById(` + item.ProductID + `)>Sửa</button><button class="btn btn-danger" onclick = "deleteProduct(` + item.ProductID + `)">Xóa</button></td></tr>`
            });
           
            $("#tbodyProduct").html(html);
        }
    })
}


function addProd() {
    var fileUpload = $("#imgProduct").get(0);
    var file = fileUpload.files;
    const formData = new FormData();
    formData.append('ProductName', $("#txtNameProd").val());
    formData.append('UnitPrice', $("#txtPrice").val());
    formData.append('Description', $("#txtDescription").val());
    formData.append('Detail', $("#txtDetail").val());
    formData.append('CategoryID', $("#cateDropDownList").val());
    formData.append('Amount', $("#txtAmount").val());
    formData.append('prodFile', file[0]);
    postData('POST', '/ProductAdmin/addPro', formData).then(function (msg) {
        $("#addProduct").modal('hide');
    })
}
function getInfoById(id) {
   
    
    $.get("/ProductAdmin/getInfoId/" + id).then(data => {
        $('#txtId').val(data.ProductID);
        $("#txtProductName").val(data.ProductName);
        $("#txtNewPrice").val(data.UnitPrice);
        $("#txtNewDescription").val(data.Description);
        $("#txtNewDetail").val(data.Detail);
        $("#txtNewAmount").val(data.Amount);
        $("#cateEditDropDownList").val(data.CategoryID);
    });
}
function editProduct() {
    
    var fileUpload = $("#newImgProduct").get(0);
    if (fileUpload === undefined || fileUpload === null) return alert("Ban can them anh");
    var file = fileUpload.files;
    const formData = new FormData();
    formData.append('ProductID', $('#txtId').val());
    formData.append('ProductNameNew', $("#txtProductName").val());
    formData.append('PriceNew', $("#txtNewPrice").val());
    formData.append('Description', $("#txtNewDescription").val());
    formData.append('NewDetail', $("#txtNewDetail").val());
    formData.append('CategoryNew', $("#cateEditDropDownList").val());
    formData.append('AmountNew', $("#txtNewAmount").val());
    formData.append('prodFile', file[0]);
    postData('POST', '/ProductAdmin/editPro', formData).then(function (msg) {
        loadData();
        document.getElementById('close').click();
    })


}
function deleteProduct(id) {

    var ans = confirm("Bạn có muốn xóa không ?");
    if (ans) {
        $.ajax({
            type: "POST",
            url: "/ProductAdmin/Delete?id=" + id,
            contentType: "application/json;charset=UTF-8",
            dataType: "json",
            success: function (result) {
                loadData();
            },
            error: function (errormessage) {
                alert(errormessage.responseText);
            }
        });

    }
}
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
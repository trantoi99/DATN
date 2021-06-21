
$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        // goi url
        type: "GET",
        url: "/Sale/listSale",
        data: "",
        dataType: "json",
        // tra ve cai gi
        success: function (response) {
            var html = "";
            response.map((item, key) => {
                html += `<tr><td>` + item.SaleID + `</td><td>` + convertDate(item.DateStart) + `</td><td>` + convertDate(item.DateEnd) + `</td><td>` + item.Name + `</td><td>` + item.Discount + `</td><td><button type="button" class="btn btn-success" data-toggle ="modal" data-target = "#editSale" >Sửa</button><button class="btn btn-danger"onclick="deleteCate(` + item.SaleID + `)" >Xóa</button></td></tr>`
            })
            $('#tbodySale').html(html);
        },
    });
}

function convertDate(data) {
    var getdate = parseInt(data.replace("/Date(", "").replace(")/", ""));
    var ConvDate = new Date(getdate);
    var month = parseInt(ConvDate.getMonth()) + 1;
    if (month < 10) {
        month = "0" + month;
    }
    return ConvDate.getFullYear() + "/" + month + "/" + ConvDate.getDate();
}

function addSale() {
    const formData = new FormData();
    formData.append('Name', $("#saleName").val());
    formData.append('DateStart', $("#dateStart").val());
    formData.append('DateEnd', $("#dateEnd").val());
    formData.append('Discount', $("#discount").val());
    postData('POST', '/Sale/addSale', formData).then(function (msg) {
        console.log(msg);
        $("#addSale").modal('toggle');
        loadData();
    })
};

/*function getInfoById(id) {
    $.get('/Sale/getInfoId/' + id).then(
        function (data) {
            $('#CategoryID').val(data.CategoryID);
            $("#txtCateName").val(data.CategoryName);
            $("#txtCateFile").val(data.ImageURL);
            $("#editSale").modal('toggle');
        }
    )

}*/



function deleteSale(id) {
    var ans = confirm("Bạn có muốn xóa không ?");
    if (ans) {
        $.ajax({
            type: "POST",
            url: "/Sale/Delete?id=" + id,
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

//fetchAPI
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

$(document).ready(function () {
    loadData();
});

function loadData() {
    $.ajax({
        // goi url
        type: "GET",
        url: "/Category/listCategory",
        data: "",
        dataType: "json",
        // tra ve cai gi
        success: function (response) {
            var html = "";
            response.map((item, key) => {
                html += `<tr><td>` + item.CategoryID + `</td><td>` + item.CategoryName + `</td><td>` + item.ImageURL + `</td><td><button type="button" class="btn btn-success" onclick = getInfoById(` + item.CategoryID + `) data-toggle ="modal" data-target = "#editCate" >Sửa</button><button class="btn btn-danger"onclick="deleteCate(` + item.CategoryID + `)" >Xóa</button></td></tr>`
            })
            $('#tbodyCate').html(html);
        },
    });
}
function addCate() {
    var fileUpload = $("#cateFile").get(0);
    var file = fileUpload.files;
    const formData = new FormData();
    formData.append('CategoryName', $("#cateName").val());
    formData.append('cateFile', file[0]);
    postData('POST', '/Category/addCate', formData).then(function (msg) {
        console.log(msg);
        $("#addCate").modal('toggle');
    })
};

function getInfoById(id) {
    $.get('/Category/getInfoId/' + id).then(
        function (data) {
            $('#CategoryID').val(data.CategoryID);
            $("#txtCateName").val(data.CategoryName);
            $("#txtCateFile").val(data.ImageURL);
            $("#image").attr("src", "~/Photo/Admin/Category/data.ImageURL");
            $("#editCate").modal('toggle');
        }
    )

 }

function editCate() {
    var obj = {
        CatergoryID: $('#CategoryID').val(),
        CatergoryName: $('#CategoryID').val(),
        ImageURL: $('#txtCateFile').val(),
    }
    var fileUpload = $("#txtCateFile").get(0);
    var file = fileUpload.files;
    const formData = new FormData();
    formData.append('CateID', $('#CategoryID').val());
    formData.append('CategoryNameNew', $("#txtCateName").val());
    formData.append('cateFile', file[0]);
    postData('POST', '/Category/editCate', formData).then(function (msg) {
        console.log(msg);
        loadData();
        $("#editCate").modal('toggle');
    })
   
}

function deleteCate(id) {
    
    var ans = confirm("Bạn có muốn xóa không ?");
    if (ans) {
        $.ajax({
            type: "POST",
            url: "/Category/Delete?id=" + id,
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
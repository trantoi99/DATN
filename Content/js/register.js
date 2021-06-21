$(document).ready(function () {
    $('.mdb-select').materialSelect();
});
function register() {
    const formData = new FormData();
    formData.append('CustomerName', $("#txtFullName").val());
    formData.append('DateBirth', $("#txtBirthday").val());
    formData.append('Gender', $("#dropDownGender").val());
    formData.append('Address', $("#txtAddress").val());
    formData.append('Email', $("#txtEmail").val());
    formData.append('PhoneNumber', $("#txtPhone").val());
    formData.append('UserName', $("#txtUserName").val());
    formData.append('Password', $("#txtPassWord").val());
    postData('POST', '/LoginClientSide/Register', formData).then(function (msg) {
        alert("Đăng ký thành công");
        window.location= "/LoginClientSide/Login";
    }).catch(function (error) {
        alert("error");
        window.location.reload();
    })
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

// validate date
var today = new Date();
var dd = today.getDate();
var mm = today.getMonth() + 1; //January is 0!
var yyyy = today.getFullYear();
if (dd < 10) {
    dd = '0' + dd
}
if (mm < 10) {
    mm = '0' + mm
}

today = yyyy + '-' + mm + '-' + dd;
document.getElementById("txtBirthday").setAttribute("max", today);
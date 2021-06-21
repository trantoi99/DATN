const { compile } = require("sizzle");

function payment() {
    let formData = new FormData();
   
    $("#txtPhoneNumber").val();
    $("#txtAddress").val();

    postData("POST", "/Cart/oderProduct/").then((data) => {
        console.log(data);
        alert(data);
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
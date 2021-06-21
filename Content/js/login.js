
function login() {
    //Sao nó đọc về customname nhỉ
    // rõ ràng là username
    const formData = new FormData();
    formData.append('UserName', $("#txtUser").val());
    formData.append('Password', $("#txtPassword").val());
    postData('POST', '/LoginClientSide/Login', formData).then(function (msg) {
        window.location = "/Home/Index";
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
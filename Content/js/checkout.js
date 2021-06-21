$(document).ready(function () {
    $('#loading-ft').removeClass('show');
    $("#txtPhone").bind("keypress", function (e) {
        var keyCode = e.which ? e.which : e.keyCode

        if (!(keyCode >= 48 && keyCode <= 57)) {
            return false;
        }
        else {
            return true;
        }
    });
});
var checkout = {
    init: function () {
        checkout.regEvents();
    },
    regEvents: function () {
        if ($("#cbTerm").is(':checked')) {
            $('#cbTerm').popover('hide');
            $('#rdDirect').popover('hide');
            $('#rdPaypal').popover('hide');
            var rdCheck = $("#rdCheck").is(':checked');
            var rdDirect = $("#rdDirect").is(':checked');
            var rdPaypal = $('#rdPaypal').is(':checked');
            if (rdCheck) {
                $('#loading-ft').addClass('show');
                let formData = new FormData();
                formData.append('FullName', $("#txtName").val());
                formData.append('Email', $("#txtEmail").val());
                formData.append('PhoneNumber', $("#txtPhone").val());
                formData.append('Address', $("#txtAddress").val());

                postData('POST', '/Cart/Payment', formData).then(function (data) {
                    $('#loading-ft').removeClass('show');
                    if (data != "Error") {
                        toastr["success"]("Payment success!");
                        setTimeout(function () {
                            window.location.href = "/www.vegefood.com/product"
                        }, 1200);
                    }
                    else {
                        toastr["success"]("Error 404!");
                    }
                })
            }
            else {
                if (rdDirect) {
                    $('#rdDirect').popover('show');
                    $('#rdPaypal').popover('hide');
                }
                else {
                    if (rdPaypal) {
                        $('#rdPaypal').popover('show');
                        $('#rdDirect').popover('hide');
                    }
                }
            }
        }
        else {
            $('#cbTerm').popover('show');
        }
        //$(".btnLogout").click(function () {
        //    var actionCallback = function (data) {
        //        if (data == 1) {
        //            window.location.reload();
        //        }
        //    };
        //    var ajaxHeaper = new httpRequest("/Home/DeleteSession");
        //    ajaxHeaper.SelectAll(actionCallback);
        //});
    }
};
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
//validate
(function ($) {
    "use strict";

    /*==================================================================
    [ Validate ]*/
    var input = $('.validate-input .form-control');
    //Đăng nhập
    $('#btncheckout').click(function () {
        var checks = true;

        for (var i = 0; i < input.length; i++) {
            if (validate(input[i]) == false) {
                showValidate(input[i]);
                checks = false;
            }
            else {
                checks = true;
            }
        }
        if (checks) {
            checkout.init();
        };
        return checks;
    });
    $('.validate-form .form-control').each(function () {
        $(this).focus(function () {
            hideValidate(this);
        });
    });

    function validate(input) {
        if ($(input).attr('type') == 'email' || $(input).attr('name') == 'email') {
            if ($(input).val().trim().match(/^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{1,5}|[0-9]{1,3})(\]?)$/) == null) {
                return false;
            }
        }
        else {
            if ($(input).val().trim() == '') {
                return false;
            }
        }
    }

    function showValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).addClass('alert-validate');
    }
    function hideValidate(input) {
        var thisAlert = $(input).parent();

        $(thisAlert).removeClass('alert-validate');
    }
    /*==================================================================
    [ Show pass ]*/
    var showPass = 0;
    $('.btn-show-pass').on('click', function () {
        if (showPass == 0) {
            $(this).next('input').attr('type', 'text');
            $(this).find('i').removeClass('fa-eye');
            $(this).find('i').addClass('fa-eye-slash');
            showPass = 1;
        }
        else {
            $(this).next('input').attr('type', 'password');
            $(this).find('i').removeClass('fa-eye-slash');
            $(this).find('i').addClass('fa-eye');
            showPass = 0;
        }

    });

})(jQuery);


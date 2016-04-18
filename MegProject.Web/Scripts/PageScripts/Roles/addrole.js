$(document).ready(function () {




    $('#formrole').bootstrapValidator({

        message: 'Değer boş olamaz!',
        feedbackIcons: {
            valid: 'glyphicon glyphicon-ok',
            invalid: 'glyphicon glyphicon-remove',
            validating: 'glyphicon glyphicon-refresh'
        },
        fields: {
            RoleName: {
                message: 'Rol Adı Boş Olamaz!',
                validators: {
                    notEmpty: {
                        message: 'Boş Olamaz!'
                    }
                }
            }
        }

    }).on('success.form.bv', function (e) {
        // Prevent form submission
        e.preventDefault();

        // Get the form instance
        var $form = $(e.target);

        // Get the BootstrapValidator instance
        var bv = $form.data('bootstrapValidator');

        // Use Ajax to submit form data
        submitRoleForm();
    });



});


var submitRoleForm = function () {

    var param = {
        "Id": $('input[name="RoleId"]').val(),
        "RoleName": $('#rolename').val(),
        "Status": "0",
        "actions": selectControllerActionsJson()
    };


    // alert(JSON.stringify( selectControllerActionsJson() ));
    // alert(JSON.stringify(param));
    $.ajax({
        type: 'post',
        url: submitUrl, //TODO: Url için role action ve controller eklenecek 
        contentType: 'application/json; utf-8;',
        dataType: 'json',
        data: JSON.stringify(param)
    }).done(function (data) {
        $.alert(data.message, {
            title: 'Kayıt İşlemi',
            closeTime: 3000,
            autoClose: true,
            withTime: true,
            type: data.result,
            isOnly: true,
            onClose: function () {
                window.location.href = "Index"
            }
        });

    });


}



var selectControllerActionsJson = function () {
    var jsonObject = [];

    // Select CheckBox Controller Id 
    $('input[name="controllerId"]:checked').each(function () {

        var item = {};
        //item['ControllerId'] = $(this).val();
        var controller_Id = $(this).val();
        $('#select_' + $(this).val()).each(function (i, selected) {


            for (var k in $(this).val()) {

                item = {};
                item["ControllerId"] = controller_Id;
                item["ActionId"] = $(this).val()[k];
                item["RoleId"] = "0";
                item["Status"] = "0";
                jsonObject.push(item);

            }

        });


    });

    return jsonObject;
}



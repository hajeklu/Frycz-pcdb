function addType() {

    var value = {
        name: $('#addTypeInput').val()
    };

    $.ajax({
        type: "POST",
        url: 'ComputerType/addJax',
        data: value,
        dataType: 'json',

        success: function () {
            location.reload();
            $("#note").show();
            setTimeout(function () { $("#note").hide(); }, 1000);
        },
        error: function () {
            alert("Error: Computer type cannot be added because is invalid or in use.");
        }
    });

}

function addUser() {
    var value = {
        firstname: $('#firstnameAjax').val(),
        lastname: $('#lastnameAjax').val()
    }

    $.ajax({
        type: "POST",
        url: 'AddUser/addJax',
        data: value,
        dataType: 'json',

        success: function () {
            $("#note").show();
            setTimeout(function () { $("#note").hide(); }, 1000);
            $("#userAdd").modal("hide");
        },
        error: function () {
            alert("Error: User cannot be add becouse is invalid or inuse.");
        }
    });
}
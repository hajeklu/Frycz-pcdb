function addType() {

    var value = {
        name: $('#addTypeInput').val()
    };

    $.ajax({
        type: "POST",
        url: '/ComputerType/addJax',
        data: value,
        dataType: 'json',

        success: function () {
            location.reload();
            $("#note").show();
            setTimeout(function () { $("#note").hide(); }, 1000);
        },
        error: function () {
            alert("Error: Computer type cannot be added because is invalid, in use or other issue.");
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
        url: '/AddUser/addJax',
        data: value,
        dataType: 'json',

        success: function () {
            $("#note").show();
            setTimeout(function () { $("#note").hide(); }, 1000);
            $("#userAdd").modal("hide");
        },
        error: function () {
            alert("Error: User cannot be add becouse is invalid, in use or other issue.");
        }
    });
}

function addParameters() {
    var value = {
        processor: $('#processor').val(),
        ram: $('#ram').val(),
        hdd: $('#hdd').val()
    }

    $.ajax({
        type: "POST",
        url: '/ComputerParameters/AddAjax',
        data: value,
        dataType: 'json',

        success: function () {
            location.reload();
            $("#note").show();
            setTimeout(function () { $("#note").hide(); }, 1000);
        },
        error: function () {
            alert("Error: Parameters cannot be add becouse is invalid, in use or other issue.");
        }
    });
}

function addOS() {
    var value = {
        name: $('#OSName').val(),
        version: $('#OSVersion').val(),
    }

    $.ajax({
        type: "POST",
        url: '/ComputerOS/AddAjax',
        data: value,
        dataType: 'json',

        success: function () {
            location.reload();
            $("#note").show();
            setTimeout(function () { $("#note").hide(); }, 1000);
        },
        error: function () {
            alert("Error: Os cannot be add becouse is invalid, in use or other issue.");
        }
    });
}

function addModel() {
    var value = {
        maker: $('#MakerM').val(),
        model: $('#ModelM').val(),
    }

    $.ajax({
        type: "POST",
        url: '/ComputerModel/AddAjax',
        data: value,
        dataType: 'json',

        success: function () {
            location.reload();
            $("#note").show();
            setTimeout(function () { $("#note").hide(); }, 1000);
        },
        error: function () {
            alert("Error: Model cannot be add becouse is invalid, in use or other issue.");
        }
    });
}
// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(function () {
    $('#btn-submit').on('click', function () {
        $("#msg").html("");
        $("#result").html("");
        if (ValidInputs()) {
            var customer = {
                firstName: "",
                middleName: "",
                lastName: "",
                amount: 0
            }  
            var options = {};
            options.url = "https://localhost:44343/api/Customer/Process";
            options.type = "POST";
            var obj = customer;
            obj.firstName = $("#FirstName").val();
            obj.middleName = $("#MiddleName").val();
            obj.lastName = $("#LastName").val();
            obj.amount = $('#Amount').val();
            options.data = JSON.stringify(obj);
            options.contentType = "application/json; charset=utf-8";
            options.success = function (data) {
                if (data.fullName && data.formattedAmount) {
                    $("#resultHeader").removeClass("hide");
                    $("#result").append("<h4>" + data.fullName + "</h4>");
                    $("#result").append("<h4>" + data.formattedAmount + "</h4>");
                }
                else {
                    $("#msg").html("Error while calling the Web API!");
                }
            };
            options.error = function () {
                $("#msg").html("Error while calling the Web API!");
            };
            $.ajax(options);
        }
        return false;
    });

    function ValidInputs() {
        if ($("#FirstName").val() == "" || $("#Amount").val() == "") {
            $("#msg").html("Please provide mandatory fields!")
            return false;
        }
        if ($("#Amount").val() != "" && /^([0-9]+(?:[\.][0-9]*)?|\.[0-9]+)$/.test($("#Amount").val()) && $("#Amount").val().length < 16) {
            return true;
        }
        else {
            $("#msg").html("Please enter valid amount!")
        }
        return false;
    }
});

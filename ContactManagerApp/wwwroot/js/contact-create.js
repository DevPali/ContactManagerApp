$(document).ready(function () {
    isAuthorized();
    $('#dateofBirth').datepicker({
        format: 'yyyy-mm-dd',
        autoclose: true,
        todayHighlight: true
    });
});

function isAuthorized() {
    $.ajax({
        method: 'GET',
        url: '/api/account/authorize',
        success: function (response) {
        },
        error: function (error) {
            if (error.status == 401) {
                alert('Access denied.');
                window.location.href = '/Contacts/Index';
            }
            else {
                alert("Something went wrong.");
            }
        }
    });
}

$('#btnCreate').click(createContact);

function createContact() {
    const data = $('#create-contact-form').serializeArray();
    const json = {};
    data.forEach((item) => json[item.name] = item.value);
    if (json.DateofBirth === "") {
        json.DateofBirth = null;
    }
    const stringify = JSON.stringify(json);

    $.ajax({
        method: 'POST',
        contentType: 'application/json',
        url: '/api/Contact',
        data: stringify,
        datatype: 'json',
        success: function () {
            clearErrors();
            clearFields();
            alert("Successfull save.");
        },
        error: function (result) {
            const errors = JSON.parse(result.responseText).errors;
            clearErrors();
            for (const key of Object.keys(errors)) {
                const value = errors[key];
                $('#' + key).html(value);
            }
        }
    });
}

function clearErrors() {
    $("[id*='Error']").text('');
}

function clearFields() {
    const ids = ['FirstName', 'LastName', 'City', 'Prefecture', 'PostalCode', 'dateofBirth', 'Mobile'];
    ids.forEach((id) => $('#' + id).val(''));
}

var currentURL = window.location.href;
var word = "Edit/";
var index = currentURL.indexOf(word);
var contactId = currentURL.substring(index + word.length);

$(document).ready(function () {
    isAuthorized();
    requestContactInfo();
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

$('#btnSave').click(editContact);

function requestContactInfo() {
    $.ajax({
        method: 'GET',
        url: '/api/Contact/' + contactId,
        success: function (response) {
            for (var i in response) {
                $('#' + i).val(response[i])
            }
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
}

function editContact() {
    const data = $('#edit-contact-form').serializeArray();
    const json = {};    
    data.forEach((item) => json[item.name] = item.value);
    if (json.DateofBirth === "") {
        json.DateofBirth = null;
    }
    const stringify = JSON.stringify(json);

    $.ajax({
        method: 'PUT',
        contentType: 'application/json',
        url: '/api/Contact/' + contactId,
        data: stringify,
        datatype: 'json',
        success: function () {
            alert("Successfull save.");
            window.location.href = '/Contacts/Index';
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
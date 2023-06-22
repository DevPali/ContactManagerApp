var currentURL = window.location.href;
var word = "Delete/";
var index = currentURL.indexOf(word);
var contactId = currentURL.substring(index + word.length);

$(document).ready(function () {
    isAuthorized();
    requestContactInfo();
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

function requestContactInfo() {    
    $.ajax({
        method: 'GET',
        url: '/api/Contact/' + contactId,
        success: function (response) {
            for (var i in response) {
                $('#' + i).text(response[i])
            }            
        },
        error: function (error) {
            console.log(error.responseText);
        }
    });
};

$('#btnDelete').click(deleteContact);

function deleteContact() {
    $.ajax({
        method: 'DELETE',
        url: '/api/Contact/' + contactId,
        success: function (response) {
            alert("Deleted successfully.");
            window.location.href = '/Contacts/Index';
        },
        error: function () {
            alert("Something went wrong.");
        }
    });
}
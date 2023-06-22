$(document).ready(function () {
    requestContacts();
});

function requestContacts() {
    $.ajax({
        method: 'GET',
        url: '/api/Contact',
        success: function (response) {
            for (var i in response) {
                response = response.map(obj => replaceNulls(obj));
                $('.table').append('<tr> <td>' +
                    response[i].firstName +
                    '</td> <td>' +
                    response[i].lastName +
                    '</td> <td>' +
                    response[i].city +
                    '</td> <td>' +
                    response[i].prefecture +
                    '</td> <td>' +
                    response[i].postalCode +
                    '</td> <td>' +
                    response[i].dateofBirth.slice(0, response[i].dateofBirth.indexOf("T")) +
                    '</td> <td>' +
                    response[i].mobile +
                    '</td> <td>' +
                    '<a href="/Contacts/Edit/' + response[i].id + '">Edit</a> |' +
                    //'<a href="/Contacts/Details/' + response[i].id + '">Details</a> |' +
                    '<a href="/Contacts/Delete/' + response[i].id + '">Delete</a>' +
                    '</td> </tr>');
            }
        },
        error: function (error) {
            alert("Something went wrong.");
        }
    });
}

function replaceNulls(jsonObject) {
    const jsonString = JSON.stringify(jsonObject, (key, value) => {
        if (value === null) {
            return '';
        }
        return value;
    });
    return JSON.parse(jsonString);
}


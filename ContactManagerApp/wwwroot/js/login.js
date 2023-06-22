$('#btnLogin').click(login);

function login() {
    const data = $('#login-form').serializeArray();
    const json = {};
    data.forEach((item) => json[item.name] = item.value);
    const stringify = JSON.stringify(json);

    $.ajax({
        method: 'POST',
        contentType: 'application/json',
        url: '/api/account/login',
        data: stringify,
        datatype: 'json',
        success: function () {
            window.location.href = '/Contacts/Index';
        },
        error: function (result) {
            const errors = JSON.parse(result.responseText).errors;
            $('#LoginError').text('');
            for (const key of Object.keys(errors)) {
                const value = errors[key];
                const error = value.join('<br/>');
                $('#LoginError').html(error);
            }
        }
    });
}
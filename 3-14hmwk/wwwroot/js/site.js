// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(() => {

    $("#submit").on('click', function () {

        const realPassword = $(this).data('password');
        const password = $("#password").val();

        if (password !== realPassword) {
            //$("#div").prepend(`<h3 class="alert-danger">Invalid password</h3>`);
            alert("Invalid password");
            $("#form").attr('action', '/Home/ViewImageRequest');
        }

        else if (password === realPassword) {
            $("#form").attr('action', '/Home/ViewImage');

        }

    });
});

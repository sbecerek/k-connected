$(document).ready(() => {


    $("#loginbutton").on('click', function (e) {
        e.preventDefault();

        $.ajax({
            type: "post",
            url: "api/login/Authenticate",
            data: $('#loginform').serialize(),
            success: function (token) {
                $.ajax({
                    type: "get",
                    url: "api/login",
                    headers: { "Authorization": 'Bearer ' + token },
                    success: function (response) {
                        console.log(response);
                        //here I can redirect to map page
                        //on map page I can get data of other users
                        // Simulate an HTTP redirect:
                        window.location.replace("../mainview.html");
                    }
                });
            }
        });
    })




})
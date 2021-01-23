
$(document).ready(() => {

    let username;
    let pass;
    $("#technologyselect").select2({
        placeholder: "Select Technologies", //placeholder
        tags: true,
        tokenSeparators: ['/', ',', ';', " "]
    });





    $('#signupbutton').on('click', function (e) {



        window.location.replace("../register.html")
    })


    function validate() {
        var x = document.getElementById("password");
        var y = document.getElementById("retypePassword");
        if (x.value == y.value) return;
        else alert("password not same");
    }


    $('#savebutton').on('click',async function (e) {
        validate();
        //e.preventDefault();

        await $('#signupform').submit();

        //if (document.getElementById('signupform').checkValidity())
            //document.forms['signupform'].reportValidity();
            //console.log($('#signupform').serialize());

        $.ajax({
                type: "post",
                url: "api/register",
                data: $('#signupform').serialize(),
                success: function (response) {
                    //console.log(response)
                    window.location.replace("../index.html");
                },
                error: function (params) {
                  alert("Invalid form" );  
                }

            });
        //else alert("Invalid Signup form");
                

        //alert("Invalid signup form")

    })


})



$(document).ready(() => {

    let username;
    let pass;
    $("#technologyselect").select2({
        placeholder: "Select Technologies", //placeholder
        tags: true,
        tokenSeparators: ['/',',',';'," "] 
    });





    $('#signupbutton').on('click',function (e) {



        window.location.replace("../register.html")
    })


    function validate() {
        var x= document.getElementById("password");
            var y= document.getElementById("retypePassword");
        if(x.value==y.value) return;
        else alert("password not same");
        }


    $('#savebutton').on('click',function (e) {
        validate();
        e.preventDefault();



        console.log($('#signupform').serialize());
        $.ajax({
            type: "post",
            url: "api/register",
            data: $('#signupform').serialize(),
            success: function (response) {
                console.log(response)
                window.location.replace("../index.html");
            }
        });
        
    })


})


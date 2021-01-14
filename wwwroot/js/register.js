
$(document).ready(() => {


    $("#technologyselect").select2({
        placeholder: "Select Technologies", //placeholder
        tags: true,
        tokenSeparators: ['/',',',';'," "] 
    });





    $('#signupbutton').on('click',function (e) {
        window.location.replace("../register.html")
    })


})


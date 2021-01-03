$(document).ready(() => {

    // let markers = [];
    // let map = L.map('idmap', {

    // }).setView([52.237049, 21.017532], 10);


    // L.tileLayer('https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=SXOFB5ujKbDrlisGWCgm', {
    //     attribution: '<a href="https://www.maptiler.com/copyright/" target="_blank">&copy; MapTiler</a> <a href="https://www.openstreetmap.org/copyright" target="_blank">&copy; OpenStreetMap contributors</a>',
    // }).addTo(map);


    // function displayData(data) {
    //     markers.forEach(m => {
    //         map.removeLayer(m);
    //     })

    //     data.forEach(element => {

    //         let coordinate = element.coordinate.split(",");

    //         let Skills = [];
    //         element.technologies.forEach(i => {
    //             Skills.push(i);
    //         })


    //         let marker = L.marker([parseFloat(coordinate[0]), parseFloat(coordinate[1])]).bindPopup("User:" + element.username + " Skills:" + Skills).addTo(map);
    //         markers.push(marker);
    //         console.log(element);
    //     });
    // }

    // $('#technologyselect').change(function (e) {
    //     let opt = $(this).val();
    //     console.log(opt);

    //     if (opt == "All")
    //         $.ajax({
    //             type: "GET",
    //             url: "api/Technology/",
    //             success: function (response) {
    //                 displayData(response);
    //             }
    //         });

    //     else
    //         $.ajax({
    //             type: "post",
    //             contentType: "application/json; charset=utf8",
    //             url: "api/Technology/",
    //             data: JSON.stringify(opt),
    //             success: function (response) {
    //                 displayData(response);
    //             }
    //         });

    // });



    // $.ajax({
    //     type: "GET",
    //     url: "api/home/",
    //     success: function (response) {
    //         displayData(response);
    //     }
    // });


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
                    }
                });
            }
        });
    })




})
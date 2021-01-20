
$(document).ready(() => {
    let token = sessionStorage.getItem('userToken');

    $("#technologyselect").select2({
        placeholder: "Select Technologies", //placeholder
        tags: true,
        tokenSeparators: ['/', ',', ';', " "]
    });


    //MAP RELATED CODE BELOW
    var userIcon = new L.Icon({
        iconUrl: '../me_logo.png',
        iconSize: [25, 25],
        iconAnchor: [12, 12],
        popupAnchor: [1, -10],

    });
    var otherIcon = new L.Icon({
        iconUrl: '../other_logo.png',
        iconSize: [25, 25],
        iconAnchor: [12, 12],
        popupAnchor: [1, -10],

    });


    let userMarker;
    let markers = [];
    let map = L.map('idmap', {

    }).setView([52.237049, 21.017532], 10);


    L.tileLayer('https://api.maptiler.com/maps/streets/{z}/{x}/{y}.png?key=SXOFB5ujKbDrlisGWCgm', {
        attribution: '<a href="https://www.maptiler.com/copyright/" target="_blank">&copy; MapTiler</a> <a href="https://www.openstreetmap.org/copyright" target="_blank">&copy; OpenStreetMap contributors</a>',
    }).addTo(map);


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

    $('#technologyselect').change(function (e) {
        let opt = $(this).val();
        console.log(opt);

        if (opt == "Any" || opt == null)
            $.ajax({
                type: "GET",
                url: "api/Home/otherusers",
                headers: { "Authorization": 'Bearer ' + token },
                success: function (response) {
                    displayData(response);
                }
            });

        else
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf8",
                headers: { "Authorization": 'Bearer ' + token },
                url: "api/Technology/",
                data: JSON.stringify(opt),
                success: function (response) {
                    console.log(response);
                    displayData(response);
                }
            });

    });




    function displayData(users) {
        markers.forEach(marker => {
            map.removeLayer(marker);
        })


        users.forEach(user => {
            let knowledge = document.createElement("div");
            knowledge.classList.add('knowledges');
            user.knowledges.forEach(k => {
                cell = document.createElement("div");
                cell.classList.add('cellbox');
                cell.innerHTML = k.skillName;
                knowledge.appendChild(cell);
            })


            //knowledges = JSON.stringify(knowledges);
            console.log(knowledge);

            L.esri.Geocoding.geocode().address(user.apartment + user.street).city(user.city).region(user.country).run(function (err, results, response) {
                if (err) {
                    console.log(err);
                    return;
                }

                let guesses = Object.values(results);
                let pointcoordinate = guesses[0][0].latlng;// first index is the one with highest score double indexing since this has many dimensions

                //console.log(pointcoordinate);
                let m = L.marker([pointcoordinate.lat, pointcoordinate.lng], { icon: otherIcon }).bindPopup(user.username + " " +
                    '<br/>' + $(knowledge).html() +
                    '<br/><textarea id="messagebox" name="text" placeholder="Write your message here"></textarea>' +
                    '<br/><button type="button" class="btn btn-primary sidebar-open-button" style="width:100%; border-radius:30px;" ">Send a mail</button>'
                ).on("popupopen", () => {
                    $(".btn").on("click", (e) => {
                        e.preventDefault();
                        sendMail(user.username, $('#messagebox').val());
                    })
                }).openPopup().addTo(map);
                markers.push(m);
            });
        });
    }



    function displayUser(users) {
        const popupOptions = { className: "customPopup" };

        users.forEach(user => {
            if (user.username != "admin") {

                let knowledge = document.createElement("div");
                knowledge.classList.add('knowledges');
                user.knowledges.forEach(k => {
                    cell = document.createElement("p");
                    cell.classList.add('cellbox');
                    cell.innerHTML = k.skillName;
                    knowledge.appendChild(cell);
                })


                //knowledges = JSON.stringify(knowledges);
                console.log(knowledge);

                L.esri.Geocoding.geocode().address(user.apartment + user.street).city(user.city).region(user.country).run(function (err, results, response) {
                    if (err) {
                        console.log(err);
                        return;
                    }

                    let guesses = Object.values(results);
                    let pointcoordinate = guesses[0][0].latlng;// first index is the one with highest score double indexing since this has many dimensions

                    //console.log(pointcoordinate);
                    userMarker = L.marker([pointcoordinate.lat, pointcoordinate.lng], { icon: userIcon }).bindPopup(user.username + " " +
                        '<br/>' + $(knowledge).html()
                    ).openPopup().addTo(map);
                });
            }
        });
    }

    function sendMail(username, text) {
        console.log(username);
        let userMessage =
        {
            username: username,
            text: text
        }

        $.ajax({
            type: "post",
            url: "api/mail/sendmail",
            contentType: "application/json; charset=utf8",
            headers: { "Authorization": 'Bearer ' + token },
            data: JSON.stringify(userMessage),
            success: function (response) {
                alert("We delivered your message")
            }
        });

    }


    $.ajax({
        type: "GET",
        url: "api/Home/otherusers",
        headers: { "Authorization": 'Bearer ' + token },
        success: function (response) {
            console.log(response)
            displayData(response);
        }
    });

    $.ajax({
        type: "GET",
        url: "api/Home/currentuser",
        headers: { "Authorization": 'Bearer ' + token },
        success: function (response) {
            console.log(response)
            displayUser(response);
        }
    });

    downloadCodeJams();

    function downloadCodeJams() {
        $.ajax({
            type: "GET",
            url: "api/Codejam",
            headers: { "Authorization": 'Bearer ' + token },
            success: function (response) {
                console.log(response)
                displayCodeJams(response);
            }
        });

    }

    function displayCodeJams(data) {
        console.log(data);
        document.getElementById("jamlist").innerHTML = "";
        data.forEach(codejam => {

            jamwrapper = document.createElement('div');
            jamwrapper.classList.add('jam');


            title = document.createElement("p");
            title.innerHTML = codejam.title;
            user = document.createElement("p");
            user.innerHTML = " Hosted by @" + codejam.host;
            desc = document.createElement("p");
            desc.innerHTML = codejam.descp;

            titlewrapper = document.createElement("div");
            titlewrapper.innerHTML = $(title).html() + $(user).html()

            jamwrapper.appendChild(titlewrapper);
            jamwrapper.appendChild(desc);

            //jamwrapper.innerHTML = codejam.title + " " + codejam.host + " " + codejam.descp; 
            //still here
            $('#jamlist').append(jamwrapper);
            $('#jamlist').append(document.createElement("hr"));

        })
    }



    $('#savebutton').on('click', function (e) {
        e.preventDefault();

        $.ajax({
            type: "post",
            url: "api/codejam/register",
            headers: { "Authorization": 'Bearer ' + token },
            data: $('#codejamform').serialize(),
            success: function (response) {
                console.log(response);
                downloadCodeJams();
            }
        });

    })




    //codejam popup

    $(".trigger_popup_fricc").click(function () {
        $('.hover_bkgr_fricc').show();
        map.dragging.disable();
        map.touchZoom.disable();
        map.doubleClickZoom.disable();
        map.scrollWheelZoom.disable();
        map.boxZoom.disable();
        map.keyboard.disable();
        if (map.tap) map.tap.disable();

    });

    $('.popupCloseButton').click(function () {
        $('.hover_bkgr_fricc').hide();
        map.dragging.enable();
        map.touchZoom.enable();
        map.doubleClickZoom.enable();
        map.scrollWheelZoom.enable();
        map.boxZoom.enable();
        map.keyboard.enable();
        if (map.tap) map.tap.enable();

    });

    $(".trigger_popup_fricc1").click(function () {
        $('.hover_bkgr_fricc1').show();
        map.dragging.disable();
        map.touchZoom.disable();
        map.doubleClickZoom.disable();
        map.scrollWheelZoom.disable();
        map.boxZoom.disable();
        map.keyboard.disable();
        if (map.tap) map.tap.disable();
        
    });

    $('.popupCloseButton').click(function () {
        $('.hover_bkgr_fricc1').hide();
        map.dragging.enable();
        map.touchZoom.enable();
        map.doubleClickZoom.enable();
        map.scrollWheelZoom.enable();
        map.boxZoom.enable();
        map.keyboard.enable();
        if (map.tap) map.tap.enable();
        
    });

})
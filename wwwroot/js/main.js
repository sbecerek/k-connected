$(document).ready(() => {
    
    $("#technologyselect").select2({
        placeholder: "Select Technologies", //placeholder
        tags: true,
        tokenSeparators: ['/',',',';'," "] 
    });


    //MAP RELATED CODE BELOW
    var greenIcon = new L.Icon({
        iconUrl: 'https://raw.githubusercontent.com/pointhi/leaflet-color-markers/master/img/marker-icon-2x-green.png',
        shadowUrl: 'https://cdnjs.cloudflare.com/ajax/libs/leaflet/0.7.7/images/marker-shadow.png',
        iconSize: [25, 41],
        iconAnchor: [12, 41],
        popupAnchor: [1, -34],
        shadowSize: [41, 41]
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
                success: function (response) {
                    displayData(response);
                }
            });

        else
            $.ajax({
                type: "post",
                contentType: "application/json; charset=utf8",
                url: "api/Technology/",
                data: JSON.stringify(opt),
                success: function (response) {
                    console.log(response);
                    displayData(response);
                }
            });

    });

    


    function displayData(users)
    {
        markers.forEach(marker=>{
            map.removeLayer(marker);
        })


        users.forEach(user => {
            let knowledges = [];
            user.knowledge.forEach(k => {
                knowledges.push(k.skillName);
            })

            knowledges = JSON.stringify(knowledges);
            console.log(knowledges);

            L.esri.Geocoding.geocode().address(user.apartment + user.street).city(user.city).region(user.country).run(function (err, results, response) {
                if (err) {
                  console.log(err);
                  return;
                }

                let guesses = Object.values(results);
                let pointcoordinate = guesses[0][0].latlng;// first index is the one with highest score double indexing since this has many dimensions

                //console.log(pointcoordinate);
                let m = L.marker([pointcoordinate.lat,pointcoordinate.lng]).bindPopup( user.username + " " + knowledges + 
                '<br/><button type="button" class="btn btn-primary sidebar-open-button" ">Send a mail</button>'
                ).on("popupopen",()=>{
                    $(".btn").on("click",(e)=>{
                        e.preventDefault();
                        sendMail(user.username);
                    })
                }).openPopup().addTo(map);
                markers.push(m);
              });
        });
    }



    function displayUser(users)
    {
            users.forEach(user => {
            if(user.username != "admin" )
            {
                
                let knowledges = [];
                user.knowledge.forEach(k => {
                    knowledges.push(k.skillName);
                })
    
                knowledges = JSON.stringify(knowledges);
                console.log(knowledges);
    
                L.esri.Geocoding.geocode().address(user.apartment + user.street).city(user.city).region(user.country).run(function (err, results, response) {
                    if (err) {
                      console.log(err);
                      return;
                    }
    
                    let guesses = Object.values(results);
                    let pointcoordinate = guesses[0][0].latlng;// first index is the one with highest score double indexing since this has many dimensions
    
                    //console.log(pointcoordinate);
                    userMarker = L.marker([pointcoordinate.lat,pointcoordinate.lng],{icon:greenIcon}).bindPopup( user.username + " " + knowledges
                    ).openPopup().addTo(map);
                  });
            }
        });
    }

    function sendMail(username) {
        console.log(username);

        $.ajax({
            type: "post",
            url: "api/mail/sendmail",
            contentType: "application/json; charset=utf8",
            data: JSON.stringify(username),
            success: function (response) {
                console.log("mail sent");
            }
        });

      }


    $.ajax({
        type: "GET",
        url: "api/Home/otherusers",
        success: function (response) {
            console.log(response)
            displayData(response);
        }
    });

    $.ajax({
        type: "GET",
        url: "api/Home/currentuser",
        success: function (response) {
            console.log(response)
            displayUser(response);
        }
    });



})
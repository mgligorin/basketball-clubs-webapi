$(document).ready(function () {

    // podaci od interesa
    var host = window.location.host;
    var token = null;
    var headers = {};
    var kosarkasiEndpoint = "/api/kosarkasi/";
    var editingId;
    var formAction = "Create";

    // okidanje ucitavanja proizvoda
    loadKosarkasi();

    // posto inicijalno nismo prijavljeni, sakrivamo odjavu
    $("#odjava").css("display", "none");

    // pripremanje dogadjaja za izmenu i brisanje
    $("body").on("click", "#btnDelete", deleteKosarkasi);
    $("body").on("click", "#btnEdit", editKosarkasi);
    $("body").on("click", "#btnOdustajanje", formaOdustajanje);
    $("body").on("click", "#btnReset", loadKosarkasi);

    $("body").on("click", "#btnRegistracijaIPrijava", registracijaIPrijava); 
    $("body").on("click", "#btnPocetak", pocetak);
    $("body").on("click", "#btnRegistracija", registracijaForma);
    $("body").on("click", "#btnPrijava", prijavaForma);

    function registracijaIPrijava() {
        $("#registracijaIPrijava").css("display", "none");
        $("#pocetak").css("display", "block");
        $("#registracija").css("display", "block");
    }

    function pocetak() {
        $("#pocetak").css("display", "none");
        $("#registracijaIPrijava").css("display", "block");
        $("#registracija").css("display", "none");
        $("#prijava").css("display", "none");
    }

    function registracijaForma() {
        $("#prijava").css("display", "none");
        $("#registracija").css("display", "block");
    }

    function prijavaForma() {
        $("#prijava").css("display", "block");
        $("#registracija").css("display", "none");
    }

    // ucitavanje proizvoda
    function loadKosarkasi() {
        var requestUrl = 'http://' + host + kosarkasiEndpoint;
        $.getJSON(requestUrl, setKosarkasi);
    }

    // metoda za postavljanje proizvoda u tabelu
    function setKosarkasi(data, status) {
        
        var $container = $("#kosarkasi");
        $container.empty();

        if (status === "success") {
            // ispis naslova
            var div = $("<div></div>");
            var h1 = $("<h1>Kosarkasi</h1>");
            div.append(h1);
            // ispis tabele
            var table = $("<table class='table table-bordered'></table>");
            if (token) {
                var header = $("<thead><tr><td>Ime i prezime</td><td>Rodjenje</td><td>Klub</td><td>Utakmica</td><td>Poeni</td><td>Brisanje</td><td>Izmena</td></tr></thead>");
            } else {
                var header = $("<thead><tr><td>Ime i prezime</td><td>Rodjenje</td><td>Klub</td><td>Utakmica</td><td>Poeni</td></tr></thead>");
            }
            
            table.append(header);
            tbody = $("<tbody></tbody>");
            for (i = 0; i < data.length; i++) {
                // prikazujemo novi red u tabeli
                var row = "<tr>";
                // prikaz podataka
                var displayData = "<td>" + data[i].ImeIPrezime + "</td><td>" + data[i].GodinaRodjenja + "</td><td>" + data[i].KosarkaskiKlub.Naziv + "</td><td>" + data[i].BrojUtakmicaZaKlub + "</td><td>" + data[i].ProsecanBrojPoena + "</td>";
                // prikaz dugmadi za izmenu i brisanje
                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id=btnDelete name=" + stringId + ">Delete</button></td>";
                var displayEdit = "<td><button id=btnEdit name=" + stringId + ">Edit</button></td>";
                // prikaz samo ako je korisnik prijavljen
                if (token) {
                    row += displayData + displayDelete + displayEdit + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }
                // dodati red
                tbody.append(row);
            }
            table.append(tbody);

            div.append(table);
            //if (token) {
                 //prikaz forme ako je korisnik prijavljen
                //$("#formProductDiv").css("display", "block");
            //}
           
            // ispis novog sadrzaja
            $container.append(div);
        }
        else {
            var div = $("<div></div>");
            var h1 = $("<h1>Greška prilikom preuzimanja Kosarkasa!</h1>");
            div.append(h1);
            $container.append(div);
        }
    }

    // registracija korisnika
    $("#registracija").submit(function (e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var loz1 = $("#regLoz").val();
        var loz2 = $("#regLoz2").val();

        // objekat koji se salje
        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        $.ajax({
            type: "POST",
            url: 'http://' + host + "/api/Account/Register",
            data: sendData

        }).done(function (data) {
            $("#info").append("Uspešna registracija. Možete se prijaviti na sistem.");

        }).fail(function (data) {
            alert(data);
        });
    });

    // prijava korisnika
    $("#prijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        // objekat koji se salje
        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "type": "POST",
            "url": 'http://' + host + "/Token",
            "data": sendData

        }).done(function (data) {
            console.log(data);
            $("#info").empty().append("Prijavljen korisnik: " + data.userName);
            token = data.access_token;
            $("#prijava").css("display", "none");
            $("#registracija").css("display", "none");
            $("#odjava").css("display", "block");
            $("#formsearch").css("display", "block");
            $("#priEmail").val('');
            $("#priLoz").val('');
            refreshTable();

        }).fail(function (data) {
            alert(data);
        });
    });

    // odjava korisnika sa sistema
    $("#odjavise").click(function () {
        token = null;
        headers = {};

        $("#prijava").css("display", "none");
        $("#registracija").css("display", "none");
        $("#registracijaIPrijava").css("display", "block");
        $("#pocetak").css("display", "none");
        $("#odjava").css("display", "none");
        $("#info").empty();
        $("#sadrzaj").empty();
        $("#formsearch").css("display", "none");
        refreshTable();
    });

    // forma za rad sa proizvodima
    $("#kosarkasiForm").submit(function (e) {
        // sprecavanje default akcije forme
        e.preventDefault();

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var imeIPrezime = $("#imeIPrezime").val();
        var rodjenje = $("#rodjenje").val();
        var klub = $("#klub").val();
        var utakmica = $("#utakmica").val();
        var poeni = $("#poeni").val();
        var httpAction;
        var sendData;
        var url;

        // u zavisnosti od akcije pripremam objekat
        if (formAction === "Create") {
            httpAction = "POST";
            url = 'http://' + host + kosarkasiEndpoint;
            sendData = {
                "ImeIPrezime": imeIPrezime,
                "GodinaRodjenja": rodjenje,
                "KosarkaskiKlubId": klub,
                "BrojUtakmicaZaKlub": utakmica,
                "ProsecanBrojPoena": poeni
            };
        }
        else {
            httpAction = "PUT";
            url = 'http://' + host + kosarkasiEndpoint + editingId.toString();
            sendData = {
                "Id": editingId,
                "ImeIPrezime": imeIPrezime,
                "GodinaRodjenja": rodjenje,
                "KosarkaskiKlubId": klub,
                "BrojUtakmicaZaKlub": utakmica,
                "ProsecanBrojPoena": poeni
            };
        }

        // izvrsavanje AJAX poziva
        $.ajax({
            url: url,
            type: httpAction,
            headers: headers,
            data: sendData
        })
            .done(function (data, status) {
                formAction = "Create";
                refreshTable();
                $("#formKosarkasiDiv").css("display", "none");
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            });
    });

    $("#searchForm").submit(function (e) {
        e.preventDefault();

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }


        var najmanje = $("#najmanje").val();
        var najvise = $("#najvise").val();

        var httpAction;
        var sendData;
        var url;

        httpAction = "POST";
        url = 'http://' + host + "/api/pretraga/";
        sendData = {
            "Min": najmanje,
            "Max": najvise
        };

        $.ajax({
            url: url,
            type: httpAction,
            headers: headers,
            data: sendData
        })
            .done(function (data, status) {
                $("#najmanje").val('');
                $("#najvise").val('');
                setKosarkasi(data, status);
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            });
    });

    // brisanje proizvoda
    function deleteKosarkasi() {
        // izvlacimo {id}
        var deleteID = this.name;

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        // saljemo zahtev 
        $.ajax({
            url: 'http://' + host + kosarkasiEndpoint + deleteID.toString(),
            type: "DELETE",
            headers: headers
        })
            .done(function (data, status) {
                refreshTable();
            })
            .fail(function (data, status) {
                alert("Desila se greska!");
            });
    }

    // izmena proizvoda
    function editKosarkasi() {
        // izvlacimo id
        var editId = this.name;

        // korisnik mora biti ulogovan
        if (token) {
            headers.Authorization = 'Bearer ' + token;
            $("#formKosarkasiDiv").css("display", "block");
        }

        var selectList = $("#klub");
        selectList.empty();
        $.ajax({
            url: 'http://' + host + '/api/kosarkaskiKlubovi',
            type: "GET",
            headers: headers
        }).done(function (klubovi, status) {
            for (i = 0; i < klubovi.length; i++) {
                if (editId === klubovi[i].Id) {
                    var displayData = '<option selected value=' + klubovi[i].Id + '>' + klubovi[i].Naziv + '</option>';
                }
                else {
                    var displayData = '<option value=' + klubovi[i].Id + '>' + klubovi[i].Naziv + '</option>';
                }
                selectList.append(displayData);
            }
            })
            .fail(function (klubovi, status) {
                formAction = "Create";
                alert("Desila se greska!");
            });

        // saljemo zahtev da dobavimo taj proizvod
        $.ajax({
            url: 'http://'+ host + kosarkasiEndpoint + editId.toString(),
            type: "GET",
            headers: headers
        })
            .done(function (data, status) {
                $("#imeIPrezime").val(data.ImeIPrezime);
                $("#rodjenje").val(data.GodinaRodjenja);
                $("#klub").val(data.KosarkaskiKlubId);
                $("#utakmica").val(data.BrojUtakmicaZaKlub);
                $("#poeni").val(data.ProsecanBrojPoena);
                editingId = data.Id;
                formAction = "Update";
            })
            .fail(function (data, status) {
                formAction = "Create";
                alert("Desila se greska!");
            });
    }

    // osvezi prikaz tabele
    function refreshTable() {
        // cistim formu
        $("#imeIPrezime").val('');
        $("#rodjenje").val('');
        $("#klub").val('');
        $("#utakmica").val('');
        $("#poeni").val('');
        // osvezavam
        loadKosarkasi();
    }

    function formaOdustajanje() {
        $("#imeIPrezime").val('');
        $("#rodjenje").val('');
        $("#klub").val('');
        $("#utakmica").val('');
        $("#poeni").val('');
        loadKosarkasi();
        $("#formKosarkasiDiv").css("display", "none");
    }
});
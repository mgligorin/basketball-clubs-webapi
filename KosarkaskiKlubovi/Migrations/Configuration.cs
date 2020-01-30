namespace KosarkaskiKlubovi.Migrations
{
    using KosarkaskiKlubovi.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<KosarkaskiKlubovi.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(KosarkaskiKlubovi.Models.ApplicationDbContext context)
        {
            context.KosarkaskiKlub.AddOrUpdate(x => x.Id,
                new KosarkaskiKlub() { Id = 1, Naziv = "Sacramento Kings", Liga = "NBA", GodinaOsnivanjaKluba = 1985, BrojOsvojenihTrofeja = 5 },
                new KosarkaskiKlub() { Id = 2, Naziv = "Dallas Mavericks", Liga = "NBA", GodinaOsnivanjaKluba = 1980, BrojOsvojenihTrofeja = 6 },
                new KosarkaskiKlub() { Id = 3, Naziv = "Indiana Pacers", Liga = "NBA", GodinaOsnivanjaKluba = 1967, BrojOsvojenihTrofeja = 13 }
            );

            context.Kosarkas.AddOrUpdate(x => x.Id,
                new Kosarkas() { Id = 1, ImeIPrezime = "Bogdan Bogdanovic", GodinaRodjenja = 1992, BrojUtakmicaZaKlub = 96, ProsecanBrojPoena = 12.3m, KosarkaskiKlubId = 1 },
                new Kosarkas() { Id = 2, ImeIPrezime = "Luka Doncic", GodinaRodjenja = 1999, BrojUtakmicaZaKlub = 26, ProsecanBrojPoena = 18.2m, KosarkaskiKlubId = 2 },
                new Kosarkas() { Id = 3, ImeIPrezime = "Bojan Bogdanovic", GodinaRodjenja = 1989, BrojUtakmicaZaKlub = 105, ProsecanBrojPoena = 14.8m, KosarkaskiKlubId = 3 },
                new Kosarkas() { Id = 4, ImeIPrezime = "Nemanja Bjelica", GodinaRodjenja = 1988, BrojUtakmicaZaKlub = 25, ProsecanBrojPoena = 10.8m, KosarkaskiKlubId = 1 }
            );
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KosarkaskiKlubovi.Models
{
    public class Kosarkas
    {
        public int Id { get; set; }
        [Required]
        [StringLength(40)]
        public string ImeIPrezime { get; set; }
        [Required]
        [Range(1976, 1999)]
        public int GodinaRodjenja { get; set; }
        [Required]
        [Range(minimum:1, maximum: 500)]
        public int BrojUtakmicaZaKlub { get; set; }
        [Required]
        [Range(1, 29)]
        public decimal ProsecanBrojPoena { get; set; }


        public int KosarkaskiKlubId { get; set; }

        public virtual KosarkaskiKlub KosarkaskiKlub { get; set; }
    }
}
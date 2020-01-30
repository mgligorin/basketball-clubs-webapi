using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace KosarkaskiKlubovi.Models
{
    public class KosarkaskiKlub
    {
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Naziv { get; set; }
        [StringLength(3, MinimumLength = 3)]
        public string Liga { get; set; }
        [Required]
        [Range(1945, 2000)]
        public int GodinaOsnivanjaKluba { get; set; }
        [Required]
        [Range(0, 19)]
        public int BrojOsvojenihTrofeja { get; set; }
    }
}
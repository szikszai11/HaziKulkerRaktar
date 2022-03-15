using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace HaziKulkerRaktar.Models
{
    public class Termek
    {
        public int Id { get; set; }


        [StringLength(60)]
        public string Elnevezes { get; set; }


        [StringLength(60)]
        public string Kategoria { get; set; }


        [StringLength(30)]
        public string CsomEgyseg { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Darabszam { get; set; }



    }

}

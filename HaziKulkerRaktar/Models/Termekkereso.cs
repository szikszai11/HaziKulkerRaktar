using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaziKulkerRaktar.Models
{
    public class Termekkereso
    {
        public string NevKereso { get; set; }
        public string ArKereso { get; set; }
        public SelectList KategoriaLista { get; set; }
        public List<Termek> Kategoriak { get; set; }
    }
}

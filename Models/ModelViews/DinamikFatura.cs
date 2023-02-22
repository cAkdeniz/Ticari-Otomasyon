using OnlineTicariOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.ModelViews
{
    public class DinamikFatura
    {
        public IEnumerable<Fatura> Faturalar { get; set; }
        public IEnumerable<FaturaKalem> FaturaKalemler { get; set; }
    }
}
﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Entity
{
    public class Fatura
    {
        [Key]
        public int Id { get; set; }

        [Column(TypeName = "Char")]
        [StringLength(1)]
        public string SeriNo { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(6)]
        public string SiraNo { get; set; }

        public DateTime Tarih { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(70)]
        public string VergiDairesi { get; set; }

        [Column(TypeName = "char")]
        [StringLength(5)]
        public string Saat { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimEden { get; set; }

        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string TeslimAlan { get; set; }

        public decimal ToplamTutar { get; set; }

        public ICollection<FaturaKalem> FaturaKalems { get; set; }
    }
}
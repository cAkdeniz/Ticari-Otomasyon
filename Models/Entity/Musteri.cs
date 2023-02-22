using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace OnlineTicariOtomasyon.Models.Entity
{
    public class Musteri
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Ad { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(30)]
        public string Soyad { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(13)]
        public string Sehir { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(70)]
        public string Mail { get; set; }
        [Column(TypeName = "Varchar")]
        [StringLength(20)]
        public string Sifre { get; set; }

        public bool Durum { get; set; } = true;

        public ICollection<SatisHareket> SatisHarekets { get; set; }
    }
}
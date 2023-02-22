using OnlineTicariOtomasyon.Models.Context;
using OnlineTicariOtomasyon.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class IstatistikController : Controller
    {
        Context db = new Context();
        // GET: Istatistik
        public ActionResult Index()
        {
            ViewBag.d1 = db.Musteris.ToList().Count;
            ViewBag.d2 = db.Uruns.ToList().Count;
            ViewBag.d3 = db.Personels.ToList().Count;
            ViewBag.d4 = db.Kategoris.ToList().Count;
            ViewBag.d5 = db.Uruns.Sum(x => x.Stok);
            ViewBag.d6 = db.Uruns.Select(x => x.Marka).Distinct().Count();
            ViewBag.d7 = db.Uruns.Count(x => x.Stok <= 10);
            ViewBag.d8 = db.Uruns.Max(x => x.SatisFiyat);
            ViewBag.d9 = db.Uruns.Min(x => x.SatisFiyat);
            ViewBag.d12 = db.Uruns.GroupBy(x => x.Marka).OrderByDescending(x => x.Count()).Select(x => x.Key).FirstOrDefault();

            ViewBag.d10 = (from SatisHareket in db.SatisHarekets
                          join Urun in db.Uruns on SatisHareket.UrunId equals Urun.Id
                          group SatisHareket by SatisHareket.Urun.Ad into grup
                          select new
                          {

                              UrunAd = grup.Key,

                              Adet = grup.Sum(x => x.Adet)

                          }).OrderByDescending(x => x.Adet).ToList().FirstOrDefault().UrunAd;

            ViewBag.d11 = db.SatisHarekets.Sum(x => x.ToplamTutar);


            DateTime bugun = DateTime.Today;
            ViewBag.d13 = db.SatisHarekets.Count(x => x.Tarih == bugun);

            if(ViewBag.d13!=0)
            {
                ViewBag.d14 = db.SatisHarekets.Where(x => x.Tarih == bugun).Sum(x => x.ToplamTutar);
            }
            else
            {
                ViewBag.d14 = "0";
            }
            //ViewBag.d14 = db.SatisHarekets.Where(x => x.Tarih == bugun).Sum(x => x.ToplamTutar);
            return View();
        }

        public ActionResult KolayTablolar()
        {
            var musteriIlListesi = (from x in db.Musteris
                                    group x by x.Sehir into g
                                    select new SinifGrup()
                                    {
                                        Key = g.Key,
                                        Sayi = g.Count()
                                    }).ToList();

            return View(musteriIlListesi);
        }

        public PartialViewResult _KategoriListesi()
        {
            var kategori = db.Kategoris.ToList();
            return PartialView(kategori);
        }

        public PartialViewResult _DepartmanPersonel()
        {
            var personeller = (from p in db.Personels
                               join d in db.Departmen on p.DepartmanId equals d.Id
                               group p by p.Departman.Ad into g
                               select new SinifGrup()
                               {
                                   Key = g.Key,
                                   Sayi = g.Count()
                               }).ToList();

            return PartialView(personeller);
        }

        public PartialViewResult _MusteriListesi()
        {
            var musteriler = db.Musteris.Where(x=>x.Durum==true).ToList();
            return PartialView(musteriler);
        }

        public PartialViewResult _UrunListesi()
        {
            var urunler = db.Uruns.Where(x => x.Durum == true).ToList();
            return PartialView(urunler);
        }

        public PartialViewResult _MarkaSayisi()
        {
            var markalar = (from u in db.Uruns
                            group u by u.Marka into g
                            select new SinifGrup()
                            {
                                Key = g.Key,
                                Sayi = g.Count()
                            }).ToList();

            return PartialView(markalar);
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }
    }
}
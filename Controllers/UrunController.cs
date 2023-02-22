using OnlineTicariOtomasyon.Models.Context;
using OnlineTicariOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class UrunController : Controller
    {
        Context db = new Context();

        // GET: Urun
        public ActionResult Index()
        {
            var urunler = db.Uruns.Where(x => x.Durum != false).ToList();
            return View(urunler);
        }

        public ActionResult Ekle()
        {
            List<SelectListItem> kategoriler = (from k in db.Kategoris.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = k.Ad,
                                                    Value = k.Id.ToString()                                                  
                                                }).ToList();

            ViewBag.Kategoriler = kategoriler;

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Urun urun)
        {
            urun.Durum = true;
            db.Uruns.Add(urun);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var urun = db.Uruns.Find(id);
            urun.Durum = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var urun = db.Uruns.Find(id);

            List<SelectListItem> kategoriler = (from k in db.Kategoris.ToList()
                                                select new SelectListItem
                                                {
                                                    Text = k.Ad,
                                                    Value = k.Id.ToString()
                                                }).ToList();

            ViewBag.Kategoriler = kategoriler;

            return View(urun);
        }

        [HttpPost]
        public ActionResult Guncelle(Urun model)
        {
            var urun = db.Uruns.Find(model.Id);
            urun.Ad = model.Ad;
            urun.Marka = model.Marka;
            urun.Stok = model.Stok;
            urun.AlisFiyat = model.AlisFiyat;
            urun.SatisFiyat = model.SatisFiyat;
            urun.KategoriId = model.KategoriId;
            urun.Resim = model.Resim;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult UrunDetay(int id)
        {
            var urun = db.Uruns.Find(id);
            return View(urun);
        }

        public ActionResult UrunListesi()
        {
            var urunler = db.Uruns.ToList();
            return View(urunler);
        }

        [HttpGet]
        public ActionResult SatisYap(int id)
        {
            var urun = db.Uruns.Find(id);

            List<SelectListItem> personeller = (from p in db.Personels.ToList()
                                                select new SelectListItem()
                                                {
                                                    Value = p.Id.ToString(),
                                                    Text = p.Ad + " " + p.Soyad
                                                }).ToList();

            List<SelectListItem> musteriler = (from p in db.Musteris.ToList()
                                                select new SelectListItem()
                                                {
                                                    Value = p.Id.ToString(),
                                                    Text = p.Ad + " " + p.Soyad
                                                }).ToList();

            ViewBag.Personeller = personeller;
            ViewBag.Musteriler = musteriler;
            ViewBag.UrunAdi = urun.Ad;

            SatisHareket satisHareket = new SatisHareket();
            satisHareket.UrunId = urun.Id;
            satisHareket.Fiyat = urun.SatisFiyat;
            return View(satisHareket);
        }

        [HttpPost]
        public ActionResult SatisYap(SatisHareket model)
        {
            model.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());
            db.SatisHarekets.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index", "Satis");
        }
    }
}
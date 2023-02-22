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
    public class MusteriController : Controller
    {
        Context db = new Context();
        // GET: Musteri
        public ActionResult Index()
        {
            var musteriler = db.Musteris.Where(x => x.Durum == true).ToList();
            return View(musteriler);
        }

        public ActionResult Sil(int id)
        {
            var musteri = db.Musteris.Find(id);
            musteri.Durum = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Musteri model)
        {
            db.Musteris.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var musteri = db.Musteris.Find(id);
            return View(musteri);
        }

        [HttpPost]
        public ActionResult Guncelle(Musteri model)
        {
            var musteri = db.Musteris.Find(model.Id);
            musteri.Ad = model.Ad;
            musteri.Soyad = model.Soyad;
            musteri.Sehir = model.Sehir;
            musteri.Mail = model.Mail;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult SatisGecmis(int id)
        {
            var satislar = db.SatisHarekets.Where(x => x.MusteriId == id).ToList();

            var musteri = db.Musteris.Find(id);
            ViewBag.MusteriAd = musteri.Ad;
            ViewBag.MusteriSoyad = musteri.Soyad;

            return View(satislar);
        }
    }
}
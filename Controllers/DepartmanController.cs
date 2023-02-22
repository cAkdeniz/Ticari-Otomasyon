using OnlineTicariOtomasyon.Models.Context;
using OnlineTicariOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    public class DepartmanController : Controller
    {
        Context db = new Context();

        // GET: Departman
        [Authorize(Roles = "A")]
        public ActionResult Index()
        {
            var departmanlar = db.Departmen.Where(x=>x.Durum!=false).ToList();
            return View(departmanlar);
        }

        public ActionResult Sil(int id)
        {
            var departman = db.Departmen.Find(id);
            departman.Durum = false;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Departman model)
        {
            model.Durum = true;
            db.Departmen.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var departman = db.Departmen.Find(id);
            return View(departman);
        }

        [HttpPost]
        public ActionResult Guncelle(Departman model)
        {
            var departman = db.Departmen.Find(model.Id);
            departman.Ad = model.Ad;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detay(int id)
        {
            var departman = db.Departmen.Find(id);
            var personeller = db.Personels.Where(x => x.DepartmanId == id).ToList();

            ViewBag.DepartmanAdi = departman.Ad;

            return View(personeller);
        }

        public ActionResult DepartmanPersonelSatis(int id)
        {
            var satislar = db.SatisHarekets.Where(x => x.PersonelId == id).ToList();
            var personel = db.Personels.Find(id);

            ViewBag.PersonelAdi = personel.Ad;
            ViewBag.PersonelSoyadi = personel.Soyad;

            return View(satislar);
        }
    }
}
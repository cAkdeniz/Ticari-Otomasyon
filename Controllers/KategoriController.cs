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
    public class KategoriController : Controller
    {
        Context db = new Context();

        // GET: Kategori
        public ActionResult Index()
        {
            var kategoriler = db.Kategoris.ToList();
            return View(kategoriler);
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Kategori kategori)
        {
            db.Kategoris.Add(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var kategori = db.Kategoris.Find(id);
            return View(kategori);
        }

        [HttpPost]
        public ActionResult Guncelle(Kategori model)
        {
            var kategori = db.Kategoris.Find(model.Id);
            kategori.Ad = model.Ad;
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Sil(int id)
        {
            var kategori = db.Kategoris.Find(id);
            db.Kategoris.Remove(kategori);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
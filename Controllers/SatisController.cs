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
    public class SatisController : Controller
    {
        Context db = new Context();
        // GET: Satis
        public ActionResult Index()
        {
            var satislar = db.SatisHarekets.ToList();
            return View(satislar);
        }

        public ActionResult Ekle()
        {
            List<SelectListItem> personeller = (from p in db.Personels.ToList()
                                                select new SelectListItem
                                                {
                                                    Value = p.Id.ToString(),
                                                    Text = p.Ad+" "+p.Soyad
                                                }).ToList();

            List<SelectListItem> musteriler = (from m in db.Musteris.ToList()
                                                select new SelectListItem
                                                {
                                                    Value = m.Id.ToString(),
                                                    Text = m.Ad+" "+m.Soyad
                                                }).ToList();

            List<SelectListItem> urunler = (from u in db.Uruns.ToList()
                                                select new SelectListItem
                                                {
                                                    Value = u.Id.ToString(),
                                                    Text = u.Ad
                                                }).ToList();

            ViewBag.Personeller = personeller;
            ViewBag.Musteriler = musteriler;
            ViewBag.Urunler = urunler;

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(SatisHareket model)
        {
            model.Tarih = DateTime.Parse(DateTime.Now.ToShortDateString());

            db.SatisHarekets.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            List<SelectListItem> personeller = (from p in db.Personels.ToList()
                                                select new SelectListItem
                                                {
                                                    Value = p.Id.ToString(),
                                                    Text = p.Ad + " " + p.Soyad
                                                }).ToList();

            List<SelectListItem> musteriler = (from m in db.Musteris.ToList()
                                               select new SelectListItem
                                               {
                                                   Value = m.Id.ToString(),
                                                   Text = m.Ad + " " + m.Soyad
                                               }).ToList();

            List<SelectListItem> urunler = (from u in db.Uruns.ToList()
                                            select new SelectListItem
                                            {
                                                Value = u.Id.ToString(),
                                                Text = u.Ad
                                            }).ToList();

            ViewBag.Personeller = personeller;
            ViewBag.Musteriler = musteriler;
            ViewBag.Urunler = urunler;

            var satis = db.SatisHarekets.Find(id);

            return View(satis);
        }

        [HttpPost]
        public ActionResult Guncelle(SatisHareket model)
        {
            var satis = db.SatisHarekets.Find(model.Id);
            satis.UrunId = model.UrunId;
            satis.PersonelId = model.PersonelId;
            satis.MusteriId = model.MusteriId;
            satis.Adet = model.Adet;
            satis.Fiyat = model.Fiyat;
            satis.ToplamTutar = model.ToplamTutar;
            satis.Tarih = model.Tarih;
            
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detay(int id)
        {
            var satis = db.SatisHarekets.Find(id);

            return View(satis);
        }
    }
}
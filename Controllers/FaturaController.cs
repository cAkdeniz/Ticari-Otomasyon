using OnlineTicariOtomasyon.Models.Context;
using OnlineTicariOtomasyon.Models.Entity;
using OnlineTicariOtomasyon.Models.ModelViews;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class FaturaController : Controller
    {
        Context db = new Context();
        // GET: Fatura
        public ActionResult Index()
        {
            var faturalar = db.Faturas.ToList();
            return View(faturalar);
        }

        public ActionResult Ekle()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Fatura fatura)
        {
            db.Faturas.Add(fatura);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var fatura = db.Faturas.Find(id);

            return View(fatura);
        }

        [HttpPost]
        public ActionResult Guncelle(Fatura model)
        {
            var fatura = db.Faturas.Find(model.Id);
            fatura.SeriNo = model.SeriNo;
            fatura.SiraNo = model.SiraNo;
            fatura.Saat = model.Saat;
            fatura.Tarih = model.Tarih;
            fatura.TeslimAlan = model.TeslimAlan;
            fatura.TeslimEden = model.TeslimEden;
            fatura.VergiDairesi = model.VergiDairesi;

            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detay (int id)
        {
            var faturaKalemler = db.FaturaKalems.Where(x => x.FaturaId == id).ToList();

            ViewBag.FaturaId = id;

            return View(faturaKalemler);
        }

        public ActionResult KalemEkle(int id)
        {
            var fatura = db.Faturas.Find(id);

            FaturaKalem faturaKalem = new FaturaKalem();
            faturaKalem.FaturaId = fatura.Id;
            
            return View(faturaKalem);
        }

        [HttpPost]
        public ActionResult KalemEkle(FaturaKalem faturaKalem)
        {
            db.FaturaKalems.Add(faturaKalem);
            db.SaveChanges();

            return RedirectToAction($"Detay/{faturaKalem.FaturaId}");
        }

        public ActionResult Dinamik()
        {
            DinamikFatura dinamikFatura = new DinamikFatura();
            dinamikFatura.Faturalar = db.Faturas.ToList();
            dinamikFatura.FaturaKalemler = db.FaturaKalems.ToList();

            return View(dinamikFatura);
        }
    }
}
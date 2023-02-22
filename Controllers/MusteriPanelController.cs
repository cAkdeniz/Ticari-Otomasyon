using OnlineTicariOtomasyon.Models.Context;
using OnlineTicariOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class MusteriPanelController : Controller
    {
        Context db = new Context();

        public Musteri AktifKullanici()
        {
            var mail = (string)Session["Mail"];
            var aktifKullanici = db.Musteris.Where(x => x.Mail == mail).FirstOrDefault();

            return aktifKullanici;
        }


        // GET: MusteriPanel
        public ActionResult Index()
        {
            var aktifKullanici = AktifKullanici();
            ViewBag.ToplamAlis = db.SatisHarekets.Where(x => x.MusteriId == aktifKullanici.Id).Count();
            ViewBag.ToplamAlisTutar = db.SatisHarekets.Where(x => x.MusteriId == aktifKullanici.Id).Sum(x => x.ToplamTutar);
            ViewBag.ToplamUrun = db.SatisHarekets.Where(x => x.MusteriId == aktifKullanici.Id).Sum(x => x.Adet);
            return View(aktifKullanici);
        }

        public ActionResult CikisYap()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Login");
        }

        public ActionResult Siparislerim()
        {
            var aktifKullanici = AktifKullanici();

            var siparisler = db.SatisHarekets.Where(x => x.MusteriId == aktifKullanici.Id).ToList();

            return View(siparisler);
        }

        public ActionResult Kargolarim(string p)
        {
            if (string.IsNullOrEmpty(p))
            {
                var kargolar = db.KargoDetays.Where(x => x.TakipKodu == "1").ToList();
                return View(kargolar);
            }
            else
            {
                var kargolar = db.KargoDetays.Where(x => x.TakipKodu == p).ToList();
                return View(kargolar);
            }
        }

        public ActionResult KargoDetay(string id)
        {
            var kargoTakip = db.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(kargoTakip);
        }

        public PartialViewResult _ProfilGuncelle()
        {
            var aktifKullanici = AktifKullanici();
            return PartialView(aktifKullanici);
        }

        public PartialViewResult _Duyuru()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult Guncelle(Musteri model)
        {
            var musteri = db.Musteris.Find(model.Id);
            musteri.Ad = model.Ad;
            musteri.Soyad = model.Soyad;
            musteri.Sehir = model.Sehir;
            musteri.Mail = model.Mail;
            musteri.Sifre = model.Sifre;

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
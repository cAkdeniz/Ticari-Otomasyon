using OnlineTicariOtomasyon.Models.Context;
using OnlineTicariOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace OnlineTicariOtomasyon.Controllers
{
    public class LoginController : Controller
    {
        Context db = new Context();
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public PartialViewResult _KayitOlPartial()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult _KayitOlPartial(Musteri musteri)
        {
            var kontrolMail = db.Musteris.Where(x => x.Mail == musteri.Mail).FirstOrDefault();
            if (kontrolMail!=null)
            {
                ModelState.AddModelError("mail", "Mail adresi kullanılıyor.");
                return PartialView("_KayitOlPartial");
            }
            else
            {
                musteri.Durum = true;
                db.Musteris.Add(musteri);
                db.SaveChanges();

                return PartialView();
            }
        }

        [HttpPost]
        public JsonResult MusteriGiris(Musteri m)
        {
            var kontrol = db.Musteris.Where(x => x.Mail == m.Mail && x.Sifre == m.Sifre).FirstOrDefault();
            if(kontrol!=null)
            {
                FormsAuthentication.SetAuthCookie(kontrol.Mail, false);
                Session["Mail"] = kontrol.Mail.ToString();
                return Json(new { success = true, redirectToUrl = Url.Action("Index", "MusteriPanel") });
            }
            else
            {
                return Json(new { success = false, redirectToUrl = Url.Action("Index") });
            }
        }

        [HttpPost]
        public ActionResult AdminGiris(Admin admin)
        {
            var kontrol = db.Admins.Where(x => x.KullaniciAd == admin.KullaniciAd && x.Sifre == admin.Sifre).FirstOrDefault();
            if (kontrol != null)
            {
                FormsAuthentication.SetAuthCookie(kontrol.KullaniciAd, false);
                Session["KullaniciAd"] = kontrol.KullaniciAd.ToString();
                return RedirectToAction("Index", "Istatistik");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
    }
}
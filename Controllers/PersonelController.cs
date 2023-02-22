using OnlineTicariOtomasyon.Models.Context;
using OnlineTicariOtomasyon.Models.Entity;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class PersonelController : Controller
    {
        Context db = new Context();
        // GET: Personel
        public ActionResult Index()
        {
            var personeller = db.Personels.ToList();
            return View(personeller);
        }

        public ActionResult Ekle()
        {
            List<SelectListItem> departmanlar = (from d in db.Departmen.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Value = d.Id.ToString(),
                                                     Text = d.Ad
                                                 }).ToList();

            ViewBag.Departmanlar = departmanlar;

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(Personel personel, HttpPostedFileBase Resim)
        {
            if (Resim.ContentLength > 0 && Resim != null)
            {
                string fileName = Path.GetFileName(Resim.FileName);
                string path = Path.Combine(Server.MapPath("~/img"), fileName);
                Resim.SaveAs(path);

                personel.Resim = Resim.FileName;
            }

            db.Personels.Add(personel);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Guncelle(int id)
        {
            var personel = db.Personels.Find(id);

            List<SelectListItem> departmanlar = (from d in db.Departmen.ToList()
                                                 select new SelectListItem
                                                 {
                                                     Value = d.Id.ToString(),
                                                     Text = d.Ad
                                                 }).ToList();

            ViewBag.Departmanlar = departmanlar;

            return View(personel);
        }

        [HttpPost]
        public ActionResult Guncelle(Personel model, HttpPostedFileBase Resim)
        {
            var personel = db.Personels.Find(model.Id);

            personel.Ad = model.Ad;
            personel.Soyad = model.Soyad;
            personel.DepartmanId = model.DepartmanId;

            if (Resim!=null)
            {
                string fileName = Path.GetFileName(Resim.FileName);
                string path = Path.Combine(Server.MapPath("~/img"), fileName);
                Resim.SaveAs(path);

                personel.Resim = Resim.FileName;
            }

            db.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
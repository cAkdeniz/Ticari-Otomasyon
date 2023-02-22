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
    public class KargoController : Controller
    {
        Context db = new Context();
        // GET: Kargo
        public ActionResult Index(string p)
        {
            if(!string.IsNullOrEmpty(p))
            {
                var kargolar = db.KargoDetays.Where(x => x.TakipKodu.Contains(p)).ToList();
                return View(kargolar);
            }
            else
            {
                var kargolar = db.KargoDetays.ToList();
                return View(kargolar);
            }    
        }

        [HttpGet]
        public ActionResult Ekle()
        {
            Random rnd = new Random();
            string[] karakterler = { "A", "B", "C", "D", "E", "F", "G", "H" };
            int k1 = rnd.Next(0, karakterler.Length);
            int k2 = rnd.Next(0, karakterler.Length);
            int k3 = rnd.Next(0, karakterler.Length);

            int s1 = rnd.Next(100, 1000);
            int s2 = rnd.Next(10, 100);
            int s3 = rnd.Next(10, 100);

            var takipKodu = s1 + karakterler[k1] + s2 + karakterler[k2] + s3 + karakterler[k3];
            ViewBag.TakipKodu = takipKodu;

            return View();
        }

        [HttpPost]
        public ActionResult Ekle(KargoDetay model)
        {
            model.Tarih = DateTime.Now;
            db.KargoDetays.Add(model);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Detay(string id)
        {
            var kargoTakip = db.KargoTakips.Where(x => x.TakipKodu == id).ToList();

            return View(kargoTakip);
        }
    }
}
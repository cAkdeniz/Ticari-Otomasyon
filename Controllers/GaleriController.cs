using OnlineTicariOtomasyon.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineTicariOtomasyon.Controllers
{
    [Authorize]
    public class GaleriController : Controller
    {
        Context db = new Context();
        // GET: Galeri
        public ActionResult Index()
        {
            var urunler = db.Uruns.ToList();
            return View(urunler);
        }
    }
}
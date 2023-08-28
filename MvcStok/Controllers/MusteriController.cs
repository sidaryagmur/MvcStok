using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class MusteriController : Controller
    {
        // GET: Musteri
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(string p)
        {
         //ındex parantez içine int sayfa=1   
           // var degerler = db.Müsteriler.ToList().ToPagedList(sayfa,3);
           var degerler = from d in db.Müsteriler select d;
            if (!string.IsNullOrEmpty(p))
            {
                degerler = degerler.Where(m => m.MüsteriAd.Contains(p));
            }
            return View(degerler.ToList());
           // return View(degerler);
        }
        [HttpGet]
        public ActionResult YeniMusteri()
        {
            return View();
        }
        [HttpPost]
        public ActionResult YeniMusteri(Müsteriler p1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteri");
            }
            db.Müsteriler.Add(p1);
            db.SaveChanges();
            return View();
        }

        public ActionResult SIL(int id)
        {
            var musteriler = db.Müsteriler.Find(id);
            db.Müsteriler.Remove(musteriler);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult MusteriGetir(int id)
        {
            var mus = db.Müsteriler.Find(id);
            return View("MusteriGetir", mus);
        }
        public ActionResult Guncelle(Müsteriler p1)
        {
          
            var mus = db.Müsteriler.Find(p1.MüsteriId);
            mus.MüsteriAd = p1.MüsteriAd;
            mus.MüsteriSoyad = p1.MüsteriSoyad;
            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
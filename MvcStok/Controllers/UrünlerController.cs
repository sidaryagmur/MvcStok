using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcStok.Models.Entity;
using MvcStok.Models.Urunler;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class UrünlerController : Controller
    {
        // GET: Urünler
        MvcDbStokEntities db = new MvcDbStokEntities();
        public ActionResult Index(int sayfa=1)
        {
            
            //var değerler =( from urunler in db.Urünler
            //               join Kategorilist in db.Kategoriler on urunler.UrünKategori equals Kategorilist.KategoriId
            //               select new UrunlerViewModel
            //               {
            //                   KategoriAdi = Kategorilist.KategoriAd,
            //                   UrünAd = urunler.UrünAd,
            //                   Fiyat = urunler.Fiyat,
            //                   Stok = urunler.Stok,
            //                   Marka = urunler.Marka,
            //                   UrünId = urunler.UrünId,
            //                   UrünKategori = urunler.UrünKategori
            //               }).ToList();

            var değerler = db.Urünler.ToList().ToPagedList(sayfa, 6);




            return View(değerler);
        }
        [HttpGet]
        public ActionResult UrunEkle()
        {
            List<SelectListItem> degerler = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }
                                           ).ToList();
            ViewBag.dgr = degerler;
            return View();
        }
        [HttpPost]
        public ActionResult UrunEkle(Urünler p2)
        {
            var ktg = db.Kategoriler.Where(m => m.KategoriId == p2.Kategoriler.KategoriId).FirstOrDefault();
            p2.Kategoriler = ktg;
            db.Urünler.Add(p2);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.Urünler.Find(id);
            db.Urünler.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult UrunGetir(int id)
        {
            var urn = db.Urünler.Find(id);

            List<SelectListItem> degerler = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KategoriAd,
                                                 Value = i.KategoriId.ToString()
                                             }
                                           ).ToList();



            ViewBag.dgr = degerler;

            return View("UrunGetir", urn);
        }
        public ActionResult Guncelle(Urünler p1)
        {
            var urn = db.Urünler.Find(p1.UrünId);
            urn.UrünAd = p1.UrünAd;
            urn.Marka = p1.Marka;
            // urn.UrünKategori = p1.UrünKategori;

            urn.Fiyat = p1.Fiyat;
            urn.Stok = p1.Stok;
            var ktg = db.Kategoriler.Where(m => m.KategoriId == p1.Kategoriler.KategoriId).FirstOrDefault();
            urn.UrünKategori = ktg.KategoriId;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}   
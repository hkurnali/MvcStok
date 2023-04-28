using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;

namespace MvcStok.Controllers
{
    public class UrunlerController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Urunler
        public ActionResult Index(int sayfa=1)
        {
            //var degerler = db.TBLURUNLER.ToList();
            var degerler = db.TBLURUNLER.ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }

        [HttpGet]

        public ActionResult YeniUrunler()
        {
            List<SelectListItem> degerler = (from i in db.TBLKATEGORİLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORİAD,
                                                 Value = i.KATEGORİID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;

            return View();
        }



        [HttpPost]
        public ActionResult YeniUrunler(TBLURUNLER P1)
        {
            var ktg = db.TBLKATEGORİLER.Where(m => m.KATEGORİID == P1.TBLKATEGORİLER.KATEGORİID).FirstOrDefault();
            P1.TBLKATEGORİLER = ktg;
            db.TBLURUNLER.Add(P1);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            db.TBLURUNLER.Remove(urun);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public ActionResult UrunGetir(int id)
        {
            var urun = db.TBLURUNLER.Find(id);
            List<SelectListItem> degerler = (from i in db.TBLKATEGORİLER.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.KATEGORİAD,
                                                 Value = i.KATEGORİID.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return  View("UrunGetir",urun);

        }
        public ActionResult Guncelle(TBLURUNLER k1)
        {
            var urn = db.TBLURUNLER.Find(k1.URUNID);
            urn.URUNAD = k1.URUNAD;
            urn.MARKA = k1.MARKA;
            //urn.URUNKATEGORİ = k1.URUNKATEGORİ;
           
            urn.FİYAT = k1.FİYAT;
            urn.STOK=k1.STOK;
            var ktg = db.TBLKATEGORİLER.Where(m => m.KATEGORİID == k1.TBLKATEGORİLER.KATEGORİID).FirstOrDefault();
            urn.URUNKATEGORİ = ktg.KATEGORİID;
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
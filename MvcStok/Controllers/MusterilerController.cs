using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcStok.Models.Entity;
using PagedList;

namespace MvcStok.Controllers
{
    public class MusterilerController : Controller
    { MvcDbStokEntities db = new MvcDbStokEntities();
        // GET: Musteriler
        public ActionResult Index(int sayfa=1)
        {/* var degerler = db.TBLMUSTERİLER.ToList();*/
            var degerler = db.TBLMUSTERİLER.ToList().ToPagedList(sayfa,5);
            return View(degerler);
        }

        [HttpGet]

        public ActionResult YeniMusteriler()
        {
            return View();
        }



        [HttpPost]
        public ActionResult YeniMusteriler(TBLMUSTERİLER P1)
        {
            if (!ModelState.IsValid)
            {
                return View("YeniMusteriler");
            }
            db.TBLMUSTERİLER.Add(P1);
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Sil(int id)
        {
            var musteri=db.TBLMUSTERİLER.Find(id);
            db.TBLMUSTERİLER.Remove(musteri);
            db.SaveChanges();
            return RedirectToAction("Index");

        }

        public ActionResult MusteriGetir(int id)
        {
            var mus = db.TBLMUSTERİLER.Find(id);
            return View("MusteriGetir",mus);

        }
        public ActionResult Guncelle(TBLMUSTERİLER p1)
        {
            var ktg = db.TBLMUSTERİLER.Find(p1.MUSTERİID);
            ktg.MUSTERİAD = p1.MUSTERİAD;
            ktg.MUSTERİSOYAD = p1.MUSTERİSOYAD;

            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Ajax.Utilities;
using MvcStok.Models.Entity;
using PagedList;
using PagedList.Mvc;
namespace MvcStok.Controllers
{
    public class KategoriController : Controller
    {
        MvcDbStokEntities db = new MvcDbStokEntities();

       
             
        // GET: Kategori
        public ActionResult Index(int sayfa=1)

        {
            //var degerler = db.TBLKATEGORİLER.ToList();
            var degerler = db.TBLKATEGORİLER.ToList().ToPagedList(sayfa, 5);
            return View(degerler);
        }

        [HttpGet]

        public ActionResult YeniKategori()
        {
            return View(); 
        }



        [HttpPost]
        public ActionResult YeniKategori(TBLKATEGORİLER P1)
        {   if(!ModelState.IsValid) 
            {
                return View("YeniKategori");
            }
            db.TBLKATEGORİLER.Add(P1);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
        public ActionResult SİL(int id) 
        { 
         var ktg=db.TBLKATEGORİLER.Find(id);
            db.TBLKATEGORİLER.Remove(ktg);
            db.SaveChanges();
            return RedirectToAction("Index");
        
        }
        public ActionResult KategoriGetir(int id)
        {
            var ktgr = db.TBLKATEGORİLER.Find(id);
            return View("KategoriGetir",ktgr);

        }
        public ActionResult Guncelle(TBLKATEGORİLER p1)
        {  var ktg=db.TBLKATEGORİLER.Find(p1.KATEGORİID);
            ktg.KATEGORİAD = p1.KATEGORİAD;
            db.SaveChanges();
            return RedirectToAction("Index");
                }
    }
}
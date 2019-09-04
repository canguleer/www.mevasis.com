using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using MevaPro.Models;


namespace MevaPro.Controllers
{
    [Authorize]
    public class HaberlersController : Controller
    {
        private mevasisEntities db = new mevasisEntities();

        // GET: Haberlers
        public ActionResult Index()
        {
            var haberlers = db.Haberlers.Include(h => h.Kategori).Include(h => h.Uye);
            return View(haberlers.ToList());
        }

        // GET: Haberlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haberler haberler = db.Haberlers.Find(id);
            if (haberler == null)
            {
                return HttpNotFound();
            }
            return View(haberler);
        }

        // GET: Haberlers/Create
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi");
            ViewBag.UyeId = new SelectList(db.Uyes, "UyeId", "AdSoyad");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Haberler haberler, HttpPostedFileBase Foto)
        {
            if (ModelState.IsValid)
            {
                if (Foto != null)
                {
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/Foto/" + newfoto);
                    haberler.Foto = "/Uploads/Foto/" + newfoto;
                }

                haberler.seourl = Seoconvert.Recover(haberler.Baslik);


                db.Haberlers.Add(haberler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(haberler);
        }

        // GET: Haberlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haberler haberler = db.Haberlers.Find(id);
            if (haberler == null)
            {
                return HttpNotFound();
            }

            ViewBag.KategoriId = new SelectList(db.Kategoris, "KategoriId", "KategoriAdi", haberler.KategoriId);
            ViewBag.UyeId = new SelectList(db.Uyes, "UyeId", "AdSoyad", haberler.UyeId);
            return View(haberler);
        }

        // POST: Haberlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Haberler haberler)
        {
            try
            {
                var haberlers = db.Haberlers.Where(m => m.HaberId == id).SingleOrDefault();


                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(haberlers.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(haberlers.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/Foto/" + newfoto);
                    haberlers.Foto = "/Uploads/Foto/" + newfoto;

                }
                haberlers.Baslik = haberler.Baslik;
                haberlers.Icerik = haberler.Icerik;
                haberlers.HaberId = haberler.HaberId;
                haberlers.seourl = Seoconvert.Recover(haberler.Baslik);
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {

                return View();

            }
        }

        // GET: Haberlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Haberler haberler = db.Haberlers.Find(id);
            if (haberler == null)
            {
                return HttpNotFound();
            }
            return View(haberler);
        }

        // POST: Haberlers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var haberlers = db.Haberlers.Where(m => m.HaberId == id).SingleOrDefault();
                if (haberlers == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(haberlers.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(haberlers.Foto));
                }



                db.Haberlers.Remove(haberlers);
                db.SaveChanges();

                return RedirectToAction("Index");

            }
            catch
            {
                return View();
            }
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

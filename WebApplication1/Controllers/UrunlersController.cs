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
    public class UrunlersController : Controller
    {
        private mevasisEntities db = new mevasisEntities();

        // GET: Urunlers
        public ActionResult Index()
        {
            return View(db.Urunlers.ToList());
        }

        // GET: Urunlers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // GET: Urunlers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Urunlers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Urunler urunler, HttpPostedFileBase Foto)
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
                    urunler.Foto = "/Uploads/Foto/" + newfoto;
                }

                urunler.seourl = Seoconvert.Recover(urunler.Baslik);
                db.Urunlers.Add(urunler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(urunler);
        }

        // GET: Urunlers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }






        // POST: Urunlers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Urunler urunler)
        {
            try
            {
                var Urunlers = db.Urunlers.Where(m => m.UrunId == id).SingleOrDefault();

                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(Urunlers.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(Urunlers.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/Foto/" + newfoto);
                    Urunlers.Foto = "/Uploads/Foto/" + newfoto;

                }
                Urunlers.Baslik = urunler.Baslik;
                Urunlers.KısaIcerik = urunler.KısaIcerik;
                Urunlers.Icerik = urunler.Icerik;
                Urunlers.UrunId = urunler.UrunId;
                Urunlers.seourl = Seoconvert.Recover(urunler.Baslik);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

                return View();

            }
        }




        // GET: Urunlers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Urunler urunler = db.Urunlers.Find(id);
            if (urunler == null)
            {
                return HttpNotFound();
            }
            return View(urunler);
        }

        // POST: Urunlers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var Urunlers = db.Urunlers.Where(m => m.UrunId == id).SingleOrDefault();
                if (Urunlers == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(Urunlers.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(Urunlers.Foto));
                }



                db.Urunlers.Remove(Urunlers);
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

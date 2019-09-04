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
    public class ReferanslarsController : Controller
    {
        private mevasisEntities db = new mevasisEntities();

        // GET: Projelers
        public ActionResult Index()
        {
            return View(db.Referanslars.ToList());
        }

        // GET: Projelers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslars.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // GET: Projelers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projelers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Referanslar referanslar, HttpPostedFileBase Foto)
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
                    referanslar.Foto = "/Uploads/Foto/" + newfoto;
                }


                db.Referanslars.Add(referanslar);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(referanslar);
        }

        // GET: Projelers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslars.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }






        // POST: Projelers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Referanslar referanslar)
        {
            try
            {
                var referanslars = db.Referanslars.Where(m => m.ReferansId == id).SingleOrDefault();

                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(referanslars.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(referanslars.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/Foto/" + newfoto);
                    referanslars.Foto = "/Uploads/Foto/" + newfoto;

                }
                referanslars.Baslik = referanslar.Baslik;
                referanslars.Icerik = referanslar.Icerik;
                referanslars.ReferansId = referanslar.ReferansId;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch
            {

                return View();

            }
        }




        // GET: Projelers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Referanslar referanslar = db.Referanslars.Find(id);
            if (referanslar == null)
            {
                return HttpNotFound();
            }
            return View(referanslar);
        }

        // POST: Projelers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var referanslars = db.Referanslars.Where(m => m.ReferansId == id).SingleOrDefault();
                if (referanslars == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(referanslars.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(referanslars.Foto));
                }



                db.Referanslars.Remove(referanslars);
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

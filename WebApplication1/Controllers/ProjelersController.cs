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
    public class ProjelersController : Controller
    {
        private mevasisEntities db = new mevasisEntities();

        // GET: Projelers
        public ActionResult Index()
        {
            return View(db.Projelers.ToList());
        }

        // GET: Projelers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeler projeler = db.Projelers.Find(id);
            if (projeler == null)
            {
                return HttpNotFound();
            }
            return View(projeler);
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
        public ActionResult Create(Projeler projeler, HttpPostedFileBase Foto)
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
                    projeler.Foto = "/Uploads/Foto/" + newfoto;
                }
                projeler.seourl = Seoconvert.Recover(projeler.Baslik);
                db.Projelers.Add(projeler);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(projeler);
        }

        // GET: Projelers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Projeler projeler = db.Projelers.Find(id);
            if (projeler == null)
            {
                return HttpNotFound();
            }
            return View(projeler);
        }






        // POST: Projelers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, HttpPostedFileBase Foto, Projeler projeler)
        {
            try
            {
                var projelers = db.Projelers.Where(m => m.ProjeId == id).SingleOrDefault();
                            
                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(projelers.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(projelers.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/Foto/" + newfoto);
                    projelers.Foto = "/Uploads/Foto/" + newfoto;
                   
                }
                
                projelers.Baslik = projeler.Baslik;
                projelers.Icerik = projeler.Icerik;
                projelers.ProjeId = projeler.ProjeId;
                projelers.seourl = Seoconvert.Recover(projeler.Baslik);
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
            Projeler projeler = db.Projelers.Find(id);
            if (projeler == null)
            {
                return HttpNotFound();
            }
            return View(projeler);
        }

        // POST: Projelers/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            try
            {
                var projelers = db.Projelers.Where(m => m.ProjeId == id).SingleOrDefault();
                if (projelers == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(projelers.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(projelers.Foto));
                }



                db.Projelers.Remove(projelers);
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

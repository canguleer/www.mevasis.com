using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MevaPro.Models;
using System.IO;
using System.Web.Helpers;

namespace MevaPro.Controllers
{
    [Authorize]
    public class HizmetlerimizsController : Controller
    {
        private mevasisEntities db = new mevasisEntities();

      
        public ActionResult Index()
        {
            return View(db.Hizmetlerimizs.ToList());
        }

     
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetlerimiz hizmetlerimiz = db.Hizmetlerimizs.Find(id);
            if (hizmetlerimiz == null)
            {
                return HttpNotFound();
            }
            return View(hizmetlerimiz);
        }

       
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Hizmetlerimiz hizmetlerimiz, HttpPostedFileBase Foto)
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
                    hizmetlerimiz.Foto = "/Uploads/Foto/" + newfoto;
                }

                hizmetlerimiz.seourl = Seoconvert.Recover(hizmetlerimiz.Baslik);
                db.Hizmetlerimizs.Add(hizmetlerimiz);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(hizmetlerimiz);
        }

       
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Hizmetlerimiz hizmetlerimiz = db.Hizmetlerimizs.Find(id);
            if (hizmetlerimiz == null)
            {
                return HttpNotFound();
            }
            return View(hizmetlerimiz);
        }

      
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(HttpPostedFileBase Foto, Hizmetlerimiz hizmetlerimiz)
        {

            try
            {                          

                if (Foto != null)
                {
                    if (System.IO.File.Exists(Server.MapPath(hizmetlerimiz.Foto)))
                    {
                        System.IO.File.Delete(Server.MapPath(hizmetlerimiz.Foto));
                    }
                    WebImage img = new WebImage(Foto.InputStream);
                    FileInfo fotoinfo = new FileInfo(Foto.FileName);

                    string newfoto = Guid.NewGuid().ToString() + fotoinfo.Extension;
                    img.Resize(800, 350);
                    img.Save("~/Uploads/Foto/" + newfoto);
                    hizmetlerimiz.Foto = "/Uploads/Foto/" + newfoto;
                    
                }               
                hizmetlerimiz.seourl = Seoconvert.Recover(hizmetlerimiz.Baslik);
                db.Entry(hizmetlerimiz).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");

            }
            catch
            {

                return View(hizmetlerimiz);

            }
        }

        //GET: Hizmetlerimizs/Delete/5
        public ActionResult Delete(int id)
        {
            var hizmetlerimiz = db.Hizmetlerimizs.Where(m => m.HizmetId == id).SingleOrDefault();

            if (hizmetlerimiz == null)
            {
                return HttpNotFound();
            }
            return View(hizmetlerimiz);
        }

        // POST: Hizmetlerimizs/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int? id)
        {
            try
            {
                var hizmetlerimizs = db.Hizmetlerimizs.Where(m => m.HizmetId == id).SingleOrDefault();
                if (hizmetlerimizs == null)
                {
                    return HttpNotFound();
                }
                if (System.IO.File.Exists(Server.MapPath(hizmetlerimizs.Foto)))
                {
                    System.IO.File.Delete(Server.MapPath(hizmetlerimizs.Foto));
                }



                db.Hizmetlerimizs.Remove(hizmetlerimizs);
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
